using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using wpfsudokulib.Commands;
using wpfsudokulib.Enums;
using wpfsudokulib.Models;
using wpfsudokulib.Repositories;
using wpfsudokulib.Services;
using wpfsudokulib.ViewModels;

namespace wpfsudokulib
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private readonly GameStateRepository _gameStateRepository;

        public readonly SudokuService SudokuService;

        public SudokuBoardViewModel SudokuBoardViewModel { get; set; }

        public GameStateViewModel GameStateViewModel { get; set; }

        public GameCommand StartGameCommand { get; private set; }

        public GameCommand SaveGameCommand { get; private set; }

        public GameCommand LoadGameCommand { get; private set; }

        public GameCommand UndoCommand { get; private set; }

        public GameCommand RedoCommand { get; private set; }

        public GameCommand EditCellCommand { get; private set; }

        public MainViewModel(GameStateRepository gameStateRepository, SudokuService sudokuService)
        {
            _gameStateRepository = gameStateRepository;
            SudokuService = sudokuService;

            SudokuBoardViewModel = new SudokuBoardViewModel();
            GameStateViewModel = new GameStateViewModel(gameStateRepository, new GameState());

            StartGameCommand = new GameCommand(StartGame);
            SaveGameCommand = new GameCommand(SaveGame);
            LoadGameCommand = new GameCommand(LoadGame);
            UndoCommand = new GameCommand(Undo);
            RedoCommand = new GameCommand(Redo);
            EditCellCommand = new GameCommand(EditCell);
        }

        private void StartGame()
        {
            GameStateViewModel.StopTimer();
            var newGame = new GameState(SudokuService, GameStateViewModel.Difficulty);
            GameStateViewModel = new GameStateViewModel(_gameStateRepository, newGame);
            SudokuBoardViewModel = new SudokuBoardViewModel(newGame);
            GameStateViewModel.StartTimer();
        }
        
        private void SaveGame()
        {
            if (GameStateViewModel.Status == GameStatuses.NotStarted)
            {
                return;
            }
            
            _gameStateRepository.SaveGame(new GameState(GameStateViewModel, SudokuBoardViewModel));
            GameStateViewModel.LoadedGames = new ObservableCollection<GameState>(_gameStateRepository.ListGames());
            GameStateViewModel.SelectedGameId = null;
        }

        private void LoadGame()
        {
            if (GameStateViewModel.SelectedGameId == null)
            {
                return;
            }

            GameStateViewModel.StopTimer();
            var loadedGame = _gameStateRepository.LoadGame(GameStateViewModel.SelectedGameId.Value);
            GameStateViewModel = new GameStateViewModel(_gameStateRepository, loadedGame);
            SudokuBoardViewModel = new SudokuBoardViewModel(loadedGame);
            if (GameStateViewModel.Status == GameStatuses.Playing)
            {
                GameStateViewModel.StartTimer();
            }

            GameStateViewModel.SelectedGameId = null;
        }

        private void Undo()
        {
            if (GameStateViewModel.Status != GameStatuses.Playing || GameStateViewModel.Undo.Count == 0)
            {
                return;
            }

            var rows = new List<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(new SudokuRow(SudokuBoardViewModel.Rows[i]));
            }
            GameStateViewModel.Redo.Add(rows);
            var oldBoard = GameStateViewModel.Undo[GameStateViewModel.Undo.Count - 1];
            GameStateViewModel.Undo.RemoveAt(GameStateViewModel.Undo.Count - 1);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoardViewModel.Rows[i][j].Data = oldBoard[i][j].Data;
                }
            }
        }

        private void Redo()
        {
            if (GameStateViewModel.Status != GameStatuses.Playing || GameStateViewModel.Redo.Count == 0)
            {
                return;
            }

            var rows = new List<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(new SudokuRow(SudokuBoardViewModel.Rows[i]));
            }
            GameStateViewModel.Undo.Add(rows);
            var newBoard = GameStateViewModel.Redo[GameStateViewModel.Redo.Count - 1];
            GameStateViewModel.Redo.RemoveAt(GameStateViewModel.Redo.Count - 1);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoardViewModel.Rows[i][j].Data = newBoard[i][j].Data;
                }
            }
        }
        
        private void EditCell()
        {
            var board = new byte?[81];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i * 9 + j] = SudokuBoardViewModel.Rows[i][j].Data;
                }
            }
            
            if (SudokuService.CheckBoard(board))
            {
                GameStateViewModel.Status = GameStatuses.Finished;
                GameStateViewModel.StopTimer();
                return;
            }
        }
    }
}
