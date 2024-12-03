using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GolfAppBackend.Models.Enums;

namespace GolfAppBackend.Models.DTOs
{
    public class ShotDto
    {
        [JsonIgnore]
        public long shotId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Shot number must be a positive integer.")]
        public int shotNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Distance must be a non-negative integer.")]
        public int distance { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Remaining distance must be a non-negative integer.")]
        public int remainingDistance { get; set; }

        [Required]
        public ClubUsed clubUsed { get; set; }

        [Required]
        public BallDirection ballDirection { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Enum shotType { get; set; }

        [Required]
        public string shotTypeName { get; set; } // Enumの種類を示す（ShotType or PuttType）

        [Required]
        public BallHeight ballHeight { get; set; }

        [Required]
        public Lie lie { get; set; } = Lie.Tee;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Enum shotResult { get; set; }

        public string shotResultName { get; set; } // Enumの種類を示す（ShotResult or PuttResult）

        public string notes { get; set; }
    }

}
