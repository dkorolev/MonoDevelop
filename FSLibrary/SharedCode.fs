namespace SharedCodeFS

open System
open NUnit.Framework
open FsUnit

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
