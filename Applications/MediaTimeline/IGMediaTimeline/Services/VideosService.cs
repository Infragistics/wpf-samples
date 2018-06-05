using System.Text;
using MediaTimeline.Models;

namespace MediaTimeline.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Diagnostics;

    public sealed class VideosService  
    {
		#region Private Member Variables
        // TODO: Switch over to YouTube API version 3 as current API in use is deprecated.
        private const string YOUTUBE_RSS_URL = "http://gdata.youtube.com/feeds/base/videos?max-results={1}&start-index={2}&alt=rss&q={0}&orderby={3}&format=5";
        private RssReader _reader;
		#endregion Private Member Variables

		#region Public Methods
		/// <summary>
        /// Gets the video clips.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="resultsCount">The results count.</param>
        /// <returns></returns>
        public IEnumerable<VideoClip> GetVideoClips(string query, string orderBy, int resultsCount)
        {
            var result = new List<VideoClip>();

            // Bridge
            // For instance you can combine ITune Videos and YouTube
            _reader = new RssReaderYouTube();
            //else _reader = new RssReaderITunes();

            int maxrResults = resultsCount == 25 ? 25 : 50;

            for (int i = 1; i <= resultsCount; i = i+50)
            {
                var data = string.Empty;
                lock (this)
                {
                    try
                    {
                        using (var client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;
                            data = client.DownloadString(String.Format(YOUTUBE_RSS_URL, Uri.EscapeUriString(query), maxrResults, i, orderBy));
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Assert(false, ex.Message);
                    }
                }
                
                if (!String.IsNullOrEmpty(data))
                {
                    IEnumerable<VideoClip> clips = _reader.ProcessVideoClips(data);
                    result.AddRange(clips);
                }
            }

            return result;
        }
		#endregion Public Methods
	}
}


