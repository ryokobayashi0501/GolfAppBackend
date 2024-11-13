using System.ComponentModel.DataAnnotations;

namespace GolfAppBackend.Models.DTOs
{
    public class UserDto
    {

        [Key]
        public long userId { get; set; }

        [Required]
        public string name { get; set; } = "";

        public string username { get; set; } = "";

        [Required]
        public string email { get; set; } = "";

        public int yearsOfExperience { get; set; }

        public int averageScore { get; set; }

        public int practiceFrequency { get; set; }

        public int scoreGoal { get; set; }

        public string puttingGoal { get; set; } = string.Empty;

        public String approachGoal { get; set; } = "";
        public String shotGoal { get; set; } = "";

        public String passwordHash { get; set; } = "";
        public DateTime createdAt { get; set; } = DateTime.Now;

    }
}
