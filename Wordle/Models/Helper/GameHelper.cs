using Microsoft.CodeAnalysis;
using System.Net;
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
                .Where(x => x.UserRefId == refId)
                .Where(x => x.GameCompleted == false)
                .FirstOrDefault();

            return game;
        }

        public GameModel? UpdateGameSession(string gameId, string guess, HttpContext httpContext)
        {
            GameModel? game = _context.Games.Where(x => x.PublicId == gameId).SingleOrDefault();

            if (game == null)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return game;
            }
            else if (game.GameOver is true || game.GameCompleted is true)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return game;
            }
            else
            {
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
}
