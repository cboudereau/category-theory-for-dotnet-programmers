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

x.SequenceEqual(new List<int> { 0, 1 }) //true


class Const<A, C> { }