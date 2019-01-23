using System;
using System.Collections.Generic;
using System.Linq;

interface Maybe<T> { }
class Nothing<T> : Maybe<T>
{
    public Nothing() { }
}

class Just<T> : Maybe<T>
{
    public T Value { get; }
    public Just(T v) => Value = v;
}

static class Maybe
{
    public static Maybe<R> map<T, R>(Func<T, R> f, Maybe<T> x)
    {
        switch (x)
        {
            case Nothing<T> _: return (Maybe<R>)new Nothing<T>();
            case Just<T> j: return new Just<R>(f(j.Value));
        }
        throw new NotImplementedException("absurd");
    }
}

static class ListExtension
{
    public static Maybe<List<T>> tryTail<T>(List<T> l)
    {
        if (l.Any()) return new Just<List<T>>(l.Skip(1).ToList());
        else return new Nothing<List<T>>();
    }

    //As you may notice Select is the equivalent of map. 
    //The keyword is inspired by the SQL syntax but behind it is a monad
    public static List<R> map<T, R>(Func<T, R> f, List<T> x) => x.Select(f).ToList();
}

Maybe<List<int>> nothing = ListExtension.tryTail(new List<int>());
var just = ListExtension.tryTail(new List<int> { 1, 2 });

Maybe.map(x => ListExtension.map(y => $"my value is {y}", x), just); //"my value is 2"
Maybe.map(x => ListExtension.map(y => $"my value is {y}", x), nothing); //Nothing

