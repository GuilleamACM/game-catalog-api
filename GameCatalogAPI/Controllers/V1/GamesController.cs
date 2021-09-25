using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Services;
using GameCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService m_gameService;

        public GamesController(IGameService gameService)
        {
            m_gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await m_gameService.Get(page, quantity);
            
            if (games.Count == 0)
                return NoContent();
            
            return Ok(games);
        }
        
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameId)
        {
            var game = await m_gameService.Get(gameId);

            if (game == null)
                return NoContent();
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await m_gameService.Insert(gameInputModel);

                return Ok(game);
            }
            //catch (GameRegisterException ex)
            catch(Exception ex)
            {
                return UnprocessableEntity("A game with this Name and Developer has already been registered");
            }
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await m_gameService.Update(gameId, gameInputModel);

                return Ok();
            }
            catch(Exception ex)
            //catch(GameNotRegisteredException ex)
            {
                return NotFound("This Game does not exists");
            }
        }
        
        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await m_gameService.Update(gameId, price);

                return Ok();
            }
            //catch(GameNotRegisteredException ex)
            catch(Exception ex)
            {
                return NotFound("This Game does not exists");
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            try
            {
                await m_gameService.Remove(gameId);

                return Ok();
            }
            //catch(GameNotRegisteredException ex)
            catch(Exception ex)
            {
                return NotFound("This Game does not exists");
            }
        }
    }
}