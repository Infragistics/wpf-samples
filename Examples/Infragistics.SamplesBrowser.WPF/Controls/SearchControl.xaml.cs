using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Infragistics.SamplesBrowser.ViewModel;
using ResStrings = Infragistics.SamplesBrowser.Properties.Resources;

namespace Infragistics.SamplesBrowser.Controls
{
    /// <summary>
    /// This control is shown above the navigation area in the
    /// main Window.  It allows the user to enter a search string
    /// and will host the resulting list of samples in an 
    /// instance of the SearchResults control.
    /// </summary>
    public partial class SearchControl : UserControl
    {
        #region Data

        private UIElementAdorner<SearchResultsControl> _searchResultsAdorner;
        private TableOfContentsViewModel _toc;

        #endregion // Data

        #region Constructor

        public SearchControl()
        {
            InitializeComponent();

            this.Loaded += this.OnLoaded;
            this.DataContextChanged += this.OnDataContextChanged;
            this.IsVisibleChanged += this.OnIsVisibleChanged;
        }

        #endregion // Constructor

        #region Event Handling Methods

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _toc = e.NewValue as TableOfContentsViewModel;
            if (_toc != null)
                _toc.PropertyChanged += this.OnTocPropertyChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_searchResultsAdorner == null)
            {
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(this);
                if (layer == null)
                    return;

                _searchResultsAdorner = new UIElementAdorner<SearchResultsControl>(this);
                _searchResultsAdorner.Child = new SearchResultsControl(this);

                layer.Add(_searchResultsAdorner);

                _searchResultsAdorner.SetOffsets(8.5, base.ActualHeight);

                BindingOperations.SetBinding(
                    _searchResultsAdorner,
                    FrameworkElement.WidthProperty,
                    new Binding("ActualWidth") { Source = this.SearchBoxBackground });
            }
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_searchResultsAdorner == null)
                return;

            if (this.IsVisible)
                _searchResultsAdorner.ClearValue(UIElement.VisibilityProperty);
            else
                _searchResultsAdorner.SetValue(UIElement.VisibilityProperty, Visibility.Collapsed);
        }

        private void OnTocPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasSearchResults" && !_toc.HasSearchResults && _toc.HasSearchText)
            {
                Storyboard sb = this.TryFindResource("NoSearchResult") as Storyboard;
                if (sb != null)
                    sb.Begin(this);
            }
            else if (e.PropertyName == "HasSearchText")
            {
                this.CoerceValue(IsSearchAreaActiveProperty);
            }
        }

        #endregion // Event Handling Methods

        #region IsSearchAreaActive

        private static readonly DependencyPropertyKey IsSearchAreaActivePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsSearchAreaActive",
                typeof(bool),
                typeof(SearchControl),
                new UIPropertyMetadata(false, null, OnCoerceIsSearchAreaActive));

        public static readonly DependencyProperty IsSearchAreaActiveProperty =
            IsSearchAreaActivePropertyKey.DependencyProperty;

        public bool IsSearchAreaActive
        {
            get { return (bool)GetValue(IsSearchAreaActiveProperty); }
            private set { SetValue(IsSearchAreaActivePropertyKey, value); }
        }

        private static object OnCoerceIsSearchAreaActive(DependencyObject depObj, object value)
        {
            SearchControl searchControl = depObj as SearchControl;
            return searchControl != null && (searchControl._toc.HasSearchText || searchControl.IsKeyboardFocusWithin);
        }

        #endregion // IsSearchAreaActive

        #region ClearSearch

        /// <summary>
        /// Invoked when the user wants to clear out the search area
        /// by pressing the Escape (and sometimes Enter) key.
        /// </summary>
        internal void ClearSearch()
        {
            if (_toc != null && _toc.HasSearchResults)
            {
                _toc.ClearSearchCommand.Execute(null);
                this.searchTextBox.Focus();
            }
        }

        #endregion // ClearSearch

        #region Base Class Overrides

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (_searchResultsAdorner.Child.Visibility != Visibility.Visible)
                return;

            if (e.Key == Key.Down)
            {
                base.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    (Action)delegate { _searchResultsAdorner.Child.ActivateFirstItem(); });
            }
            else if (e.Key == Key.Tab && this.clearSearchButton.IsKeyboardFocusWithin)
            {
                // Tabbing away from the Clear Search button should give focus to the search results.
                // Since the search results live in the adorner layer, we must handle this manually.
                e.Handled = true;
                _searchResultsAdorner.Child.ActivateSelectedItem();
            }
            else if (e.Key == Key.Escape)
            {
                this.ClearSearch();
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);

            this.CoerceValue(IsSearchAreaActiveProperty);
        }

        #endregion // Base Class Overrides
    }
}
