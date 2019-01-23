# 7 Functor

## 7.1.1 The Maybe Functor

### Personal Notes
In .Net there is the nullable equivalent for value type. For reference type we have to wait https://github.com/dotnet/csharplang/wiki/Nullable-Reference-Types-Preview.
But you can still build your own with FSharp.Core option type or a Maybe one.

By using it you will see that the custom ```?``` operator is useless when you define fmap and so on.. But it is an another story..

In Fsharp, Option type is the equivalent of the Maybe type.

## 7.1.2 Equational Reasoning

Read the chapter. To summarize, equality is important when you want to prove thing except when you function use side effect.

Those equalities has been use in 7.1.1 scripts.

## 7.1.3 Optional

As always read it. The author explain very well what we try to implement in 7.1.1 for csx script.

## 7.1.4 Typeclasses (Mostly Personal notes)
```Haskell
class Functor f where
    fmap :: (a -> b) -> f a -> f b
```

Type classes does not exists in .Net. 
The C++ equivalent is template-template whereas there is no generic-generic or generic of generic. 
This is a limitation of .Net.

If you want to see an equivalent, you should read the chapter 7.1.5 Functor in C++.

Even if type classes could be available in .Net, we could not implement Functor as is.
We need a feature called type constructor which is generic-generic dependent.
In the given definition of functor ```f a``` means ```f<a>``` where ```f``` and ```a``` are generics. 

To implement properly Functor type classes we need 3 things in order : 

 1/ [Types classes or Trait](https://github.com/fsharp/fslang-suggestions/issues/243)
 2/ [Generic of Generic](https://github.com/dotnet/csharplang/issues/339)
 3/ [Type constructor](https://github.com/fsharp/fslang-suggestions/issues/243#issuecomment-260186368)

[FStan](https://github.com/thautwarm/FSTan/blob/master/README.md) is an excellent alternative. 
You can still use abtract class and interface and made static things by defining some functions in a prelude class/module and you have type classes after all.
But you may consider that this abstraction has a cost at runtime. 

### Deal with it!
In .Net you don't have type classes stricly checked by the compiler but you can use implicitly.
To do it right, you should check the Haskell wiki : https://wiki.haskell.org/File:Typeclassopedia-diagram.png

For example : it is easy to traverse structure through fold because a foldable monoid is traversable.
You can fold the monoid (zero and mappend) first and transform it to another type like the aggregate function do with list.

If you build types by following the typeclassopedia rules, you have the benefits of the type composition property.

The aim of the book is to understand Catgeory theory through programming lang.
I guess it is not very import to have type classes. 
We can just continue with csharp and fsharp by following the rules. 

In fsharp modules there is no type classes but fmap (map) is available on mostly all modules.
You can follow the same rules in your domain as substitution of type classes as an informal way.

### No Silver Bullet (Very personal but not offensive notes :))

I often see questions like : What is the best FP lang ? And there is a lots of answer (Scala is better, Fsharp, Haskell, Rust and so on..).
I think it is a very personal choice dependending or OUR context and it works fine.
But if you think that your FP lang is best, did you try to use it in a completely different context ?

Let's check that we have [No Silver Bullet](https://en.wikipedia.org/wiki/No_Silver_Bullet)
 - Haskell is pure and well constructed thanks to functional pattern by design.
 - FSharp is hybrid and bring functional first language to .Net ecosystem which Haskell can't (but some project try to do it : https://wiki.haskell.org/Common_Language_Runtime)..
 - Scala is the same as FSharp to Java except that they bring types classes and type constructor but it is [not perfect](https://github.com/lampepfl/dotty/issues/2047) due to the OOP model. [Sparkle](https://github.com/tweag/sparkle) uses jvm to use spark infrastructure with Haskell.
 - Except FSharp, none of this functional programming offers type provider. I use/abuse it in my daily coding because I have to implement at least 200 apis (I work in a software editor with hundreds of partnerships). Even if you can use template in Haskell, you may endup with some compiler limits.
 - And there is a lots of langs : https://en.wikipedia.org/wiki/Functional_programming#Coding_styles

So dependending of your context you may have to choose 1 or 2 FP lang and interop thanks to microservices to have the full power of functional programming. 
I guess that context is very rare and honestly one per ecosystem (legacy) is ok.

There is [Idris](https://www.idris-lang.org/) (maybe a future bronze bullet :)) (based on the Haskell ecosystem) which try to bring fsharp [type provider with dependent typing](http://www.davidchristiansen.dk/pubs/dependent-type-providers.pdf).

If you want to go deeper with dependent typing after Category Theory for Programmers you should consider reading [The little typer](https://mitpress.mit.edu/books/little-typer), it blows my mind!

Read the [Idris paper](http://www.davidchristiansen.dk/pubs/dependent-type-providers.pdf) 2.1 chapter which gives a definition of type provider better than fsharp

To see a concrete of the Deal With it pattern, jump to 7.1.6 List Functor.

## 7.1.6 List Functor (Personal notes).

Type classes does not exists in .Net and we have to deal with it and follow the rule of fsharp (to code like if we have type classes virtually/in mind).

In fsharp list implementation, there is associated function definition of a type inside a module (ie: List module).
The module follows the rules of the functor by defining a function map but the compiler can't check that for us.
We can opt for fsi file to check but it is manual and we have to copy the functor def on all modules.

The structure of the type list is recursive. In .Net we have some issue when the method/function is not tail recursive, you may endup with a StackOverflow.
To avoid this, we defined the method that traverse the list structure in a tail recursive way (fold).

## 7.1.7 The Reader Functor
In fsharp we could define the reader functor by using the ```<<``` operator.

## 7.2 Functors as Containers
Read the chapter to have the full story.
Where .Net implement infinite with a state machine (implemented with goto and mutation). 
Haskell use a function (with closure) and eval it when we need the value (this is why Haskell is lazy).
Haskell is lazy dy default you can build an infinite list when a fsharp list is finite. 
Seq, alias of .net IEnumerable uses lazy with yield keyword.
Like Haskell you can't compute the length of an infinite list of values.
