using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Get()
        {
            return Ok();
        }
        
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get(Guid gameId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame(GameInputModel game)
        {
            return Ok();
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, GameInputModel game)
        {
            return Ok();
        }
        
        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, double price)
        {
            return Ok();
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame(Guid gameId)
        {
            return Ok();
        }
    }
}