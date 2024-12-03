using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BallDirection
    {
        Fairway,
        LeftRough,
        RightRough,
        LeftTrees,
        RightTrees,
        LeftOB,
        RightOB,
        WaterHazardLeft,
        WaterHazardRight,
        WaterHazardFront,
        SandBunkerLeft,
        SandBunkerRight,
        Green,
        Fringe,
        DeepRough,
        CartPath,
        Unplayable,
        BehindTrees,
        InBoundsPenalty,
        Holed, // ホールアウトした場合
    }
}
