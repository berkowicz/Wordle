using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wordle.Data;
using Wordle.Models.Helper;
using Wordle.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wordle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProfileHelper _profileHelper;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
            _profileHelper = new ProfileHelper(context);
        }

        // GET: api/<ProfileController>
        [HttpGet]
        public IActionResult Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Not authorized");
            }

            ProfileViewModel userData = _profileHelper.UserGameData(userId);
            if (userData == null)
            {
                return NotFound("No active game");
            }
            return Ok(userData); // Returns the GameHelper object as JSON
        }
    }
}
