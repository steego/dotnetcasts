<Query Kind="FSharpProgram">
  <Reference>&lt;ProgramFilesX86&gt;\Reference Assemblies\Microsoft\FSharp\.NETFramework\v4.0\4.3.0.0\FSharp.Core.dll</Reference>
  <NuGetReference>FSharp.Control.AsyncSeq</NuGetReference>
</Query>

open FSharp.Control

let generate() = asyncSeq {
  for i in 1..100 do
    do! Async.Sleep(100)
    yield i
}

let transform(input:AsyncSeq<int>) = asyncSeq {
  for item in input do
    yield (item, item * item)
}

//  This lets us have the effect of piping programs 
//  or functions in a lazy and asynchronous way
generate() |> transform |> AsyncSeq.toObservable |> Dump