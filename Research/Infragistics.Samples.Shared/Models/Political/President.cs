using System.Windows;

namespace Infragistics.Samples.Shared.Models
{
    public class President : ObservableModel
	{

		public President()
		{
		}
		
		private PoliticalPartyType _partyType;
		public PoliticalPartyType PartyType
		{
			get
			{
				return _partyType;
			}
			set
			{
				if (_partyType != value)
				{
					_partyType = value;
					this.OnPropertyChanged("PartyType");
				}
			}
		}

		public Visibility  UseRepuplicanStyle
		{
			get
			{
				Visibility visibility = Visibility.Collapsed;
				
				if (_partyType == PoliticalPartyType.Republican)
				{
					visibility = Visibility.Visible;
				}

				return visibility;
			}
		}

		public Visibility UseDemocratStyle
		{
			get
			{
				Visibility visibility = Visibility.Collapsed;

				if (_partyType == PoliticalPartyType.Democrat)
				{
					visibility = Visibility.Visible;
				}

				return visibility;
			}
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

		private string _years;
		public string Years
		{
			get
			{
				return _years;
			}
			set
			{
				if (_years != value)
				{
					_years = value;
					this.OnPropertyChanged("Years");
				}
			}
		}

		private string _spouse;
		public string Spouse
		{
			get
			{
				return _spouse;
			}
			set
			{
				if (_spouse != value)
				{
					_spouse = value;
					this.OnPropertyChanged("Spouse");
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
	}
}
