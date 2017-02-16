<Query Kind="FSharpProgram" />

//  Today's example is courtesy of @jindraivanek

//  https://gist.github.com/jindraivanek/5ff029577d2b544b2cd739d994750a18#file-memoizerec-fsx

//  As you remember, there was a warning on line 14

//  For a good explaination, see @tomaspetricek's Stack Overflow post.

//  http://stackoverflow.com/questions/8636630/recursive-objects-in-f

open System.Collections.Concurrent

//  Here's a solution: It's inspired by the Y-combinator (Not the VC firm)

//  Now we pass that function here...
let memoize (f:(('a -> 'b) -> 'a -> 'b)) = 
  let cache = ConcurrentDictionary<'a,'b>(HashIdentity.Structural)
  let rec memoizedFunction x =
    let getValue = f memoizedFunction
    cache.GetOrAdd(x, getValue)
  memoizedFunction

//  It seems more needlessly complicated to remove a warning, but
//  this solution actually opens the doors for more interesting
//  higher-ordered functions.  It also lets us decide *how* something
//  will recurse.  That will be useful in future casts.

let fib = 
  memoize(fun recurse n -> if n < 1 then 1 
                                else 
                                  let r = recurse(n - 1) + recurse(n - 2)
                                  printfn "Calc: fib(%i) -> %i" n r
                                  r)

fib(8).Dump()

//  Stay tuned...

//  FIN...