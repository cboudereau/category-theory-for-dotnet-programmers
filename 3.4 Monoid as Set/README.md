# Monoid as Set

Monoid is just a way to provide the porcelain and plumbing parts to traverse/fold the structure easily (an empty/neutral/zero value and an operation like 0 and + for the addition sample of the previous chapter).

## String Concatenation sample
In CSharp, we can check that default LINQ aggregation without intial seed fails on empty list.
But if we use the Aggregate with intial seed, there is no problem to handle the empty list.
If you want to build better app that will not crash on empty list, you can use this one.
The monoid is here: To have a TOTAL aggregate function over the list, we have to supply 2 things : the initial seed (mempty) and the aggregate function (mappend).
So we already have a monoid in csharp over enumerable but it is implicit.

In fsharp sample, list module does not provide a implicit non empty aggregate function. 
That way you avoid to crash implicitly your app on empty list by design. 
Fold in fsharp is TOTAL by design.

This kind of bug is like null reference exception. 
Before dotnet nullable reference type we have no garantee for reference type if the instance is null or not.

This is why using Monoid can help you to build a better app.

Monoid can be helpful for async operation, optional value and so on...