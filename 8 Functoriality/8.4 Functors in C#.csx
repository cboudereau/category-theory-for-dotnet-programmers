using System;

static class BiFunctor
{
    static ValueTuple<R1,R2> bimap<T1,T2, R1, R2>(Func<T1, R1> f, Func<T2, R2> g, ValueTuple<T1, T2> x)
    {
        var (y, z) = x;
        return (f(y), g(z));
    }
}

interface Tree<T> { }

class Node<T> : Tree<T>
{
    public Tree<T> Left { get; }
    public Tree<T> Right { get; }

    public Node(Tree<T> left, Tree<T> right)
    {
        Left = left;
        Right = right;
    }
}

class Leaf<T> : Tree<T>
{
    public T Value { get; }

    public Leaf(T value)
    {
        Value = value;
    }
}

static class TreeExtension
{
    public static Tree<R> map<T, R> (Func<T, R> f, Tree<T> x)
    {
        switch (x)
        {
            case Leaf<T> l : return new Leaf<R>(f(l.Value));
            case Node<T> n :
                return new Node<R>(map<T, R>(f, n.Left), map<T, R>(f, n.Right));
        }
        throw new NotImplementedException("absurd");
    }

    public static Tree<int> tree(int depth)
    {
        Tree<int> build(int d, Tree<int> r)
        {
            if (d == 0) return r;
            else return build(d - 1, new Node<int>(new Leaf<int>(d), r));
        }
        return build(depth, new Leaf<int>(depth));
    }
}

var t1 = TreeExtension.tree(100000); //it works on console app not in csharp interactive.

var r = map(x => x, t1); //It fail with StackOverflow even if your are in release x64.

var t11 = TreeExtension.tree(10); //with less depth it works

//For a better experience, see the fsharp one in with taicall optimization.

//Personal notes, this is the tree structure for x64 release mode in csharp. For fsharp this implementation works perfectly.

class OptimizedNode<T> : Tree<T>
{
    public T Head { get; }
    public Tree<T> Tail { get; }

    public OptimizedNode(T head, Tree<T> tail)
    {
        Head = Head;
        Tail = tail;
    }
}

Tree<int> tree(int depth)
{
    Tree<int> build(int d, Tree<int> r)
    {
        if (d == 0) return r;
        else return build(d - 1, new OptimizedNode<int>(d, r));
    }

    return build(depth, new Leaf<int>(depth));
}

var t2 = tree(1000000); //this works on app console with release x64.

