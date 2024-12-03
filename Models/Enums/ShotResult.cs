using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShotResult
    {
        Perfect,
        Draw,
        Fade,
        Hook,
        Slice,
        Pull,
        Push,
        Shank,
        Top,
        Chunk,
        Sky,
        Flub,
        Punch,
        Recovery,
        LayUp,
        Holed, // パットがホールアウトした場合
    }
}
