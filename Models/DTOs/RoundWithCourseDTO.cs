namespace GolfAppBackend.Models.DTOs
{
    public class RoundWithCourseDTO
    {
        public long roundId { get; set; }
        public long courseId { get; set; }
        public DateTime roundDate { get; set; }
        public string courseName { get; set; } = string.Empty;
        public string? imageUri { get; set; }
    }
}
