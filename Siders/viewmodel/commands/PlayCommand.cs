using Siders.viewmodel.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Siders.viewmodel.commands
{
    class PlayCommand : ICommand
    {
        private ViewModelLogin _viewModel;
        public event EventHandler CanExecuteChanged;

        public PlayCommand(ViewModelLogin viewmodel)
        {
            _viewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.redirectToGame();
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
