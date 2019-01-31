type Tree<'a> = Leaf of 'a | Node of Tree<'a> * Tree<'a>

module BiFunctor = 
    let bimap f g (x, y) = (f x, g y)

module Tree = 
    //Not a production code.
    let rec map f = function
        | Leaf x -> Leaf (f x)
        | Node (t1,t2) -> Node (BiFunctor.bimap (map f) (map f) (t1, t2))

let tree depth = 
    let rec build depth r = 
        if depth = 0 then r
        else build (depth - 1) (Node (r, Leaf depth))
    build depth (Leaf depth)

let t = tree 100000

Tree.map id t //Process is terminated due to StackOverflowException. Like said the book, this code is not optimized.

//Personal notes
//Here is an optimized tree structure with head and tail structure on Node case.
type OptimizedTree<'a> = Leaf of 'a | Node of 'a * OptimizedTree<'a>

module OptimizedTree = 
    //This function make traversable possible with tailcall optimization
    let rec fold folder state = function
        | Leaf l -> folder state l
        | Node (v, t) -> 
            //This is a tail call. Here we don't have 2 tree to map. 
            //We can fold the left value and continue to fold only on right.
            fold folder (folder state v) t
    
    //reuse fold for map with head as neutral because Tree should have at least one Leaf.
    let map f = function
        | Leaf l -> Leaf (f l)
        | Node (v, t) -> fold (fun state x -> Node (f x, state)) (Leaf v) t
    
    let reverse t = map id t

let tree2 depth = 
    let rec build depth r = 
        if depth = 0 then r
        else build (depth - 1) (Node (depth, r))
    build depth (Leaf depth)

let t2 = tree2 100000

//We have to reverse the tree because fold/map reverse the order
OptimizedTree.map id t2 |> OptimizedTree.reverse = t2 //true with stack overflow.
