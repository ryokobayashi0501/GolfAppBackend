namespace GolfAppBackend.Models
{
    public class RoundDetail
    {
        public long roundDetailID { get; set; } // Primary Key
        public long roundID { get; set; }       // Foreign Key to Round
        public long shotID { get; set; }        // Foreign Key to Shot
        public long holeNumber { get; set; }    // The number of the hole
    }
}
