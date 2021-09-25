using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Exceptions;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Repositories;
using GameCatalogAPI.ViewModel;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository m_gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            m_gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var games = await m_gameRepository.Get(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await m_gameRepository.Get(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameEntity = await m_gameRepository.Get(game.Title, game.Developer);

            if (gameEntity.Count > 0)
                throw new GameRegisteredException();

            var newGame = new Game()
            {
                Id = Guid.NewGuid(),
                Title = game.Title,
                Developer = game.Developer,
                Price = game.Price
            };

            await m_gameRepository.Insert(newGame);

            return new GameViewModel
            {
                Id = newGame.Id,
                Title = newGame.Title,
                Developer = newGame.Title,
                Price = newGame.Price,
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameEntity = await m_gameRepository.Get(id);
            
            if(gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Title = game.Title;
            gameEntity.Developer = game.Developer;
            gameEntity.Price = game.Price;

            await m_gameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double price)
        {
            var gameEntity = await m_gameRepository.Get(id);
            
            if(gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Price = price;

            await m_gameRepository.Update(gameEntity);
        }

        public async Task Remove(Guid id)
        {
            var gameEntity = await m_gameRepository.Get(id);
            
            if(gameEntity == null)
                throw new GameNotRegisteredException();

            await m_gameRepository.Remove(id);
        }

        public void Dispose()
        {
            m_gameRepository?.Dispose();
        }
    }
}