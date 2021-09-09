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

namespace Graph_Lab_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double x1 = 0;
        double x2 = 0;
        double y1 = 0;
        double y2 = 0;

        bool isPressed = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Canva_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

                Canva.Children.Clear();
                isPressed = true;
                Point p = e.GetPosition(this);
                x1 = Convert.ToInt32(p.X);
                y1 = Convert.ToInt32(p.Y);

        }


        private void Canva_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isPressed)
            { 
                Point p = e.GetPosition(this);
                x2 = Convert.ToInt32(p.X);
                y2 = Convert.ToInt32(p.Y);

                SolidColorBrush color = new SolidColorBrush();
                color.Color = Colors.Black;

                CreateLine(x1, y1, x2, y2, color);

                x1 = 0;
                x2 = 0;
                y1 = 0;
                y2 = 0;

                isPressed = false;
            }
        }
     

        public void CreateLine(double x1, double y1, double x2, double y2, SolidColorBrush color)
        {
            Point point = new Point(GetCoord(x1, y1, x2, y2));


            return;
        }




        public IEnumerable<Tuple<double, double>> GetCoord(double x1, double y1, double x2, double y2)
          {
            double dx = Math.Abs(x2 - x1);
            double dy = -Math.Abs(y2 - y1);

            double sx = x1 < x2 ? 1 : -1;
            double sy = y1 < y2 ? 1 : -1;

            double err = dx + dy;
            double e2;
              while(true)
                {
                    yield return Tuple.Create(x1, y1);

                    if (x1 == x2 && y1 == y2) break;

                    e2 = 2 * err;

                    if (e2 > dy)
                    {
                        err += dy;
                        x1 += sx;
                    }
                    else if (e2 < dx)
                    { 
                        err += dx;
                        y1 += sy;
                    }
                }
        }
            
         }
 



    }
}
