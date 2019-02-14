using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpfsudokulib.Commands
{
    /// <summary>
    /// Used to call viewModel methods directly
    /// </summary>
    public class GameCommand : ICommand
    {
        #region PrivateProperties

        /// <summary>
        /// The action (method) to be executed by the command
        /// </summary>
        private Action Action;

        #endregion

        #region Constructors

        /// <summary>
        /// Sets the private action variable so it can be used later
        /// </summary>
        /// <param name="action"></param>
        public GameCommand(Action action)
        {
            Action = action;
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Never used because CanExecute never changes
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Always true as we can always execute commands
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the action wby calling its Invoke method
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Action.Invoke();
        }

        #endregion
    }
}
