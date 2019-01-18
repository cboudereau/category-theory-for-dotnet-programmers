using System.Collections.Generic;
using System.Linq;

//Here is a convenient way to build a Void type
//Sealed is important to stop sum and be sure that this type as no value and is impossible to construct in any way.
sealed class Void
{
    private Void() { }
    //Try to call this function !
    public static T absurd<T>(Void _) => throw new System.NotImplementedException("you can't call me it is absurd!");
}

//Sum type thanks to Subtyping. It works for this moment.. But lets go deeper in the book :)

//Personal notes
//Is not an antipattern by having marker interface ?? Lets discuss on a pull request or twitter.
//I don't know but I will use it for this simple case to understand the principle and compliant with the book.
//link : https://blog.ndepend.com/marker-interface-isnt-pattern-good-idea/
interface Either { }

struct Left<T> : Either
{
    public T Value { get; }
    public Left(T v) => Value = v;
}

//Because there is no Higher kinded types aka generics of generics, I have to write the same code for Right.
//link : https://github.com/dotnet/csharplang/issues/339
//In fsharp sum types already exists so this porcelain and plumbing code not exists in fsharp implementation. 

struct Right<T> : Either
{
    public T Value { get; }
    public Right(T v) => Value = v;
}

class Functions
{

    //Convenient way to adapt things. Here the compiler ensures that Right is absurd. 
    //Personal note
    //If we have encoded that with NotImplementedException only, the check is at runtime instead at compile time. 
    //So you may have less problems!
    //Here you have a boxing issue : if you would like to use sum type on struct for a O alloc (like Kestrel team plan to do)
    public static T simple<T>(Either x)
    {
        switch (x)
        {
            case Left<Void> l: return Void.absurd<T>(l.Value);
            case Right<T> y: return y.Value;
        }
        throw new NotImplementedException("The compiler can't check if it is total, so it is not really a sum type");
    }
}

//Here Either is isomorphic to Right because Left is absurd! 
var r1 = new Right<string>(Functions.simple<string>(new Right<string>("hello")));
var r2 = new Right<string>("hello");

// /!\ Execute this code line by line due to expression/statement issue
r1.Equals(r2) //true

//Since we can only build the right part, this type is isomorphic to Right

/// Personal notes
//Now think about how to implement the StreamReader and StreamWriter. 
//One only reads the stream, it is absurd to write. The opposite is true for the writer
//We can build instead a Pipe (reader/writer) and use inheritance polymorphism to build the reader and the writer

/// Link Pipe in Haskell : https://stackoverflow.com/questions/14131856/whats-the-absurd-function-in-data-void-useful-for
/// For example, Kestrel in dotnet core uses Pipeline pattern for synchronization/timing purpose.

/// Simple Sum type like enum : 

enum Color
{
    Red,
    Green,
    Blue
}

// Maybe
//Personal notes : can't be encoded by using enum
interface Maybe<T>
{
}

sealed class Nothing<T> : Maybe<T>
{
    private Nothing () { }
    public static Nothing<T> Singleton<T> () => new Nothing<T>();
}

struct Just<T> : Maybe<T>
{
    public T Value;
    public Just(T value) => Value = value;
}

//Personal notes
//In csharp Nullable is equivalent to Maybe in haskell and Option in fsharp when nullable reference type will be available.
//link : https://blogs.msdn.microsoft.com/dotnet/2017/11/15/nullable-reference-types-in-csharp/

//This type could be construct behind Either thanks to our unit type like this : 
// In fact Either interface does not keep track of types : lets create a generic one : 
interface Either2<L, R> { }

//Personal notes : this part is not in the book, but I have to translate it in csharp and I don't know what is the approach. Lets discuss it in a pull request

//It is a little bit strange, now in the definition of Left we have to keep the Right type ??
struct Left2<L, R> : Either2<L, R>
{
    public L Value { get; }
    public Left2(L x) => Value = x;
}

//Duplicating stuff
struct Right2<L, R> : Either2<L, R>
{
    public R Value { get; }
    public Right2(R x) => Value = x;
}

sealed class Unit
{
    private Unit(){ }
}

//Now it is okay but we have introduced a mutual dependency in the type definition between Left and Right type ?! 
class MaybeE<T> : Either2<Unit, T> { }

//We can define the Either with only one generic type because. It is a sum after all.. But ...
interface Either3<T> { }

struct Left3<L> : Either3<L>
{
    public L Value { get; }
    public Left3(L x) => Value = x;
}

//Duplicating stuff
struct Right3<R> : Either3<R>
{
    public R Value { get; }
    public Right3(R x) => Value = x;
}

static class Either3Extension
{
    //How could we write the factorizers.
    //We could do it but it is not a sum anymore.. The type of left and right should be the same..
    //If this sample is a little bit hard, try to implement the prodToSum and sumToProd of the chapter 6.4
    public static Either3<R> factorizers<T, R>(Either3<T> x, Func<T, R> f, Func<T, R> g)
    {
        switch (x)
        {
            case Left3<T> l: return new Left3<R>(f(l.Value));
            case Right3<T> r: return new Right3<R>(g(r.Value));
        }
        throw new System.NotImplementedException("unreachable");
    }
}

//Here, only the Either2 is valid and is compliant with properties but the type definition of left and right are mutually dependent..

////////////////////////////////

// list : 
// yield is the keyword equivalent of Cons
// Enumerable.Empty is the equivalent of Nil + Linq conversion to the type List
// The most type used for list in c# is List<T>

var empty = Enumerable.Empty<int>().ToList();

var l1 = empty.ToList();
//The add method mute the list. So the last element will be different depending where you add items..
l1.Add(1);
var x = l1.Last();

l1.Add(2);
var y = l1.Last();

x == y //false!

//link Give a try at ImmutableList : https://msdn.microsoft.com/en-us/library/dn467185(v=vs.111).aspx

empty.FirstOrDefault() //0 ?? what?? Implicit zero on default constructor provided by struct.

var l3 = new[] { 0 };

empty.FirstOrDefault() == l3.FirstOrDefault() //true, now we are not able to see if it is the first element or not ?0?

//now it is better but the compiler does not help us because there is no Nullable<T> for FirstOrDefault (due to the reference type issue in Nullable)

//A better one for value types
empty.Select(x => new Nullable<int>(x)).FirstOrDefault() // It outputs null but it is a true Nullable without value..

