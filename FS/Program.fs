open Suave
open SharedCodeCS
open SharedCodeFS

[<EntryPoint>]
let main argv =
    Web.defaultConfig |> Web.startWebServer <| Http.Successful.OK (FooClass.Foo() + " + " + SharedFSType.ForFS + " = <3\n")
    0