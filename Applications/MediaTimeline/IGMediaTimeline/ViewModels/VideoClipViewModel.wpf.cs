using System;
using System.ComponentModel;
using MediaTimeline.Models;

namespace MediaTimeline.ViewModels
{
    public class VideoClipViewModel : INotifyPropertyChanged
    {
        #region Private Member Variables
        private readonly VideoClip _data;
        private int _imageSize;
        #endregion Private Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoClipViewModel"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public VideoClipViewModel(VideoClip data)
        {
            _data = data;
        }
        #endregion Constructors

        #region Public Properties
        /// <summary>
        /// Gets the details. 
        /// </summary>
        /// <value>The details.</value>
        public VideoClipViewModel Details { get { return this; } }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get { return _data.Id; } }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get { return _data.Title; } }

        /// <summary>
        /// Gets the release date.
        /// </summary>
        /// <value>The release date.</value>
        public DateTime ReleaseDate { get { return _data.ReleaseDate; } }

        /// <summary>
        /// Gets the video link.
        /// </summary>
        /// <value>The video link.</value>
        public Uri VideoLink { get { return _data.VideoLink; } }

        /// <summary>
        /// Gets the picture link.
        /// </summary>
        /// <value>The picture link.</value>
        public Uri PictureLink { get { return _data.PictureLink; } }

        /// <summary>
        /// Gets the HTML code.
        /// </summary>
        /// <value>The HTML code.</value>
        public string HtmlCode { get { return _data.Description; } }

        /// <summary>
        /// Gets or sets the size of the image.
        /// </summary>
        /// <value>The size of the image.</value>
        public int ImageSize
        {
            get { return _imageSize; }
            set
            {
                if (_imageSize == value) return;

                _imageSize = value;
                OnPropertyChanged("ImageSize");
            }
        }

        /// <summary>
        /// Gets the views count.
        /// </summary>
        /// <value>The views count.</value>
        public int ViewsCount { get { return _data.ViewsCount; } }
        #endregion Public Properties

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
