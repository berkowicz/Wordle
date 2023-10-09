using System.ComponentModel.DataAnnotations.Schema;

namespace Wordle.Models
{
    public class GameModel
    {
        
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string GameWord { get; set; }

        
        public string? Attempt1 { get; set; } 
        public string? Attempt2 { get; set; }
        public string? Attempt3 { get; set; }
        public string? Attempt4 { get; set; } 
        public string? Attempt5 { get; set; }

        public bool DidNotFinish { get; set; }
        public bool GameCompleted { get; set; } = false;

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User {  get; set; }


    }
}
