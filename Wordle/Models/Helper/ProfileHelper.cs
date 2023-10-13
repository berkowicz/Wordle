using Wordle.Data;

namespace Wordle.Models.Helper
{
    public class ProfileHelper
    {
        private readonly ApplicationDbContext _context;

        public ProfileHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        /*public ProfileViewModel UserGameData(string userId)
        {
            // Sort out alltime top 10 ressult by score then by time.
            var userGames = _context.Games
                .Include(x => x.UserRefId == userId)
                .ToList();
        }*/
    }
}
