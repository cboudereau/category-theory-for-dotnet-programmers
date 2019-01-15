let upper x = (x:string).ToUpperInvariant()
let words s = (s:string).Split(' ')

//If we want to explain what we are doing with a log, we can use pair to get the log : 

type Writer<'a> = Writer of 'a * string

//Explained functions
let toUpper x = Writer (upper x, "toUpper ")
let toWords x = Writer (words x, "toWords ")
let identity x = Writer (x, "")

//Composition
let process' x = 
    let (Writer (y, l1)) = toUpper x
    let (Writer (z, l2)) = toWords y
    Writer (z, l1 + l2)

process' "hello world" 

//How to compose more than 2 explained functions ?

//Here is the Kleili composition. It is like the process' function excepts that function are supplied as parameter.
module Writer = 
    module Operators = 
        let (>=>) f g = 
            fun x -> 
                let (Writer (y, l1)) = f x
                let (Writer (z, l2)) = g y
                Writer (z, l1 + l2)

open Writer.Operators

//Here is our final function composition. We can add other function easily with the fish operator
let composition = toUpper >=> toWords

composition "hello world" = process' "hello world" //true

(toUpper >=> toWords) "hello world" = (toUpper >=> toWords >=> identity) "hello world"