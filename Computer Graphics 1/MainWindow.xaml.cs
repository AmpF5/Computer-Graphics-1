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
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            if(index == 0)
            {
                index = -1;
                Line_Button.BorderBrush = Brushes.Gray;
                IsFirstPoint = false;
            }
            else
            {
                index = 0;
                Line_Button.BorderBrush = Brushes.Aquamarine;
                Rectangle_Button.BorderBrush = Brushes.Gray;
                Circle_Button.BorderBrush = Brushes.Gray;
            }
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            if(index == 1)
            {
                index = -1;
                Rectangle_Button.BorderBrush = Brushes.Gray;
                IsFirstPoint = false;
            }
            else
            {
                index = 1;
                Line_Button.BorderBrush = Brushes.Gray;
                Rectangle_Button.BorderBrush = Brushes.Aquamarine;
                Circle_Button.BorderBrush = Brushes.Gray;
            }
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            if(index == 2)
            {
                index = -1;
                Circle_Button.BorderBrush = Brushes.Gray;
                IsFirstPoint = false;
            }
            else
            {
                index = 2;
                Line_Button.BorderBrush = Brushes.Gray;
                Rectangle_Button.BorderBrush = Brushes.Gray;
                Circle_Button.BorderBrush = Brushes.Aquamarine;
            }
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
                        DrawLine();
                        break;
                    case 1:
                        DrawRectangle();
                        break;
                    case 2:
                        DrawCircle();
                        break;
                }
                IsFirstPoint = true;
            }
        }
        private void Create_Shape_Click(object sender, RoutedEventArgs e)
        {
            var x1 = Double.Parse(X_1.Text);
            var y1 = Double.Parse(Y_1.Text);
            var x2 = Double.Parse(X_2.Text);
            var y2 = Double.Parse(Y_2.Text);
            switch(index)
            {
                case 0:
                    DrawLine(x1, y1, x2, y2);
                    break;
                case 1:
                    DrawRectangle(x1, y1, x2, y2);
                    break;
                case 2:
                    DrawCircle(x1, y1, x2, y2);
                    break;
            }
        }

        private void DrawCircle()
        {
            double X1C = StartPoint.X;
            double Y1C = StartPoint.Y;
            double X2C = Mouse.GetPosition(this).X;
            double Y2C = Mouse.GetPosition(this).Y;
            Ellipse ellipse = new();
            ellipse.Width = ellipse.Height = Math.Sqrt(Math.Pow(X2C - X1C, 2) + Math.Pow(Y2C - Y1C, 2));
            ellipse.Stroke = brush;
            ellipse.StrokeThickness = 3;
            Canvas.SetTop(ellipse, Y1C);
            Canvas.SetLeft(ellipse, X1C);
            ellipse.MouseRightButtonDown += Shape_MouseRightButtonDown;
            ellipse.MouseMove += Shape_MouseMove;
            ellipse.MouseRightButtonUp += Shape_MouseRightButtonUp;
            Canvas_Board.Children.Add(ellipse);
        }
        private void DrawCircle(double x1, double y1, double x2, double y2)
        {
            Ellipse ellipse = new();
            ellipse.Width = ellipse.Height = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            ellipse.Stroke = brush;
            ellipse.StrokeThickness = 3;
            Canvas.SetTop(ellipse, y1);
            Canvas.SetLeft(ellipse, x1);
            ellipse.MouseRightButtonDown += Shape_MouseRightButtonDown;
            ellipse.MouseMove += Shape_MouseMove;
            ellipse.MouseRightButtonUp += Shape_MouseRightButtonUp;
            Canvas_Board.Children.Add(ellipse);
        }

        private void DrawRectangle()
        {
            double X1R = StartPoint.X;
            double Y1R = StartPoint.Y;
            double X2R = Mouse.GetPosition(this).X;
            double Y2R = Mouse.GetPosition(this).Y;
            Rectangle rectangle = new Rectangle()
            {
                Height = Math.Sqrt(Math.Pow(Y2R - Y1R, 2)),
                Width = Math.Sqrt(Math.Pow(X1R - X2R, 2)),
                Stroke = brush,
                StrokeThickness = 4,
            };
            Canvas.SetTop(rectangle, Y1R);
            Canvas.SetLeft(rectangle, X1R);
            rectangle.MouseRightButtonDown += Shape_MouseRightButtonDown;
            rectangle.MouseMove += Shape_MouseMove;
            rectangle.MouseRightButtonUp += Shape_MouseRightButtonUp;
            Canvas_Board.Children.Add(rectangle);
        }
        private void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            Rectangle rectangle = new Rectangle()
            {
                Height = Math.Sqrt(Math.Pow(y2 - y1, 2)),
                Width = Math.Sqrt(Math.Pow(x1 - x2, 2)),
                Stroke = brush,
                StrokeThickness = 4
            };
            Canvas.SetTop(rectangle, y1);
            Canvas.SetLeft(rectangle, x1);
            rectangle.MouseRightButtonDown += Shape_MouseRightButtonDown;
            rectangle.MouseMove += Shape_MouseMove;
            rectangle.MouseRightButtonUp += Shape_MouseRightButtonUp;
            Canvas_Board.Children.Add(rectangle);
        }

        private void DrawLine()
        {
            Line line = new Line()
            {
                X1 = StartPoint.X,
                Y1 = StartPoint.Y,
                X2 = Mouse.GetPosition(this).X,
                Y2 = Mouse.GetPosition(this).Y,
                Stroke = brush,
                StrokeThickness = 5
            };
            line.MouseRightButtonDown += Shape_MouseRightButtonDown;
            line.MouseMove += Shape_MouseMove;
            line.MouseRightButtonUp += Shape_MouseRightButtonUp;
            Canvas_Board.Children.Add(line);
        }
        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            Line line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = brush,
                StrokeThickness = 5
            };
            line.MouseRightButtonDown += Shape_MouseRightButtonDown;
            line.MouseMove += Shape_MouseMove;
            line.MouseRightButtonUp += Shape_MouseRightButtonUp;
            Canvas_Board.Children.Add(line);
        }
        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        private void Shape_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = sender as Shape;
            draggable.ReleaseMouseCapture();
        }

        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as Shape;
            if (isDragging && draggableControl != null && index == -1)
            {
                Point currentPosition = e.GetPosition(this);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);
                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
            }
        }

        private void Shape_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var draggableControl = sender as Shape;
            originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            isDragging = true;
            clickPosition = e.GetPosition(this);
            draggableControl.CaptureMouse();
            if(sender is Line)
            {
                var tempLine = sender as Line;
                X_1.Text = tempLine.X1.ToString();
                Y_1.Text = tempLine.Y1.ToString();
                X_2.Text = tempLine.X2.ToString();
                Y_2.Text = tempLine.Y2.ToString();
            }
            if(sender is Rectangle)
            {
                var tempRectangle = sender as Rectangle;
                var tempX1 = Canvas.GetLeft(tempRectangle);
                var tempY1 = Canvas.GetTop(tempRectangle);
                X_1.Text = tempX1.ToString();
                Y_1.Text = tempY1.ToString();

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
            //brush.Color.R == 0 && brush.Color.G == 0 && brush.Color.B == 0 ;
            Change_Color.BorderBrush = brush;
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
            if(r == 0 && g == 0 && b == 0)
            {
                c = 0;
                m = 0;
                y = 0;
            }
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
