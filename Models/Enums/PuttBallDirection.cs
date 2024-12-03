using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetPuttBallDirection
    {
        public static List<string> GetPuttBallDirectionList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> puttBallDirection = new List<string>();
            foreach (var value in Enum.GetValues(typeof(PuttBallDirection)))
            {
                puttBallDirection.Add(value.ToString());
            }
            return puttBallDirection;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PuttBallDirection
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
