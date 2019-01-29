type Either<'a, 'b> = Left of 'a | Right of 'b

module Either = 
    let bimap f g = function
        | Left x -> Left (f x)
        | Right y -> Right (g y)

let l = Left 1
let r = Right "hello"

Either.bimap id id l = l //true
Either.bimap id id r = r //true

