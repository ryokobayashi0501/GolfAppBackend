using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PuttResult
    {
        // パッティング
        ShortofHole,
        LongofHole,
        LeftofHole,
        RightofHole,
        LipOutFringe,
        Holed
    }
}
