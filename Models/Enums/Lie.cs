using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Lie
    {
        Tee,
        Fairway,
        Rough,
        DeepRough,
        Bunker,
        Hardpan,
        Divot,
        Uphill,
        Downhill,
        Sidehill_BallBelowFeet,
        Sidehill_BallAboveFeet,
        Green,
        Fringe
    }
}
