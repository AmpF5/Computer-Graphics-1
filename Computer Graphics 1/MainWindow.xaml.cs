using System;
using System.Collections.Generic;
using System.Drawing;
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
using Pen = System.Drawing.Pen;
using Point = System.Drawing.Point;

namespace Computer_Graphics_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(System.Drawing.Color.Black, 1);
        int index;
        public MainWindow()
        {
            InitializeComponent();

            bm = new Bitmap(1280, 720);
            g = Graphics.FromImage(bm);
            g.Clear(System.Drawing.Color.White);
            
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {

        }

        //private Rectangle GetRect()
        //{
        //    Point pointXY;
        //    Point pointX1Y1;
        //    Rectangle rect = new();
        //    rect.RadiusX = Math.Min(pointXY.X, pointX1Y1.X);
        //    rect.RadiusY = Math.Min(pointXY.Y, pointX1Y1.Y);
        //    rect.Width = Math.Abs(pointXY.X - pointX1Y1.X);
        //    rect.Height = Math.Abs(pointXY.Y - pointX1Y1.Y);
        //    return rect;
        //}
    }
}
