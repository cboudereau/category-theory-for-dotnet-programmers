using System;
using System.Collections.Generic;
using System.Linq;

//those function is helpful and avoid lambda deconstruction.
//Because constructing a tuple is like passing all parameters to function, it is harder to deconstruct tuple in method argument..
static class Functions
{
    public static T1 fst<T1, T2>(ValueTuple<T1, T2> t)
    {
        var (x, _) = t;
        return x;
    }

    public static T2 snd<T1, T2>(ValueTuple<T1, T2> t)
    {
        var (_, y) = t;
        return y;
    }

    //Do you understand why WITHOUT type inference on function it is hard to define those things ?!
    public static ValueTuple<ValueTuple<L, R>, ValueTuple<T1, T2>> factorizer<L, R, T1, T2>(Func<ValueTuple<L, R>, T1> f, Func<ValueTuple<L, R>, T2> g, ValueTuple<L, R> x)
    {
        //Without inference, the signature is larger than the implementation..
        return (x, (f(x), g(x)));
    }
}

// construct a pair like fsharp
var pair = (1, true);

var (x1, y2) = pair;

// /!\ This code does not compile because constructing a pair and passing as argument is different. 
// It is an issue to use pair instead of out parameter for example.
var x = Functions.fst(pair);
var y = Functions.snd((1, true));

var r = Functions.factorizer(Functions.fst, Functions.snd, pair);


//Type as set with list sample : 

class TypeAsSet
{
    public static int[] surjective(int x) => new[] { x, 2 * x };
    public static int[] injective() => new[] { 1, 2 };
}

//surjective or onto : domain size is lower than codomain. One element of the domain map n elements of the codomain
var surjective = new[] { 1 }.SelectMany(TypeAsSet.surjective);

//injective or one-to-one : unit map multiple element of the codomain
var injective = TypeAsSet.injective();

//bijection
class Functions2
{
    public static int f(int x) => x + 1;
    public static int cof(int x) => x - 1;
}
var l = new[] { 1, 2 };
var l2 = l.Select(x => Functions2.cof(Functions2.f(x))); // [1; 2], l2 == l

injective()
