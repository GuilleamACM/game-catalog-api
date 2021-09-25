using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("8fcce2a0-5bcd-4be3-9c92-06084749d26a"), new Game{Id = Guid.Parse("8fcce2a0-5bcd-4be3-9c92-06084749d26a"), Title = "ARK: Survival Evolved", Developer = "Studio Wildcard", Price = 57.99}},
            {Guid.Parse("e232e085-2e47-409e-842f-f00f58360814"), new Game{Id = Guid.Parse("e232e085-2e47-409e-842f-f00f58360814"), Title = "Cyberpunk 2077", Developer = "CD PROJECT RED", Price = 199.90}},
            {Guid.Parse("6ae00514-db3b-4743-b4de-6de28d7a3540"), new Game{Id = Guid.Parse("6ae00514-db3b-4743-b4de-6de28d7a3540"), Title = "Divinity: Original Sin - Enhanced Edition", Developer = "Larian Studios", Price = 72.99}},
            {Guid.Parse("1c11412d-201d-4bde-8ecd-a0a6d2ae90ff"), new Game{Id = Guid.Parse("1c11412d-201d-4bde-8ecd-a0a6d2ae90ff"), Title = "Hollow Knight", Developer = "Team Cherry", Price = 27.99}},
            {Guid.Parse("f9178d46-7e67-489d-ac79-c13082878800"), new Game{Id = Guid.Parse("f9178d46-7e67-489d-ac79-c13082878800"), Title = "The Witcher 3: Wild Hunt", Developer = "CD PROJECT RED", Price = 79.99}}
        };
        
        public Task<List<Game>> Get(int page, int quantity)
        {
            return Task.FromResult(_games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            if (!_games.ContainsKey(id))
                return null;

            return Task.FromResult(_games[id]);
        }

        public Task<List<Game>> Get(string name, string developer)
        {
            return Task.FromResult(_games.Values.Where(game => game.Title.Equals(name) && game.Developer.Equals(developer)).ToList());
        }

        public Task Insert(Game game)
        {
            _games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Guid id)
        {
            _games.Remove(id);
            return Task.CompletedTask;
        }
        
        public void Dispose()
        {
            //Closing connection with database
        }
    }
}