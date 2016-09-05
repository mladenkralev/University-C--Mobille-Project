using Siders.viewmodel.viewmodel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Siders.viewmodel.commands
{
    class LeftButtonCommand : ICommand
    {

        public object CommandManager { get; private set; }
        public event EventHandler CanExecuteChanged;
        public ViewModel ViewModel
        {
            get; set;
        }

        public LeftButtonCommand(ViewModel viewmodel)
        {
            ViewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var image = parameter as Image;
            double prevPositionLeftOfPoint = Canvas.GetLeft(image);
            double prevPositionTopOfPoint = Canvas.GetTop(image);
            double prevPositionTopImage;
            double prevPositionLeftImage;

            if (image != null)
            {
                if (ViewModel.FromTopToButtom)
                {
                    if (prevPositionLeftOfPoint > 0)
                    {
                        foreach (UIElement child in ViewModel.CanvasTopButtom.Children)
                        {
                            prevPositionTopImage = Canvas.GetTop(child);
                            prevPositionLeftImage = Canvas.GetLeft(child);

                            var imageUI = child as Image; // get image
                            string str = ((BitmapImage)imageUI.Source).UriSource.ToString();
                            Rect myRectangle = new Rect(prevPositionLeftImage, prevPositionTopImage, image.Width, image.Height);
                            myRectangle.Intersect(ViewModel.RectanglePoint);

                            if (((str.Equals("ms-appx:/src/swap_H.png")) || str.Equals("ms-appx:/src/swap_V.png"))
                                 && ((!myRectangle.IsEmpty)))
                            {
                                ViewModel.CanvasTopButtom.Children.Remove(child);
                            }
                            else {
                                if (!myRectangle.IsEmpty)
                                {
                                    ViewModel.showEndMessege();
                                }
                            }
                        }
                        ViewModel.RectanglePoint = new Rect(prevPositionLeftOfPoint - ViewModel.STEPLEFTRIGHTPOINT, prevPositionTopOfPoint, ViewModel.WidthOfPoint, ViewModel.HeightOfPoint);
                        Canvas.SetLeft(image, prevPositionLeftOfPoint - ViewModel.STEPLEFTRIGHTPOINT);

                    }
                }
                else
                {
                    if ((prevPositionTopOfPoint > 0))
                    {
                        foreach (UIElement child in ViewModel.CanvasTopButtom.Children)
                        {
                            prevPositionTopImage = Canvas.GetTop(child);
                            prevPositionLeftImage = Canvas.GetLeft(child);


                            var imageUI = child as Image; // get image
                            string str = ((BitmapImage)imageUI.Source).UriSource.ToString();
                            Rect myRectangle = new Rect(prevPositionLeftImage, prevPositionTopImage, image.Width, image.Height);
                            myRectangle.Intersect(ViewModel.RectanglePoint);

                            if (((str.Equals("ms-appx:/src/swap_H.png")) || str.Equals("ms-appx:/src/swap_V.png"))
                                 && ((!myRectangle.IsEmpty)))
                            {
                                ViewModel.CanvasTopButtom.Children.Remove(child);
                            }
                            else {
                                if (!myRectangle.IsEmpty)
                                {
                                    ViewModel.showEndMessege();
                                }
                            }

                        }
                        ViewModel.RectanglePoint = new Rect(prevPositionLeftOfPoint + ViewModel.STEPLEFTRIGHTPOINT, prevPositionTopOfPoint, ViewModel.WidthOfPoint, ViewModel.HeightOfPoint);
                        Canvas.SetTop(image, prevPositionTopOfPoint - ViewModel.STEPTOPBOTTOMPOINT);
                    }
                }
            }
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

