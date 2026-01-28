using Infragistics.Samples.Shared.Resources;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Infragistics.Samples.Shared.Controls
{
    /// <summary>
    /// Represents a panel control that can be drag within its parent control's boundaries. 
    /// <para>This control is used in samples that provide options/controls for interacting with IG controls. </para>
    /// </summary>
    public class OptionsPanel : ItemsControl
    {
        public OptionsPanel()
        {
            // TODO: change moving logic based on docking/alignment properties  
            // TODO: change expanding/collapsing logic based on current docking/alignment properties  

            // TODO: this control should collapse to the alignment location instead of top/left
            // TODO: load default styles in themes\generic.xaml
            string stylePath = "/Infragistics.Samples.Shared;component/Controls/OptionsPanel.xaml";

            base.DefaultStyleKey = typeof(OptionsPanel);
            //this.VerticalAlignment = VerticalAlignment.Top;
            //this.HorizontalAlignment = HorizontalAlignment.Left;
            //this.IsMovable = true;
            //this.IsEnabled = true;

            //Canvas.SetZIndex(this, 100);

            this.Loaded += OnOptionsPanelLoaded;

            // Add the default style to the control's resources
            var style = new ResourceDictionary();
            style.Source = new System.Uri(stylePath, System.UriKind.Relative);
            this.Resources.MergedDictionaries.Add(style);

            this.HeaderText = CommonStrings.XW_Options;
#if WPF
            // this prevents control moving
            //Infragistics.Windows.Utilities.SetSnapElementToDevicePixels(this, true);
#endif
        }
        ScrollViewer _contentViewer;
        ToggleButton _contentToggleButton;
        private bool _recalculateMargins = true;

        #region Events Handlers
        void OnParentLayoutUpdated(object sender, System.EventArgs e)
        {
            CalculateMargins();
            if (tTranslate.X > _maxMarginLeft)
                tTranslate.X = _maxMarginLeft;
            if (tTranslate.Y > _maxMarginTop)
                tTranslate.Y = _maxMarginTop;
        }
        void OnOptionsPanelLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = this.Parent as FrameworkElement;
                parent.LayoutUpdated += OnParentLayoutUpdated;

                this.UpdateLayout();
                if (VisualTreeHelper.GetChildrenCount(this) > 0)
                {
                    var rootElement = VisualTreeHelper.GetChild(this, 0) as Border;
                    if (rootElement != null)
                    {
                        var winHandle = rootElement.FindName("headerPanel") as Border;
                        _contentToggleButton = rootElement.FindName("contentToggleButton") as ToggleButton;
                        _contentViewer = rootElement.FindName("contentViewer") as ScrollViewer;

                        if (winHandle != null)
                        {
                            winHandle.MouseMove += OnWinHandleMouseMove;
                            winHandle.MouseLeftButtonDown += OnWinHandleMouseLeftButtonDown;
                            winHandle.MouseLeftButtonUp += OnWinHandleMouseLeftButtonUp;
                            winHandle.MouseRightButtonDown += OnWinHandleMouseRightButtonDown;
                            winHandle.MouseRightButtonUp += OnWinHandleMouseRightButtonUp;
                            winHandle.MouseLeave += OnWinHandleMouseLeave;
                        }
                    }
                }

                this.LayoutUpdated += OnOptionsPanelLayoutUpdated;

                if (this.IsMovable)
                    SetTransform();

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("OptionsPanel Loading Error: " + ex.Message);
            }
        }
        void OnOptionsPanelLayoutUpdated(object sender, System.EventArgs e)
        {
            if (_recalculateMargins)
            {
                CalculateMargins();
                Move(this.tTranslate.X, this.tTranslate.Y);
                _recalculateMargins = false;
            }
        }
        #endregion
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

        #region Moving Logic
        TranslateTransform tTranslate = new TranslateTransform();
        Point _borderPosition;
        Point _currentPosition;
        double _maxMarginLeft;
        double _maxMarginTop;
        bool _dragOn = false;
        private void OnWinHandleMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.Opacity = 0.0;
        }
        private void OnWinHandleMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.Opacity = 1.0;
        }
        private void OnWinHandleMouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = 1.0;
        }
        private void OnWinHandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.IsExpanded = !this.IsExpanded;
            }

            if (!this.IsMovable) return;

            var c = sender as FrameworkElement;
            _dragOn = true;
            this.Opacity *= 0.5;
            _borderPosition = e.GetPosition(sender as Border);

            CalculateMargins();

            if (c != null) c.CaptureMouse();
        }
        private void OnWinHandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_dragOn)
            {
                var c = sender as FrameworkElement;
                this.Opacity = 1;
                if (c != null) c.ReleaseMouseCapture();
                _dragOn = false;
            }
        }

        private void OnWinHandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragOn)
            {
                _currentPosition = e.GetPosition(sender as Border);
                var x = tTranslate.X + _currentPosition.X - _borderPosition.X;
                var y = tTranslate.Y + _currentPosition.Y - _borderPosition.Y;

                Move(x, y);
            }
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

        private void Move(double x, double y)
        {
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            if (x > _maxMarginLeft)
                x = _maxMarginLeft;
            if (y > _maxMarginTop)
                y = _maxMarginTop;
            tTranslate.X = x;
            tTranslate.Y = y;
        }
        #endregion

        private void ToggleExpandedState(bool isExpanded)
        {

        }

        #region Dependency Properties

        public static DependencyProperty HeaderTextProperty = DependencyProperty.Register(
         "HeaderText", typeof(string), typeof(OptionsPanel), null);

        /// <summary>
        /// Gets or sets Header Text of the OptionsPanel
        /// </summary>
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public const string IsMovablePropertyName = "IsMovable";
        public static DependencyProperty IsMovableProperty = DependencyProperty.Register(
         IsMovablePropertyName, typeof(bool), typeof(OptionsPanel), new PropertyMetadata(true, (sender, e) =>
         {
             (sender as OptionsPanel).OnPropertyChanged(new PropertyChangedEventArgs(IsMovablePropertyName));
         }));

        /// <summary>
        /// Gets or sets whether the OptionsPanel can be movable within its parent control's boundaries.  
        /// </summary>
        public bool IsMovable
        {
            get { return (bool)GetValue(IsMovableProperty); }
            set { SetValue(IsMovableProperty, value); }
        }
        public const string IsExpandablePropertyName = "IsExpandable";
        public static DependencyProperty IsExpandableProperty = DependencyProperty.Register(
            IsExpandablePropertyName, typeof(bool), typeof(OptionsPanel), new PropertyMetadata(true, (sender, e) =>
            {
                (sender as OptionsPanel).OnPropertyChanged(new PropertyChangedEventArgs(IsExpandablePropertyName));
            }));

        /// <summary>
        /// Gets or sets whether the OptionsPanel can be movable within its parent control's boundaries.  
        /// </summary>
        public bool IsExpandable
        {
            get { return (bool)GetValue(IsExpandableProperty); }
            set { SetValue(IsExpandableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public const string IsExpandedPropertyName = "IsExpanded";
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(IsExpandedPropertyName, typeof(bool), typeof(OptionsPanel), new PropertyMetadata(true, (sender, e) =>
         {
             (sender as OptionsPanel).OnPropertyChanged(new PropertyChangedEventArgs(IsExpandedPropertyName));
         }));

        /// <summary>
        /// Gets or sets whether the OptionsPanel is expanded
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case IsMovablePropertyName:
                    {
                        if (this.IsMovable)
                            SetTransform();
                        break;
                    }
                case IsExpandablePropertyName:
                    {
                        // behavior implemented using binding to this property in the generic style
                        break;
                    }
                case IsExpandedPropertyName:
                    {
                        _recalculateMargins = true;
                        break;
                    }
            }
        }
        #endregion


    }
}
