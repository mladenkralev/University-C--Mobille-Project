using Siders.model;
using Siders.view;
using Siders.viewmodel.commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Siders.viewmodel.viewmodel
{
    class ViewModel : ObservableCollection<Image>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Canvas _canvasTopBottom = new Canvas();
        private DispatcherTimer _dispatcherTimerWalls;
        private DispatcherTimer _dispatcherGenerateWalls;
        private int _currentGeneratedRandomWallSpaceLeft { get; set; }
        private int _currentGeneratedRandomWallSpaceTop { get; set; }
        private int _currentGeneratedRandomType { get; set; }
        private int _currentCountOfWalls { get; set; }
        private Random _rndGeneratorOfTypesOfThings = new Random();
        private RandomGeneratorOfWalls _rndGenratorOfWalls;
        private List<Score> ScoreList = new List<model.Score>();


        #region Property

        private Score _score;

        public Score Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        private Color _colorRight;
        public Color ColorRight
        {
            get
            {
                return _colorRight;
            }

            set
            {
                _colorRight = value;
                OnPropertyChanged("Color");
            }
        }

        private Color _colorLeft;
        public Color ColorLeft
        {
            get
            {
                return _colorLeft;
            }

            set
            {
                _colorLeft = value;
                OnPropertyChanged("ColorLeft");
            }
        }

        private int _counterOfSwaps = 0;
        public int CounterOfSwaps
        {
            get { return _counterOfSwaps; }
            set
            {
                _counterOfSwaps = value;
                OnPropertyChanged("CounterOfSwaps");
            }
        }

        private byte _counterOfTicksForReversingFromTopToRight = 1;
        public byte CounterOfTicksForReversingFromTopToRight
        {
            get { return _counterOfTicksForReversingFromTopToRight; }
            set
            {
                _counterOfTicksForReversingFromTopToRight = value;
                OnPropertyChanged("CounterOfTicksForReversingFromTopToRight");
            }
        }


        private bool _toButtom;

        public bool ToButtom
        {
            get { return _toButtom; }
            set
            {
                _toButtom = value;
                OnPropertyChanged("ToButtom");
            }
        }


        private bool _toLeft;
        public bool ToLeft
        {
            get { return _toLeft; }
            set
            {
                _toLeft = value;
                OnPropertyChanged("ToLeft");
            }
        }


        private bool _fromTopToButtom;
        public bool FromTopToButtom
        {
            get { return _fromTopToButtom; }
            set
            {
                _fromTopToButtom = value;
                OnPropertyChanged("FromTopToButtom");
            }
        }

        private int _heightOfPoint;

        public int HeightOfPoint
        {
            get { return _heightOfPoint; }
            set
            {
                _heightOfPoint = value;
                OnPropertyChanged("HeightOfPoint");
            }
        }

        private int _widthOfPoint;

        public int WidthOfPoint
        {
            get { return _widthOfPoint; }
            set
            {
                _widthOfPoint = value;
                OnPropertyChanged("WidthOfPoint");
            }
        }



        private LeftButtonCommand _leftButtonCommand;

        public LeftButtonCommand LeftButtonCommand
        {
            get { return _leftButtonCommand; }
            set
            {
                _leftButtonCommand = value;
                OnPropertyChanged("LeftButtonCommand");
            }
        }

        private RightButtonCommand _rightButtonCommand;

        public RightButtonCommand RightButtonCommand
        {
            get { return _rightButtonCommand; }
            set
            {
                _rightButtonCommand = value;
                OnPropertyChanged("RightButtonCommand");
            }
        }

        private double _canvasTop;

        public double CanvasTopPoint
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged("CanvasTop");
            }
        }


        private double _canvasLeft;

        public double CanvasLeftPoint
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged("CanvasLeft");
            }
        }


        public Canvas CanvasTopButtom
        {
            get
            {
                return _canvasTopBottom;
            }
            set
            {
                _canvasTopBottom = value;
                OnPropertyChanged("CanvasTopButtom");
            }
        }

        private Rect _rectanglePoint;

        public Rect RectanglePoint
        {
            get { return _rectanglePoint; }
            set { _rectanglePoint = value; }
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        private ObservableCollection<WallModel> _collectionOfImages = new ObservableCollection<WallModel>();

        public ObservableCollection<WallModel> CollectionOfImages
        {
            get { return _collectionOfImages; }
            set
            {
                _collectionOfImages = value;
                OnPropertyChanged("CollectionOfImages");
            }
        }
        #endregion  
        #region CONSTANTS

        private const int WIDTHSMALL_H = 25, HEIGHTSMALL_H = 10;
        private const int WIDTHMEDIUM_H = 50, HEIGHTMEDIUM_H = 10;
        private const int WIDTHLARGE_H = 75, HEIGHTLARGE_H = 10;

        private const int WIDTHSMALL_V = 10, HEIGHTSMALL_V = 25;
        private const int WIDTHMEDIUM_V = 10, HEIGHTMEDIUM_V = 50;
        private const int WIDTHLARGE_V = 10, HEIGHTLARGE_V = 75;

        private const int WIDTH_EVERY_SWITCH = 20, HEIGHT_EVERY_SWITCH = 20;

        private const int STEPLEFTRIGHTWALLS = 5;
        private const int STEPTOPBOTTOMWALLS = 5;
        public const int STEPLEFTRIGHTPOINT = 5;
        public const int STEPTOPBOTTOMPOINT = 5;
        #endregion



        public ViewModel()
        {
            foreach (UIElement child in _canvasTopBottom.Children)
            {
                _canvasTopBottom.Children.Remove(child);
            }

            //setting score as default
            Score = new Score(0, "Unknowned");
            startingGameControls();

            //starting from? 
            setStartingDefaultDirections();

            timersWalls();

        }

        private void startingGameControls()
        {
            ColorRight = model.Colors.Blue;
            ColorLeft = model.Colors.White;

            //CREATE TWO BUTTONS
            RightButtonCommand = new RightButtonCommand(this);
            LeftButtonCommand = new LeftButtonCommand(this);

            // COORDINATES FOR THE POINT
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var width = Math.Round(Window.Current.Bounds.Width * scaleFactor, 0);
            var height = Math.Round(Window.Current.Bounds.Height * scaleFactor, 0);

            // SIZE AND ACTUAL COORDINATES HERE
            WidthOfPoint = 10;
            HeightOfPoint = 10;
            CanvasLeftPoint = height / 2.5;
            CanvasTopPoint = width / 4.5;

            // RECT AROUND THE IMAGE ON THE CANVAS
            RectanglePoint = new Rect(CanvasLeftPoint, CanvasTopPoint, WidthOfPoint, HeightOfPoint);

            model.Point movingPoint = new model.Point();
            Source = movingPoint.ImagePath;
        }

        private void setStartingDefaultDirections()
        {
            FromTopToButtom = false;
            ToLeft = false;
            ToButtom = false;
        }

        private void timersWalls()
        {
            //Timer CREATE WALLS
            _dispatcherGenerateWalls = new DispatcherTimer();
            _dispatcherGenerateWalls.Tick += createWallsTimer;
            _dispatcherGenerateWalls.Interval = TimeSpan.FromMilliseconds(2000);
            bool enabledGeneratingWalls = _dispatcherGenerateWalls.IsEnabled;
            _dispatcherGenerateWalls.Start();

            // Timer MOVE Walls
            _dispatcherTimerWalls = new DispatcherTimer();
            _dispatcherTimerWalls.Tick += movingWallTimer;
            _dispatcherTimerWalls.Interval = TimeSpan.FromMilliseconds(70);
            bool enabledMovingWalls = _dispatcherTimerWalls.IsEnabled;
            _dispatcherTimerWalls.Start();
        }

        private void createWallsTimer(object sender, object e)
        {
            /******************************

                |***********************|
                |                       |
                |                       |
                generete walls somewhere up

            *******************************/
            _rndGenratorOfWalls = new RandomGeneratorOfWalls();
            _currentCountOfWalls = _rndGenratorOfWalls.generateNumberOfWalls(1, 3);

            if (FromTopToButtom)
            {
                for (int i = 0; i < _currentCountOfWalls; i++)
                {
                    _currentGeneratedRandomType = _rndGeneratorOfTypesOfThings.Next(0, 3); // type
                    if (ToButtom)
                    {
                        _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfHorizontalWalls(0, 300);
                        _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfHorizontalWalls(0, 75);
                        switch (_currentGeneratedRandomType)
                        {
                            case 0:
                                generateMeAWall(WIDTHSMALL_H, HEIGHTSMALL_H, true, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 1:
                                generateMeAWall(WIDTHMEDIUM_H, HEIGHTMEDIUM_H, true, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 2:
                                generateMeAWall(WIDTHLARGE_H, HEIGHTLARGE_H, true, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfHorizontalWalls(0, 300);
                        _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfHorizontalWalls(325, 350);
                        switch (_currentGeneratedRandomType)
                        {
                            case 0:
                                generateMeAWall(WIDTHSMALL_H, HEIGHTSMALL_H, true, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 1:
                                generateMeAWall(WIDTHMEDIUM_H, HEIGHTMEDIUM_H, true, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 2:
                                generateMeAWall(WIDTHLARGE_H, HEIGHTLARGE_H, true, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _currentCountOfWalls; i++)
                {
                    _currentGeneratedRandomType = _rndGeneratorOfTypesOfThings.Next(0, 3); // type
                    if (ToLeft)
                    {
                        _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfVerticalWalls(350, 400);
                        _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfVerticalWalls(0, 350);
                        switch (_currentGeneratedRandomType)
                        {
                            case 0:
                                generateMeAWall(WIDTHSMALL_V, HEIGHTSMALL_V, false, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 1:
                                generateMeAWall(WIDTHMEDIUM_V, HEIGHTMEDIUM_V, false, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 2:
                                generateMeAWall(WIDTHLARGE_V, HEIGHTLARGE_V, false, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            default:
                                break;
                        }
                    }
                    else {
                        _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfVerticalWalls(0, 25);
                        _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfVerticalWalls(0, 350);
                        switch (_currentGeneratedRandomType)
                        {
                            case 0:
                                generateMeAWall(WIDTHSMALL_V, HEIGHTSMALL_V, false, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 1:
                                generateMeAWall(WIDTHMEDIUM_V, HEIGHTMEDIUM_V, false, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            case 2:
                                generateMeAWall(WIDTHLARGE_V, HEIGHTLARGE_V, false, _currentGeneratedRandomWallSpaceTop, _currentGeneratedRandomWallSpaceLeft);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (_counterOfSwaps % 3 == 0)
            {
                generateMeASwap(WIDTH_EVERY_SWITCH, HEIGHT_EVERY_SWITCH, FromTopToButtom);
            }

        }

        private void movingWallTimer(object sender, object e)
        {
            double prevPositionTop;
            double prevPositionLeft;

            foreach (UIElement child in _canvasTopBottom.Children)
            {
                prevPositionTop = Canvas.GetTop(child);
                prevPositionLeft = Canvas.GetLeft(child);


                var image = child as Image; // get image
                string str = ((BitmapImage)image.Source).UriSource.ToString(); // get imageName
                Rect myRectangle = new Rect(prevPositionLeft, prevPositionTop, image.Width, image.Height);
                myRectangle.Intersect(RectanglePoint);

                /*
                    Swap Logic
                */

                if (((str.Equals("ms-appx:/src/swap_H.png")))
                    && ((!myRectangle.IsEmpty)))
                {
                    if (ToButtom == false)
                    {
                        ToButtom = true;
                    }
                    else
                    {
                        ToButtom = false;
                    }

                    _canvasTopBottom.Children.Remove(child);
                }
                else {
                    if (str.Equals("ms-appx:/src/swap_V.png") && ((!myRectangle.IsEmpty)))
                    {
                        if (ToLeft == false)
                        {
                            ToLeft = true;
                        }
                        else
                        {
                            ToLeft = false;
                        }
                        _canvasTopBottom.Children.Remove(child);
                    }
                    else {
                        if (!myRectangle.IsEmpty)
                        {
                            showEndMessege();
                        }
                    }
                }
                // WALLS
                if (str.Equals("ms-appx:/src/wall_H.png") && (FromTopToButtom))
                {
                    if (ToButtom)
                    {
                        Canvas.SetLeft(child, prevPositionLeft);
                        Canvas.SetTop(child, prevPositionTop + STEPTOPBOTTOMWALLS);
                    }
                    else {
                        Canvas.SetLeft(child, prevPositionLeft);
                        Canvas.SetTop(child, prevPositionTop - STEPTOPBOTTOMWALLS);
                    }
                }
                if (str.Equals("ms-appx:/src/wall_V.png") && (FromTopToButtom == false))
                {
                    if (ToLeft)
                    {
                        Canvas.SetLeft(child, prevPositionLeft - STEPLEFTRIGHTWALLS);
                        Canvas.SetTop(child, prevPositionTop);
                    }
                    else
                    {
                        Canvas.SetLeft(child, prevPositionLeft + STEPLEFTRIGHTWALLS);
                        Canvas.SetTop(child, prevPositionTop);
                    }
                }
                // SWAP
                if (str.Equals("ms-appx:/src/swap_H.png") && (FromTopToButtom))
                {
                    if (ToButtom)
                    {
                        Canvas.SetLeft(child, prevPositionLeft);
                        Canvas.SetTop(child, prevPositionTop - STEPTOPBOTTOMWALLS); // same step as the wall
                    }
                    else
                    {
                        Canvas.SetLeft(child, prevPositionLeft);
                        Canvas.SetTop(child, prevPositionTop + STEPTOPBOTTOMWALLS);
                    }
                }

                if (str.Equals("ms-appx:/src/swap_V.png") && (FromTopToButtom == false))
                {
                    if (ToLeft)
                    {
                        Canvas.SetLeft(child, prevPositionLeft - STEPLEFTRIGHTWALLS);
                        Canvas.SetTop(child, prevPositionTop);  // same step as the wall
                    }
                    else
                    {
                        Canvas.SetLeft(child, prevPositionLeft + STEPLEFTRIGHTWALLS);
                        Canvas.SetTop(child, prevPositionTop);  // same step as the wall
                    }
                }

                /*
                *   REMOVING UNNESSESERY THINGS
                * 
                */
                if (Canvas.GetLeft(child) < -20)
                {
                    _canvasTopBottom.Children.Remove(child);
                }
                if (Canvas.GetTop(child) < -20)
                {
                    _canvasTopBottom.Children.Remove(child);
                }
                if (Canvas.GetTop(child) > 600)
                {
                    _canvasTopBottom.Children.Remove(child);
                }
                if (Canvas.GetLeft(child) > 600)
                {
                    _canvasTopBottom.Children.Remove(child);
                }

            }

            reverseEverthingOnTick();



        }

        public void reverseEverthingOnTick()
        {
            if ((CounterOfTicksForReversingFromTopToRight % 51) == 0)
            {
                if (FromTopToButtom)
                    FromTopToButtom = false;
                else
                    FromTopToButtom = true;
            }

            if (CounterOfTicksForReversingFromTopToRight == 51)
            {
                CounterOfTicksForReversingFromTopToRight = 1;
                CounterOfSwaps++;
            }
            else
            {
                CounterOfTicksForReversingFromTopToRight++;
            }

            if ((CounterOfTicksForReversingFromTopToRight % 10) == 0)
            {
                Score.ScoreNumber += 1;
            }
        }

        public async void showEndMessege()
        {
            _dispatcherGenerateWalls.Stop();
            _dispatcherTimerWalls.Stop();


            StorageFolder folder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile =
                await folder.CreateFileAsync("sample.txt", CreationCollisionOption.OpenIfExists);

            await Windows.Storage.FileIO.AppendTextAsync(sampleFile, Score.User + " " + Score.ScoreNumber + "\n");


            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(EndingMesseges), Score);


        }

        public void generateMeASwap(int width, int height, bool type)
        {
            SwitcherModel model;
            if (type)
            {
                if (ToButtom)
                {

                }
                else
                {
                    _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfHorizontalWalls(0, 300);
                    _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfHorizontalWalls(0, 75);
                }
            }
            else {
                if (ToLeft)
                {
                    _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfVerticalWalls(350, 400);
                    _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfVerticalWalls(0, 350);

                }
                else
                {
                    _currentGeneratedRandomWallSpaceLeft = _rndGenratorOfWalls.generateLeftOfHorizontalWalls(0, 25);
                    _currentGeneratedRandomWallSpaceTop = _rndGenratorOfWalls.generateTopOfHorizontalWalls(0, 350);
                }
            }

            model = new SwitcherModel(width, height, type);
            Canvas.SetZIndex(model.Image, 2);
            Canvas.SetLeft(model.Image, _currentGeneratedRandomWallSpaceLeft);
            Canvas.SetTop(model.Image, _currentGeneratedRandomWallSpaceTop);
            _canvasTopBottom.Children.Add(model.Image);

        }

        public void generateMeAWall(int width, int height, bool type, int top, int left)
        {
            WallModel wallmodel;
            wallmodel = new WallModel(width, height, type);
            Canvas.SetZIndex(wallmodel.Image, 2);
            Canvas.SetLeft(wallmodel.Image, left);
            Canvas.SetTop(wallmodel.Image, top);
            _canvasTopBottom.Children.Add(wallmodel.Image);
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
