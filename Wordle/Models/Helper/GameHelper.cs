using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
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
                .FirstOrDefault();

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

                
                //View model to save attempt status to db
                GameViewModel attempt = new GameViewModel { Guess = guessArr, LetterStatus = GetLetterStatus(guess, game.GameWord) };
                if (game.GameWord.ToUpper().Equals(guess.ToUpper())) 
                {
                    attempt.Correct = true;
                }

                string attemptJson = attempt.ToJson();

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
                    HighscoreModel hsModel = new HighscoreModel()
                    {
                        Score = game.Score,
                        Timer = (game.Timer.Second - DateTime.Now.Second),
                        Date = DateTime.Now.Date,
                        GameRefId = game.Id,
                    };
                    game.CompleteTime = hsModel.Timer;
                    _context.Highscores.Add(hsModel);
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

            GameViewModel result = new GameViewModel
                { Guess = guess.ToCharArray(), LetterStatus = GetLetterStatus(guess.ToUpper(), gameWord.ToUpper()) };
            
            if (guess.ToUpper() == gameWord.ToUpper())
            {
                result.Correct = true;
            }

            return result;
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

        public string RandomWord(string refId)
        {
            string jsonString = File.ReadAllText("./Data/wordlist.json"); //Get list of words
            
            JObject jsonData = JObject.Parse(jsonString); //Parse to Json object
            JArray words = jsonData["words"] as JArray; //Make array of key "words"
                                                        //string randomWord = words[new Random().Next(words.Count)].ToString().ToUpper();
            string randomWord;
            bool wordExistsInGame;

            do
            {
                // Get a new random word
                randomWord = words[new Random().Next(words.Count)].ToString().ToUpper();

                // Get last n games 
                var lastGames = _context.Games
                    .Where(x => x.UserRefId == refId)
                    .OrderByDescending(x => x.Timer) // Assuming you have an "Id" or a timestamp to determine the order
                    .Take(25)
                    .ToList();

                //Check if user got the word reacently, then re-run the generator
                wordExistsInGame = lastGames.Any(game => game.GameWord == randomWord);

            } while (wordExistsInGame);


            return randomWord;
        }

    }
}
