//  LINQPad is great, but Ionide is a full F# editor with excellent REPL capabilities

//  Let's pickup where we left off...


type Tree<'a>(value:'a, getChildren: 'a -> seq<'a>) = 
  //  This is how you make a private field for F# classes
  let children = lazy [ for item in getChildren(value) do
                          yield Tree(item, getChildren) ]
  member this.Value = value
  member this.Children = children.Value

let tree = 
