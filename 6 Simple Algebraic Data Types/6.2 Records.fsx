let isPrefixOf s x = (x:string).StartsWith s

//This kind of code is hard to reuse outside its context due to the risk of confusion between name and symbol
let startsWithSymbol (name, symbol, _) = isPrefixOf symbol name

//Record Type version
type Element = { Name:string; Symbol:string; AtomicNumber:int }

let tupleToElement (n, s, a) = { Name = n; Symbol = s; AtomicNumber = a }
let elemToTuple e = e.Name, e.Symbol, e.AtomicNumber

//We can swap parameter to make it more human readable:
let isPrefixOf' x y = isPrefixOf y x

//Redefine the new function:
let startsWithSymbol2 e = e.Name |> isPrefixOf' e.Symbol 
