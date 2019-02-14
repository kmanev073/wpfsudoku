using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Models;

namespace wpfsudokulib.Repositories
{
    /// <summary>
    /// A repository wrapper around LiteDB
    /// </summary>
    public class GameStateRepository
    {
        #region PrivateProperties

        /// <summary>
        /// Database instance
        /// </summary>
        private readonly LiteDatabase _database;

        /// <summary>
        /// Instance of the "Games" collection
        /// </summary>
        private LiteCollection<GameState> Collection
            => _database.GetCollection<GameState>("Games");

        #endregion

        #region Construcotrs

        /// <summary>
        /// Constructor used to setup the database
        /// </summary>
        /// <param name="databaseName">The name of the database</param>
        public GameStateRepository(string databaseName)
        {
            _database = new LiteDatabase(databaseName);
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Saves a GameState to LiteDB
        /// </summary>
        /// <param name="gameState">The GameState to be saved</param>
        /// <returns></returns>
        public bool SaveGame(GameState gameState)
            => Collection.Upsert(gameState);

        /// <summary>
        /// Finds and returns GameState by id
        /// </summary>
        /// <param name="gameId">The id of the desired GameState</param>
        /// <returns></returns>
        public GameState LoadGame(Guid gameId)
            => Collection.FindById(gameId);

        /// <summary>
        /// Lists all saved GameStates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GameState> ListGames()
            => Collection.FindAll().OrderBy(savedGame => savedGame.SaveTimestamp).Reverse();

        #endregion
    }
}
