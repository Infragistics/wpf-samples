using System.ComponentModel;

namespace IGShowcase.EarthQuake
{
	public interface IPropertyChangedListener
	{
		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
	}
}