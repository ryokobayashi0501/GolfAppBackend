namespace GolfAppBackend.Models
{
    public class Round
    {
        public long roundID { get; set; }  // Primary Key
        public long userID { get; set; }   // Foreign Key to User
        public long courseID { get; set; } // Foreign Key to Course
        public DateTime roundDate { get; set; }
    }
}
