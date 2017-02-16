<Query Kind="FSharpProgram" />

open System.IO
open System.Collections.Concurrent

// A Merkle tree for a file system might look like this

// PASTE!!
let isDirectory(path:string) = 
  let attr = File.GetAttributes(path)
  attr.HasFlag(FileAttributes.Directory)

let hashStream(stream:Stream) = 
  use sha1 = System.Security.Cryptography.SHA1.Create()
  let hash = sha1.ComputeHash(stream)
  BitConverter.ToString(hash).Replace("-", "")  

let rec getDirDescription(path:string) : string = 
  let descriptions = 
    [| for file in Directory.EnumerateFiles(path) do
         let filename = Path.GetFileName(file)
         yield sprintf "%s - %s" filename (hashPath file)
       for dir in Directory.EnumerateDirectories(path) do
         yield sprintf "%s - %s" dir (hashPath dir)
    |]
  String.Join("\n", descriptions)

//  If you want mutual recursion, use the and keyword
and hashPath(path:string) = 
  if isDirectory path = false then
    use stream = File.OpenRead(path)
    hashStream(stream)
  else
    let desc = getDirDescription(path)
    let bytes = Encoding.UTF8.GetBytes(desc)
    let stream = new MemoryStream(bytes)
    hashStream(stream)
    


type Tree = 
  | File of Path:string
  | Folder of Path:string * Children:Lazy<list<Tree>>
  member this.Hash = 
    match this with
    | File(path) -> lazy hashPath path
    | Folder(path,_) -> lazy hashPath path
  member this.Description = 
    match this with
    | File(path) -> lazy getDirDescription path
    | Folder(path,_) -> lazy getDirDescription path
    
    
  

//  Let's create a "constructor"
let rec fromPath(path:string) = 
  if isDirectory path = false then File(path)
  else Folder(path, lazy [ for file in Directory.EnumerateFiles(path) do
                             yield File(file)
                           for dir in Directory.EnumerateDirectories(path) do
                             yield fromPath(dir)
                         ])

//  Nice...  Everything I change this file, the hash of my parent file 
//  now changes
fromPath(@"C:\Projects\LINQPad\Casts\").Dump()