type BiFunctor<'a, 'b> = BiFunctor of ('a * 'b)

module BiFunctor = 
    let bimap g h (BiFunctor x) = (x |> fst |> g, x |> snd |> h) |> BiFunctor
    let first g = bimap g id
    let second h = bimap id h

let x = BiFunctor (1,2)

BiFunctor.bimap id id x = x //true
BiFunctor.first id x = x //true
BiFunctor.second id x = x //true

BiFunctor.bimap string string x = BiFunctor ("1", "2") //true
BiFunctor.first string x = BiFunctor ("1", 2) //true
BiFunctor.second string x = BiFunctor (1, "2") //true
