//Infinite List.
using System;
using System.Collections.Generic;
using System.Linq;

static class EnumerableExtension
{
    public static IEnumerable<T> InitInfinite<T>(Func<int, T> f)
    {
        int counter = 0;
        while (true)
        {
            yield return f(counter);
            counter++;
        }
    }
}

var x = EnumerableExtension.InitInfinite(x=>x).Take(2).ToList();

x.SequenceEqual(new List<int> { 0, 1 }); //true

class Const<C>
{
    public C Value { get; }
    public Const(C v) => Value = v;
}
class Const<C, A> : Const<C>
{
    public Const(C v) : base(v) { }
}

static class Const
{    
    //"fmap is free to ignore its function upon"
    static Const<C, B> fmap<A, B, C>(Func<A, B> _, Const<C, A> x) => new Const<C, B>(x.Value);
}

