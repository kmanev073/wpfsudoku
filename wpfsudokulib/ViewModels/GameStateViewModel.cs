using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Models;
using wpfsudokulib.Repositories;
using System.Windows.Threading;
using wpfsudokulib.Enums;
using PropertyChanged;
using System.Threading;
using System.Collections.ObjectModel;

namespace wpfsudokulib.ViewModels
{
    /// <summary>
    /// ViewModel used to controll the game
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class GameStateViewModel
    {
        /// <summary>
        /// The repository used to store the game in LiteDB
        /// </summary>
        private GameStateRepository _gameStateRepository;
        
        /// <summary>
        /// Timer used for counting the elapsed time
        /// </summary>
        private DispatcherTimer Timer { get; set; }

        /// <summary>
        /// The id of the game used as id in LiteDB too
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// The game difficulty
        /// </summary>
        public GameDifficulties Difficulty { get; set; }

        /// <summary>
        /// Seconds elapsed since the beggining of the game
        /// </summary>
        public int ElapsedSeconds { get; set; }

        /// <summary>
        /// Well formated (HH:MM:SS) string showing the elapsed time
        /// </summary>
        public string ElapsedTime { get; set; }
        
        /// <summary>
        /// Used to list the saved games in the ComboBox
        /// </summary>
        public ObservableCollection<GameState> LoadedGames { get; set; }

        /// <summary>
        /// Hold the selected game id from the ComboBox
        /// </summary>
        public Guid? SelectedGameId { get; set; }

        /// <summary>
        /// Holds previous game states that can be undone
        /// </summary>
        public List<List<SudokuRow>> Undo { get; set; }

        /// <summary>
        /// Holds future game states that can be redone
        /// </summary>
        public List<List<SudokuRow>> Redo { get; set; }

        /// <summary>
        /// The status of the game
        /// </summary>
        public GameStatuses Status { get; set; }

        /// <summary>
        /// Constructor used for starting new games or loading old ones
        /// </summary>
        /// <param name="gameStateRepository"></param>
        /// <param name="gameState"></param>
        public GameStateViewModel(GameStateRepository gameStateRepository, GameState gameState)
        {
            _gameStateRepository = gameStateRepository;

            GameId = gameState.Id;

            Difficulty = gameState.Difficulty;

            ElapsedSeconds = gameState.ElapsedSeconds;

            SetElapsedTime(ElapsedSeconds);
            
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(TimerTick);
            Timer.Interval = new TimeSpan(0, 0, 1);

            LoadedGames = new ObservableCollection<GameState>(gameStateRepository.ListGames());
            
            Undo = gameState.Undo;

            Redo = gameState.Redo;

            Status = gameState.Status;
        }

        /// <summary>
        /// Used to start the time counter
        /// </summary>
        public void StartTimer()
        {
            Timer.Start();
        }

        /// <summary>
        /// Used to stop the time counter
        /// </summary>
        public void StopTimer()
        {
            Timer.Stop();
        }

        /// <summary>
        /// Called each second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            ElapsedSeconds++;
            SetElapsedTime(ElapsedSeconds);
        }

        /// <summary>
        /// Translates ElapsedSeconds to ElapsedTime
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        private void SetElapsedTime(int elapsedSeconds)
        {
            var seconds = ElapsedSeconds % 60;
            var minutes = ElapsedSeconds / 60;
            var hours = ElapsedSeconds / 3600;
            ElapsedTime = $"{(hours > 9 ? "" : "0")}{hours}:{(minutes > 9 ? "" : "0")}{minutes}:{(seconds > 9 ? "" : "0")}{seconds}";
        }
    }
}
