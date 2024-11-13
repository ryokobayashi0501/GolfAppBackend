using System.ComponentModel.DataAnnotations;

namespace GolfAppBackend.Models
{
    public class Holes
    {
        [Key]
        public long holeId { get; set; }

        public long courseId { get; set; }
        public int holeNumber { get; set; }
        public int par { get; set; }
        public int yardage { get; set; }
    }
}
