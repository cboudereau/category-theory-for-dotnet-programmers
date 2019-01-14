let f x = sprintf "f(%s)" x
let g x = sprintf "g(%s)" x

//The book introduce the left to right function composition with the >> operator. 
//To be mathematics/Haskell compliant, let's use the << operator 
let fog = f << g

//Here the function composition is right to left
fog "x" = "f(g(x))"
(f >> g) "x" = "g(f(x))"