using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Security.Claims;
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


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var publicId = Guid.NewGuid().ToString();
            var gameword = "hello";
            _context.Add(new GameModel() { PublicId = publicId, GameWord = gameword, UserRefId = userId });
            _context.SaveChanges();
            return new NewGameViewModel() { GameId = publicId };
        }

        // GET api/<GameController>/5
        [HttpGet]
        public IActionResult Get()
        {
            GameHelper helper = new GameHelper(_context);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         
            if (userId == null)
            {
                return Unauthorized("Not authorized");
            }


            GameModel loadGame = helper.FindGame(userId);

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
