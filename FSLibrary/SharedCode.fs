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
and Rectangle = {
    width : float
    length : float
}
and SeveralShapes = {
    foo : int
    bar : Shape array
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
            bar = [| shape1; shape2 |]
        }
        let json1 = JsonConvert.SerializeObject(shape1)
        let json2 = JsonConvert.SerializeObject(shape2)
        let json3 = JsonConvert.SerializeObject(shape3)
        let json4 = JsonConvert.SerializeObject(shape4)

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
        let parsed1 = JsonConvert.DeserializeObject<Shape> json1
        let parsed2 = JsonConvert.DeserializeObject<Shape> json2
        let parsed3 = JsonConvert.DeserializeObject<Shape> json3
        let parsed4 = JsonConvert.DeserializeObject<Shape> json4
        let parse_error =
            try
                JsonConvert.DeserializeObject<Shape> "Meh!"
            with
            | :? JsonException -> Error
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
        Error = parse_error |> should be True
