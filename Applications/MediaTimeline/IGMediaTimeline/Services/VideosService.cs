//-------------------------------------------------------------------------
// <copyright file="VideosService.cs" company="Infragistics">
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

using System.Text;
using MediaTimeline.Models;

namespace MediaTimeline.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Diagnostics;

    public  sealed class VideosService  
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


