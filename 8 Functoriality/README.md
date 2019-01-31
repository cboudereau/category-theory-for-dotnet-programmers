# 8 Functoriality

## 8.1 Bifunctors (Personal note)
In this sample, fsharp style has been used: define a type (BiFunctor) and define associated functions inside a module (BiFunctor).
In fsharp, all primitives are organized like this.

## 8.2 Product and Coproduct Bifunctors
To define a set as a monoidal category with respect to Cartesian product, This chapter explains very well how to define : 
 - the binary operation (+ or mappend) as bifunctor 
 - and zero/mempty as unit ```()```
 
## 8.3 Functorial Algebraic Data Types

### Personal notes
In this sample, if we want to simulate a type class with fsharp style, we have to define a fmap function with [Statically Resolved Type Parameters](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/generics/statically-resolved-type-parameters).
The question is when and why ? 
 - When you want to compose types and reuse in depth function composition like : 
    ```newtype BiComp bf fu gu a b = BiComp (bf (fu a) (gu b))```

	```instance (Bifunctor bf, Functor fu, Functor gu) => Bifunctor (BiComp bf fu gu) where bimap f1 f2 (BiComp x) = BiComp ((bimap (fmap f1) (fmap f2)) x)```

The compiler chooses the best overloading of fmap and bimap.

//Link : https://stackoverflow.com/questions/39065724/using-statically-resolved-type-parameters-is-it-possible-to-call-class-method-wi
The bad news : This approach does not work with curried functions.. It would be great if we have it to attach map functions defined in different modules (List, Option and so on..)
It is not possible to create those member in type Augmentation

You can write this function case per case. Maybe there was one or two instance behind in the program and the traverse one is easier to write first.

## 8.4 Functors in C++ (Personal notes)
I will translate this example from C++ to F# and C#

### BiFunctor / Production code ready
I added a real production code for tree. The unoptimized is interesting to introduce the BiFunctor.
The main difference between an optimized one and a product one are : 
 - Product is easy to build mappend with tuple but the traverse is [harder](https://hackage.haskell.org/package/bifunctors-5/docs/Data-Bifunctor.html#t:Bifunctor) : you need [clown](https://hackage.haskell.org/package/bifunctors-5/docs/Data-Bifunctor-Clown.html#t:Clown) and [joker](https://hackage.haskell.org/package/bifunctors-5/docs/Data-Bifunctor-Joker.html#t:Joker)
 - Head and tail : fold is easy to write but the mappend is harder because you don't have product and bifunctor to do it quickly.

### C# (Personal notes)
I translate the product one to use a bifunctor and the optimized one but the csharp interactive is quite limited. See the fsharp one to have a full interactive experience.
