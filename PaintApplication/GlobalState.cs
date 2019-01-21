using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PaintApplication
{
    static class GlobalState
    {
        public static event EventHandler ChangeColor;
        private static Brush _color = Brushes.Black;
        

        public static Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                ChangeColor(value, null);
            }
        }

        public static double StrokeSize { get; set; }
    }
}
