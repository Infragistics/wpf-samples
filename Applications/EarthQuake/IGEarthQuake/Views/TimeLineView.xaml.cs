using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using IGExtensions.Framework.Extensions;
using IGShowcase.EarthQuake.ViewModels;

namespace IGShowcase.EarthQuake.Views
{
    public partial class TimeLineView : IGExtensions.Framework.Controls.NavigationPage
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeLineView"/> class.
		/// </summary>
		public TimeLineView()
		{
			InitializeComponent();
            // Attached properties to storyboards
            _rightPanelExpanded = (Storyboard)this.Resources["RightPanelExpanded"];
            _rightPanelCollapsed = (Storyboard)this.Resources["RightPanelCollapsed"];
            Storyboard.SetTargetProperty(_rightPanelExpanded, new PropertyPath(GridExtension.ColumnWidthProperty));
            Storyboard.SetTargetProperty(_rightPanelCollapsed, new PropertyPath(GridExtension.ColumnWidthProperty));
        }
		#endregion Constructors
        private Storyboard _rightPanelExpanded;
        private Storyboard _rightPanelCollapsed;
      
		#region Protected Methods
        protected override void OnPageNavigated(NavigationEventArgs e)
		{
			if (timeLineViewModel == null)
			{
				timeLineViewModel = (TimeLineViewModel)DataContext;
                // update thumbs of the magnitude range slider with MinMagnitude and MaxMagnitude values
                //(this.magnitudeRange.Thumbs[0] as XamSliderNumericThumb).Value = timeLineViewModel.MinMagnitude;
                //(this.magnitudeRange.Thumbs[1] as XamSliderNumericThumb).Value = timeLineViewModel.MaxMagnitude;
                
			}
			timeLineViewModel.Shake += OnShake;
		}
		#endregion Protected Methods

		#region Private Event Handlers
        private bool _isExpandedRightPanel = true;
        private void RightPanelToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isExpandedRightPanel)
                _rightPanelCollapsed.Begin();
            else
                _rightPanelExpanded.Begin();

            _isExpandedRightPanel = !_isExpandedRightPanel;
        }

		/// <summary>
		/// Called when [time line view model property changed].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void OnShake(object sender, EventArgs e)
		{
            var sb = (this.Resources["StoryGroundShake"] as Storyboard);
            if (sb != null) sb.Begin(); 
		}
		/// <summary>
		/// Handles the MouseLeftButtonDown event of the StackPanel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var earthQuake = ((Panel)(sender)).DataContext as EarthQuakeViewModel;
            if (earthQuake != null)
            {
                timeLineViewModel.SelectedTime = earthQuake.Updated;
            }            
        }

		#endregion Private Event Handlers

        private void DetailsView_DialogClosed(object sender, EventArgs e)
        {
            // deselect selected earthquake on closing details dialog about the current earthquake
            timeLineViewModel.SelectedTime = DateTime.MinValue;
            //timeLineViewModel.SelectedTime = null;
        }

        
	}
}
