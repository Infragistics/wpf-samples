using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Infragistics.SamplesBrowser.ViewModel;

namespace Infragistics.SamplesBrowser.Controls
{
    /// <summary>
    /// This control displays a list of samples that match the
    /// search string typed into the SearchControl.  This is
    /// hosted in the adorner layer, and has built-in support
    /// for showing and hiding itself when appropriate.
    /// </summary>
    public partial class SearchResultsControl : UserControl
    {
        #region Data

        double _previousHeight;
        TableOfContentsViewModel _toc;

        #endregion // Data

        #region Constructors

        public SearchResultsControl()
        {
            InitializeComponent();

            this.DataContextChanged += this.OnDataContextChanged;
            this.sampleSelector.SelectionChanged += this.OnSampleSelectorSelectionChanged;
        }

        public SearchResultsControl(SearchControl owner)
            : this()
        {
            this.Owner = owner;
        }

        #endregion // Constructors

        #region Routed Events

        public static readonly RoutedEvent HideSearchResultsEvent =
            EventManager.RegisterRoutedEvent(
            "HideSearchResults",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(SearchResultsControl));

        public event RoutedEventHandler HideSearchResults
        {
            add { base.AddHandler(HideSearchResultsEvent, value); }
            remove { base.RemoveHandler(HideSearchResultsEvent, value); }
        }

        public static readonly RoutedEvent ShowSearchResultsEvent =
            EventManager.RegisterRoutedEvent(
            "ShowSearchResults",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(SearchResultsControl));

        public event RoutedEventHandler ShowSearchResults
        {
            add { base.AddHandler(ShowSearchResultsEvent, value); }
            remove { base.RemoveHandler(ShowSearchResultsEvent, value); }
        }

        #endregion // Routed Events

        #region Public Interface

        /// <summary>
        /// Gives focus to and selects the first item in the list of search results.
        /// </summary>
        public void ActivateFirstItem()
        {
            this.ActivateItem(0);
        }

        /// <summary>
        /// Gives focus to the selected item in the list of search results.
        /// </summary>
        public void ActivateSelectedItem()
        {
            if (this.sampleSelector.SelectedIndex == -1)
                this.ActivateItem(0);
            else
                this.ActivateItem(this.sampleSelector.SelectedIndex);
        }

        /// <summary>
        /// Returns the SearchControl that displays this control.
        /// </summary>
        public SearchControl Owner { get; private set; }

        #endregion // Public Interface

        #region Event Handling Methods

        void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _toc = e.NewValue as TableOfContentsViewModel;
            if (_toc != null)
                _toc.PropertyChanged += this.OnTocPropertyChanged;
        }

        void OnTocPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasSearchResults")
                this.ProcessHasSearchResultsChanged();
        }

        #endregion // Event Handling Methods

        #region Base Class Overrides

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((e.Key == Key.Escape || e.Key == Key.Enter) && this.Owner != null)
            {
                this.Owner.ClearSearch();
            }
            else if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }

            base.OnPreviewKeyDown(e);
        }

        #endregion // Base Class Overrides

        #region Private Helpers

        void ActivateItem(int itemIndex)
        {
            if (this.sampleSelector.Items.Count == 0 ||
                this.sampleSelector.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated ||
                itemIndex < 0 ||
                this.sampleSelector.Items.Count <= itemIndex)
                return;

            ListBoxItem item = this.sampleSelector.ItemContainerGenerator.ContainerFromIndex(itemIndex) as ListBoxItem;
            if (item == null)
                return;

            item.Focus();
            (item.DataContext as TocItemViewModel).IsSelected = true;
        }

        void AnimateResultsPane()
        {
            // Force the control's metrics to update so we can have valid source/destination values.
            base.InvalidateMeasure();
            base.UpdateLayout();

            // Find the animations that change the Height of this control.
            DoubleAnimation showAnim = base.TryFindResource("Animation_ShowSearchResults_Height") as DoubleAnimation;
            DoubleAnimation hideAnim = base.TryFindResource("Animation_HideSearchResults_Height") as DoubleAnimation;

            RoutedEvent eventToRaise;
            if (_toc.HasSearchResults)
            {
                eventToRaise = ShowSearchResultsEvent;

                if (showAnim != null)
                {
                    showAnim.From = _previousHeight;
                    showAnim.To = base.DesiredSize.Height;
                }
            }
            else
            {
                eventToRaise = HideSearchResultsEvent;

                if (hideAnim != null)
                {
                    hideAnim.From = base.DesiredSize.Height;
                    hideAnim.To = 0.0;
                }
            }

            _previousHeight = base.DesiredSize.Height;

            base.RaiseEvent(new RoutedEventArgs(eventToRaise));
        }

        void ProcessHasSearchResultsChanged()
        {
            if (_toc == null)
            {
                Debug.Fail("ProcessHasSearchResultsChanged called when _toc is null.");
                return;
            }

            // Verify the scroll position, in case the list was scrolled down before.
            if (this.sampleSelector.Items.Count != 0)
                this.sampleSelector.ScrollIntoView(this.sampleSelector.Items[0]);

            if (_toc.HasSearchResults)
            {
                // Sort and group the results by Control name.
                ICollectionView view = CollectionViewSource.GetDefaultView(_toc.SearchResults);

                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Parent.Parent.Name", ListSortDirection.Ascending));

                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("Parent.Parent.Name"));

                this.sampleSelector.ItemsSource = view;
            }
            else
            {
                this.sampleSelector.ItemsSource = null;
            }

            this.AnimateResultsPane();
        }

        #endregion // Private Helpers

        #region Workaround

        void OnSampleSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = this.sampleSelector.SelectedIndex;
            if (idx < 0)
                return;

            ListBoxItem selectedItem = this.sampleSelector.ItemContainerGenerator.ContainerFromIndex(idx) as ListBoxItem;
            if (selectedItem == null)
                return;

            // The following code is a workaround.  When binding to a ListCollectionView that contains GroupDescriptions
            // the initially selected item in the list (if one exists) has its IsSelected property set to true (as a Local value).  
            // Doing so breaks the bindings we depend on to keep the selected item in the ListBox in sync with the TreeView. This
            // workaround checks to see if the item has been given a local value, and if so, clears it out and re-establishes the
            // binding that hooks it up to a ViewModel object.
            bool hasBeenAssignedLocalIsSelectedValue =
                DependencyPropertyHelper.GetValueSource(selectedItem, ListBoxItem.IsSelectedProperty).BaseValueSource == BaseValueSource.Local;

            if (hasBeenAssignedLocalIsSelectedValue)
            {
                selectedItem.ClearValue(ListBoxItem.IsSelectedProperty);
                BindingOperations.SetBinding(selectedItem, ListBoxItem.IsSelectedProperty, new Binding("IsSelected"));
                (selectedItem.DataContext as TocItemViewModel).IsSelected = true;
            }
            // clear search results when a user selects a sample
            //[GE]TFS243122 - Verify the IsKeyboardFocusWithin is true before executing ClearSearchCommand to prevent an exception and not opening the search results panel when a sample is already selected in the samples tree.
            if ((sender as ListBox).IsKeyboardFocusWithin)
            {
                if (_toc != null && _toc.HasSearchResults)
                {
                    _toc.ClearSearchCommand.Execute(null);
                    _toc.SearchText = "";
                }
            }
        }

        #endregion // Workaround
    }
}
