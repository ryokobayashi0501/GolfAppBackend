using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetLie
    {
        public static List<string> GetLieList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> lie = new List<string>();
            foreach (var value in Enum.GetValues(typeof(Lie)))
            {
                lie.Add(value.ToString());
            }
            return lie;
        }
    }

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
