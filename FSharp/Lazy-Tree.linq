<Query Kind="FSharpProgram" />

type Tree<'a>(value:'a, getChildren: 'a -> seq<'a>) = 
  member this.Value = value
  member this.Children = seq { for item in getChildren(value) do
                                  yield Tree(item, getChildren) }

//  Stay tuned, we're going to add some fun methods like a
//  .Where and .Select method that's going to make it a lot
//  more exciting!!!!!!
  
open System.IO

Tree(@"C:\Temp", Directory.EnumerateDirectories).Dump()