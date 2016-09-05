using Siders.viewmodel.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Siders.viewmodel.commands
{
    class GoBackCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public ViewModelList Viewmodel { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        
        public GoBackCommand(ViewModelList viewModel)
        {
            Viewmodel = viewModel;
        }

        public async void Execute(object parameter)
        {
            Viewmodel.redirectToList();
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
