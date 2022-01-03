namespace StudentScores

//record type
type Student = 
    {
        //Name : string //record field
        FirstName : string
        Surname : string
        Id : string
        SchoolName: string
        MeanScore : float
        MinScore : float
        MaxScore : float
    }

//best practice to name the module after the record type it processes
module Student = 

    open System.Collections.Generic

    let namePart (s: string) = 
        let elements = s.Split(',')

        //Method 1: return the part of the name via a passed index value
        //elements.[i].Trim() //f# type interface works on the i because index is always an int
        
        //Method 2: split the name in the method and return both in a tuple 
        //let surname = elements.[0].Trim()
        //let giveName = elements.[1].Trim()
        //surname, giveName

        //Method 3: Pattern Matching returning tuples
        //match elements with        
        //| [|surname; givenName|] -> surname.Trim(), givenName.Trim()
        //| [|surname|] -> surname.Trim(), "(None)"
        //| _ -> raise (System.FormatException(sprintf "Invalid name format: \"%s\"" s))
        //The disadvantage of pattern matching against tuples elsewhere in code is that the elements could get mixed up

        //Method 4: Pattern Matching returning anaoymous record type
        // anaoymous = {| |}, explicit = { }
        match elements with        
        | [|surname; givenName|] ->
            {| Surname = surname.Trim() 
               GivenName = givenName.Trim() |}
        | [|surname|] ->
            {| Surname = surname.Trim()
               GivenName = "(None)" |}
        | _ -> raise (System.FormatException(sprintf "Invalid name format: \"%s\"" s))


    let fromString (schoolCodes: IDictionary<int, string>)(row : string) = //explicit type annotation needed here otherwise the .Split method will fall over
        let elements = row.Split('\t')
        //let name = elements.[0]
        // TODO: inefficient to split the name twice
        //let given = namePart 1 name //removed the index parameter from namePart
        //let surname = namePart 0 name
        //Using both names in return of tuple
        //let surname, givenName = name |> namePart

        let name = elements[0] |> namePart
        let id = elements.[1]
        let schoolCode = elements.[2] |> int
        let schoolName = schoolCodes.[schoolCode]
        let scores = 
            elements 
            |> Array.skip 3 //skip name and id
            |> Array.map TestResult.fromString
            |> Array.choose TestResult.tryEffectiveScore
            //|> Array.choose Float.tryFromString //choose is like a combined mapping and filtering operation. Removes the options wrapper
            //|> Array.map (Float.fromStringOr 50.) //example to override NA in the scores with a default value
            //|> Array.map float //float
            //|> Array.average
            //|> Array.averageBy float
        let mean = scores |> Array.average
        let max = scores |> Array.max
        let min = scores |> Array.min
        //functions return the last value that is created or referenced in their body
        { //F# can work out which record type is being used by the record names being bound
            FirstName = name.GivenName
            Surname = name.Surname
            Id = id
            SchoolName = schoolName
            MeanScore = mean
            MinScore = min
            MaxScore = max
        }

    let printSummary (student: Student) = 
        printfn "%s\t%s\t%s\t%s\t%0.1f\t%0.1f\t%0.1f" student.FirstName student.Surname student.Id student.SchoolName student.MeanScore student.MaxScore student.MinScore