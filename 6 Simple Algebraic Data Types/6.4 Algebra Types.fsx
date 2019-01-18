type Either<'a, 'b> = Left of 'a | Right of 'b

//distribution :

let prodToSum (a, b) = 
    match b with
    | Left b' -> Left (a, b')
    | Right b' -> Right (a, b')

let sumToProd = function
    | Left (a, b) -> a, Left b
    | Right (a, b) -> a, Right b


let x = ("hello", Left "world")

//semiring or rig : No type substraction is provided
(prodToSum >> sumToProd) x = x //true