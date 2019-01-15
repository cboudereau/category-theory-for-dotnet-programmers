// In fsharp and in dotnet, there is nor trait nor type class.
// It is implicit and you have to provide functions by yourself

module String = 
    let mempty = ""
    let mappend x y = sprintf "%s%s" x y

    //our aggregate function in fsharp without implicit first element as initial seed.
    //You can provide traverse function from list to string like this : 
    let ofList = List.fold mappend mempty //We are using partial application on the list


String.mappend "hello" " world" = (String.ofList ["hello"; " "; "world"])

//In fsharp, the list does not provide a function with implicit zero as head, 
//you have to provide the neutral by yourself. The monoid is implicit but necessary when you fold/traverse structure