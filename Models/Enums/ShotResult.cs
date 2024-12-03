using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetShotResult
    {
        public static List<string> GetShotResultList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> shotResult = new List<string>();
            foreach (var value in Enum.GetValues(typeof(ShotResult)))
            {
                shotResult.Add(value.ToString());
            }
            return shotResult;
        }
    }

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
        ShotHoled, // パットがホールアウトした場合
    }
}
