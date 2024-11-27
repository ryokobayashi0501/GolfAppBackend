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

        public long userId { get; set; }

        public long courseId { get; set; }

        
        public DateTime roundDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
