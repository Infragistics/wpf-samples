//-------------------------------------------------------------------------
// <copyright file="MediaTimelineViewModel.cs" company="Infragistics">
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using MediaTimeline.Web.DomainServices;
using MediaTimeline.Web.Models;

namespace MediaTimeline.ViewModels
{
	public class MediaTimelineViewModel : INotifyPropertyChanged
	{
        #region Constants
        
        private const int MIN_PICTURE_SIZE = 50;
        private const int MAX_PICTURE_SIZE = 200;
        #endregion Constants

        #region Private Member Variables
        private IEnumerable<VideoClipViewModel> _youTubeEvents;

        private readonly ProxyDomainContext _context;
        private string _serachText = "Infragistics";
        private string _orderBy = string.Empty;
        private bool _isBusy;
        private DateTime _maxTime;
        private DateTime _minTime;
        private int _maxResults;
        //private int _pageStartIndex;
        private int _minViewsCountYouTubeCollection;
        private int _maxViewsCountYouTubeCollection;
        #endregion Private Member Variables

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaTimelineViewModel"/> class.
		/// </summary>
        public MediaTimelineViewModel()
        {
            _context = new ProxyDomainContext();
            _youTubeEvents = new ObservableCollection<VideoClipViewModel>();

            MaxResults = 25;
            OrderBy = "relevance";
            _isBusy = true;

			try
			{
				// By default load 25 videos by not specified query, ordered by relevance
				//_context.Load(_context.GetVideoClipsQuery(SearchText, OrderBy, MaxResults) (SearchText, 25, OrderBy)), DataLoaded, null);
			    GetYouTubeVideos();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}

        }
        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get { return _serachText; }
            set
            {
                if (_serachText != value)
                {
                    _serachText = value;
                    OnPropertyChanged("SearchText");
                   
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is text completion enabled.
        /// Enable completion only if language is English.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is text completion enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsTextCompletionEnabled
        {
            get { return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en" ? true : false; }
        }

        /// <summary>
        /// Gets or sets the max results.
        /// </summary>
        /// <value>The max results.</value>
        public int MaxResults 
        {
            get { return _maxResults; }
            set
            {
                if (_maxResults != value)
                {
                    _maxResults = value;
                    OnPropertyChanged("MaxResults");
                }
            }
        }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>The order by.</value>
        public string OrderBy
        {
            get { return _orderBy; }
            set
            {
                if (_orderBy != value)
                {
                    _orderBy = value;
                    OnPropertyChanged("OrderBy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the min time.
        /// </summary>
        /// <value>The min time.</value>
        public DateTime MinTime
        {
            get
            {
                if (_youTubeEvents != null && _youTubeEvents.Count() > 0)
                {
                    return _youTubeEvents.Min(x => x.ReleaseDate);
                }
                else
                {
                    return DateTime.Today;
                }
            }
            set
            {
                if (_minTime != value)
                {
                    _minTime = value;
                    OnPropertyChanged("MinTime");
                }
            }
        }

        /// <summary>
        /// Gets or sets the max time.
        /// </summary>
        /// <value>The max time.</value>
        public DateTime MaxTime
        {
            get { return _maxTime; }
            set
            {
                if (_maxTime != value)
                {
                    _maxTime = value;
                    OnPropertyChanged("MaxTime");
                }
            }
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        public IEnumerable<VideoClipViewModel> YouTubeEvents
        {
            get { return _youTubeEvents; }
            set
            {
                if (value != _youTubeEvents)
                {
                    _youTubeEvents = value;
                    OnPropertyChanged("YouTubeEvents");
                }
            }
        }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Searches this instance.
        /// </summary>
        public void GetYouTubeVideos()
        {
            IsBusy = true;
            YouTubeEvents = null;
            _context.Load(_context.GetVideoClipsQuery(SearchText, OrderBy, MaxResults), DataLoaded, null);
        }
        #endregion Public Methods

        #region Private Methods
		/// <summary>
		/// Datas the loaded.
		/// </summary>
		/// <param name="op">The op.</param>
        private void DataLoaded(LoadOperation<VideoClip> op)
        {
            if (op.Entities.Count() > 0)
            {
                _minViewsCountYouTubeCollection = op.Entities.Min(x => x.ViewsCount);
                _maxViewsCountYouTubeCollection = op.Entities.Max(x => x.ViewsCount);

                YouTubeEvents = op.Entities.Select(x =>
                                    {
                                        VideoClipViewModel videoClipViewModel = new VideoClipViewModel(x);
                                        videoClipViewModel.ImageSize = GetPictureSize(videoClipViewModel.ViewsCount);
                                        return videoClipViewModel;
                                    }).ToArray();
            }

            IsBusy = false;
        }

		/// <summary>
		/// Gets the size of the picture.
		/// </summary>
		/// <param name="viewsCount">The views count.</param>
		/// <returns></returns>
        private int GetPictureSize(int viewsCount)
        {
            int result = 160;
            if (viewsCount >= _minViewsCountYouTubeCollection &&
                viewsCount <= _maxViewsCountYouTubeCollection)
            {
                double percent = (double)(viewsCount - _minViewsCountYouTubeCollection) / (_maxViewsCountYouTubeCollection - _minViewsCountYouTubeCollection);

                result = (int)(percent * MAX_PICTURE_SIZE - percent * MIN_PICTURE_SIZE + MIN_PICTURE_SIZE);
            }

            return result;
        }
        #endregion Private Methods

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
