using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GameCatalogAPI.Exceptions;
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
        
        /// <summary>
        /// Search for every game in a pagination form
        /// </summary>
        /// <remarks>
        /// It's not possible to return games without pagination
        /// </remarks>
        /// <param name="page">Indicates which page is being consulted. Minimum one</param>
        /// <param name="quantity">Indicates the number of records per page. Minimum 1 and maximum 50</param>
        /// <response code="200">Returns the games list</response>
        /// /// <response code="204">If there are no games</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await m_gameService.Get(page, quantity);
            
            if (games.Count == 0)
                return NoContent();
            
            return Ok(games);
        }
        
        /// <summary>
        /// Search for a game by its Id
        /// </summary>
        /// <param name="gameId">Fetched game Id</param>
        /// <response code="200">Return the selected game</response>
        /// /// <response code="204">If there are no game with this id</response>
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameId)
        {
            var game = await m_gameService.Get(gameId);

            if (game == null)
                return NoContent();
            
            return Ok(game);
        }
        
        /// <summary>
        /// Insert a new game into the Database
        /// </summary>
        /// <param name="gameInputModel">The game data to be inserted</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await m_gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch(GameRegisteredException ex)
            {
                return UnprocessableEntity("A game with this Name and Developer has already been registered");
            }
        }
        
        /// <summary>
        /// Select one game by Id and then update its data
        /// </summary>
        /// <param name="gameId">Id of the selected game to be updated</param>
        /// <param name="gameInputModel">The new data of the selected game</param>
        /// <returns></returns>
        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await m_gameService.Update(gameId, gameInputModel);

                return Ok();
            }
            catch(GameNotRegisteredException ex)
            {
                return NotFound("This Game does not exists");
            }
        }
        
        /// <summary>
        /// Select one game by Id and then update its price
        /// </summary>
        /// <param name="gameId">The Id of the selected game to be updated</param>
        /// <param name="price">The new price of the selected game</param>
        /// <returns></returns>
        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await m_gameService.Update(gameId, price);

                return Ok();
            }
            catch(GameNotRegisteredException ex)
            {
                return NotFound("This Game does not exists");
            }
        }
        
        /// <summary>
        /// Delete a game by its Id
        /// </summary>
        /// <param name="gameId">The Id of the game to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            try
            {
                await m_gameService.Remove(gameId);

                return Ok();
            }
            catch(GameNotRegisteredException ex)
            {
                return NotFound("This Game does not exists");
            }
        }
    }
}