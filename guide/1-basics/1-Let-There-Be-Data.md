# Let There Be Data

![Let There Be Data](Let-There-Be-Data.gif)

The most important keyword in F# is let. We use it to define variables:

```fsharp
let myName = "Steve"                      // string
let myAge = 40                            // int
let iLoveFSharp = true                    // bool
```

Oh yeah. For a lot of definitions, you don't have to F# the type. It will figure it out on its own 
using a technique called type inferencing. Put simply, type inferencing is a technique the F# uses 
to figure out what type your variables should be. In these example, it's not hard. Clearly "Steve" 
is a string, so it naturally follows that myName is a string.

If you don't like type inferencing, you can explicitly define variables with types.

```fsharp
let myName : string = "Steve"
let myAge : int = 40
let iLoveFSharp : bool = true
```

