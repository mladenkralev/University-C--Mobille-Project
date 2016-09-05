using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Siders.model
{
    class SwitcherModel
    {
        private Image _image;

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
            }
        }

        public SwitcherModel(int width, int height, bool isHorizontal)
        {
            Image image = new Image();
            if (isHorizontal)
            {
                image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new System.Uri("ms-appx:/src/swap_H.png"));
            }
            else
            {
                image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new System.Uri("ms-appx:/src/swap_V.png"));
            }
            image.Width = width;
            image.Height = height;
            Image = image;

        }
    }
}
