//This is a DU - Discriminated Union
namespace StudentScores

type TestResult =
    | Absent //This is a discriminated union with the 'Absent' label and represents where the student didn't attend the test
    | Voided
    | Excused
    | Scored of float //if the case needs a payload, such as the score value, add 'of' with type

module TestResult =

    let fromString s =
        if s = "A" then
            //use instances of the DU by simply using the case name
            Absent
        elif s = "V" then
            Voided
        elif s = "E" then
            Excused
        else
            let value = s |> float
            // ... if the case has a payload you'll need to add pass that value
            Scored value

    let tryEffectiveScore (testResult: TestResult) =
        //to alter the behaviour of code in a DU instance, use a match expression
        match testResult with
        | Absent -> Some 0.0
        | Voided //This will inheret it's behavour from the next explicit instruction, ie -> None
        | Excused -> None        
        | Scored score -> Some score