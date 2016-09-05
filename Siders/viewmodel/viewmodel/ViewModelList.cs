using Siders.view;
using Siders.viewmodel.commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Siders.viewmodel.viewmodel
{
    class ViewModelList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private String _scoreString;
        private GoBackCommand _goBackCommand;
        public String ScoreString
        {
            get { return _scoreString; }
            set
            {
                _scoreString = value;
                OnPropertyChanged("ScoreString");
            }
        }


        public ViewModelList()
        {
            GoBackCommand = new GoBackCommand(this);
            readScores();
        }

        public GoBackCommand GoBackCommand
        {
            get { return _goBackCommand; }
            set
            {
                _goBackCommand = value;
                OnPropertyChanged("GoBackCommand");
            }
        }

        public async void readScores()
        {
            StorageFolder storageFolder =
               Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile1 =
                await storageFolder.GetFileAsync("sample.txt");
            ScoreString = await Windows.Storage.FileIO.ReadTextAsync(sampleFile1);
        }

        public void redirectToList()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login));
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
