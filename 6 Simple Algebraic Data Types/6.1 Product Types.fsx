//Pair is not commutative
let pair1 = (1, true)
let pair2 = (true, 1)

//We can provide a swap function that reverse the pair
let swap (x, y) = (y, x)

pair1 = swap pair2 //true in type and by value thanks to swap

pair1 = (swap >> swap) pair1 //true

let alpha (x, (y, z)) = ((x, y), z)
let alpha_inv ((x, y), z) = (x, (y, z))

//Isomorphism as associativity law in monoids. 
(alpha >> alpha_inv) ("a", ("b", "c")) = ("a", ("b", "c")) //true

//Now check the zero one : 
let rho (x, ()) = x
let rho_inv x = x, ()

(rho_inv >> rho) 1 = 1 //true


//Pair as single case

type Pair<'a, 'b> = P of 'a * 'b

let stmt = P ("This statements is", false)

//link : https://en.wikipedia.org/wiki/Currying
//like haskell function with 2 arguments instead of having one tuple arg. Better in case of partial application. 
let p x y = P (x, y)
let stmt2 = p "This statements is" false

