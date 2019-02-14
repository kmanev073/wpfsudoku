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
    /// <summary>
    /// Unites SudokuBoardViewModel and GameStateViewModel
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        #region PrivateProperties

        /// <summary>
        /// The repository used to store the game states in LiteDB
        /// </summary>
        private readonly GameStateRepository _gameStateRepository;

        #endregion

        #region PublicProperties

        /// <summary>
        /// The sudoku serives used for generating and checking sudoku boards
        /// </summary>
        public readonly SudokuService SudokuService;

        /// <summary>
        /// The ViewModel for the sudoku board
        /// </summary>
        public SudokuBoardViewModel SudokuBoardViewModel { get; set; }

        /// <summary>
        /// The ViewModel for the game state
        /// </summary>
        public GameStateViewModel GameStateViewModel { get; set; }

        /// <summary>
        /// Command triggered when the user starts a new game
        /// </summary>
        public GameCommand StartGameCommand { get; private set; }

        /// <summary>
        /// Command triggered when the user saves a game
        /// </summary>
        public GameCommand SaveGameCommand { get; private set; }

        /// <summary>
        /// Command triggered when the user loads a game
        /// </summary>
        public GameCommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Command tirggered when the user clicks the Undo button
        /// </summary>
        public GameCommand UndoCommand { get; private set; }

        /// <summary>
        /// Command triggered when the user clicks the Redo button
        /// </summary>
        public GameCommand RedoCommand { get; private set; }

        /// <summary>
        /// Command triggered when the user edits a sudoku cell
        /// </summary>
        public GameCommand EditCellCommand { get; private set; }

        #endregion

        #region Construcotors

        /// <summary>
        /// Used to initialize the readonly properties, view models and the commands
        /// </summary>
        /// <param name="gameStateRepository"></param>
        /// <param name="sudokuService"></param>
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

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Executed by the StartGameCommand
        /// </summary>
        private void StartGame()
        {
            //Stop the timer
            GameStateViewModel.StopTimer();

            //Create a new game state
            var newGame = new GameState(SudokuService, GameStateViewModel.Difficulty);

            //Create new view models and start the new game
            GameStateViewModel = new GameStateViewModel(_gameStateRepository, newGame);
            SudokuBoardViewModel = new SudokuBoardViewModel(newGame);

            //Start the timer
            GameStateViewModel.StartTimer();
        }
        
        /// <summary>
        /// Exectuted by the SaveGameCommand
        /// </summary>
        private void SaveGame()
        {
            //If the game is not started nothing should be saved in LiteDB
            if (GameStateViewModel.Status == GameStatuses.NotStarted)
            {
                return;
            }
            
            //Save the current game state
            _gameStateRepository.SaveGame(new GameState(GameStateViewModel, SudokuBoardViewModel));

            //Update the ComboBox
            GameStateViewModel.LoadedGames = new ObservableCollection<GameState>(_gameStateRepository.ListGames());
            GameStateViewModel.SelectedGameId = null;
        }
        
        /// <summary>
        /// Executed by the LoadGameCommand
        /// </summary>
        private void LoadGame()
        {
            //If there is no selected game do nothing
            if (GameStateViewModel.SelectedGameId == null)
            {
                return;
            }

            //Stop the timer
            GameStateViewModel.StopTimer();

            //Load the game from LiteDB
            var loadedGame = _gameStateRepository.LoadGame(GameStateViewModel.SelectedGameId.Value);

            //Create new view models
            GameStateViewModel = new GameStateViewModel(_gameStateRepository, loadedGame);
            SudokuBoardViewModel = new SudokuBoardViewModel(loadedGame);

            //Start the timer only if the game is not finished
            if (GameStateViewModel.Status == GameStatuses.Playing)
            {
                GameStateViewModel.StartTimer();
            }

            //Update the ComboBox selected value
            GameStateViewModel.SelectedGameId = null;
        }

        /// <summary>
        /// Executed by the UndoCommand
        /// </summary>
        private void Undo()
        {
            //Only undo if the game is running and if there are moves to be undone
            if (GameStateViewModel.Status != GameStatuses.Playing || GameStateViewModel.Undo.Count == 0)
            {
                return;
            }

            //Make a deep copy of the board
            var rows = new List<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(new SudokuRow(SudokuBoardViewModel.Rows[i]));
            }
            
            //Add the current board to the redo list in case the user wants to redo a move
            GameStateViewModel.Redo.Add(rows);

            //Get the old board state from the undo list
            var oldBoard = GameStateViewModel.Undo[GameStateViewModel.Undo.Count - 1];
            GameStateViewModel.Undo.RemoveAt(GameStateViewModel.Undo.Count - 1);

            //Apply the old state to the sudoku view model
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoardViewModel.Rows[i][j].Data = oldBoard[i][j].Data;
                }
            }
        }

        /// <summary>
        /// Executed by the RedoCommand
        /// </summary>
        private void Redo()
        {
            //Only redo if the game is running and if there are moves to be undone
            if (GameStateViewModel.Status != GameStatuses.Playing || GameStateViewModel.Redo.Count == 0)
            {
                return;
            }

            //Make a deep copy of the board
            var rows = new List<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(new SudokuRow(SudokuBoardViewModel.Rows[i]));
            }

            //Add the current board to the undo list in case the user wants to undo a move
            GameStateViewModel.Undo.Add(rows);

            //Get the new board state from the redo list
            var newBoard = GameStateViewModel.Redo[GameStateViewModel.Redo.Count - 1];
            GameStateViewModel.Redo.RemoveAt(GameStateViewModel.Redo.Count - 1);

            //Apply the new state to the sudoku view model
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoardViewModel.Rows[i][j].Data = newBoard[i][j].Data;
                }
            }
        }

        /// <summary>
        /// Called by the EditCellCommand
        /// </summary>
        private void EditCell()
        {
            //Initialize an empty byte array of size 9x9=81 (sudoku's size)
            var board = new byte?[81];

            //Fill the byte array with data from the sudoku view model
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i * 9 + j] = SudokuBoardViewModel.Rows[i][j].Data;
                }
            }
            
            //Check if the sudoku is solved
            if (SudokuService.CheckBoard(board))
            {
                //If yes set the game status to finished and stop the timer
                GameStateViewModel.Status = GameStatuses.Finished;
                GameStateViewModel.StopTimer();
                return;
            }
        }

        #endregion
    }
}
