using System.CodeDom;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RefactoringKata
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Color
    {
        unknown = 0,
        blue = 1,
        red,
        yellow
    }
}