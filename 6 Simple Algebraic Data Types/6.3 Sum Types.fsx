//Here is a convenient way to build a Void type
//Sealed is important to stop sum the sum and be sure that this type as no value and is impossible to construct in any way.
type [<Sealed>] Void = private new () = { }

//Try to call this function !
let absurd (_:Void) = failwith "you can't call me it is absurd!"

type Either<'a, 'b> = Left of 'a | Right of 'b

//Convenient way to adapt things. Here the compiler ensures that Right is absurd. 
//If we have encoded that with NotImplementedException only, the check is at runtime instead at compile time. 
//So you may have less problems!
let simple = function
    | Left x -> absurd x
    | Right y -> y

//Here Either is isomorphic to Right because Left is absurd! 
Right "hello" |> simple |> Right = Right "hello"
//Since we can only build the right part, this type is isomorphic to Right

/// Personal notes
//Now think about how to implement the StreamReader and StreamWriter. 
//One read only the streamm it is absurd to write and the opposite is true for the writer
//We can build instead a Pipe (reader/writer) and use inheritance polymorphism to build the reader and the writer

/// Link Pipe in Haskell : https://stackoverflow.com/questions/14131856/whats-the-absurd-function-in-data-void-useful-for
/// For example, Kestrel in dotnet core uses Pipeline technique for synchronization/timing purpose.

/// Simple Sum type like enum : 
type Color = Red | Green | Blue

// Maybe
type Maybe<'a> = Nothing | Just of 'a
//This type could be construct behing Either thanks to our unit type like this : 
type MaybeE<'a> = Either<unit, 'a>

//In fsharp, Option is the equivalent of the Maybe one. None = Nothing and Just = Some.

// list : 
// (::) is the equivalent of Cons
// [] is the equivalent of Nil
1 :: [] = [1] //true
1 :: 2 :: [] = [1;2] //true 

//equivalent to maybeHead
[] |> List.tryHead = None
[1] |> List.tryHead = Some 1

//Same things for last : equivalent to maybeTail
[] |> List.tryLast = None
[1] |> List.tryLast = Some 1