namespace Wordle.Models.ViewModels
{
    public class GameViewModel
    {
        public char[] Guess { get; set; }
        public char[] LetterStatus { get; set; }
    }
}
