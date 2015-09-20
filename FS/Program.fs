open Suave
open SharedCode

[<EntryPoint>]
let main argv =
    Web.defaultConfig |> Web.startWebServer <| Http.Successful.OK (FooClass.Foo())
    0