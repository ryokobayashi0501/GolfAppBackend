using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GolfAppBackend.Models
{
    public class Hole
    {
        [Key]
        public long holeId { get; set; }
        public long courseId { get; set; }

        [Required]
        public int holeNumber { get; set; }

        [Required]
        public int par { get; set; }

        [Required]
        public int yardage { get; set; }

        // Courseのナビゲーションプロパティをnullableにし、バリデーションを無視する
        [JsonIgnore]
        [ValidateNever]
        public Course? Course { get; set; }

        public List<RoundHole> RoundHoles { get; set; } = new List<RoundHole>();
    }
}
