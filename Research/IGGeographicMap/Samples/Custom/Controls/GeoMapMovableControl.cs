using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IGGeographicMap.Samples.Custom
{
    [TemplatePart(Name = "MovablePanel", Type = typeof(Border))]
    public class GeoMapMovableControl : Control
    {
        public GeoMapMovableControl()
        {
            base.DefaultStyleKey = typeof(GeoMapMovableControl);
            this.IsMovable = true;

            this.Loaded += OnLoaded;
        }
        #region Event Handlers - Moving

        protected bool IsDragging = false;
        protected bool IsUpdating = false;
        protected bool IsRecalculateMargins = true;

        readonly TranslateTransform tTranslate = new TranslateTransform();
        private double _desiredOpacity = 1.0;
        Point _borderPosition;
        protected Point CurrentPosition;
        double _maxMarginLeft;
        double _maxMarginTop;

         
        void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = this.Parent as FrameworkElement;
                parent.LayoutUpdated += OnParentLayoutUpdated;

                this.UpdateLayout();
                if (VisualTreeHelper.GetChildrenCount(this) > 0)
                {
                    var rootElement = VisualTreeHelper.GetChild(this, 0) as Grid;
                    if (rootElement != null)
                    {
                        var winHandle = rootElement.FindName("MovablePanel") as Border;

                        if (winHandle != null)
                        {
                            winHandle.MouseMove += OnWinHandleMouseMove;
                            winHandle.MouseLeftButtonDown += OnWinHandleMouseLeftButtonDown;
                            winHandle.MouseLeftButtonUp += OnWinHandleMouseLeftButtonUp;
                            winHandle.MouseRightButtonDown += OnWinHandleMouseRightButtonDown;
                            winHandle.MouseRightButtonUp += OnWinHandleMouseRightButtonUp;
                            winHandle.MouseLeave += OnWinHandleMouseLeave;
                            winHandle.MouseEnter += OnWinHandleMouseEnter;
                        }
                         
                    }
                }
                this.LayoutUpdated += OnGeoMapLocatorLayoutUpdated;

                if (IsMovable)
                    SetTransform();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GeoMapLocator Loading Error: " + ex.Message);
            }
        }
        private void OnParentLayoutUpdated(object sender, EventArgs e)
        {
            CalculateMargins();
            if (tTranslate.X > _maxMarginLeft)
                tTranslate.X = _maxMarginLeft;
            if (tTranslate.Y > _maxMarginTop)
                tTranslate.Y = _maxMarginTop;
        }
        private void OnGeoMapLocatorLayoutUpdated(object sender, EventArgs e)
        {
            if (IsRecalculateMargins)
            {
                CalculateMargins();
                Move(this.tTranslate.X, this.tTranslate.Y);
                IsRecalculateMargins = false;
            }
        }

        private void OnWinHandleMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Opacity = 0.0;
        }
        private void OnWinHandleMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Opacity = _desiredOpacity;
        }
        private void OnWinHandleMouseEnter(object sender, MouseEventArgs e)
        {
            _desiredOpacity = this.Opacity;
        }
        private void OnWinHandleMouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = _desiredOpacity;
        }

        private void OnWinHandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsMovable) return;

            var c = sender as FrameworkElement;
            IsDragging = true;
            _desiredOpacity = this.Opacity;
            this.Opacity *= 0.5;
            _borderPosition = e.GetPosition(sender as Border);

            CalculateMargins();

            if (c != null) c.CaptureMouse();
        }
        private void OnWinHandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDragging)
            {
                var c = sender as FrameworkElement;
                this.Opacity = _desiredOpacity;
                if (c != null) c.ReleaseMouseCapture();
                IsDragging = false;
            }
        }

        private void OnWinHandleMouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                CurrentPosition = e.GetPosition(sender as Border);
                var x = tTranslate.X + CurrentPosition.X - _borderPosition.X;
                var y = tTranslate.Y + CurrentPosition.Y - _borderPosition.Y;

                Move(x, y);
            }
        }

        #endregion

        #region Methods
        private void Move(double x, double y)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > _maxMarginLeft)
                x = _maxMarginLeft;
            if (y > _maxMarginTop)
                y = _maxMarginTop;
            tTranslate.X = x;
            tTranslate.Y = y;
        }
        private void CalculateMargins()
        {
            var parent = (this.Parent as FrameworkElement);
            if (parent != null)
            {
                //TODO: offset by control's Margin
                _maxMarginLeft = parent.ActualWidth - this.ActualWidth;
                _maxMarginTop = parent.ActualHeight - this.ActualHeight;
            }
        }

        private void SetTransform()
        {
            if (this.Parent as FrameworkElement != null)
            {
                GeneralTransform objGeneralTransform = this.TransformToVisual(this.Parent as FrameworkElement);
                Point point = objGeneralTransform.Transform(new Point(0, 0));
                tTranslate.X = point.X;
                tTranslate.Y = point.Y;
                this.Margin = new Thickness(0);
                this.VerticalAlignment = VerticalAlignment.Top;
                this.HorizontalAlignment = HorizontalAlignment.Left;
                this.RenderTransform = tTranslate;
            }

        }

        #endregion

        public const string IsMovablePropertyName = "IsMovable";
        public static DependencyProperty IsMovableProperty = DependencyProperty.Register(
            IsMovablePropertyName, typeof(bool), typeof(GeoMapMovableControl), null);

        /// <summary>
        /// Gets or sets whether the OptionsPanel can be movable within its parent control's boundaries.  
        /// </summary>
        public bool IsMovable
        {
            get
            {
                return (bool)GetValue(IsMovableProperty);
            }
            set
            {
                SetValue(IsMovableProperty, value);
                if (value) SetTransform();
            }
        }
    }
}