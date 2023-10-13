using Microsoft.CodeAnalysis;
using NuGet.Protocol;
using Wordle.Data;
using Wordle.Models.ViewModels;

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
                .Where(x => x.UserRefId == refId && x.GameCompleted == false && x.GameOver == false)
                .Single();

            return game;
        }
    
        // Updates GameModel in DB and add to highscore if game is won
        public GameModel UpdateGameModel(string userId, string guess)
        {
            // Finds gamesession
            var game = FindGame(userId);

            // If game is not found or finished return null
            if (game == null || game.GameOver is true || game.GameCompleted is true)
            {
                return null;
            }
            // Else update Gamestate
            else
            {
                char[] guessArr = guess.ToCharArray();
                char[] letterStatus = new char[5];
                
                string attemptJson = new GameViewModel { Guess = guessArr, LetterStatus = GetLetterStatus( guess, game.GameWord) }.ToJson();
                
                game.Score++; //Dual purpose. Keeps track of attemps untill game is finished. Then keeprs score

                switch (game.Score) //Set guess to attempt(x)
                {
                    case 1:
                        game.Attempt1 = attemptJson;
                        break;
                    case 2:
                        game.Attempt2 = attemptJson;
                        break;
                    case 3:
                        game.Attempt3 = attemptJson;
                        break;
                    case 4:
                        game.Attempt4 = attemptJson;
                        break;
                    case 5:
                        game.Attempt5 = attemptJson;
                        break;
                }
                // Win scenario also Post result to Highscore
                if (game.GameWord.ToUpper().Equals(guess.ToUpper()))
                {
                    game.GameCompleted = true;
                    HighscoreModel x = new HighscoreModel()
                    {
                        Score = game.Score,
                        Timer = (game.Timer.Second - DateTime.Now.Second),
                        Date = DateTime.Now.Date,
                        GameRefId = game.Id,
                    };
                    _context.Highscores.Add(x);
                }
                // Game Over scenario
                else if (game.Score == 5)
                {
                    game.GameOver = true;
                    game.Score = 0;
                }
                // Save changes and return GameModel
                _context.SaveChanges();
                return game;
            }

        }

        //Checks the placement of each letter in guess
        public GameViewModel CheckWord(string guess, string gameWord)
        {
           return new GameViewModel { Guess = guess.ToCharArray(), LetterStatus = GetLetterStatus( guess, gameWord ) };
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

        private char[] GetLetterStatus(string guess, string gameWord)
        {
            char[] guessArr = guess.ToUpper().ToCharArray();
            char[] gameWordArr = gameWord.ToUpper().ToCharArray();
            char[] letterStatus = new char[5];
            
            if (guess.Equals(gameWord))
            {
                letterStatus = new char[] { '1', '1', '1', '1', '1' };
            }
            // Else loop to see each letter position
            else
            {
                for (int i = 0; i < guessArr.Length; i++)
                {
                    if (gameWord.Contains(guessArr[i]))
                    {
                        if (guessArr[i].Equals(gameWordArr[i]))
                        {
                            letterStatus[i] = '1'; //Letter in right place
                        }
                        else
                            letterStatus[i] = '2'; //Contains letter but wrong place
                    }
                    else
                    {
                        letterStatus[i] = '3'; //Doesn't contain letter
                    }
                }
            }

            return letterStatus;
        }

    }
}
