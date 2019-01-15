static class Composition
{
    public static Func<T1,R2> fog<T1,R1,R2>(Func<T1,R1> f, Func<R1,R2> g) {
        return x => g(f(x));
    }
    
    ///The main difference with previous : it returns a value and the type inference works. 
    ///But we cannot separate composition and execution. This notion is important because program is composition THEN execution
    public static R2 fog2<T1,R1,R2>(Func<T1,R1> f, Func<R1,R2> g, T1 x) {
        return g(f(x));
    }
}

static class Functions
{
    public static string f (string x) => System.String.Format($"f({x})");
    public static string g (string x) => System.String.Format($"g({x})");
}

//The type inference doesn't help us. We have to provide function types.
Func<string, string> fog = Composition.fog<string, string, string> (Functions.f, Functions.g);
System.Console.WriteLine(fog ("x"));

//Type inference is quite limited when method returns function. Type inference works well with value.
var fogx = Composition.fog2 (Functions.f, Functions.g, "x");

System.Console.WriteLine(fogx);

