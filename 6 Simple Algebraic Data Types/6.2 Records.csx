static class Functions
{
    public static string isPrefixOf(string s, string x) => x.StartsWith(s);
    //swap argument
    public static string isPrefixOf2(string x, string s) => isPrefixOf(s, x);
    //This kind of code is hard to reuse outside its context due to the risk of confusion between name and symbol
    public static bool startsWithSymbol(string name, string symbol, bool _) => isPrefixOf(symbol, name);
}

struct Element
{
    public string Name { get; }
    public string Symbol { get; }
    public int AtomicNumber { get; }

    public static Element tupleToElement (string n, string s, int a) => Element(Name = n, Symbol = s, AtomicNumber = a);
    public static elemToTuple(Element e) => (e.Name, e.Symbol, e.AtomicNumber);
    //Redefine the new function:
    //I don't know how to do a human readable
    public static bool startsWithSymbol2 (Element e) => Functions.isPrefixOf2(e.Symbol, e.Name);
}

//Personal notes
// link : https://github.com/dotnet/csharplang/blob/master/proposals/records.md
//I will use a struct instead but at the same time I will loose the deconstruct pattern

