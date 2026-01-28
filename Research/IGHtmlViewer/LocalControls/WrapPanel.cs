using System.Windows;
using System.Windows.Controls;

namespace IGHtmlViewer.LocalControls
{
    public class WrapPanel : Panel
    {
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation",
            typeof(Orientation), typeof(WrapPanel), null);

        public WrapPanel()
        {
            // we need horizontal orientation for the "flow" layout
            Orientation = Orientation.Horizontal;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
            {
                child.Measure(new Size(availableSize.Width, availableSize.Height));
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point mainPoint = new Point(0, 0);
            int i = 0;
            if (Orientation == Orientation.Horizontal)
            {
                double largestHeight = 0.0;
                foreach (UIElement child in Children)
                {
                    Point point = new Point(mainPoint.X + child.DesiredSize.Width, mainPoint.Y + child.DesiredSize.Height);
                    Rect r = new Rect(mainPoint, point);
                    child.Arrange(r);
                    if (child.DesiredSize.Height > largestHeight)
                    {
                        largestHeight = child.DesiredSize.Height;
                    }
                    mainPoint.X = mainPoint.X + child.DesiredSize.Width;
                    if ((i + 1) < Children.Count)
                    {
                        if ((mainPoint.X + Children[i + 1].DesiredSize.Width) > finalSize.Width)
                        {
                            mainPoint.X = 0;
                            mainPoint.Y = mainPoint.Y + largestHeight;
                            largestHeight = 0.0;
                        }
                    }
                    i++;
                }
            }
            else
            {
                double largestWidth = 0.0;
                foreach (UIElement child in Children)
                {
                    Point point = new Point(mainPoint.X + child.DesiredSize.Width, mainPoint.Y + child.DesiredSize.Height);
                    Rect r = new Rect(mainPoint, point);
                    child.Arrange(r);
                    if (child.DesiredSize.Width > largestWidth)
                    {
                        largestWidth = child.DesiredSize.Width;
                    }
                    mainPoint.Y = mainPoint.Y + child.DesiredSize.Height;
                    if ((i + 1) < Children.Count)
                    {
                        if ((mainPoint.Y + Children[i + 1].DesiredSize.Height) > finalSize.Height)
                        {
                            mainPoint.Y = 0;
                            mainPoint.X = mainPoint.X + largestWidth;
                            largestWidth = 0.0;
                        }
                    }
                    i++;
                }
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}
