using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;
//using Rectangle = System.Windows.Shapes.Rectangle;

namespace Computer_Graphics_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int index;
        bool IsFirstPoint = true;
        Point StartPoint;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            index = 0;

        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            index = 1;
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            index = 2;
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsFirstPoint)
            {
                StartPoint = (Mouse.GetPosition(this));
                IsFirstPoint = false;
            }
            else
            {
                switch (index)
                {
                    case 0:
                        Line line = new Line()
                        {
                            X1 = StartPoint.X,
                            Y1 = StartPoint.Y,
                            X2 = Mouse.GetPosition(this).X,
                            Y2 = Mouse.GetPosition(this).Y,
                            Stroke = Brushes.Black 
                        };
                        Canvas_Board.Children.Add(line);
                        break;
                    case 1:
                        double X1 = StartPoint.X;
                        double Y1 = StartPoint.Y;
                        double X2 = Mouse.GetPosition(this).X;
                        double Y2 = Mouse.GetPosition(this).Y;
                        Rectangle rectangle = new Rectangle()
                        {
                            Height = Math.Sqrt(Math.Pow(Y2 - Y1, 2)),
                            Width = Math.Sqrt(Math.Pow(X1 - X2, 2)),
                            Stroke = Brushes.Black,
                            StrokeThickness = 2,
                        };
                        Canvas.SetTop(rectangle, Y1);
                        Canvas.SetLeft(rectangle, X1);
                        Canvas_Board.Children.Add(rectangle);
                        break;
                    case 2:
                        X1 = StartPoint.X;
                        Y1 = StartPoint.Y;
                        X2 = Mouse.GetPosition(this).X;
                        Y2 = Mouse.GetPosition(this).Y;
                        Ellipse ellipse = new();
                        ellipse.Width = ellipse.Height = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                        ellipse.Stroke = Brushes.Black;
                        ellipse.StrokeThickness = 2;
                        Canvas.SetTop(ellipse, Y1);
                        Canvas.SetLeft(ellipse, X1);
                        Canvas_Board.Children.Add(ellipse);
                        break;
                }
                IsFirstPoint = true;
            }

        }

        private void Canvas_Board_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!IsFirstPoint)
            //{
            //    if (Canvas_Board.Children.Count > 0)
            //    {
            //        var child = (from c in Canvas_Board.Children.OfType<FrameworkElement>()
            //                     where "tempLine".Equals(c.Tag)
            //                     select c).First();
            //        if (child != null)
            //        {
            //            Canvas_Board.Children.Remove(child);
            //        }
            //    }
            //    switch (index)
            //    {
            //        case 0:
            //            Line line = new Line()
            //            {
            //                Tag = "tempLine",
            //                X1 = StartPoint.X,
            //                Y1 = StartPoint.Y,
            //                X2 = Mouse.GetPosition(this).X,
            //                Y2 = Mouse.GetPosition(this).Y,
            //                Stroke = Brushes.Black
            //            };
            //            Canvas_Board.Children.Add(line);
            //            return;
            //    }
            //}
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
