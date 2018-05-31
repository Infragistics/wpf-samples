using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using IGExtensions.Framework.Controls;
using System;
using System.Windows.Media;

namespace IGShowcase.EarthQuake.Views
{
    public sealed partial class DetailsView  : UserControl  
	{
        #region Constructors
        public DetailsView()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "ColapsedDetails", false);
            SetBinding(MyDataContextProperty, new Binding());
            //_isFirstTime = true;
        }
        #endregion Constructors

        #region Public Properties
        /// <summary>
        /// Gets or sets the map tab visibility.
        /// </summary>
        /// <value>The map tab visibility.</value>
        public Visibility MapTabVisibility
        {
            get { return tabMap.Visibility; }
            set { tabMap.Visibility = value; }
        }

        public static readonly DependencyProperty MyDataContextProperty = DependencyProperty.Register(
          "MyDataContext", typeof(object), typeof(DetailsView), new PropertyMetadata(null, OnMyDataContextChanged));

        private static void OnMyDataContextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var dv = (DetailsView)sender;
            dv.OnMyDataContextChanged(e);
        }
        #endregion

        #region Private Methods
        private void OnMyDataContextChanged(DependencyPropertyChangedEventArgs e)
        {
            // Show animation
            if (e.NewValue != null )
            {
                if(e.OldValue!=null)
                {
                    VisualStateManager.GoToState(this, "ColapsedDetails", false);
                }
                VisualStateManager.GoToState(this, "VisibleDetails", true);
            }
            // Hide animation
            else
            {
                VisualStateManager.GoToState(this, "ColapsedDetails", false);
            }
        }

        private void AlignDetailsView(object sender, SizeChangedEventArgs e)
        {
            //Get the parent element
            var detailsView = VisualTreeHelper.GetParent(this) as UIElement;

            //Get the position of the parent element relative to the root visual
           // GeneralTransform generalTransform = detailsView.TransformToVisual(Application.Current.RootVisual);
            GeneralTransform generalTransform = detailsView.TransformToVisual(NavigationApp.MainVisual());
            Point detailsViewPosition = generalTransform.Transform(new Point(0, 0));

            //Align the DetailsView
            this.HorizontalAlignment = (detailsViewPosition.X > 0)
                                           ? HorizontalAlignment.Left
                                           : HorizontalAlignment.Right;
        }

		
        #endregion

        #region Private Properties
       
        
        #endregion

        public event EventHandler DialogClosed;
        public void OnDialogClosed()
        {
            if (DialogClosed != null)
            {
                DialogClosed(this, EventArgs.Empty);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "ColapsedDetails", false);
            OnDialogClosed();
        }
    }
}
