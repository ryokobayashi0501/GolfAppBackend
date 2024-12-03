using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BallHeight
    {
        Low,
        Medium,
        High,
        Default // パッティングなど関係ない場合
    }
}
