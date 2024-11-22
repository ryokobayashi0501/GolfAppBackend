using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GolfAppBackend.Models
{
    public class Course
    {
        [Key]
        public long? courseId { get; set; }

        [Required]
        public string courseName { get; set; }

        // Holesのコレクション
        public List<Hole> Holes { get; set; } = new List<Hole>();

        // Roundsのコレクションをnullableにし、バリデーションを無視する
        [JsonIgnore]
        [ValidateNever]
        public List<Round>? Rounds { get; set; } = new List<Round>();
    }
}
