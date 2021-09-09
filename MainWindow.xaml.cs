using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graph_Lab_1
{
    public partial class MainWindow : Window
    {
        private Point _first;
        private Point _last;

        private readonly SolidColorBrush _color = new SolidColorBrush();
        private bool _isPressed;

        public MainWindow()
        {
            InitializeComponent();
            _color.Color = Colors.Black;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DefineFirstPoint(object sender, MouseButtonEventArgs e)
        {
            Canva.Children.Clear();
            _isPressed = true;
            _first = e.GetPosition(this);

        }

        private void DrawLineToPoint(object sender, MouseButtonEventArgs e)
        {
            if (_isPressed)
            {
                _last = e.GetPosition(this);


                try
                {
                    var str = Data.Text;
                    var numb = SplitStrToInt(str);
                    DrawLine(_first, _last, numb);
                }
                catch (ArgumentException _)
                {
                    MessageBox.Show("Введите корректные параметры штрихов!");
                    return;
                }

                _isPressed = false;
            }
        }

        public void DrawLine(Point first, Point last, List<int> numb)
        {
            var coords = GetCoordinates(first, last).AlternateElements(numb);
            var brushColor = new SolidColorBrush(Colors.Black);


            foreach (var (x, y) in coords)
            {
                var rec = new Rectangle();
                Canvas.SetTop(rec, y - Canva.Margin.Top);
                Canvas.SetLeft(rec, x - Canva.Margin.Left);

                rec.Width = 1;
                rec.Height = 1;
                rec.Fill = brushColor;

                Canva.Children.Add(rec);
            }
        }

        public List<int> SplitStrToInt(string str)
        {
            var check = IsStringValid(str);
            if (check)
            {
                var intList = str.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                var containZeroes = intList.Any(i => i == 0);
                if (containZeroes)
                    throw new ArgumentException();

                return intList;
            }

            throw new ArgumentException();
        }

        public bool IsStringValid(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            var isOtherCharsDetected = str.Any(c => !char.IsNumber(c) && !char.IsWhiteSpace(c));
            if (isOtherCharsDetected)
                return false;

            return true;
        }

        public IEnumerable<Tuple<double, double>> GetCoordinates(Point first, Point last)
        {
            var x1 = first.X;
            var y1 = first.Y;
            var x2 = last.X;
            var y2 = last.Y;

            var sx = x1 < x2 ? 1 : -1;
            var sy = y1 < y2 ? 1 : -1;

            var dx = Math.Abs(x2 - x1);
            var dy = -Math.Abs(y2 - y1);
            var err = dx + dy;

            while (true)
            {
                yield return Tuple.Create(x1, y1);

                if (Math.Abs(x1 - x2) < 0.000001 && Math.Abs(y1 - y2) < 0.000001)
                    break;

                var e2 = 2 * err;

                if (e2 >= dy)
                {
                    err += dy;
                    x1 += sx;
                }
                if (e2 <= dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
    }
}