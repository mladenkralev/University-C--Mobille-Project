using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Siders.model
{
    class WallModel
    {
        private static int _classIdCounter = 0;
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private Image _image;

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
            }
        }


        public WallModel(int width, int height, bool isHorizontal)
        {
            Image image = new Image();
            if (isHorizontal)
            {
                image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new System.Uri("ms-appx:/src/wall_H.png"));
            }
            else
            {
                image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new System.Uri("ms-appx:/src/wall_V.png"));
            }
            image.Name = _classIdCounter.ToString();
            image.Width = width;
            image.Height = height;
            Image = image;
            WallModel._classIdCounter++;
        }

        public void destroyReference()
        {
            Image = null;
            _image = null;
            _classIdCounter--;
        }
    }
}
