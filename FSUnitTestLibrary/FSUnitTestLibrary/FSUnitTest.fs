namespace FSUnitTestLibrary

open System
open NUnit.Framework
open FsUnit

[<TestFixture>]
type Test() = 

    [<Test>]
    member x.Test1() =
        Assert.IsTrue(true)

    [<Test>]
    member x.Test2() = 
        1 |> should equal 1
