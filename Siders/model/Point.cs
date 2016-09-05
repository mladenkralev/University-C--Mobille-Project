using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Siders.model
{
    class Point
    {
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
            }
        }

        public Point()
        {
            ImagePath = @"/src/point.png";
        }
    }
}
