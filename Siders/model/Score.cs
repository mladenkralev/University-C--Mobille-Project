using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siders.model
{
    class Score : INotifyPropertyChanged
    {
        private long _score;

        public long ScoreNumber
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("ScoreNumber");
            }
        }

        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }


        public Score(int score, string name)
        {
            this._score = score;
            this._user = name;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
