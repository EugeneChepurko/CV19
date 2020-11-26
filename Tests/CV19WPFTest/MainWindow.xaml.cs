using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CV19WPFTest
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double canvasWidth;
        public double CanvasWidth
        {
            get => canvasWidth;
            set { OnPropertyChanged(ref canvasWidth, value); }
        }

        private double canvasHeight;
        public double CanvasHeight
        {
            get => canvasHeight;
            set { OnPropertyChanged(ref canvasHeight, value); }
        }

        private double scaleX = 1;
        public double ScaleX
        {
            get => scaleX;
            set { OnPropertyChanged(ref scaleX, value); }
        }

        private double scaleY = 1;
        public double ScaleY
        {
            get => scaleY;
            set { OnPropertyChanged(ref scaleY, value); }
        }

        private double centerX;
        public double CenterX
        {
            get => centerX;
            set { OnPropertyChanged(ref centerX, value); }
        }

        private double centerY;
        public double CenterY
        {
            get => centerY;
            set { OnPropertyChanged(ref centerY, value); }
        }


        private double translateX;
        public double TranslateX
        {
            get => translateX;
            set { OnPropertyChanged(ref translateX, value); }
        }
        
        private double translateY;
        public double TranslateY
        {
            get => translateY;
            set { OnPropertyChanged(ref translateY, value); }
        }
        
        private int tickness = 1;
        public int Tickness
        {
            get => tickness;
            set { OnPropertyChanged(ref tickness, value); }
        }
        
        private Matrix matrix;
        public Matrix Matrix
        {
            get => matrix;
            set { OnPropertyChanged(ref matrix, value); }
        }

        Point start;
        Point origin;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            UpdateMatrixSizeChanged(null, null);

            CanvasWidth = backgroundCanvas.Width;
            CanvasHeight = backgroundCanvas.Height;

            //Rectangle rect = new Rectangle();
            //rect.Stroke = new SolidColorBrush(Colors.Black);
            //rect.Width = 400;
            //rect.Height = 200;
            //rect.Fill = Brushes.Black;
            //rect.StrokeThickness = 2;

            //backgroundCanvas.Children.Add(rect);
            //Canvas.SetLeft(rect, 0);
            //Canvas.SetTop(rect, 0);
        }

        private void Initialize()
        {
            //backgroundCanvas.ActualWidth = 2400; 
        }

        private double eDeltaWheelCouter = 0;
        private void Window_MouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            //double sX = ScaleX;
            //double sY = ScaleY;
            //if ((sX -= 0.1) < 1 || (sY -= -0.1) < 1)
            //{
            //    ScaleX = 1;
            //    ScaleY = 1;
            //    return;
            //}

            double stopW = ActualWidth;
            double stopH = ActualHeight;

            var w = backgroundCanvas.ActualWidth;
            var h = backgroundCanvas.ActualHeight;

            double deltaScaleX = w * 1.1 - w;
            double deltaScaleY = h * 1.1 - h;

            if (e.Delta > 0)
            {
                ////CenterX = e.GetPosition(this).X;
                ////CenterY = e.GetPosition(this).Y;
                ScaleX += 0.1;
                ScaleY += 0.1;

                //CanvasWidth = w * ScaleX;
                //CanvasHeight = h * ScaleY;

                ////stopW = -CanvasWidth + Width;
                ////stopH = -CanvasHeight + Height;
                ////TranslateX += stopW * 0.1;
                ////TranslateY += stopH * 0.1;

                //eDeltaWheel = e.Delta;

                var element = sender as UIElement;
                Point position = e.GetPosition(element);
                MatrixTransform transform = (MatrixTransform)((TransformGroup)element.RenderTransform).Children.FirstOrDefault(tr => tr is MatrixTransform);

                Matrix matrix = transform.Matrix;

                matrix.ScaleAtPrepend(ScaleX, ScaleY, position.X, position.Y);
                Matrix = matrix;
            }
            else
            {
                double sX = ScaleX;
                double sY = ScaleY;
                if ((sX -= 0.1) < 1 || (sY -= -0.1) < 1)
                    return;
               
                stopW = -CanvasWidth + Width;
                stopH = -CanvasHeight + Height;
                //TranslateX = TranslateX / ScaleX;
                if (backgroundCanvas.ActualWidth < CanvasWidth)
                {
                    CanvasWidth = CanvasWidth - deltaScaleX;
                    if ((TranslateX + deltaScaleX) > 0)
                        TranslateX = 0;

                    TranslateX = TranslateX <= stopW ? TranslateX = stopW + deltaScaleX : TranslateX;
                    double dScaleX = Math.Abs(stopW - TranslateX);

                    if (dScaleX < deltaScaleX)
                        TranslateX = stopW + deltaScaleX;
                    
                    
                    //TranslateX = (TranslateX + deltaScaleX) > 0 ? 0 : (TranslateX + deltaScaleX);
                }
                if (backgroundCanvas.ActualHeight < CanvasHeight)
                {
                    CanvasHeight = CanvasHeight - deltaScaleY;
                    if ((TranslateY + deltaScaleY) > 0)
                        TranslateY = 0;

                    TranslateY = TranslateY <= stopH ? TranslateY = stopH + deltaScaleY : TranslateY;

                    double dScaleY = Math.Abs(stopH - TranslateY);

                    if (dScaleY < deltaScaleY)
                        TranslateY = stopH + deltaScaleY;
                    //TranslateY = (TranslateY + deltaScaleY) > 0 ? 0 : (TranslateY + deltaScaleY);
                    //TranslateY = TranslateY + deltaScaleY;
                }

                ScaleX -= 0.1;
                ScaleY -= 0.1;
                var scale = e.Delta >= 0d ? 1.1 : (1d / 1.1);
                var element = sender as UIElement;
                Point position = e.GetPosition(element);
                MatrixTransform transform = (MatrixTransform)((TransformGroup)element.RenderTransform).Children.FirstOrDefault(tr => tr is MatrixTransform);

                Matrix matrix = transform.Matrix;

                //matrix.ScaleAtPrepend(ScaleX, ScaleY, position.X, position.Y);
                matrix.Scale(scale, scale);
                Matrix = matrix;

                //CanvasWidth = w / ScaleX;
                //CanvasHeight = h / ScaleY;

                eDeltaWheel = e.Delta;
                //Console.WriteLine("Mouse Wheel" + e.Delta);
            }
        }

        private void XXX()
        {
            int p = 0;
            int step = 10;

            for (int x = 0; x < 10; x++)
            {
                Line line = new Line();
                line.X1 = p;
                line.Y1 = 0;
                line.X2 = p;
                line.Y2 = backgroundCanvas.ActualHeight;
                line.Stroke = Brushes.Red;
                line.StrokeThickness = 2;
                SnapsToDevicePixels = true;
                line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                backgroundCanvas.Children.Add(line);
                p = p + step;
            }
        }

        private void UpdateMatrixSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var w = backgroundCanvas.ActualWidth;
            var h = backgroundCanvas.ActualHeight;

            backgroundCanvas.Children.Clear();
            for (int x = 30; x < w; x += 50)
                AddLineToBackground(x, 0, x, h);
            for (int y = 30; y < h; y += 50)
                AddLineToBackground(0, y, w, y);

            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Width = 400;
            rect.Height = 200;
            rect.Fill = Brushes.Gray;
            rect.StrokeThickness = 2;

            backgroundCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, backgroundCanvas.ActualWidth - rect.Width - 30);
            Canvas.SetTop(rect, 15);
        }

        void AddLineToBackground(double x1, double y1, double x2, double y2)
        {
            var line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                SnapsToDevicePixels = true
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            backgroundCanvas.Children.Add(line);
        }

        private void MouseLeftButtonDownDragGrid(object sender, MouseButtonEventArgs e)  // <<------- 1
        {
            FrameworkElement canvasTable = (FrameworkElement)sender;

            //TranslateTransform tt = (TranslateTransform)((TransformGroup)canvasTable.RenderTransform).Children.First(tr => tr is TranslateTransform);

            start = e.GetPosition(this/*canvasTable*/);
            //origin = new Point(tt.X, tt.Y);
            origin = new Point(TranslateX, TranslateY);
           
            canvasTable.CaptureMouse();
        }

        private double tX = 0;
        private double tY = 0;
        private double eDeltaWheel = 0;
       

        private void MouseMoveGrid(object sender, MouseEventArgs e)                     // <<------- 2
        {
            //---------------------------       NEW MATRIX      --------------------------------------------------
            //var element = sender as UIElement;
            //Point position = e.GetPosition(element);
            //MatrixTransform transform = (MatrixTransform)((TransformGroup)element.RenderTransform).Children.FirstOrDefault(tr => tr is MatrixTransform);

            //Matrix matrix = transform.Matrix;

            //matrix.ScaleAtPrepend(ScaleX, ScaleY, position.X, position.Y);
            //Matrix = matrix;



            //---------------------------     END NEW MATRIX      ------------------------------------------------
            //double w = 0;
            //double h = 0;
            double stopW = ActualWidth;
            double stopH = ActualHeight;
            double backCanvasW = backgroundCanvas.ActualWidth;
            double backCanvasH = backgroundCanvas.ActualHeight;
            //CanvasWidth = backgroundCanvas.ActualWidth;
            //CanvasHeight = backgroundCanvas.ActualHeight;

            
            FrameworkElement canvasTable = (FrameworkElement)sender;

            if (canvasTable.IsMouseCaptured)
            {
                //TranslateTransform tt = (TranslateTransform)((TransformGroup)canvasTable.RenderTransform).Children.First(tr => tr is TranslateTransform);
                Vector v = start - e.GetPosition(this);
                //tt.X = origin.X - v.X;
                //tt.Y = origin.Y - v.Y;
              
                TranslateX = origin.X - v.X;
                TranslateY = origin.Y - v.Y;

                TranslateX = TranslateX > 0 ? TranslateX = 0 : TranslateX;
                TranslateY = TranslateY > 0 ? TranslateY = 0 : TranslateY;

                //stopW = -backCanvasW + Width;
                //stopH = -backCanvasH + Height;
                stopW = -CanvasWidth + Width;
                stopH = -CanvasHeight + Height;


                TranslateX = TranslateX <= stopW ? TranslateX = stopW : TranslateX;
                TranslateY = TranslateY <= stopH ? TranslateY = stopH : TranslateY;
            }
            Console.Clear();
            Console.WriteLine("Mouse X,Y = " + e.GetPosition(this));
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Canvas H = " + backgroundCanvas.ActualHeight);
            Console.WriteLine("Canvas W = " + backgroundCanvas.ActualWidth);
            Console.WriteLine("TranslateX = " + TranslateX);
            Console.WriteLine("TranslateY = " + TranslateY);
            Console.WriteLine("ScaleX = " + ScaleX);
            Console.WriteLine("ScaleY = " + ScaleY);
            Console.WriteLine("Canvas Scale W = " + CanvasWidth);
            Console.WriteLine("Canvas Scale H = " + CanvasHeight);

            //Console.WriteLine("Mouse Wheel = " + eDeltaWheel);
            Console.WriteLine("Stop W = " + stopW);
            Console.WriteLine("Stop H = " + stopH);
            Console.WriteLine("CenterX = " + CenterX);
            Console.WriteLine("CenterY = " + CenterY);
        }

        private void MouseLeftButtonUpDragGrid(object sender, MouseButtonEventArgs e) // <<------- 3
        {
            FrameworkElement canvasTable = (FrameworkElement)sender;

            if (canvasTable != null)
            {
                //double w = canvasTable.ActualWidth;
                //double h = canvasTable.ActualHeight;

                //backgroundCanvas.Children.Clear();
                //for (int x = 10; x < w; x += 20)
                //    AddLineToBackground(x, 0, x, h);
                //for (int y = 10; y < h; y += 20)
                //    AddLineToBackground(0, y, w, y);

                canvasTable.ReleaseMouseCapture();
            }
        }
    }
}
