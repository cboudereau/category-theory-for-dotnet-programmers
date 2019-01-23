
type List<'a> = Nil | Cons of 'a * List<'a>

//Here all comments are personal notes to have a list functor with tail-recursive optimization.

//Aka Instance of Functor (pattern used in fsharp implementation) : Build a module and define function and implementation thanks to the typeclassopedia (Haskell wiki)
module List = 
    //we have to use the rec keyword. This version is not tail-recursive (in the end we will have a stack overflow). Through fold it is possible
    //Here we have to wait the tail result before returning
    let rec map f = function Nil -> Nil | Cons (x, t) -> Cons (f x, map f t)
    
    let init f n = 
        let rec initRec f n l =
            if n > 0 then initRec f (n - 1) (Cons(f n, l))
            else l
        initRec f n Nil

//Personal Notes
module TailRecursiveList = 
    //We have to use the rec keyword but here we are rebuilding the list one by one and it the tail-recursive optimization works.
    //(seed acts as accumulator)
    // We are converting the Monoid to another one (Nil as seed/mempty  and folder as Cons+folder/mappend)
    let rec fold folder seed = function Nil -> seed | Cons (h, t) -> fold folder (folder seed h) t

    //A reverse function because every time we use fold, by rebuilding from zero, in the end, the order is reversed.
    let rev l = fold (fun seed x -> Cons (x, seed)) Nil l

    //Here we are rebuilding the list from Nil by applying every x to f. Note that the order is reversed
    //write map through fold + rev to have tail-recursive optimization 
    //Here we are using the List Monoid to map function (Nil as mempty and Cons as mappend) through fold
    //Here we are on O(n^2) (fold + rev) but actual Fsharp List implementation is better than this one.
    let map f = fold (fun t x -> Cons (f x, t)) Nil >> rev

let x = List.init id 100000000

let x' = x |> List.map id //Stack overflow!

let x'' = x |> TailRecursiveList.map id //No stack overflow anymore (with --optimize option or --tailcall)!

x'' = x //answer is long but true!

//In fsharp list already exists!

let y = FSharp.Collections.List.init 100000000 id 
let y' = y |> FSharp.Collections.List.map id //No stack overflow, no reversed order problem :)
y = y' //true 