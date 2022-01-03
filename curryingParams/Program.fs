open System

let add a b =
    a + b

let c = add 2 3

//This is a 'partial application' of a curried function
//When you don't supply all the arguements when calling a curried function: a b
//what you get is another function that expects all the arguement you didn't give values for in the first function
let d = add 2

//this would be the second arguement
let e = d 4



let quote symbol s =
    sprintf "%c%s%c" symbol s symbol
 
let singleQuote = quote '\''
let doubleQuote = quote '"'


[<EntryPoint>]
let main argv =
    printfn "e: %i" e
    
    printfn "%s" (singleQuote "It was the best of times, it was the worst of times.")
    printfn "%s" (doubleQuote "It was the best of times, it was the worst of times.")
    0

// curried parameters are function parameter which support partial application