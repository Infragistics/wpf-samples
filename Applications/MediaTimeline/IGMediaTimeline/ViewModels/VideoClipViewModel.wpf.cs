//-------------------------------------------------------------------------
// <copyright file="VideoClipViewModel.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------


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
