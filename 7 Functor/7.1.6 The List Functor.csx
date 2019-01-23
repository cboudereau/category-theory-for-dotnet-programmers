using System;

interface List<T> { }

class Nil<T> : List<T> { }

class Cons<T> : List<T>
{
    public T Head { get; }
    public List<T> Tail { get; }

    public Cons(T head, List<T> tail)
    {
        Head = head;
        Tail = tail;
    }
}

static class List
{
    public static List<R> map<T, R>(Func<T, R> f, List<T> l)
    {
        switch (l)
        {
            case Nil<T> _: return new Nil<R>();
            case Cons<T> c: return new Cons<R>(f(c.Head), map(f, c.Tail));
        }
        throw new NotImplementedException("absurd");
    }

    public static List<T> init<T>(Func<int, T> f, int n)
    {
        List<T> l = new Nil<T>();
        for (var i = n; i > 0; i--)
        {
            //Here l is mutating..
            l = new Cons<T>(f(i), l);
        }
        return l;
    }

    public static List<T> initRec<T>(Func<int, T> f, int n)
    {
        List<T> nestedInitRec(int m, List<T> l)
        {
            if (m > 0)
            {
                return nestedInitRec(m - 1, new Cons<T>(f(m), l));
            }
            else return l;
        }

        return nestedInitRec(n, new Nil<T>());
    }
}

static class TailRecursiveList
{
    // This code works with tailcall optimization only in x64 debug and release for dotnetcore but only release for dotnet framework.
    // We are converting the Monoid to another one (Nil as seed/mempty  and folder as Cons+folder/mappend)
    public static R fold<T, R>(Func<R, T, R> folder, R seed, List<T> x)
    {
        switch (x)
        {
            case Nil<T> _: return seed;
            case Cons<T> c: return fold(folder, folder(seed, c.Head), c.Tail);
        }
        throw new NotImplementedException("absurd");
    }

    //A reverse function because every time we use fold, by rebuilding from zero, in the end, the order is reversed.
    public static List<T> rev<T>(List<T> l) => fold((t, x) => new Cons<T>(x, t), (List<T>)new Nil<T>(), l);
    
    //Here we are rebuilding the list from Nil by applying every x to f. Note that the order is reversed
    //write map through fold + rev to have tail-recursive optimization 
    //Here we are using the List Monoid to map function (Nil as mempty and Cons as mappend) through fold
    //Here we are on O(n^2) (fold + rev) but actual Fsharp List implementation is better than this one.
    public static List<R> map<T, R>(Func<T, R> f, List<T> l) => rev(fold((t, x) => new Cons<R>(f(x), t), (List<R>)new Nil<R>(), l));
}

//This impl does not works in csharp interactive but works on release x64 configuration.
//I don't know how to configure CshapInteractive ?!
var of = List.initRec(y => y, 100000000);

var x = List.init(y => y, 100000000);
var x2 = List.map(y => y, x); //StackOverflow!

//Here we have a stack overflow!
var x3 = TailRecursiveList.map(y => y, x);

//Lets try with a little set..

var x4 = List.initRec(y => y, 100);
var x5 = List.map(x => x, x4); 

