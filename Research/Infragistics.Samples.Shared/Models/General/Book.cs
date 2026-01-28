using System;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class Book : ObservableModel
	{

		public Book()
		{
		}

		private string _author;
		public string Author
		{
			get
			{
				return _author;
			}
			set
			{
				if (_author != value)
				{
					_author = value;
					this.OnPropertyChanged("Author");
				}
			}
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

		private string _releaseDate;
		public string ReleaseDate
		{
			get
			{
				return _releaseDate;
			}
			set
			{
				if (_releaseDate != value)
				{
					_releaseDate = value;
					this.OnPropertyChanged("ReleaseDate");
				}
			}
		}

		private Decimal _unitPrice;
		public Decimal UnitPrice
		{
			get
			{
				return _unitPrice;
			}
			set
			{
				if (_unitPrice != value)
				{
					_unitPrice = value;
					this.OnPropertyChanged("UnitPrice");
				}
			}
		}

		private string _url;
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				if (_url != value)
				{
					_url = value;
					this.OnPropertyChanged("Url");
				}
			}
		}

		private IEnumerable<Chapter> _chapters;
		public IEnumerable<Chapter> Chapters
		{
			get
			{
				return _chapters;
			}
			set
			{
				if (_chapters != value)
				{
					_chapters = value;
					this.OnPropertyChanged("Chapters");
				}
			}
		}

	}
   
}
