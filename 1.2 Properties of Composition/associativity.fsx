///Associativity with addition samples

//f is add one function
let f x = x + 1

//g is add 2 function
let g x = x + 2

let fog = f << g
let gof = g << f

fog 1 = 4 //true
gof 1 = 4 //true

(f << g) 1 = (g << f) 1

//Identity in addition sample

(f << id) 1 = 2
(id << f) 1 = 2

(f << id) 1 = (id << f) 1 

let neutral = 0

let identity = (+) neutral // aka let identity x = x + 0

//In fsharp, like Haskell, an identity function is already defined named 'id'
identity 1 = id 1 //true