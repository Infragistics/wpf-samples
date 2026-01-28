using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
	public enum PoliticalPartyType
	{
		Republican = 1,
		Democrat = 2
	}

    public class PoliticalParty : ObservableModel
	{
		
		public PoliticalParty()
		{
		}

		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (_name != value)
				{
					_name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private string _imageResourcePath;
		public string ImageResourcePath
		{
			get
			{
				return _imageResourcePath;
			}
			set
			{
				if (_imageResourcePath != value)
				{
					_imageResourcePath = value;
					this.OnPropertyChanged("ImageResourcePath");
				}
			}
		}

		private IEnumerable<President> _presidents;
		public IEnumerable<President> Presidents
		{
			get
			{
				return _presidents;
			}
			set
			{
				if (_presidents != value)
				{
					_presidents = value;
					this.OnPropertyChanged("Presidents");
				}
			}
		}
	}
}
