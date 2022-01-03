namespace StudentScores
open System.IO

module Summary =

    let printGroupSummary (surname : string)(students : Student[]) =
        printfn "%s" (surname.ToUpperInvariant())
        students
        |> Array.sortBy(fun student -> 
            student.FirstName, student.Id) //id is added in the event names are the same
        |> Array.iter(fun student ->
            printfn "%s\t%s\t%s\t%0.1f\t%0.1f\t%0.1f" 
                student.FirstName student.Surname student.Id student.MeanScore student.MaxScore student.MinScore)

    let summarize schoolCodesFilePath filePath =
        let rows = File.ReadAllLines filePath //returns an array with each element a line from the file
        let studentCount = rows |> Array.skip 1 |> Array.length
        printfn "STUDENT COUNT: %i" studentCount
        printfn "FIRST NAME\tSURNAME\t\tSTUDENT_ID\tMEAN\tMAX\tMIN"
        let schoolCodes = SchoolCodes.load schoolCodesFilePath
        rows
        |> Array.skip 1 //skip header row
        //|> Array.iter printMeanScore
        |> Array.map (Student.fromString schoolCodes)
        |> Array.groupBy (fun s -> s.Surname)
        |> Array.sortBy fst
        |> Array.iter (fun (surname, students) ->
            printGroupSummary surname students)
        //|> Array.sortBy (fun student -> student.Surname)
        //|> Array.map Student.printSummary