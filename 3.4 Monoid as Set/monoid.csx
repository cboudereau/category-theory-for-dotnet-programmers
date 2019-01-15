using System;
using System.Collections.Generic;
using System.Linq;

//An example of implementation by using interface because there is nor Trait nor static interface in CSharp
interface Monoid<T>
{
    T mempty();
    T mappend(T x, T Y);
}

class StringM : Monoid<string>
{
    public string mempty() => "";
    public string mappend(string x, string y) => x + y;
}

var stringM = new StringM();

var welcome = stringM.mappend("hello", " world");

//Monoid : a foldable/traversable structure; like aggregating in LINQ enumerable after all..

var words = new[] { "hello", " ", "world" };

//In this case, the first element of our list is the seed and it works...
var r = words.Aggregate((state, x) => stringM.mappend(state, x));

//... but what about empty list ? it fails with : Sequence contains no elements! So why providing default implementation for non empty list with no garantee ?
var r2 = new List<string>().Aggregate((state, x) => stringM.mappend(state, x));

// A better one : providing the initial seed : the neutral, the mempty of our Monoid
var r3 = new List<string>().Aggregate(stringM.mempty(), stringM.mappend);
