namespace GolfAppBackend.Models
{
    public class Shot
    {
        public long shotID { get; set; }            // Primary Key
        public long roundID { get; set; }           // Foreign Key to Round
        public int remainingDistance { get; set; }  // Distance remaining to the hole
        public string clubUsed { get; set; } = "";    // Club used for the shot
        public string shotTrajectory { get; set; } = ""; // Description of the shot's trajectory
        public string shotResult { get; set; } = "";      // Outcome of the shot
        public int score { get; set; }              // Score for the shot (if applicable)
    }
}
