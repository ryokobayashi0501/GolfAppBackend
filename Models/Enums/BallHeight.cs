using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetBallHeight
    {
        public static List<string> GetBallHeightList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> ballHeight = new List<string>();
            foreach (var value in Enum.GetValues(typeof(BallHeight)))
            {
                ballHeight.Add(value.ToString());
            }
            return ballHeight;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BallHeight
    {
        Low,
        Medium,
        High,
        Default // パッティングなど関係ない場合
    }
}
