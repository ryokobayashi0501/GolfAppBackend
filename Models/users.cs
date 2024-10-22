﻿using System.ComponentModel.DataAnnotations;

namespace WebApi_test.models
{
    public class Users
    {
        [Key]
        public int userId { get; set; }

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

    }
}
