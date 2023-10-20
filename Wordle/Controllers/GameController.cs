using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wordle.Data;
using Wordle.Models;
using Wordle.Models.Helper;
using Wordle.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wordle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly GameHelper _gameHelper;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
            _gameHelper = new GameHelper(context);
        }

        // POST: api/<GameController>
        [HttpPost]
        [Authorize]
        public NewGameViewModel NewGame() // Create new game
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            var publicId = Guid.NewGuid().ToString();
            var gameWord = _gameHelper.RandomWord(userId);

            //Adds new row to GameModel
            _context.Add(new GameModel() { PublicId = publicId, GameWord = gameWord, UserRefId = userId });
            _context.SaveChanges();
            return new NewGameViewModel() { GameId = publicId };
        }

        // GET api/<GameController>
        [HttpGet]
        [Authorize]
        public IActionResult Get() // Load existing game
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            GameModel loadGame = _gameHelper.FindGame(userId);
            if (loadGame == null)
            {
                return NotFound("No active game");
            }

            FetchGameViewModel gameView = new FetchGameViewModel(loadGame);

            return Ok(gameView); // Returns the GameHelper object as JSON

        }

        // PUT api/<GameController>/guessWord
        [HttpPut("{guess}")]
        [Authorize]
        //Return if correct, or correct characters
        public IActionResult UpdateGame(string guess)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header

            //Updates GameModel
            var gameModel = _gameHelper.UpdateGameModel(userId, guess);
            if (gameModel is null)
            {
                throw new ArgumentNullException("gameid");
            }
            else
            {
                // Returns char array with info of each letters position
                var viewModel = _gameHelper.CheckWord(guess, gameModel.GameWord);

                if (gameModel.GameOver)
                {
                    //Set Word to GameWord to display to user
                    viewModel.Word = gameModel.GameWord;
                }

                return Ok(viewModel);
            }

        }
    }
}
