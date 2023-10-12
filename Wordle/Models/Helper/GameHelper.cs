using Microsoft.CodeAnalysis;
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
                .Where(x => x.UserRefId == refId && x.GameCompleted == false)
                .Single();

            return game;
        }

        // Updates GameModel in DB and add to highscore if game is won
        public GameModel UpdateGameModel(string gameId, string guess)
        {
            var game = _context.Games.Where(x => x.PublicId == gameId).SingleOrDefault();

            // If game is not found or finished return null
            if (game == null || game.GameOver is true || game.GameCompleted is true)
            {
                return null;
            }
            // Else update Gamestate
            else
            {
                game.Score++; //Dual purpose. Keeps track of attemps untill game is finished. Then keeprs score

                switch (game.Score) //Set guess to attempt(x)
                {
                    case 1:
                        game.Attempt1 = guess;
                        break;
                    case 2:
                        game.Attempt2 = guess;
                        break;
                    case 3:
                        game.Attempt3 = guess;
                        break;
                    case 4:
                        game.Attempt4 = guess;
                        break;
                    case 5:
                        game.Attempt5 = guess;
                        break;
                }
                // Win scenario also Post result to Highscore
                if (game.GameWord.ToUpper().Equals(guess.ToUpper()))
                {
                    game.GameCompleted = true;
                    //game.Score--; //Db wont allow 0 so -- because counter starts at 1. 
                    HighscoreModel x = new HighscoreModel() { Score = game.Score, Timer = (DateTime.Now.Second - game.Timer.Second), GameRefId = game.Id };
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
            char[] guessArr = guess.ToCharArray();
            char[] gameWordArr = gameWord.ToCharArray();
            char[] letterStatus = new char[5];

            // If Word correct return all 1
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
            return new GameViewModel { Guess = guessArr, LetterStatus = letterStatus };
        }
    }
}
