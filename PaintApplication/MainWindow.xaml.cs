using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PaintApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Shape
        {
            Line, Ellipse, Rectangle, Brush
        }

        private Point _start;
        private Point _end;

        private Shape _currentShape;

        public MainWindow()
        {
            InitializeComponent();
            GlobalState.ChangeColor += ChangeColorSample;
            GlobalState.Color = Brushes.Black;
            GlobalState.StrokeSize = 1;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = Shape.Line;
        }

        private void EclipseButton_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = Shape.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = Shape.Rectangle;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _start = e.GetPosition(this);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _end = e.GetPosition(this);

                if (_currentShape == Shape.Brush)
                {
                    DrawBrush(e);
                }
            }
        }

        private void Brush_Selected(object sender, RoutedEventArgs e)
        {
            _currentShape = Shape.Brush;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (_currentShape)
            {
                case Shape.Line:
                    DrawLine();
                    break;
                case Shape.Ellipse:
                    DrawEllipse();
                    break;
                case Shape.Rectangle:
                    DrawRectangle();
                    break;
                default:
                    break;
            }
        }

        private void DrawBrush(MouseEventArgs e)
        {
            Line line = new Line();
            line.Stroke = GlobalState.Color;
            line.StrokeThickness = GlobalState.StrokeSize;
            line.X1 = _start.X;
            line.Y1 = _start.Y - Constants.HeaderHeight;
            line.X2 = _end.X;
            line.Y2 = _end.Y - Constants.HeaderHeight;

            _start = e.GetPosition(this);

            Canvas.Children.Add(line);
        }

        private void DrawRectangle()
        {
            Rectangle rectangle = new Rectangle
            {
                Stroke = GlobalState.Color,
                Fill = Brushes.White,
                StrokeThickness = GlobalState.StrokeSize
            };

            if (_end.Y == 0 && _end.X == 0)
            {
                _end.Y = _start.Y;
                _end.X = _start.X;
            }

            if (_end.X >= _start.X)
            {
                rectangle.SetValue(Canvas.LeftProperty, _start.X);
                rectangle.Width = _end.X - _start.X;
            }
            else
            {
                rectangle.SetValue(Canvas.LeftProperty, _end.X);
                rectangle.Width = _start.X - _end.Y;
            }

            if (_end.Y >= _start.Y)
            {
                rectangle.SetValue(Canvas.TopProperty, _start.Y - Constants.HeaderHeight);
                rectangle.Height = _end.Y - _start.Y;
            }
            else
            {
                rectangle.SetValue(Canvas.TopProperty, _end.Y - Constants.HeaderHeight);
                rectangle.Height = _start.Y - _end.Y;
            }

            Canvas.Children.Add(rectangle);
        }

        private void DrawEllipse()
        {
            Ellipse ellipse = new Ellipse
            {
                Stroke = GlobalState.Color,
                Fill = Brushes.White,
                StrokeThickness = GlobalState.StrokeSize,
                Height = 10,
                Width = 10
            };
            if (_end.X >= _start.X)
            {
                ellipse.SetValue(Canvas.LeftProperty, _start.X);
                ellipse.Width = _end.X - _start.X;
            }
            else
            {
                ellipse.SetValue(Canvas.LeftProperty, _end.X);
                ellipse.Width = _start.X - _end.Y;
            }

            if (_end.Y >= _start.Y)
            {
                ellipse.SetValue(Canvas.TopProperty, _start.Y - Constants.HeaderHeight);
                ellipse.Height = _end.Y - _start.Y;
            }
            else
            {
                ellipse.SetValue(Canvas.TopProperty, _end.Y - Constants.HeaderHeight);
                ellipse.Height = _start.Y - _end.Y;
            }
            Canvas.Children.Add(ellipse);
        }

        private void DrawLine()
        {
            Line line = new Line
            {
                Stroke = GlobalState.Color,
                X1 = _start.X,
                X2 = _end.X,
                Y1 = _start.Y - Constants.HeaderHeight,
                Y2 = _end.Y - Constants.HeaderHeight,
                StrokeThickness = GlobalState.StrokeSize
            };
            Canvas.Children.Add(line);
        }

        private void ChangeColorSample(object sender, EventArgs e)
        {
            Sample.Fill = GlobalState.Color;
        }

        private void ChangeBrushColor(object sender, RoutedEventArgs e)
        {
            GlobalState.Color = (sender as RadioButton).Background;
        }

        private void SelectThickNess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectThickNess.SelectedIndex)
            {
                case 0:
                    GlobalState.StrokeSize = 1;
                    break;
                case 1:
                    GlobalState.StrokeSize = 2;
                    break;
                case 2:
                    GlobalState.StrokeSize = 3;
                    break;
                default:
                    break;
            }
        }
    }
}
