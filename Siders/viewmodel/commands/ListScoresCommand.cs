using Siders.viewmodel.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Siders.viewmodel.commands
{
    class ListScoresCommand : ICommand
    {
        private ViewModelLogin _viewModel;
        public event EventHandler CanExecuteChanged;

        public ListScoresCommand(ViewModelLogin viewmodel)
        {
            _viewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.redirectToList();
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
