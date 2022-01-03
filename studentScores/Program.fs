open System
open System.IO
open StudentScores

[<EntryPoint>]
let main argv =
    if argv.Length = 1 then
        let filePath = argv.[0]
        let schoolFilePath = argv.[1]
        
        if File.Exists filePath then 
            printfn "Processing %s" filePath //dotnet run \samples\StudentScores.txt
            try 
                let result = Summary.summarize schoolFilePath filePath
                0
            with
            // :? is the type test operator for match expressions
            // if the type of exception is raised which matches what is listed, the code will execute
            // add ex to bind it to the exception instance that was caught deeper in code 
            | :? FormatException as ex ->
                printfn "error: %s" ex.Message
                printfn "The file was not in the expected format."
                1
            | :? IOException as ex ->
                printfn "error: %s" ex.Message
                printfn "The file is open in another progam, please close it."
                2
            | _ as ex ->
                 printfn "Unexpected error: %s" ex.Message
                 3
        else
            printfn "File not found: %s" filePath
            4
    else
        printfn "Please specify a file"
        5 //it's traditional to use non-zero exit codes when a command does not succeed.
