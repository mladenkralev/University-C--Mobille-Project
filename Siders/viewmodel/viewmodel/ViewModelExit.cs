using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siders.viewmodel.commands;
using System.ComponentModel;
using Siders.model;

namespace Siders.viewmodel.viewmodel
{
    class ViewModelExit : INotifyPropertyChanged
    {

        private ExitAppCommand _exitAppCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelExit()
        {
            ExitAppCommand = new ExitAppCommand(this);
        }

        public ExitAppCommand ExitAppCommand
        {
            get { return _exitAppCommand; }
            set
            {
                _exitAppCommand = value;
                OnPropertyChanged("ExitAppCommand");
            }
        }

        
        private void OnPropertyChanged(String propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

