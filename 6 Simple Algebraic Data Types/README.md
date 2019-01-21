# Simple Algebraic Data Types

This chapter explains well what is a Sum and Product types and how we could use them together.

## Personal notes
In csharp we often use Subtyping as Sum type. There is 2 things in Sum types : the total one, where the compiler checks that
each case is treated (Discriminated union in fsharp, can't do that in csharp for now) and the open one (partial) with Subtype.

Sum type as Subtyping creates strange mutually type dependencies in the Either implementation : Pull request accepted!

Because csharp does not support sumtype, I would like to summurize solutions because there is no total equivalent :

Here is all implementation

 - [Pattern matching](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching) : you can deconstruct what you have construct. 
For either you can unwrap the left case and get the value inside. The pattern matching feature with sum type in functional programming is really helpful because you can combine case deeper and deeper with less cyclomatic complexity in your code.

You can encode your pattern matching thanks to if statements but you may have a higher cyclomatic complexity and have to split your method.

- Boxing : When you have to use a struct that implement an interface, every time you supplied the struct as interface, you have a boxing issue. For 0 alloc pattern it could be an issue

- Marker interface : https://blog.ndepend.com/marker-interface-isnt-pattern-good-idea/. Is it an antipattern ?

- Polymorphism : implementation without the left and right type in the Either type definition can help you to implement it faster but you can't use polymorphism and reuse your code explicitly'

Note that if we use inheritance and use the pattern matching switch expression we have to use an interface. 
If we have to inherit from case, we could not use struct at root. So inheritance at case level is not possible for struct.

- Either : inheritance at case level. Can't use struct but the pattern matching syntax works with upper cast.

- Either2 : try to make inheritance at case level with struct support. 
But for each new case you have to add the new type on all types. By doing this you may have some regretion. 
Cases are mutually dependent. 
- Either3 : try to keep one type to avoid case dependencies. But now we can't match case anymore.. It is not a valid solution at all.
We can define the type Maybe ```(class Maybe2<T> : Either2<Unit, T> { })``` only for info.. (can't convert Maybe2 -> Right directly).
- Either4 : Now the left and right type is inside. Now the type definition is better and at a first glance same as fsharp but 
we have now 2 way to get the value and only one is valid (Left and Right property).

| Ranking | Name          | Pattern matching | Boxing | Marker interface |    Polymorphism    | Support struct |
|---------|---------------|------------------|--------|------------------|--------------------|----------------|
|   #4    | Either        | Switch keyword   |   -    |       Yes        |   At case level    |      No        |
|   #2    | Either2       | Switch keyword   |  Yes   |       Yes        |   At case level    |     Yes        |
| -       | Either3       | Not Possible     |  Yes   |       Yes        |   At case level    |     Yes        |
|   #1    | Either4       | Switch keyword   |   -    |       No         |   At type level    |      No        |
|   #3    | Either5       | No.              |  Yes   |       Yes        |   At case level    |     Yes        |

To summarize, there is no total solution for csharp sum type. You have to choose one dependending your case but we cannot build a lib that supply only one valid implementation for either and compose maybe or nullable over it.
Having sum types can increase composability of types.

Thanks to [giuliohome](https://twitter.com/giuliohome_2017) who help me to add more implementations.