using System;
using System.ComponentModel.DataAnnotations;
using WebApi_test.models;

namespace GolfAppBackend.Models
{
    public class Round
    {
        [Key]
        public long roundId { get; set; }

        public long userId { get; set; }

        public long courseId { get; set; }

        [Required]
        public DateTime roundDate { get; set; } = DateTime.Now;

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
