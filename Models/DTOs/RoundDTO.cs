namespace GolfAppBackend.Models.DTOs
{
    public class RoundDTO
    {
        public long roundId { get; set; }
        public long courseId { get; set; }
        public long userId { get; set; }
        public string courseName { get; set; }
        public string imageUri { get; set; }
        public DateTime roundDate { get; set; }
    }


}
