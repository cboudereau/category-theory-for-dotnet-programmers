type Functor<'r, 'a> = ('r -> 'a)

module Functor = 
    let map (f:'a -> 'b) (g:Functor<'r, 'a>) : Functor<'r, 'b> = f << g

module Functor2 = 
    let map = (<<)

Functor.map id id 1 = Functor2.map id id 1 // We have 