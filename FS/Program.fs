// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open SharedCode

[<EntryPoint>]
let main argv = 
    printfn "%s %A" (FooClass.Foo()) argv
    0 // return an integer exit code

