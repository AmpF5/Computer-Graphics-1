using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
using Color = System.Windows.Media.Color;
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
        byte r = 0, g = 0, b = 0;
        double c = 0, m = 0, y = 0;
        double k;
        SolidColorBrush brush = new(Color.FromRgb(0,0,0));

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
                            Stroke = brush
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
                            Stroke = brush,
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
                        ellipse.Stroke = brush;
                        ellipse.StrokeThickness = 2;
                        Canvas.SetTop(ellipse, Y1);
                        Canvas.SetLeft(ellipse, X1);
                        Canvas_Board.Children.Add(ellipse);
                        break;
                }
                IsFirstPoint = true;
            }

        }

        private void R_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorRgb('r', R_Value);
        }

        private void B_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorRgb('b', B_Value);
        }

        private void G_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorRgb('g', G_Value);
        }

        private void C_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorCmyk('c', C_Value);
        }

        private void M_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorCmyk('m', M_Value);
        }

        private void Y_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorCmyk('y', Y_Value);
        }

        private void K_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeColorCmyk('k', K_Value);
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

        private void Change_Color_Click(object sender, RoutedEventArgs e)
        {
            brush = GetBrush();
        }
        private SolidColorBrush GetBrush()
        {
            var brush = new SolidColorBrush(aColor.Color);
            return brush;
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void ChangeColorRgb(char colorLetter, TextBox textBox)
        {
            if (textBox.Text == "") textBox.Text = "0";
            var x = Byte.Parse(textBox.Text);
            switch(colorLetter)
            {
                case 'r':
                    r = x;
                    break;
                case 'g':
                    g = x;
                    break;
                case 'b':
                    b = x;
                    break;
            }
            RgbToCmyk(r, g, b);
            var color = Color.FromRgb(r, g, b);
            aColor.Color = color;
        }
        private void ChangeColorCmyk(char colorLetter, TextBox textBox)
        {
            if (textBox.Text == "") textBox.Text = "0";
            var x = Double.Parse(textBox.Text);
            switch (colorLetter)
            {
                case 'c':
                    c = x;
                    break;
                case 'm':
                    m = x;
                    break;
                case 'y':
                    y = x;
                    break;
                case 'k':
                    k = x;
                    break;
            }
            CmykToRgb(c, m, y, k);

        }
        private void RgbToCmyk(byte r, byte g, byte b)
        {
            var Pr = r / 255.0;
            var Pg = g / 255.0;
            var Pb = b / 255.0;
            k = Math.Min(1 - Pr, Math.Min(1 - Pg, 1 - Pb));
            c = (int)((1 - Pr - k) / (1 - k) * 100);
            //if (Double.IsNaN(c)) c = 0;
            m = (int)((1 - Pg - k) / (1 - k) * 100);
            //if (Double.IsNaN(m)) m = 0;
            y = (int)((1 - Pb - k) / (1 - k) * 100);
            //if (Double.IsNaN(y)) y = 0;
            C_Value.Text = c.ToString();
            Y_Value.Text = y.ToString();
            M_Value.Text = m.ToString();
            K_Value.Text = Math.Round(k * 100, 0).ToString();
 
        }
        private void CmykToRgb(double c, double m, double y, double k)
        {
            var Pc = c / 100.0;
            var Pm = m / 100.0;
            var Py = y / 100.0;
            var Pk = k / 100.0;
            byte r = (byte)((1 - Math.Min(1.0, Pc * (1.0 - Pk) + Pk)) * 255);
            byte g = (byte)((1 - Math.Min(1.0, Pm * (1.0 - Pk) + Pk)) * 255);
            byte b = (byte)((1 - Math.Min(1.0, Py * (1.0 - Pk) + Pk)) * 255);
            //R_Value.Text = r.ToString();
            //G_Value.Text = g.ToString();
            //B_Value.Text = b.ToString();
            var color = Color.FromRgb(r, g, b);
            aColor.Color = color;
        }
    }
}
