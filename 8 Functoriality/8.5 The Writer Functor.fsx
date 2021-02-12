type Writer<'a> = Writer of 'a * string

module Writer = 
    let (>=>) f g = 
        fun x -> 
            let (Writer (y, s1)) = f x
            let (Writer (z, s2)) = g y
            Writer (z, s1 + s2)

    let ret x = Writer (x, "")
    let fmap f = id >=> (f >> ret)

let w1 = Writer.ret 1
let w2 = Writer.ret 2

