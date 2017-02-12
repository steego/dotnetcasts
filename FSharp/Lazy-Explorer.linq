<Query Kind="FSharpProgram" />

// Let's create a quick file explorer
open System.IO

let lazyLink caption (lazyVal:Lazy<_>) = 
  Util.OnDemand(caption, fun() -> lazyVal.Value)

//  Recursion FTW again...
let rec getSubFolders path =
  [ for dir in Directory.EnumerateDirectories(path) do 
       yield lazy getSubFolders dir |> lazyLink dir ]

let root = "C:\\Temp" 
lazy getSubFolders root
  |> lazyLink root
  |> Dump