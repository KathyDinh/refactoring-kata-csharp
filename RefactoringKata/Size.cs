using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RefactoringKata
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Size
    {
        NotApplicable = -1,
        [EnumMember(Value = "Invalid Size")]
        Invalid = 0,
        XS = 1,
        S,
        M,
        L,
        XL,
        XXL
    }
}