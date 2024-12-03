using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PuttType
    {
        // パッティング
        StraightPutt,
        BreakingToRightPutt,
        BreakingToLeftPutt,
        LagPutt,
        DownhillPutt,
        UphillPutt,
        ShortPut
    }
}
