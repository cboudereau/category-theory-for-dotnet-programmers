type Reader<'r, 'a> = 'r -> 'a

let fmap f g = f << g

type Op<'r, 'a> = 'a -> 'r

// Introduction of opposite (Contravariant functor) : Impossible to implement the following fmap without it. 
// We have to found an opposite Functor of 'a -> 'b to 'b -> 'a. 
module Op = 
    let fmap (f:'a -> 'b) (x:'a -> 'r) : ('b -> 'r) = 
        failwith "not yet implemented" 

module Contravariant = 
    let flip f y x = f x y
    let contramap f g = flip (<<) f g 

let isEven x = x % 2 = 0
let headIsEven = Contravariant.contramap List.head isEven
headIsEven [0..10]

//Contravariant Functor (map input) is an Opposite Covariant Functor (map output).

//Personal notes : 

//The (>>) operator is the opposite of (<<) operator ?

open Contravariant

let f = (contramap List.head) >> (contramap isEven)
f [0..10]
