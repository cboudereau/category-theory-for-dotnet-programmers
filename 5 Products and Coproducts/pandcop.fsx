//The simplest product is pair of types : 

let pair = 1, true

//Here is 2 functions that extract the first or second part

let first (x,_) = x

let second (_,y) = y

first pair = 1 //true
second pair = true //true

//Those functions already exists as fst and snd like Haskell
fst pair = first pair //true
snd pair = second pair

//Now how can I reverse the first function to our initial pair ?

(fst pair |> fun x -> x, true) = pair //true
//this previous sample works because I already know that the second value is true. 
//To do that correctly we have to not loss information. This is the aim of factorizers : 

let factorizer f g x = x, (f x, g x)

let pair_factorizer = factorizer fst snd

pair_factorizer pair |> fst = pair //true
let pair2 = 10, false
pair_factorizer pair2 |> fst = pair2 //true
//So now it works because we are maintening the initial value.
//Now we are able to write coproduct of pair thanks to our pair_factorizer (coproduct is the dual operation of product)


//Type as set with list sample : 

//surjective or onto domain : function that use less space of the codomain like having a singleton in list. 
let surjective x = List.singleton x
surjective 1

//injective or one-to-one : unit map multiple element of the codomain
let injective () = [ 1;2 ]
injective ()

//bijection
let f x =  x + 1
let cof x = x - 1
let l = [1;2]
l |> List.map (f >> cof) = l

