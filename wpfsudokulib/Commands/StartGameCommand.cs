using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpfsudokulib.Commands
{
    public class GameCommand : ICommand
    {
        private Action Action;
        
        public GameCommand(Action action)
        {
            Action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action.Invoke();
        }
    }
}
