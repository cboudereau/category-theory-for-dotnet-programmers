using System;
using System.Linq;

static class Functions {
    /// Add one function
    public static int f (int x) => x + 1;
    /// Add 2
    public static int g (int x) => x + 2;

    public static int identity (int x) => x + 0;

    public static T id<T> (T x) => x;
}

//add functions is Associative op
Console.WriteLine(Functions.f(Functions.g(1)) == Functions.g(Functions.f(1))); //True

//adding 0 in + op is identity because 0 is the neutral value of + op.
Console.WriteLine(Functions.identity (1) == Functions.id(1)); //True

//An example : flatten

var listOfList = new[] { new[]{1,2}, new[]{3,4} };

//Identity is everywhere in csharp but implicity thanks to lambda.
var flattenList = listOfList.SelectMany(x => x);

//We can define our own id function in csharp but in this sample it is longer than the lambda one.
var flattenListWithIdentity = listOfList.SelectMany(Functions.id);

flattenList //{ 1, 2, 3, 4 }
flattenListWithIdentity //{ 1, 2, 3, 4 }
