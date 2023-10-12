using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wordle.Models
{
    public class GameModel
    {
        [Key]
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string GameWord { get; set; }


        public string? Attempt1 { get; set; }
        public string? Attempt2 { get; set; }
        public string? Attempt3 { get; set; }
        public string? Attempt4 { get; set; }
        public string? Attempt5 { get; set; }

        public bool GameCompleted { get; set; } = false;
        public bool GameOver { get; set; } = false;
        public DateTime Timer { get; set; } = DateTime.Now;

        public int Score { get; set; } = 0;

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
