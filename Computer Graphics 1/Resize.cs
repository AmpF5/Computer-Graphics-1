using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Documents;

namespace Computer_Graphics_1
{
    internal class Resize : Adorner
    {
        VisualCollection AdornerVisuals;
        Thumb thumb1, thumb2;
        Rectangle Rec;

        public Resize(UIElement adornedElement) : base(adornedElement)
        {
            AdornerVisuals = new VisualCollection(this);

            thumb1 = new Thumb() { Background = Brushes.DarkRed, Height = 10, Width = 10 };
            thumb2 = new Thumb() { Background = Brushes.DarkRed, Height = 10, Width = 10 };
            //Rec = new Rectangle() { Stroke = Brushes.Coral, StrokeThickness = 2, StrokeDashArray = { 3, 2 } };
            thumb2.DragDelta += Thumb2_DragDelta;

            //AdornerVisuals.Add(Rec);
            AdornerVisuals.Add(thumb1);
            AdornerVisuals.Add(thumb2);
        }

        private void Thumb2_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var ele = (FrameworkElement)AdornedElement;
            ele.Height = ele.Height + e.VerticalChange < 0 ? 0 : ele.Height + e.VerticalChange;
            ele.Width = ele.Width + e.HorizontalChange < 0 ? 0 : ele.Width + e.HorizontalChange;

            ResizeLine(e, ele);
        }

        private static void ResizeLine(DragDeltaEventArgs e, FrameworkElement ele)
        {
            if (ele.GetType() == typeof(Line))
            {
                ((Line)ele).X2 += e.HorizontalChange;
                ((Line)ele).Y2 += e.VerticalChange;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return AdornerVisuals[index];
        }

        protected override int VisualChildrenCount => AdornerVisuals.Count;

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (AdornedElement.GetType() != typeof(Line))
            {
                //Rec.Arrange(new Rect(-2.5, -2.5, AdornedElement.DesiredSize.Width + 5,
                //    AdornedElement.DesiredSize.Height + 5));
                //thumb1.Arrange(new Rect(-5, -5, 10, 10));
                thumb2.Arrange(new Rect(AdornedElement.DesiredSize.Width - 5, AdornedElement.DesiredSize.Height - 5,
                    10, 10));
            }
            else
            {
                //thumb1.Arrange(new Rect(-5, -5, 10, 10));
                thumb2.Arrange(new Rect(((Line)AdornedElement).X2, ((Line)AdornedElement).Y2,
                    10, 10));
            }

            return base.ArrangeOverride(finalSize);
        }

    }
}
