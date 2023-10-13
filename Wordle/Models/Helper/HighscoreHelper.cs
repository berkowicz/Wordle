using Wordle.Data;
using Wordle.Models.ViewModels;

namespace Wordle.Models.Helper
{
    public class HighscoreHelper
    {
        private readonly ApplicationDbContext _context;

        public HighscoreHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        // Highscore Alltime
        public IEnumerable<HighscoreViewModel> HighscoreAllTime()
        {
            // Sort out alltime top 10 ressult by score then by time.
            List<HighscoreModel> alltime = _context.Highscores
                .OrderByDescending(x => x.Score)
                .ThenByDescending(x => x.Timer)
                .Take(10)
                .ToList();

            List<HighscoreViewModel> hsViewModel = new List<HighscoreViewModel>(HighscoreToViewModel(alltime));

            return hsViewModel;
        }

        // Highscore Today
        public IEnumerable<HighscoreViewModel> HighscoreToday()
        {
            // Sort out todays top 10 ressult by score then by time.
            List<HighscoreModel> today = _context.Highscores
                .Where(x => x.Date == DateTime.Now.Date)
                .OrderByDescending(x => x.Score)
                .ThenByDescending(x => x.Timer)
                .Take(10)
                .ToList();

            List<HighscoreViewModel> hsViewModel = new List<HighscoreViewModel>(HighscoreToViewModel(today));

            return hsViewModel;
        }

        // Creates HighscoreViewModel list of HighscoreModel
        public IEnumerable<HighscoreViewModel> HighscoreToViewModel(IEnumerable<HighscoreModel> hsModel)
        {
            List<HighscoreViewModel> hsViewModel = new List<HighscoreViewModel>();

            // Puts ressults into viewmodel
            foreach (HighscoreModel model in hsModel)
            {
                HighscoreViewModel x = new HighscoreViewModel()
                {
                    Score = model.Score,
                    Timer = model.Timer,
                    Date = model.Date,
                };
                hsViewModel.Add(x);
            }

            return hsViewModel;
        }
    }
}
