type [<Struct>] Maybe<'a> = Nothing | Just of 'a

//Here is the fmap implementation with types
let fmap (f:'a->'b) (x:Maybe<'a>) : Maybe<'b> = 
    match x with
    | Nothing -> Nothing
    | Just x' -> Just(f x') 

fmap id Nothing = Nothing //true

//Now declare the fmap in a module with lighter syntax : 
module Maybe = 
    let fmap f = function Nothing -> Nothing | Just x -> Just (f x)

Maybe.fmap id Nothing = Nothing //true

//In fsharp, Option already exists and act as Maybe
Option.map id None = None
Option.map id (Some 1) = Some 1