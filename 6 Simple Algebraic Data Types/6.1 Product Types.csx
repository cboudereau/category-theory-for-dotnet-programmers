//Pair is not commutative
var pair1 = (1, true);
var pair2 = (true, 1);

sealed class Unit
{
    private Unit()
    {

    }

    public static Unit Singleton = new Unit();
}

static class Functions
{
    //We can provide a swap function that reverse the pair
    public static ValueTuple<T2, T1> swap<T1, T2>(ValueTuple<T1, T2> t)
    {
        var (x, y) = t;
        return (y, x);
    }
    public static ValueTuple<ValueTuple<T1, T2>, T3> alpha<T1, T2, T3>(ValueTuple<T1, ValueTuple<T2, T3>> t)
    {
        var (x, (y, z)) = t;
        return ((x, y), z);
    }
    public static ValueTuple<T1, ValueTuple<T2, T3>> alpha_inv<T1, T2, T3>(ValueTuple<ValueTuple<T1, T2>, T3> t)
    {
        var ((x, y), z) = t;
        return (x, (y, z));
    }

    public static T rho<T>(ValueTuple<T, Unit> t)
    {
        var (x, _) = t;
        return x;
    }
    public static ValueTuple<T, Unit> rho_inv<T>(T x) => (x, Unit.Singleton);
    public static ValueTuple<string, bool> P (string s, bool b) => (s, b);
    public static ValueTuple<T1, T2> p<T1, T2>(T1 x, T2 y) => (x, y);
    public static Func<T2, R> partial_app<T1, T2, R> (Func<T1, T2, R> f, T1 x) => (T2 y) => f(x, y);
}

// /!\ Execute this script line by line to switch from statement to expression (an expression returns a value displayed into the console)
//Main issue in csx : everything is statement. If it was expression instead, a value would have been displayed in the the console.
//So if I remove the ';' at the end it is a kind of expression in csx and the value is displayed but multiple lines fail at compile/design time because the ';' at the end is missing ?!
pair1 == Functions.swap(pair2) //true in type and by value thanks to swap

pair1 == Functions.swap(Functions.swap(pair1)) //true

//Isomorphism as associativity law in monoids. 
Functions.alpha_inv(Functions.alpha(("a", ("b", "c")))) == ("a", ("b", "c")) //true

//Now check the zero one : 

Functions.rho(Functions.rho_inv( 1 )) == 1 //true


//Pair as single case
var stmt = Functions.P("This statements is", false);

// Personal notes
//link : https://en.wikipedia.org/wiki/Currying
//link : http://blog.ploeh.dk/2017/01/30/partial-application-is-dependency-injection/
//Partial application is hard here because multiple arguments of function is ambiguous with tuple because you can't generate function with n-1 parameters
//Partial application can replace dependency injection frameworks (dependency injection with partial application) and could reduce complexity in frameworks (WCF, Kestrel and so on..)

//We can adapt by building our own partial_app function but due to type inference issue it is hard to keep track of types.
var p2 = Functions.partial_app<string, bool, ValueTuple<string, bool>>(Functions.p, "Hello");

var stmt2 = p2(false);

