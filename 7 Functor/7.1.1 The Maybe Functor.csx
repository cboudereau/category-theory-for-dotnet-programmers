using System;

interface Maybe<T> { }

struct Nothing<T> : Maybe<T> { }

struct Just<T> : Maybe<T>
{
    public T Value { get; }
    public Just(T v) => Value = v;
}

static class Maybe
{
    public static Maybe<R> fmap<T, R>(Func<T, R> f, Maybe<T> x)
    {
        switch (x)
        {
            case Nothing<T> _: return new Nothing<R>();
            case Just<T> j: return new Just<R>(f(j.Value));
        }
        throw new NotImplementedException("absurd, there is no other cases but we don't have he garantee");
    }
}

Maybe.fmap(x => x, new Nothing<string>()).Equals(new Nothing<string>()) //true

//Nullable is the new equivalent of Maybe (Nullable will be available for reference type in csharp soon)
//Link : https://blogs.msdn.microsoft.com/dotnet/2017/11/15/nullable-reference-types-in-csharp/

//In csharp there is an operator to traverse structure non null value or passing as parameter inside a function : 

static class NullableExtension
{
    public static Nullable<R> fmap<T, R>(Func<T, R> f, Nullable<T> x)
        where T : struct
        where R : struct
    {
        if (x.HasValue) return new Nullable<R>(f(x.Value));
        return new Nullable<R>();
    }
}

// Nullable is not completely useful because it is compatible with struct only.
// As you may notice the default constructor occurs on the GetValueOrDefault with implicit default constructor as zero causing less compatibility with reference types.

//
new Nullable<int> ().Equals(NullableExtension.fmap (x => x, new Nullable<int>())) //true

// The maybe one is compatible with struct and reference type.