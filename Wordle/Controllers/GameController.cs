﻿using Microsoft.AspNetCore.Mvc;
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

        public GameController(ApplicationDbContext context, GameHelper gameHelper)
        {
            _context = context;
            _gameHelper = gameHelper;
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
        [HttpGet("{id}")]
        public GameModel? Get(string gameGuid)
        {
            GameModel loadGame = _gameHelper.FindGame(gameGuid);

            return loadGame;
        }

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
