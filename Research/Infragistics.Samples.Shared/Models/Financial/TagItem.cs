using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class TagItem : ObservableModel
	{

		public TagItem()
		{
		}

		private string _id;
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				if (_id != value)
				{
					_id = value;
					this.OnPropertyChanged("Id");
				}
			}
		}
		
		private string _content;
		public string Content
		{
			get
			{
				return _content;
			}
			set
			{
				if (_content != value)
				{
					_content = value;
					this.OnPropertyChanged("Content");
				}
			}
		}
		
		private int _weight;
		public int Weight
		{
			get
			{
				return _weight;
			}
			set
			{
				if (_weight != value)
				{
					_weight = value;
					this.OnPropertyChanged("Weight");
				}
			}
		}

		private string _navigateUri;
		public string NavigateUri
		{
			get
			{
				return _navigateUri;
			}
			set
			{
				if (_navigateUri != value)
				{
					_navigateUri = value;
					this.OnPropertyChanged("NavigateUri");
				}
			}
		}

		public bool HasChildren
		{
			get
			{
				bool hasChildren = false;

				if (_tags != null && _tags.Count > 0)
				{
					hasChildren = true;
				}

				return hasChildren;
			}
		}
				
		private IList<TagItem> _tags;
		public IList<TagItem> Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				if (_tags != value)
				{
					_tags = value;
					this.OnPropertyChanged("Tags");
				}
			}
		}
		
	}
}
