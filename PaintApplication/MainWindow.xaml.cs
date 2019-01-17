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
            Line, Ellipse, Rectangle
        }

        private Point _start;
        private Point _end;

        private Shape _currentShape;

        public MainWindow()
        {
            InitializeComponent();
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
            // Lấy tọa độ X, Y lần click đầu tiên
            _start = e.GetPosition(this);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _end = e.GetPosition(this);
            }
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

        private void DrawRectangle()
        {
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.Orange,
                Fill = Brushes.AntiqueWhite,
                StrokeThickness = 4
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
                rectangle.SetValue(Canvas.TopProperty, _start.Y - 50);
                rectangle.Height = _end.Y - _start.Y;
            }
            else
            {
                rectangle.SetValue(Canvas.TopProperty, _end.Y - 50);
                rectangle.Height = _start.Y - _end.Y;
            }

            Canvas.Children.Add(rectangle);
        }

        private void DrawEllipse()
        {
            Ellipse ellipse = new Ellipse
            {
                Stroke = Brushes.Green,
                Fill = Brushes.AntiqueWhite,
                StrokeThickness = 4,
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
                ellipse.SetValue(Canvas.TopProperty, _start.Y - 50);
                ellipse.Height = _end.Y - _start.Y;
            }
            else
            {
                ellipse.SetValue(Canvas.TopProperty, _end.Y - 50);
                ellipse.Height = _start.Y - _end.Y;
            }
            Canvas.Children.Add(ellipse);
        }

        private void DrawLine()
        {
            Line line = new Line
            {
                Stroke = Brushes.Green,
                X1 = _start.X,
                X2 = _end.X,
                Y1 = _start.Y - 50,
                Y2 = _end.Y - 50
            };
            Canvas.Children.Add(line);
        }
    }
}
