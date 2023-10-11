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

        // POST: api/game
        [HttpPost]
        public NewGameViewModel NewGame()
        {


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get user ID from header


            var publicId = Guid.NewGuid().ToString();
            var gameWord = "hello";
            _context.Add(new GameModel() { PublicId = publicId, GameWord = gameWord.ToUpper(), UserRefId = userId });
            _context.SaveChanges();
            return new NewGameViewModel() { GameId = publicId };
        }

        // GET api/game
        [HttpGet]
        public IActionResult Get()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Not authorized");
            }

           

            GameModel loadGame = _gameHelper.FindGame(userId);
            if (loadGame == null)
            {
                return NotFound("No active game");
            }
            return Ok(loadGame); // Returns the GameHelper object as JSON

        }


        /*[HttpPut("{gameid}/{guess}")]
        //Return if correct, or correct characters
        public IActionResult UpdateGame(string gameid, string guess)
        {
            GameModel? update = _gameHelper.UpdateGameSession(gameid, guess, HttpContext);

            if (HttpContext.Response.StatusCode == 404)
            {
                return NotFound("Could not find game");
            }
            else if (HttpContext.Response.StatusCode == 400)
            {
                return BadRequest("You allready played this game through");
            }
            else
            {
                return Ok(update);
            }
        }*/

        [HttpPut("{gameid}/{guess}")]
        //Return if correct, or correct characters
        public IActionResult UpdateGame(string gameid, string guess)
        {
            var gameModel = _gameHelper.UpdateGameModel(gameid, guess);
            if (gameModel is null)
            {
                throw new ArgumentNullException("gameid");
            }
            else
            {
                // Returns char array with info of each letters position
                var viewModel = _gameHelper.CheckWord(guess.ToUpper(), gameModel.GameWord.ToUpper());
                return Ok(viewModel);
            }

        }


        [HttpPost("highscore")]
        //Post finished game to high score


        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
