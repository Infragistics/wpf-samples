using System;
using System.Globalization;
using System.ServiceModel.Syndication;
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
