using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using IGExtensions.Framework.Controls;

namespace IGShowcase.HospitalFloorPlan.Views
{
	public partial class DetailsView : UserControl
	{
		#region Private Variables
		private bool _isShown;
		#endregion Private Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DetailsView"/> class.
		/// </summary>
		public DetailsView()
		{
			InitializeComponent();
            if (!NavigationApp.Current.IsInDesingMode())
            {
                //VisualStateManager.GoToState(this, "Visible", true);
             }
            VisualStateManager.GoToState(this, "Collapsed", false);
        }
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Shows this instance.
		/// </summary>
		public void Show()
		{
			if (_isShown)
			{
				VisualStateManager.GoToState(this, "Collapsed", false);
			}

            //var storyboard = this.Resources["sbHeartRate"] as Storyboard;
		    //if (storyboard != null) storyboard.Begin();

		    VisualStateManager.GoToState(this, "Visible", true);
			_isShown = true;
		}

		/// <summary>
		/// Hides this instance.
		/// </summary>
		public void Hide()
		{
            VisualStateManager.GoToState(this, "Collapsed", true);
			_isShown = false;
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Closes the button click.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void CloseButtonClick(object sender, System.Windows.RoutedEventArgs e)
		{
			Hide();
		}
		#endregion Private Methods
	}
}
