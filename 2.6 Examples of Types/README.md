# Examples of types

## Introducing Void and unit 

Why void cannot but used as unit in csharp and why unit is usefull. 

For compatibility unit type in fsharp are converted to void type in csharp (for interop).

In fsharp you can write : ignore 1 = ignore 1, the code compiles but in csharp you can't compare void type. 
The type unit does not really exists in csharp even if you a function without argument : ().

## Concrete sample in .Net where unit is needed.
Some aspect oriented programming or mock framework libs (like Moq, RhinoMocks, ...) have defined a Void or Unit type to simplify reflection. 

| Action could be Action<unit> and Action<unit> could be Func<unit, unit> 

In mock framework you may have at least 3 overloads to mock a call.. Having only 1 call helps to avoid overloading in favor of type inference
Overloading is a feature but also a limitation for the type inference system: the developper should choose one of them.

With this little convention only 1 type instead of 3 is needed to start with reflection.
You understand now why this difference make senses when you want to compose a program.
By reducing the number of paths you increase the composability of your program.