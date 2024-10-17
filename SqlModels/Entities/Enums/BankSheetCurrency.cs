using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SqlModels.Entities.Attributes;

namespace SqlModels.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BankSheetCurrency
{
    [EnumMember]
    [NotSupportedEnumMember]
    Unknown,
    
    [EnumMember]
    [CurrencySymbol("$")]
    [CurrencyAsciiSymbol("USD")]
    // ReSharper disable once InconsistentNaming
    USD,
    
    [EnumMember]
    [CurrencySymbol("\u20bd")]
    [CurrencyAsciiSymbol("RUB")]
    // ReSharper disable once InconsistentNaming
    RUB,
    
    [EnumMember]
    [CurrencySymbol("BYN")]
    [CurrencyAsciiSymbol("BYN")]
    // ReSharper disable once InconsistentNaming
    BYN,
}