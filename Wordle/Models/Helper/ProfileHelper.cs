using Microsoft.EntityFrameworkCore;
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

        public ProfileViewModel UserGameData(string userId)
        {
            // Sort out alltime top 10 ressult by score then by time.
            var userGames = _context.Games
                .Include(x => x.UserRefId == userId)
                .ToList();

            int totalGames = 0;
            int wonGames = 0;
            int score = 0;
            int time = 0;

            foreach (var userGame in userGames)
            {
                totalGames++;
                if (userGame.GameCompleted is true)
                {
                    var hsModel = _context.Highscores
                        .Include(x => x.GameRefId == userGame.Id)
                        .FirstOrDefault();
                    time += hsModel.Timer;
                    wonGames++;
                    score += userGame.Score;
                }
            }

            return new ProfileViewModel
            {
                Score = score,
                Time = time,
                WinPercent = wonGames / totalGames * 100,
            };
        }
    }
}

