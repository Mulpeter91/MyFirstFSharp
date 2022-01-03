namespace StudentScores

//Generics
type Optional<'T> = //In this case we're pretending as if the built in option type doesn't exist
    | Something of 'T
    | Nothing

module Optional =

    //let a = Something "abc"
    //let b = Something 1
    //let c = Something 1.2
    //let d = Nothing

    let defaultValue (d: 'T) (optional : Optional<'T>) =
        match optional with
        | Something v -> v
        | Nothing -> d