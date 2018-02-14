//-------------------------------------------------------------------------
// <copyright file="RssReader.cs" company="Infragistics">
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


using System.Xml;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MediaTimeline.Models;
using System.ServiceModel.Syndication;

namespace MediaTimeline.Services
{
    /// <summary>
    /// Base class for all the readers
    /// </summary>
    internal abstract class RssReader
    {
        /// <summary>
        /// Processes the video clips.
        /// </summary>
        /// <param name="stringFeed">The string feed.</param>
        /// <returns></returns>
        internal IEnumerable<VideoClip> ProcessVideoClips(string stringFeed)
        {
            IEnumerable<VideoClip> result = null;
            using (XmlReader reader = XmlReader.Create(new StringReader(stringFeed)))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                if (feed != null)
                {
                    result = feed.Items.Select(x => ExtractVideoClip(x));
                }
            }
            return result;
        }

		/// <summary>
		/// Extracts the video clip.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
        protected virtual VideoClip ExtractVideoClip(SyndicationItem item)
        {
            //TODO: Logging
            return null;
        }
    }
}
