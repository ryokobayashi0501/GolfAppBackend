using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GolfAppBackend.Models.DTOs
{
    public class ShotDto
    {
        [JsonIgnore]
        public long shotId { get; set; } // Ignored during serialization

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Shot number must be a positive integer.")]
        public int shotNumber { get; set; } // Represents the number of the shot in sequence

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Distance must be a non-negative integer.")]
        public int distance { get; set; } // Total distance covered in the shot

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Remaining distance must be a non-negative integer.")]
        public int remainingDistance { get; set; } // Remaining distance after the shot

        [Required]
        public string clubUsed { get; set; } // Enum representing the club used for the shot

        [Required]
        public string ballDirection { get; set; } // Enum representing the direction of the ball

        [Required]
        public string shotTypeName { get; set; } // Indicates the specific type of the shot (e.g., "ShotType" or "PuttType")

        [Required]
        public string shotType { get; set; } // Enum name representing either ShotType or PuttType

        [Required]
        public string ballHeight { get; set; } // Enum representing the height of the ball trajectory

        [Required]
        public string lie { get; set; } // Enum representing the lie (where the ball lies)

        [Required]
        public string shotResultName { get; set; } // Indicates the specific result of the shot (e.g., "ShotResult" or "PuttResult")

        [Required]
        public string shotResult { get; set; } // Enum name representing either ShotResult or PuttResult

        public string notes { get; set; } // Additional notes on the shot
    }
}
