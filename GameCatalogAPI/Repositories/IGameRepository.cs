using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repositories
{
    public interface IGameRepository
    {
        Task<List<Game>> Get(int page, int quantity);

        Task<Game> Get(Guid id);

        Task<Game> Get(string name, string developer);
        
        Task<Game> Insert(Game game);
        
        Task Update(Game game);

        Task Remove(Guid id);
    }
}