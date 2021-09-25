using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;
using Microsoft.Extensions.Configuration;

namespace GameCatalogAPI.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection m_sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            m_sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        
        public async Task<List<Game>> Get(int page, int quantity)
        {
            var games = new List<Game>();

            var command =
                $"select * from Games order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await m_sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, m_sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Title"],
                    Developer = (string)sqlDataReader["Developer"],
                    Price = (double)sqlDataReader["Price"],
                });
            }

            await m_sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> Get(Guid id)
        {
            Game game = null;

            var command = $"select * from Games where Id = '{id}'";

            await m_sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, m_sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            
            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Title"],
                    Developer = (string)sqlDataReader["Developer"],
                    Price = (double)sqlDataReader["Price"],
                };
            }

            await m_sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Get(string name, string developer)
        {
            var games = new List<Game>();

            var command = $"select * from Games where Title = '{name}' and Developer = '{developer}'";

            await m_sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, m_sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Title"],
                    Developer = (string)sqlDataReader["Developer"],
                    Price = (double)sqlDataReader["Price"],
                });
            }

            await m_sqlConnection.CloseAsync();

            return games;
        }

        public async Task Insert(Game game)
        {
            var command = $"insert Games (Id, Title, Developer, Price) values ('{game.Id}', '{game.Title}', '{game.Developer}', '{game.Price.ToString().Replace(",", ".")}')";
            
            await m_sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, m_sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await m_sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var command = $"update Games set Title = '{game.Title}', Developer = '{game.Developer}', Price = '{game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'')";
            
            await m_sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, m_sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await m_sqlConnection.CloseAsync();
        }

        public async Task Remove(Guid id)
        {
            var command = $"delete from Games where Id = '{id}'";
            
            await m_sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, m_sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await m_sqlConnection.CloseAsync();
        }
        
        public void Dispose()
        {
            m_sqlConnection?.Close();
            m_sqlConnection?.Dispose();
        }
    }
}