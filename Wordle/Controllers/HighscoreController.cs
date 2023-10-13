using Microsoft.AspNetCore.Mvc;
using Wordle.Data;
using Wordle.Models.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wordle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly GameHelper _gameHelper;

        public HighscoreController(ApplicationDbContext context)
        {
            _context = context;
            _gameHelper = new GameHelper(context);
        }

        // GET api/<HighscoreController>
        [HttpGet]
        public IActionResult Get()
        {
            var highscoreAllTime = _gameHelper.HighscoreAllTime();
            var highscoreToday = _gameHelper.HighscoreToday();

            var result = new
            {
                HighscoreAllTime = highscoreAllTime,
                HighscoreToday = highscoreToday
            };

            return Ok(result);
        }

        // POST api/<HighscoreController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HighscoreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HighscoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
