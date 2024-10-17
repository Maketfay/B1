namespace SqlModels.Entities.Attributes;

public class CurrencyAsciiSymbolAttribute : Attribute
{
    public string Symbol { get; }

    public CurrencyAsciiSymbolAttribute(string symbol)
    {
        Symbol = symbol;
    }
}