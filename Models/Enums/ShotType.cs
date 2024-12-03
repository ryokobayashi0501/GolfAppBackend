using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetShotType
    {
        public static List<string> GetShotTypeList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> shotType = new List<string>();
            foreach (var value in Enum.GetValues(typeof(ShotType)))
            {
                shotType.Add(value.ToString());
            }
            return shotType;
        }
    }

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
