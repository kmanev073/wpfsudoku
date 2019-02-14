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

        private CancellationTokenSource TimerTokenSource { get; set; }

        public Guid GameId { get; set; }
        
        public GameDifficulties Difficulty { get; set; }

        public int ElapsedSeconds { get; set; }

        public string ElapsedTime { get; set; }
        
        public ObservableCollection<GameState> LoadedGames { get; set; }

        public Guid? SelectedGameId { get; set; }

        public Stack<Move> Undo { get; set; }

        public Stack<Move> Redo { get; set; }

        public GameStatuses Status { get; set; }

        public GameStateViewModel(GameStateRepository gameStateRepository, GameState gameState)
        {
            _gameStateRepository = gameStateRepository;

            GameId = gameState.Id;

            Difficulty = gameState.Difficulty;

            ElapsedSeconds = gameState.ElapsedSeconds;

            SetElapsedTime(ElapsedSeconds);

            TimerTokenSource = new CancellationTokenSource();

            LoadedGames = new ObservableCollection<GameState>(gameStateRepository.ListGames());
            
            Undo = gameState.Undo;

            Undo = gameState.Redo;

            Status = gameState.Status;
        }

        public void StartTimer()
        {
            Task.Run(TimerTick, TimerTokenSource.Token);
        }

        public void StopTimer()
        {
            TimerTokenSource.Cancel();
        }

        private async Task TimerTick()
        {
            while (true)
            {
                await Task.Delay(1000);
                ElapsedSeconds++;
                SetElapsedTime(ElapsedSeconds);
            }   
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
