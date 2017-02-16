<Query Kind="FSharpProgram" />

//  Today's example is courtesy of @jindraivanek

//  https://gist.github.com/jindraivanek/5ff029577d2b544b2cd739d994750a18#file-memoizerec-fsx

open System.Collections.Concurrent

let memoize (f:'a -> 'b) = 
  let cache = ConcurrentDictionary<'a,'b>(HashIdentity.Structural)
  fun x -> cache.GetOrAdd(x, f)

let rec fib = 
  memoize(fun n -> if n < 1 then 1 
                                else 
                                  let r = fib(n - 1) + fib(n - 2)
                                  printfn "Calc: fib(%i) -> %i" n r
                                  r)

fib(8).Dump()

//  Almost done, but we have a problem, notice the warning on line 14

//  It says this and other recursive referenences to objects...

//  It's a long warning, but it's saying there may be some costs when
//  using our memoize function against recursive functions.

//  In the next video, I'll show you Ivanek's clever solution (Sorry for
//  butchering your last name)