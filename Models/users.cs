using GolfAppBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApi_test.models
{
    public class User
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

        public double puttingGoal { get; set; }

        public String approachGoal { get; set; } = "";
        public String shotGoal { get; set; } = "";

        public String passwordHash { get; set; } = "";
        public DateTime createdAt { get; set; } = DateTime.Now;

        public ICollection<Round> Rounds { get; set; }

        // コンストラクタで初期化
        public User()
        {
            Rounds = new HashSet<Round>();
        }
    }
}
