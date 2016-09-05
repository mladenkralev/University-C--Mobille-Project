using Siders.viewmodel.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace Siders.viewmodel.commands
{
    class ExitAppCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ViewModelExit Viewmodel { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        
        public ExitAppCommand(ViewModelExit viewModel)
        {
            Viewmodel = viewModel;
        }

        public async void Execute(object parameter)
        {

            App.Current.Exit();
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
