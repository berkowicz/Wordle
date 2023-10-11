namespace Wordle.Models.ViewModels
{
    public class GameViewModel
    {
        public string? Attempt1 { get; set; }
        public string? Attempt2 { get; set; }
        public string? Attempt3 { get; set; }
        public string? Attempt4 { get; set; }
        public string? Attempt5 { get; set; }

        public int? Score { get; set; }
    }
}
