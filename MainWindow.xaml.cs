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
        Point first;
        Point last;

        SolidColorBrush color = new SolidColorBrush();
        bool isPressed = false;

        public MainWindow()
        {
            InitializeComponent();
            color.Color = Colors.Black;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DefineFirstPoint(object sender, MouseButtonEventArgs e)
        {
            Canva.Children.Clear();
            isPressed = true;
            first = e.GetPosition(this);
        }

        private void DrawLineToPoint(object sender, MouseButtonEventArgs e)
        {
            if (isPressed)
            {
                last = e.GetPosition(this);

                var str = Data.Text;
                var numb = Razdel(str);

                DrawLine(first, last, numb);

                isPressed = false;
            }
        }

        public void DrawLine(Point first, Point last, List<int> numb)
        {

            var coords = GetCoord(first, last).AlternateElements(numb);
            SolidColorBrush brushcolor = new SolidColorBrush(Colors.Black);


            foreach (var coor in coords)
            {

                var x = coor.Item1;
                var y = coor.Item2;
                Rectangle rec = new Rectangle();
                Canvas.SetTop(rec, y);
                Canvas.SetLeft(rec, x);

                rec.Width = 1;
                rec.Height = 1;
                rec.Fill = brushcolor;

                Canva.Children.Add(rec);
            }
            return;
        }

        public List<int> Razdel(string str)
        {
            return str.Split().Select(i => int.Parse(i)).ToList();
        }

        public IEnumerable<Tuple<double, double>> GetCoord(Point first, Point last)
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

                if (x1 == x2 && y1 == y2) break;

                var e2 = 2 * err;

                if (e2 > dy)
                {
                    err += dy;
                    x1 += sx;
                }
                else
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
    }

    static class EnumExtentions
    {
        public static IEnumerable<T> AlternateElements<T>(this IEnumerable<T> source, List<int> num)
        {

            var i = 0;
            var k = num[i];
            var paint = true;
            foreach (var element in source)
            {
                if (k > 0)
                {
                    k--;
                    if (paint)
                        yield return element;
                    else
                        continue;
                }
                else
                {
                    i++;

                    if (i == num.Count)
                        i = 0;

                    k = num[i];
                    paint = !paint;
                }
            }
        }
    }
}
