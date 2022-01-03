open System

[<EntryPoint>]
let main argv =
 
    let numbers = [|1; 2; 4; 8; 16|]

    let isEven x =
        x % 2 = 0

    let todayIsSunday() =
        DateTime.Now.DayOfWeek = DayOfWeek.Sunday

    let numbers2 = 
        [|
            for i in 0..4 -> pown 2 i //pown will raise 2 to the power of i
        |]

    //adding all this logic inside the creation of the array is called array comprehension
    let numbers3 = 
        [|
            if todayIsSunday() then 47364
            42
            for i in 0..9 do
                let x = i * i
                if x |> isEven then
                    x
            999
        |]

    let total =
        [| 
            for i in 1..1000 do
                yield i * i
        |]
        |> Array.sum

    //reworking the above with sequence
    //sequences are collections like arrays which have collection functions, like sum
    //arrays are allocated to memory and usually populated in one go
    //whereas elements of a sequence are populated on demand
    //if you asked for position 1 in an array, .NET would go to a physical location in memory to read it
    //in a sequence it would go to the logic you coded and execute it again
    let total2 = 
        //seq { for i in 1..1000 -> i * i}
        // Seq.initInfinite // something you can't do with arrays
        Seq.init 1000 (fun i ->
            let x = i + 1
            x * x
        )
        |> Seq.sum 
    //when to use?
    // - Only exists to be summarized or piped onwards
    // - Long in theory but only some elements needed to access
    // - Stick to Array when iterating / consuming list multiple times
    // - Or accessing elements by index (though remember you have Seq.cache)

    // Array.ofSeq / Seq.toArray

    // seq.unfold is a function related to dependencies within the sequence


    let numbers4 = Array.init 5 (fun i -> pown 2 i)

    //arrays in F# are always mutable
    let initiallyZeros = Array.zeroCreate<int> 10
    initiallyZeros.[0] <- 42

    printfn "%A" numbers3
    printfn "%A" numbers4
    printfn "%i" total

    0