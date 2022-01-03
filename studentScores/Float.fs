namespace StudentScores
// in f# file order matters during the compliation process
// files can only use functions from a previous file
// This is why you need to add files via Ionide (F#)

module Float = 
    let tryFromString s =
        if s = "N/A" then
            Nothing 
            //None
        else
            Something (float s)
            //Some (float s) //Some is there because the return type of the statment must be the same of the first. Returns option<float>

    // type annotations and return type example: fromStringOr (d : float)(s: string) : float =
    // if you were to do (d, s) it would created the signature float * string -> float where * is not multiply but a tuple
    // You'd call this method with a lamdba expression, eg: |> Arry.map (fun s -> Float.fromStringOr(50.0, s))
    let fromStringOr d s =
        s
        |> tryFromString
        |> Optional.defaultValue d
        //|> Option.defaultValue d

    let tuple = (99, 3.1, "abc")