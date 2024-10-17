namespace SqlModels.Entities.Attributes;

public class CurrencySymbolAttribute : Attribute
{
    public string Symbol { get; }

    public CurrencySymbolAttribute(string symbol)
    {
        Symbol = symbol;
    }
}