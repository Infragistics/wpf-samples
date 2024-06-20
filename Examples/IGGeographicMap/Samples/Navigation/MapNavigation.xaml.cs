using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Infragistics.Controls;

namespace IGGeographicMap.Samples
{
    public partial class MapNavigation : Infragistics.Samples.Framework.SampleContainer
    {
        public MapNavigation()
        {
            InitializeComponent();

            this.NavOverviewPaneCheckBox.IsChecked = true;
            this.NavOverviewPaneCheckBox.Click += OnNavigationControlCheckBoxClick;

            this.NavZoomOutButton.Click += OnNavigationButtonClick;
            this.NavZoomInButton.Click += OnNavigationButtonClick;
            this.NavZoomFitButton.Click += OnNavigationButtonClick;
            this.NavPanUpButton.Click += OnNavigationButtonClick;
            this.NavPanRightButton.Click += OnNavigationButtonClick;
            this.NavPanLeftButton.Click += OnNavigationButtonClick;
            this.NavPanDownButton.Click += OnNavigationButtonClick;

            this.MapInteractionComboBox.SelectionChanged += OnMapInteractionComboBoxSelectionChanged;
            this.MapPanModifiersComboBox.SelectionChanged += OnMapPanModifiersComboBoxSelectionChanged;
            this.MapDragModifiersComboBox.SelectionChanged += OnMapDragModifiersComboBoxSelectionChanged;
        }

        void OnMapPanModifiersComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var modifierKeys = (ModifierKeys)e.AddedItems[0];
            this.GeoMap.PanModifier = modifierKeys;
        }
        void OnMapDragModifiersComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var modifierKeys = (ModifierKeys)e.AddedItems[0];
            this.GeoMap.DragModifier = modifierKeys;
        }
        void OnMapInteractionComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var interactionState = (InteractionState)e.AddedItems[0];
            this.GeoMap.DefaultInteraction = interactionState;
        }

        void OnNavigationControlCheckBoxClick(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            bool? isChecked = checkBox.IsChecked;
            bool isVisible = isChecked.HasValue ? isChecked.Value : false;
            Visibility visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;

            if (checkBox.Tag.Equals("NavOverviewPane"))
            {
                this.GeoMap.OverviewPlusDetailPaneVisibility = visibility;
            }
        }

        void OnNavigationButtonClick(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();

            Rect window = this.GeoMap.WindowRect;
            double zoomScale = 0.05;
            double panScale = System.Math.Min(window.Width, window.Height) * 0.2;

            #region Zoom Operations
            if (tag.Equals("NavZoomIn"))
            {
                window.Width = System.Math.Max(0.01, window.Width - (2 * zoomScale));
                window.Height = System.Math.Max(0.01, window.Height - (2 * zoomScale));

                if (window.Width > 0.01) window.X = System.Math.Min(1.0, window.X + zoomScale);
                if (window.Height > 0.01) window.Y = System.Math.Min(1.0, window.Y + zoomScale);
            }
            else if (tag.Equals("NavZoomOut"))
            {
                window.Width = System.Math.Min(1.0, window.Width + (2 * zoomScale));
                window.Height = System.Math.Min(1.0, window.Height + (2 * zoomScale));

                if (window.Width < 1) window.X = System.Math.Max(0.0, window.X - zoomScale);
                if (window.Height < 1) window.Y = System.Math.Max(0.0, window.Y - zoomScale);
            }
            else if (tag.Equals("NavZoomFit"))
            {
                window = new Rect(0, 0, 1, 1);
            }
            #endregion
            #region Pan Operations
            else if (tag.Equals("NavPanUp"))
            {
                window.Y = System.Math.Max(0.0, window.Y - panScale);
            }
            else if (tag.Equals("NavPanDown"))
            {
                window.Y = System.Math.Min(1.0 - window.Height, window.Y + panScale);
            }
            else if (tag.Equals("NavPanLeft"))
            {
                window.X = System.Math.Max(0.0, window.X - panScale);
            }
            else if (tag.Equals("NavPanRight"))
            {
                window.X = System.Math.Min(1.0 - window.Width, window.X + panScale);
            }
            #endregion
            this.GeoMap.WindowRect = window;
        }

    }
}
