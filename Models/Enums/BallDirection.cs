using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetBallDirection 
    {
        public static List<string> GetBallDirectionList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> ballDirections = new List<string>();
            foreach (var value in Enum.GetValues(typeof(BallDirection)))
            {
                ballDirections.Add(value.ToString());
            }
            return ballDirections;
        }
    }


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
