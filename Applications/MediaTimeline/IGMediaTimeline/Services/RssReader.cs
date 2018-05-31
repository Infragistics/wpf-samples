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
