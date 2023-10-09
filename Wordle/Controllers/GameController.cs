using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Text.Json.Serialization;
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

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/game
        [HttpPost]
        public NewGameViewModel NewGame()
        {
            var publicId = Guid.NewGuid().ToString();
            var gameword = "hello";
            _context.Add(new GameModel() { PublicId = publicId, GameWord = gameword });
            _context.SaveChanges();
            return new NewGameViewModel() { GameId = publicId };
        }

        // GET api/<GameController>/5
        [HttpGet("{UserId}")]
        public IActionResult Get(string UserId)
        {
            GameHelper helper = new GameHelper(_context);

            GameModel loadGame = helper.FindGame(UserId);

            return Ok(loadGame); // Returns the GameHelper object as JSON
          
        }


        [HttpPatch("{gameid}/{word}/{attempt}")]
        //Return if correct, or correct characters

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
