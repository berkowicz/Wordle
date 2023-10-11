using Microsoft.CodeAnalysis;
using Wordle.Data;

namespace Wordle.Models.Helper
{
    public class GameHelper
    {
        private readonly ApplicationDbContext _context;

        public GameHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public GameModel? FindGame(string refId)
        {
            GameModel? game = _context.Games
                .Where(x => x.UserRefId == refId && x.GameCompleted == false)   
                .Single();

            return game;
        }

        public GameModel UpdateGameSession(string gameId, string guess)
        {
            GameModel? game = _context.Games.Where(x => x.PublicId == gameId).FirstOrDefault();

            game.Score++;

            switch (game.Score)
            {
                case 2:
                    game.Attempt1 = guess;
                    break;
                case 3:
                    game.Attempt2 = guess;
                    break;
                case 4:
                    game.Attempt3 = guess;
                    break;
                case 5:
                    game.Attempt4 = guess;
                    break;
                case 6:
                    game.Attempt5 = guess;
                    break;
            }

            if (game.GameWord.ToUpper().Equals(guess.ToUpper()))
            {
                game.GameCompleted = true;
                game.Score--;
            }
            else if (game.Score == 6)
            {
                game.GameOver = true;
                game.Score = null;
            }

            _context.SaveChanges();
            return game;
        }
    }
}
