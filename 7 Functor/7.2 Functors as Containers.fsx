// Infinite seq

let x = Seq.initInfinite id

x |> Seq.take 2 |> Seq.toList = [ 0; 1 ]

// Const : demo where value inside the functor is not important.

type Const<'c, 'a> = Const of 'c

module Const = 
    //"fmap is free to ignore its function upon"
    let fmap ((_:'a -> 'b), ((Const v):Const<'c, 'a>)) : Const<'c, 'b> = Const (v)