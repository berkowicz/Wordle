using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wordle.Models
{
    public class HighscoreModel
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public int Timer { get; set; }

        [ForeignKey("Game")]
        public int GameRefId { get; set; }
        public virtual GameModel Game { get; set; }
    }
}
