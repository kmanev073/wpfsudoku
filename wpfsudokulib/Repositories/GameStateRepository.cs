using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Models;

namespace wpfsudokulib.Repositories
{
    public class GameStateRepository
    {
        private readonly LiteDatabase _database;

        private LiteCollection<GameState> Collection
            => _database.GetCollection<GameState>("Games");

        public GameStateRepository(string databaseName)
        {
            _database = new LiteDatabase(databaseName);
        }

        public bool SaveGame(GameState gameState)
            => Collection.Upsert(gameState);

        public GameState LoadGame(Guid gameId)
            => Collection.FindById(gameId);

        public IEnumerable<GameState> ListGames()
            => Collection.FindAll().OrderBy(savedGame => savedGame.SaveTimestamp).Reverse();
    }
}
