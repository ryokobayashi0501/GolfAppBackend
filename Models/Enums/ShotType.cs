using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShotType
    {
        // 一般的なショット
        Straight,
        Fade,
        Draw,
        Slice,
        Hook,
        Top,
        Chunk,
        Shank,
        Punch,
        BumpAndRun,
    }
}
