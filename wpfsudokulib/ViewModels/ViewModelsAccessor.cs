using PropertyChanged;
using System.Collections.ObjectModel;
using wpfsudokulib.Commands;
using wpfsudokulib.Enums;
using wpfsudokulib.Models;
using wpfsudokulib.Repositories;
using wpfsudokulib.ViewModels;

namespace wpfsudokulib
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelsAccessor
    {
        private readonly GameStateRepository _gameStateRepository;

        public SudokuBoardViewModel SudokuBoardViewModel { get; set; }

        public GameStateViewModel GameStateViewModel { get; set; }

        public GameCommand StartGameCommand { get; private set; }

        public GameCommand SaveGameCommand { get; private set; }

        public GameCommand LoadGameCommand { get; private set; }

        public GameCommand UndoCommand { get; private set; }

        public GameCommand RedoCommand { get; private set; }

        public ViewModelsAccessor(GameStateRepository gameStateRepository)
        {
            _gameStateRepository = gameStateRepository;

            SudokuBoardViewModel = new SudokuBoardViewModel();
            GameStateViewModel = new GameStateViewModel(gameStateRepository, new GameState());

            StartGameCommand = new GameCommand(StartGame);
            SaveGameCommand = new GameCommand(SaveGame);
            LoadGameCommand = new GameCommand(LoadGame);
            UndoCommand = new GameCommand(Undo);
            RedoCommand = new GameCommand(Redo);
        }

        private void StartGame()
        {
            GameStateViewModel.StopTimer();
            var newGame = new GameState(GameStateViewModel.Difficulty);
            GameStateViewModel = new GameStateViewModel(_gameStateRepository, newGame);
            SudokuBoardViewModel = new SudokuBoardViewModel(newGame);
            GameStateViewModel.StartTimer();
        }

        private void SaveGame()
        {
            if (GameStateViewModel.Status == GameStatuses.Playing)
            {
                _gameStateRepository.SaveGame(new GameState(GameStateViewModel, SudokuBoardViewModel));
                GameStateViewModel.LoadedGames = new ObservableCollection<GameState>(_gameStateRepository.ListGames());
                GameStateViewModel.SelectedGameId = null;
            }
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
            GameStateViewModel.StartTimer();

            GameStateViewModel.SelectedGameId = null;
        }

        private void Undo()
        {

        }

        private void Redo()
        {

        }
    }
}
