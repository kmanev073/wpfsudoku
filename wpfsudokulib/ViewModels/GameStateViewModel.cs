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
    [AddINotifyPropertyChangedInterface]
    public class GameStateViewModel
    {
        private GameStateRepository _gameStateRepository;
        
        private DispatcherTimer Timer { get; set; }

        public Guid GameId { get; set; }
        
        public GameDifficulties Difficulty { get; set; }

        public int ElapsedSeconds { get; set; }

        public string ElapsedTime { get; set; }
        
        public ObservableCollection<GameState> LoadedGames { get; set; }

        public Guid? SelectedGameId { get; set; }

        public List<List<SudokuRow>> Undo { get; set; }

        public List<List<SudokuRow>> Redo { get; set; }

        public GameStatuses Status { get; set; }

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

        public void StartTimer()
        {
            Timer.Start();
        }

        public void StopTimer()
        {
            Timer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ElapsedSeconds++;
            SetElapsedTime(ElapsedSeconds);
        }

        private void SetElapsedTime(int elapsedSeconds)
        {
            var seconds = ElapsedSeconds % 60;
            var minutes = ElapsedSeconds / 60;
            var hours = ElapsedSeconds / 3600;
            ElapsedTime = $"{(hours > 9 ? "" : "0")}{hours}:{(minutes > 9 ? "" : "0")}{minutes}:{(seconds > 9 ? "" : "0")}{seconds}";
        }
    }
}
