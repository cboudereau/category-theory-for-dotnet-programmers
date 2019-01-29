//Type classes workaround for fmap : use statically resolved type
let inline fmap< ^a, ^b, ^c, ^d when ^a : (static member fmap: (^b -> ^c) * ^a -> ^d) > f (x:^a) : ^d = 
    (^a : (static member fmap: (^b -> ^c) * ^a -> ^d) (f,x))

// You can use operator which is less brainer to define.
// Here, in fsharp map (map is widely used) is fmap but in haskell map is reserved for list.
let inline map f x = f <!> x

type Maybe<'a> = Nothing | Just of 'a
    with 
        static member fmap (f, x) = match x with Nothing -> Nothing | Just x' -> Just (f x')
        static member (<!>) (f, (x:Maybe<'a>)) = Maybe<'a>.fmap (f, x)

//Curried Version of Maybe functions
module Maybe = 
    let map f (x:Maybe<'a>) = Maybe<'a>.fmap(f, x)

//Behind, the maybe should have the uncurried one..
fmap (id) (Just 1) = Just 1

id <!> (Just 1) = Just 1

let inline bimap< ^a, ^b, ^c, ^d, ^e, ^f when ^a : (static member bimap: (^b -> ^c) * (^d -> ^e) * ^a -> ^f) > f g (x:^a) : ^f = 
    (^a : (static member bimap: (^b -> ^c) * (^d -> ^e) * ^a -> ^f) (f,g,x))

type Either<'a, 'b> = Left of 'a | Right of 'b
    with static member bimap (f,g,x) = 
            match x with
            | Left x -> Left (f x)
            | Right y -> Right (g y)

bimap id id (Left 5) = Left 5

type Const<'c, 'a> = Const of 'c
    with static member fmap ((_:'a -> 'b), ((Const v):Const<'c, 'a>)) : Const<'c, 'b> = Const v

type Identity<'a> = Identity of 'a
    with static member fmap (f, Identity x) = Identity (f x)

type BiComp<'a> = BiComp of 'a

module BiComp = 
    let inline bimap f1 f2 (BiComp x) = BiComp ((bimap (fmap f1) (fmap f2)) x)

module Maybe2 = 
    let inline just (x:'a) : Either<Const<unit, 'a>, Identity<'a>> = Right (Identity x) 

BiComp (Maybe2.just 2) |> BiComp.bimap id id = BiComp (Right (Identity 2)) //true


//Personal example : the traverse one : 
module Identity = 
    let map f (Identity x) = Identity (f x)

module Const = 
    let map (f:'a->'b) ((Const v):Const<'c, 'a>) : Const<'c, 'b> = Const v

module Either = 
    let bimap f g x = 
        match x with
        | Left l -> Left (f l)
        | Right r -> Right (g r)

BiComp (Maybe2.just 2) |> (fun (BiComp x) -> Either.bimap (Const.map ignore) (fun x -> Identity.map id x) x) |> BiComp = BiComp (Right (Identity 2)) //true
