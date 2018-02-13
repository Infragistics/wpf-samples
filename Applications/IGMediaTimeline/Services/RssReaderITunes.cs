//-------------------------------------------------------------------------
// <copyright file="RssReaderITunes.cs" company="Infragistics">
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using MediaTimeline.Models;

namespace MediaTimeline.Services
{
    internal class RssReaderITunes : RssReader
    {
        #region Constants
        private const string RSS = "http://itunes.apple.com/rss";
        private const string ITUNES_LOGO_URL = "http://images.apple.com/itunes/images/product_title20090909.png";
        #endregion Constants

        #region Protected Methods
        /// <summary>
        /// Extracts the video clip.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override VideoClip ExtractVideoClip(SyndicationItem item)
        {
            string id = item.Id;
            string title = item.Title.Text;
            string description = ((TextSyndicationContent)item.Content).Text;
            DateTime updated = item.LastUpdatedTime.UtcDateTime;
            Uri videoLink = null;
            Uri itunesLink = null;

            if (item.Links != null && item.Links.Count > 0)
            {
                itunesLink = item.Links[0].GetAbsoluteUri();
                videoLink = item.Links[1].GetAbsoluteUri();
            }
            // Set the release date to Now in case there is no ReleaseDate.
            DateTime releaseDate = DateTime.Now;
            // Set the defailt iTunes logo in case there is no picture.
            Uri pictureLink = new Uri(ITUNES_LOGO_URL); ;

            // Data for image and release data is stored in element extension.
            foreach (var ext in item.ElementExtensions)
            {
                if (ext.OuterNamespace == RSS)
                {
                    switch (ext.OuterName)
                    {
                        case "releaseDate":
                            string str = ext.GetObject<string>();
                            releaseDate = Convert.ToDateTime(str, CultureInfo.InvariantCulture);
                            break;
                        case "image":
                            pictureLink = new Uri(ext.GetObject<string>());
                            break;
                        default:
                            break;
                    }
                }
            }

            return new VideoClip(id, title, description, videoLink, pictureLink, itunesLink, 0, releaseDate, updated);
        }
        #endregion Protected Methods
    }
}
