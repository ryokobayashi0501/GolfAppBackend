using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{

    public class GetPuttType
    {
        public static List<string> GetPuttTypeList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> puttType = new List<string>();
            foreach (var value in Enum.GetValues(typeof(PuttType)))
            {
                puttType.Add(value.ToString());
            }
            return puttType;
        }
    }

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
