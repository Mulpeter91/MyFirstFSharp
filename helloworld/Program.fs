open System

let loopPerson person = 
    printfn "Array.Iter over %s" person

let isValid person =
    //not(String.IsNullOrWhiteSpace person)
    String.IsNullOrWhiteSpace person |> not

let isAllowed person =
    person <> "Eve"

// For more information see https://aka.ms/fsharp-console-apps
[<EntryPoint>]
let main argv =
    // if-else
    let person1 = 
        if argv.Length > 0 then
            argv.[0]
        else
            "Anonymous Person"
    printfn "%s has logged in" person1

    // #1 for loop with index
    for i in 0..argv.Length-1 do
        let person2 = argv.[i]
        printfn "Index loop over %s" person2

    // #3 for loop with iteration
    for person3 in argv do
        printfn "Iterate over %s" person3

    // #4 for loop with Array.iter (Higher Order function)
    Array.iter loopPerson argv

    // for loop with Array.iter and filter.
    //let validName = Array.filter isValid argv 
    //Array.iter loopPerson validName

    //We can use the forward piping operator to avoid temp variables per above
    argv 
    |> Array.filter isValid 
    |> Array.filter isAllowed
    |> Array.iter loopPerson
    0