using Wordle.Data;
using Wordle.Models.ViewModels;

namespace Wordle.Models.Helper
{
    public class ProfileHelper
    {
        private readonly ApplicationDbContext _context;

        public ProfileHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        // 
        public ProfileViewModel UserGameData(string userId)
        {
            // Selects all games for logged in user.
            var userGames = _context.Games
                .Where(x => x.UserRefId == userId)
                .ToList();

            float totalGames = 0;
            float wonGames = 0;
            float score = 0;
            int time = 0;

            //Loops each game to update values
            foreach (var userGame in userGames)
            {
                totalGames++;
                // Won games
                if (userGame.GameCompleted is true)
                {
                    wonGames++;
                    time += userGame.CompleteTime;
                    score += userGame.Score;
                }
            }

            return new ProfileViewModel()
            {
                Score = score / wonGames, //Get average score
                Time = time / wonGames, //Get average time
                WinPercent = wonGames / totalGames * 100, // Get winpercent
            };
        }
    }
}

