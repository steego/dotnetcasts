<Query Kind="FSharpProgram" />

type Tree<'a>(value:'a, getChildren: 'a -> seq<'a>) = 
  let children = lazy [ for item in getChildren(value) do
                          yield Tree(item, getChildren) ]
                          
  //  You see, by partially applying Seq.filter, it returns a
  //  function that I can compose with getChildren
  
  //  This is why functional programmers get all *EXCITED* when it 
  //  comes to the topic of composition.  
  member this.Value = value
  member this.Children = children.Value
  member this.Where(test) = Tree(value, getChildren >> Seq.filter test)
  member this.Select(transform) = 
    Tree(transform value, getChildren >> Seq.map transform)
  
open System.IO

Tree(@"C:\Temp", Directory.EnumerateDirectories)
  .Where(fun folder -> folder.Contains("Code"))  
  .Select(fun folder -> folder.ToUpper())
  .Dump()
  
// FIN...