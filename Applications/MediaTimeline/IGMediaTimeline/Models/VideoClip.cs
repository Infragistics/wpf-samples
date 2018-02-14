//-------------------------------------------------------------------------
// <copyright file="VideoClip.cs" company="Infragistics">
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

namespace MediaTimeline.Models
{
    /// <summary>
    /// Video clip model. Contains all the properes retreaved from YouTube that we need.
    /// </summary>
    public class VideoClip
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoClip"/> class.
        /// </summary>
        public VideoClip(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoClip"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="videoLink">The video link.</param>
        /// <param name="pictureLink">The picture link.</param>
        /// <param name="siteLink">The site link.</param>
        /// <param name="viewsCount">The views count.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="updated">The updated.</param>
        public VideoClip(string id, string title, string description, Uri videoLink, Uri pictureLink, Uri siteLink, int viewsCount, DateTime releaseDate, DateTime updated)
        {
            Id = id;
            Title = title;
            Description = description;
            VideoLink = videoLink;
            PictureLink = pictureLink;
            SiteLink = siteLink;
            ViewsCount = viewsCount;
            ReleaseDate = releaseDate;
            Updated = updated;
        }
        #endregion Constructors

        #region Public properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the response id.
        /// </summary>
        /// <value>The response id.</value>
        public string ResponseId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description. Holds information structured in HTML tags.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the link to the video.
        /// </summary>
        /// <value>The video link.</value>
        public Uri VideoLink { get; set; }

        /// <summary>
        /// Gets or sets the link to the picture.
        /// </summary>
        /// <value>The picture link.</value>
        public Uri PictureLink { get; set; }

        /// <summary>
        /// Gets or sets the site link YouTube.
        /// </summary>
        /// <value>The site link.</value>
        public Uri SiteLink { get; set; }

        /// <summary>
        /// Gets or sets the views count.
        /// </summary>
        /// <value>The views count.</value>
        public int ViewsCount { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; set; }

        #endregion Public properties
    }
}
