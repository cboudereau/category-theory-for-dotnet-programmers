using System;

//Can't do that since Func is sealed. So lets use Func directly..
//class Functor<R, A> : Func<R, A> { }

static class Functor
{
    public static Func<R, B> map<R, A, B>(Func<A, B> f, Func<R, A> g) => (x) => f(g(x));
}

var rf = (Functor.map<int, int, int>(x => x, y => y));
rf(1) == 1 //true
