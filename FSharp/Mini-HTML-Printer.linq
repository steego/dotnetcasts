<Query Kind="FSharpProgram" />

module Reflection = 
  open System.Collections
  let isSeq(t:System.Type) = 
    t.GetInterface(typeof<IEnumerable>.Name) <> null
  let isGenericSeq(t:System.Type) = 
    isSeq(t) && t.GenericTypeArguments.Length = 1

module HTML =
  open Reflection

  let print(o:obj) = 
    if o = null then "<null>"
    elif o :? string then o :?> string
    elif o.GetType() |> isGenericSeq then "List"
    else "Unknown Type..."

type Person = { Name:string; Salary: int }
    
HTML.print(null).Dump()
HTML.print("Hello").Dump()
HTML.print([1;2;3]).Dump()
HTML.print([1;2;3]).Dump()
    