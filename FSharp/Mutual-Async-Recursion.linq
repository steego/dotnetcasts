<Query Kind="FSharpProgram" />

let rec sayHi(n) = async {
    printfn "Hello %i" n     
    do! Async.Sleep(100)
    if n > 10 then
      return! doLongWait()
    else
      return! sayHi(n + 1)
  }
and doLongWait() = async {
    //  We'll wait for a second and resume
    do! Async.Sleep(1000)
    printfn "Waiting a long time"
    return! sayHi(1)
  }

sayHi(1) |> Async.Start