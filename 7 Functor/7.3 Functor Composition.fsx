//Maybe + List

type Maybe<'a> = Nothing | Just of 'a

module Maybe = 
    let map f = function Nothing -> Nothing | Just x -> Just (f x)

module List = 
    let maybeTail = function [] -> Nothing | _::tail -> Just tail
    let tryTail = function [] -> None | _::tail -> Some tail
    

[] |> List.maybeTail |> Maybe.map (List.map (sprintf "my value is %i")) //Nothing

[1] |> List.maybeTail |> Maybe.map (List.map (sprintf "my value is %i")) //Just []
[1;2] |> List.maybeTail |> Maybe.map (List.map (sprintf "my value is %i")) //Just ["my value is 2"]

//In fsharp you can use Option in place of Maybe like this :

[1] |> List.tryTail |> Option.map (List.map (sprintf "my value is %i")) //Some []
[1;2] |> List.tryTail |> Option.map (List.map (sprintf "my value is %i")) //Some ["my value is 2"]