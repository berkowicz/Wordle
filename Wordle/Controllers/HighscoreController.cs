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
        private readonly HighscoreHelper _highscoreHelper;

        public HighscoreController(ApplicationDbContext context)
        {
            _context = context;
            _highscoreHelper = new HighscoreHelper(context);
        }

        // GET api/<HighscoreController>
        [HttpGet]
        public IActionResult Get() // Get global top 10 highscore all-time and today
        {
            var highscoreAllTime = _highscoreHelper.HighscoreAllTime();
            var highscoreToday = _highscoreHelper.HighscoreToday();

            var result = new
            {
                HighscoreAllTime = highscoreAllTime,
                HighscoreToday = highscoreToday
            };

            // Returns nested arrays in JSON object
            return Ok(result);
        }
    }
}
