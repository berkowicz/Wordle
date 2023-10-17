namespace Wordle.Models.ViewModels;

public class FetchGameViewModel
{
    public FetchGameViewModel(GameModel game)
    {
        Attempt1 = game.Attempt1;
        Attempt2 = game.Attempt2;
        Attempt3 = game.Attempt3;
        Attempt4 = game.Attempt4;
        Attempt5 = game.Attempt5;
    }
    public string? Attempt1 { get; set; }
    public string? Attempt2 { get; set; }
    public string? Attempt3 { get; set; }
    public string? Attempt4 { get; set; }
    public string? Attempt5 { get; set; }
}