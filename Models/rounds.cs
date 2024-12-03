using System;
using System.ComponentModel.DataAnnotations;
using WebApi_test.models;
using System.Text.Json.Serialization;

namespace GolfAppBackend.Models
{
    public class Round
    {
        [Key]
        public long roundId { get; set; }

        [Required]  // userId を必須に設定
        public long userId { get; set; }

        [Required]  // courseId を必須に設定
        public long courseId { get; set; }

        [Required]  // roundDate を必須に設定
        public DateTime roundDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public Course? Course { get; set; }

        [JsonIgnore]
        public ICollection<RoundHole> RoundHoles { get; set; } = new List<RoundHole>();
    }

}
