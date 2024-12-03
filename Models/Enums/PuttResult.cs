using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetPuttResult
    {
        public static List<string> GetPuttResultList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> puttResult = new List<string>();
            foreach (var value in Enum.GetValues(typeof(PuttResult)))
            {
                puttResult.Add(value.ToString());
            }
            return puttResult;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PuttResult
    {
        // パッティング
        ShortofHole,
        LongofHole,
        LeftofHole,
        RightofHole,
        LipOutFringe,
        PuttHoled
    }
}
