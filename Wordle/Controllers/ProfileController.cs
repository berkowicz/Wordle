using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Get() // Get logged in user statistics, average values.
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ProfileViewModel userData = _profileHelper.UserGameData(userId);

            return Ok(userData); // Returns the ProfileHelper object as JSON
        }
    }
}
