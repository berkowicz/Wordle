using Microsoft.EntityFrameworkCore;
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
                .Include(x => x.UserRefId == refId)
                .Include(x => x.GameCompleted == false)
                .FirstOrDefault();

            return game;
        }
    }
}
