using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RefactoringKata
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Color
    {
        [EnumMember(Value = "no color")]
        unknown = 0,
        blue = 1,
        red,
        yellow
    }
}