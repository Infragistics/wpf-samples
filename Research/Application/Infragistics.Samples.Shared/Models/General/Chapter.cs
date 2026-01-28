
namespace Infragistics.Samples.Shared.Models
{
    public class Chapter : ObservableModel
	{

		public Chapter()
		{
		}
		
		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (_title != value)
				{
					_title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}
	}

}
