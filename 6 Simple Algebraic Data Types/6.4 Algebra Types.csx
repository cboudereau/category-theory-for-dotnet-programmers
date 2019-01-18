interface Either<L, R> { }

struct Left<L, R> : Either<L, R>
{
    public L Value { get; }
    public Left(L x) => Value = x;
}

struct Right<L, R> : Either<L, R>
{
    public R Value { get; }
    public Right(R x) => Value = x;
}

//distribution :

static class Functions
{
    public static Either<ValueTuple<T, L>, ValueTuple<T, R>> prodToSum<T, L, R>(ValueTuple<T, Either<L, R>> t)
    {
        var (x, e) = t;

        switch (e)
        {
            case Left<L, R> l: return new Left<ValueTuple<T, L>, ValueTuple<T, R>>((x, l.Value));
            case Right<L, R> r: return new Right<ValueTuple<T, L>, ValueTuple<T, R>>((x, r.Value));
        }
        throw new System.NotImplementedException("not reachable");
    }

    public static ValueTuple<T, Either<L, R>> sumToProd<T, L, R>(Either<ValueTuple<T, L>, ValueTuple<T, R>> e)
    {
        switch (e)
        {
            //This is why subtyping as sum type is not trivial
            case Left<ValueTuple<T, L>, ValueTuple<T, R>> l:
                var (x, v) = l.Value;
                return (x, new Left<L, R>(v));
            case Right<ValueTuple<T, L>, ValueTuple<T, R>> l:
                var (x, v) = l.Value;
                return (x, new Right<L, R>(v));
        }
        throw new System.NotImplementedException("not reachable");
    }
}

var x = ("hello", new Left<string, string>("world"));

//semiring or rig : No type substraction is provided
Functions.sumToProd((Functions.prodToSum(x))).Equals(x) //true