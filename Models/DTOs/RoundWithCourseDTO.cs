namespace GolfAppBackend.Models.DTOs
{
    public class RoundWithCourseDTO
    {
        public long RoundId { get; set; }
        public long CourseId { get; set; }
        public DateTime RoundDate { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string? ImageUri { get; set; }
    }
}
