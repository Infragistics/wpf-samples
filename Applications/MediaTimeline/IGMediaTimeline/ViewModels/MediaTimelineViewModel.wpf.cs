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
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MediaTimeline.Services;
using MediaTimeline.Models;

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

        private readonly BackgroundWorker _worker;
        private readonly YoutubeService _youtubeService;

        private string _serachText = "Infragistics";
        private string _orderBy = string.Empty;
        private bool _isBusy;
        private DateTime _maxTime;
        private DateTime _minTime;
        private int _maxResults;
        private int _minViewsCountYouTubeCollection;
        private int _maxViewsCountYouTubeCollection;
        #endregion Private Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTimelineViewModel"/> class.
        /// </summary>
        public MediaTimelineViewModel()
        {
            _youtubeService = new YoutubeService();
            _worker = new BackgroundWorker();
            _worker.RunWorkerCompleted += DataLoaded;
            _worker.DoWork += DoWork;
            _youTubeEvents = new ObservableCollection<VideoClipViewModel>();

            MaxResults = 25;
            OrderBy = "relevance";
            _isBusy = true;

            // By default load 25 videos by not specified query, ordered by relevance
            GetYouTubeVideos();
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
                if (_isBusy == value) return;

                _isBusy = value;
                OnPropertyChanged("IsBusy");
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
                if (_serachText == value) return;

                _serachText = value;
                OnPropertyChanged("SearchText");
            }
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
                if (_maxResults == value) return;

                _maxResults = value;
                OnPropertyChanged("MaxResults");
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
                if (_orderBy == value) return;

                _orderBy = value;
                OnPropertyChanged("OrderBy");
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
                return _youTubeEvents != null && _youTubeEvents.Count() > 0 ? _youTubeEvents.Min(x => x.ReleaseDate) : DateTime.Today;
            }
            set
            {
                if (_minTime == value) return;

                _minTime = value;
                OnPropertyChanged("MinTime");
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
                if (_maxTime == value) return;

                _maxTime = value;
                OnPropertyChanged("MaxTime");
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
                if (value == _youTubeEvents) return;

                _youTubeEvents = value;
                OnPropertyChanged("YouTubeEvents");
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

            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = _youtubeService.GetVideoClips(SearchText, OrderBy, MaxResults);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }


        /// <summary>
        /// Datas the loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void DataLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                IEnumerable<VideoClip> clips = e.Result as IEnumerable<VideoClip>;
                if (clips != null && clips.Count() > 0)
                {
                    _minViewsCountYouTubeCollection = clips.Min(x => x.ViewsCount);
                    _maxViewsCountYouTubeCollection = clips.Max(x => x.ViewsCount);

                    YouTubeEvents = clips.Select(x =>
                    {
                        VideoClipViewModel videoClipViewModel = new VideoClipViewModel(x);
                        videoClipViewModel.ImageSize = GetPictureSize(videoClipViewModel.ViewsCount);
                        return videoClipViewModel;
                    }).ToArray();
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(e.Error.Message);
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
