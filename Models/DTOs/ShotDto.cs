using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GolfAppBackend.Models.Enums;

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
        public ClubUsed clubUsed { get; set; } // Enum representing the club used for the shot

        [Required]
        public BallDirection ballDirection { get; set; } // Enum representing the direction of the ball

        [JsonIgnore]
        public object shotType { get; set; } // Dynamically either ShotType or PuttType (ignored during serialization)

        [Required]
        public string shotTypeName { get; set; } // Indicates the specific type of the shot (e.g., "ShotType" or "PuttType")

        [Required]
        public BallHeight ballHeight { get; set; } // Enum representing the height of the ball trajectory

        [Required]
        public Lie lie { get; set; } // Enum representing the lie (where the ball lies)

        [JsonIgnore]
        public object shotResult { get; set; } // Dynamically either ShotResult or PuttResult (ignored during serialization)

        [Required]
        public string shotResultName { get; set; } // Indicates the specific result of the shot (e.g., "ShotResult" or "PuttResult")

        public string notes { get; set; } // Additional notes on the shot

        // Custom methods for JSON handling of dynamic enums (for shotType and shotResult)

        [JsonPropertyName("shotType")]
        public string ShotTypeAsString
        {
            get
            {
                return shotType != null ? shotType.ToString() : string.Empty;
            }
            set
            {
                // Logic to convert string back to proper Enum type based on shotTypeName
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(shotTypeName))
                {
                    if (shotTypeName == "ShotType")
                    {
                        shotType = Enum.TryParse(typeof(ShotType), value, true, out var parsed) ? parsed : null;
                    }
                    else if (shotTypeName == "PuttType")
                    {
                        shotType = Enum.TryParse(typeof(PuttType), value, true, out var parsed) ? parsed : null;
                    }
                }
            }
        }

        [JsonPropertyName("shotResult")]
        public string ShotResultAsString
        {
            get
            {
                return shotResult != null ? shotResult.ToString() : string.Empty;
            }
            set
            {
                // Logic to convert string back to proper Enum type based on shotResultName
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(shotResultName))
                {
                    if (shotResultName == "ShotResult")
                    {
                        shotResult = Enum.TryParse(typeof(ShotResult), value, true, out var parsed) ? parsed : null;
                    }
                    else if (shotResultName == "PuttResult")
                    {
                        shotResult = Enum.TryParse(typeof(PuttResult), value, true, out var parsed) ? parsed : null;
                    }
                }
            }
        }
    }
}
