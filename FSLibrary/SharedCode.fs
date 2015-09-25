namespace SharedCodeFS

open System
open NUnit.Framework
open FsUnit
open Newtonsoft.Json

type Shape =
    | Rectangle of Rectangle
    | Circle of radius : float
    | Magic of SeveralShapes
    | Empty
    | Error
    static member ToJSON(shape : Shape) : string = JsonConvert.SerializeObject(shape)
    static member FromJSON(json : string) = try JsonConvert.DeserializeObject<Shape> json with | :? JsonException -> Error
and Rectangle = {
    width : float
    length : float
}
and SeveralShapes = {
    foo : int
    bar : Shape list
}

type SharedFSType() = 
    static member ForCS = "F# into CS"
    static member ForFS = "F# into FS"

[<TestFixture>]
type Test() = 

    [<Test>]
    member x.Test1() =
        Assert.IsTrue(true)

    [<Test>]
    member x.Test2() = 
        1 |> should equal 1

    [<Test>]
    member x.Test3() = 
        "F# into CS" |> should equal SharedFSType.ForCS
        "F# into FS" |> should equal SharedFSType.ForFS

    [<Test>]
    member x.Test4() = 
        let shape1 = Rectangle {
            width = 1.3
            length = 10.0
        }
        let shape2 = Circle(2.5)
        let shape3 = Empty
        let shape4 = Magic {
            foo = 42
            bar =
            [
              shape1
              shape2
            ]
        }
        let json1 = Shape.ToJSON shape1
        let json2 = Shape.ToJSON shape2
        let json3 = Shape.ToJSON shape3
        let json4 = Shape.ToJSON shape4

        """
{"Case":"Rectangle","Fields":[{"width":1.3,"length":10.0}]}
        """.Trim() |> should equal json1
        """
{"Case":"Circle","Fields":[2.5]}
        """.Trim() |> should equal json2
        """
{"Case":"Empty"}
        """.Trim() |> should equal json3
        """
{"Case":"Magic","Fields":[{"foo":42,"bar":[{"Case":"Rectangle","Fields":[{"width":1.3,"length":10.0}]},{"Case":"Circle","Fields":[2.5]}]}]}
        """.Trim() |> should equal json4
        let parsed1 = Shape.FromJSON json1
        let parsed2 = Shape.FromJSON json2
        let parsed3 = Shape.FromJSON json3
        let parsed4 = Shape.FromJSON json4
        let parsedX = Shape.FromJSON "Meh!"
        shape1 |> should equal parsed1
        shape2 |> should equal parsed2
        shape3 |> should equal parsed3
        shape4 |> should equal parsed4
        parsed1 = shape1 |> should be True
        parsed1 = shape2 |> should be False
        parsed1 = shape3 |> should be False
        parsed1 = shape4 |> should be False
        parsed2 = shape1 |> should be False
        parsed2 = shape2 |> should be True
        parsed2 = shape3 |> should be False
        parsed2 = shape4 |> should be False
        parsed3 = shape1 |> should be False
        parsed3 = shape2 |> should be False
        parsed3 = shape3 |> should be True
        parsed3 = shape4 |> should be False
        parsed4 = shape1 |> should be False
        parsed4 = shape2 |> should be False
        parsed4 = shape3 |> should be False
        parsed4 = shape4 |> should be True
        Error = parsedX |> should be True

        let extra_fields = Shape.FromJSON """
{"Case":"Rectangle","Fields":[{"width":1.3,"length":10.0,"foo":0,"bar":"meh","boo":[],"qua":{}}]}
        """
        shape1 |> should equal extra_fields

        let different_order = Shape.FromJSON """
{"Fields":[{"foo":0,"bar":"meh","length":10.0,"boo":[],"width":1.3,"qua":{}}],"Case":"Rectangle"}
        """
        shape1 |> should equal different_order
