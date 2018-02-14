//-------------------------------------------------------------------------
// <copyright file="RssReaderYouTube.cs" company="Infragistics">
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
using MediaTimeline.Models;
using System.ServiceModel.Syndication;

namespace MediaTimeline.Services
{
    /// <summary>
    /// 
    /// </summary>
    internal class RssReaderYouTube : RssReader
    {
        #region Constants
        private const string VIDEO = @"http://www.youtube.com/v/{0}?rel=0&fs=0&autoplay=1&egm=0";
        private const string PICTURE = @"http://i.ytimg.com/vi/{0}/2.jpg";
        private const string SITE = @"http://www.youtube.com/watch?v={0}";
        #endregion Constants

		#region Protected Methods
		/// <summary>
        /// Extracts the video clip.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override VideoClip ExtractVideoClip(SyndicationItem item)
        {
            string id = GetId(item.Id);
            string title = item.Title.Text;
            string description = item.Summary.Text;
            Uri videoLink = new Uri(String.Format(VIDEO, id));
            Uri pictureLink = new Uri(String.Format(PICTURE, id));
            Uri siteLink = new Uri(String.Format(SITE,id));
            int viewsCount = GetViewsCount(description);
            DateTime releaseDate = item.PublishDate.DateTime;
            DateTime updated = item.LastUpdatedTime.DateTime;

            return new VideoClip(id, title, description, videoLink, pictureLink, siteLink, viewsCount, releaseDate, updated);
        }
		#endregion Protected Methods

		#region Private Methods
		/// <summary>
        /// Gets the views count. Search in html content for particular words in order to extract views count.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        private static int GetViewsCount(string description)
        {
            int startIndex = description.IndexOf("Views:") + 6;
            startIndex = description.IndexOf("\n", startIndex) + 1;
            int endIndex = description.IndexOf('<', startIndex);
            int result;
            Int32.TryParse(description.Substring(startIndex, endIndex - startIndex), out result);

            return result;
        }

        /// <summary>
        /// Gets the id of the video.
        /// </summary>
        /// <param name="videoId">The video id.</param>
        /// <returns></returns>
        private static string GetId(string videoId)
        {
            string restlt = string.Empty;
            if (videoId.Contains("video"))
            {
                int startIndex = videoId.LastIndexOf('/');
                restlt = videoId.Substring(startIndex + 1, videoId.Length - startIndex - 1);
            }
            return restlt;
        }
        #endregion Private Methods
    }
}
