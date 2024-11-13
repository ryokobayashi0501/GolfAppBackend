using System.ComponentModel.DataAnnotations;

namespace GolfAppBackend.Models
{
    public class Courses
    {
        [Key]
        public long courseId { get; set; }

        //public long userId { get; set; }

        [Required]
        public string courseName { get; set; } = "";
    }
}
