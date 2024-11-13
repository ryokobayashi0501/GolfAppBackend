using System;
using System.ComponentModel.DataAnnotations;

namespace GolfAppBackend.Models
{
    public class Rounds
    {
        [Key]
        public long roundId { get; set; }

        public long userId { get; set; }

        public long courseId { get; set; }

        [Required]
        public DateTime roundDate { get; set; } = DateTime.Now;
    }
}
