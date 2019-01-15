using System;

static class Functions
{
    public static string upper(string x) => x.ToUpperInvariant();
    public static string[] words(string x) => x.Split(' ');
}

class Writer<T> : Tuple<T, string>
{
    public Writer(T x, string message) : base(x, message) { }
}

class ExplainedFunctions
{
    public static Writer<string> toUpper(string x) => new Writer<string>(Functions.upper(x), "toUpper ");
    public static Writer<string[]> toWords(string x) => new Writer<string[]>(Functions.words(x), "toWords ");

    public static Writer<T> identity<T>(T x) => new Writer<T>(x, "");

    public static Writer<string[]> process(string x)
    {
        //tuple deconstruct fails with a type inference issue..
        //var (y, l1) = toUpper(x);
        var y = toUpper(x);
        var z = toWords(y.Item1);

        return new Writer<string[]>(z.Item1, y.Item2 + z.Item2);
    }
}

static class Kleisli
{
    public static Func<T1, Writer<R>> Compose<T1, T2, R>(Func<T1, Writer<T2>> f, Func<T2, Writer<R>> g)
    {
        Writer<R> composition(T1 x)
        {
            Writer<T2> y = f(x);
            Writer<R> z = g(y.Item1);
            return new Writer<R>(z.Item1, y.Item2 + z.Item2);
        }
        return composition;
    }
}

//Here the composition part is hard to write in CSharp due to type inference issue.
//We have to force the type and loose inference, causing less benefits of function composition pattern.
var composition = Kleisli.Compose<string, string, string[]>(ExplainedFunctions.toUpper, ExplainedFunctions.toWords);
var r1 = composition("hello world");
var r2 = ExplainedFunctions.process("hello world");
var compositionWithIdentity = Kleisli.Compose<string, string[], string[]>(composition, ExplainedFunctions.identity);
var r3 = compositionWithIdentity("hello world");
//r1 = r2 = r3.