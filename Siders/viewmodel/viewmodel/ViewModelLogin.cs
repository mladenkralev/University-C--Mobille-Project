using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siders.viewmodel.commands;
using Siders.view;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Siders.viewmodel.viewmodel
{
    class ViewModelLogin : INotifyPropertyChanged
    {
        
        #region uselessSpam
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion

        public ViewModelLogin()
        {
            PlayCommand = new PlayCommand(this);
            ListScores = new ListScoresCommand(this);
        }

        private PlayCommand _playCommand;

        public PlayCommand PlayCommand
        {
            get { return _playCommand; }
            set
            {
                _playCommand = value;
                OnPropertyChanged("PlayCommand");
            }
        }

        private ListScoresCommand _listScores;

        public ListScoresCommand ListScores
        {
            get { return _listScores; }
            set
            {
                _listScores = value;
                OnPropertyChanged("ListScoresCommand");
            }
        }

        public void redirectToGame()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof (Game));
        }

        public void redirectToList()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ListPage));
        }

    }
}
