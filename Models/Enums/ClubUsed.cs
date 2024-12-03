using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    public class GetClubUsed
    {
        public static List<string> GetClubUsedList()
        {
            // Retrieve all possible values from the enum and convert to a list of strings
            List<string> clubUsed = new List<string>();
            foreach (var value in Enum.GetValues(typeof(ClubUsed)))
            {
                clubUsed.Add(value.ToString());
            }
            return clubUsed;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ClubUsed
    {
        Driver,
        FairwayWood_3W,
        FairwayWood_5W,
        FairwayWood_7W,
        Hybrid_3H,
        Hybrid_4H,
        Hybrid_5H,
        Iron_2i,
        Iron_3i,
        Iron_4i,
        Iron_5i,
        Iron_6i,
        Iron_7i,
        Iron_8i,
        Iron_9i,
        PitchingWedge_PW,
        GapWedge_GW,
        SandWedge_SW,
        LobWedge_LW,
        Putter // パッティングの場合
    }
}
