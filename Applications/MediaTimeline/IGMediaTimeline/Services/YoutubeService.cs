using MediaTimeline.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace MediaTimeline.Services
{
    public sealed class YoutubeService
    {
        #region Constants
        private const string UrlVideoData = "https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults={0}&q={1}&order={2}&key={3}";
        private const string UrlVideoPagingData = "https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults={0}&pageToken={1}&q={2}&order={3}&key={4}";
        private const string UrlStatsData = "https://www.googleapis.com/youtube/v3/videos?part=statistics&id={0}&key={1}";
        private const string InfraYoutubeFile = "/IGShowcase.MediaTimeline;component/Assets/SampleData/YoutubeData.xml";

        private const string VIDEO = @"http://www.youtube.com/v/{0}?rel=0&fs=0&autoplay=1&egm=0";
        private const string PICTURE = @"http://i.ytimg.com/vi/{0}/2.jpg";
        private const string SITE = @"http://www.youtube.com/watch?v={0}";

        private const int MaxResults = 50;
        #endregion

        #region Private Members

        private List<VideoClip> _cache;
        private string _apiKey;

        #endregion

        public YoutubeService()
        {
            _apiKey = GetApiKey();
        }

        public IEnumerable<VideoClip> GetVideoClips(string query, string orderBy, int resultsCount)
        {
            if (_apiKey == null)
                return LoadFromXml(resultsCount);

            var videos = new List<VideoClip>();

            // these are for paging
            int numResultsLoaded = 0;
            int nextLoadSize = MaxResults;

            if (resultsCount < MaxResults)
                nextLoadSize = resultsCount;

            try
            {
                string pageToken = string.Empty;
                while (pageToken != null)
                {
                    if (numResultsLoaded >= resultsCount)
                        break;

                    var client = new WebClient();

                    Uri uri = null;
                    if (numResultsLoaded == 0)
                        uri = new Uri(String.Format(UrlVideoData, nextLoadSize, query, orderBy, _apiKey));
                    else
                        uri = new Uri(String.Format(UrlVideoPagingData, nextLoadSize, pageToken, query, orderBy, _apiKey));

                    var stream = client.OpenRead(uri);

                    var serializer = new DataContractJsonSerializer(typeof(RootVideoObject));
                    var processedData = (RootVideoObject)serializer.ReadObject(stream);

                    pageToken = processedData.nextPageToken;
                    string videoIds = string.Empty;
                    foreach (var item in processedData.items)
                    {
                        var videoClip = new VideoClip();
                        videoClip.Id = item.id.videoId;
                        videoClip.Title = item.snippet.title;
                        videoClip.Description = item.snippet.description;
                        videoClip.VideoLink = new Uri(String.Format(VIDEO, item.id.videoId));
                        videoClip.PictureLink = new Uri(String.Format(PICTURE, item.id.videoId));
                        videoClip.SiteLink = new Uri(String.Format(SITE, item.id.videoId));
                        videoClip.ReleaseDate = DateTime.Parse(item.snippet.publishedAt);
                        videos.Add(videoClip);

                        if (string.IsNullOrEmpty(videoIds))
                            videoIds += item.id.videoId;
                        else
                            videoIds += "," + item.id.videoId;
                    }

                    stream = client.OpenRead(new Uri(String.Format(UrlStatsData, videoIds, _apiKey)));
                    serializer = new DataContractJsonSerializer(typeof(RootStatsObject));
                    var statistics = (RootStatsObject)serializer.ReadObject(stream);
                    for (int i = 0; i < statistics.items.Count; i++)
                    {
                        var viewCount = 0;
                        if (statistics.items[i].statistics.viewCount != null)
                            viewCount = int.Parse(statistics.items[i].statistics.viewCount);

                        var video = videos.Where(x => x.Id == statistics.items[i].id).FirstOrDefault();
                        video.ViewsCount = viewCount;
                    }

                    numResultsLoaded += processedData.items.Count;
                    if (resultsCount - numResultsLoaded < MaxResults)
                        nextLoadSize = resultsCount - numResultsLoaded;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage += Environment.NewLine + ex.InnerException.Message;
                Debug.Assert(false, errorMessage);
            }
            
            return videos;
        }

        private IEnumerable<VideoClip> LoadFromXml(int resultsCount)
        {
            if (_cache == null || _cache.Count == 0)
            {
                _cache = new List<VideoClip>();
                Stream fileStream;
                var uri = new Uri(InfraYoutubeFile, UriKind.Relative);
                System.Windows.Resources.StreamResourceInfo info = System.Windows.Application.GetResourceStream(uri);
                fileStream = info.Stream;

                XmlReader xmlReader = XmlReader.Create(fileStream);
                var xmlSer = new XmlSerializer(typeof(SerializeableVideoClipRoot));
                var root = (SerializeableVideoClipRoot)xmlSer.Deserialize(xmlReader);

                foreach (var clip in root.VideoClips)
                {
                    _cache.Add(new VideoClip()
                    {
                        Id = clip.Id,
                        Title = clip.Title,
                        Description = clip.Description,
                        VideoLink = new Uri(clip.VideoLink),
                        PictureLink = new Uri(clip.PictureLink),
                        SiteLink = new Uri(clip.SiteLink),
                        ReleaseDate = clip.ReleaseDate,
                        ViewsCount = clip.ViewsCount,
                    });
                }
            }
            return _cache.GetRange(0, resultsCount);
        }
        private string GetApiKey()
        {
            // you should provide your own API key for YouTube or your app will run slow with this key
            return "AIzaSyAdpePspM8zMDhslNBaa8kcZU_FCw9JK3g";
            //if (File.Exists("apikey.txt"))
            //    return File.ReadAllText("apikey.txt");
            //
            //return null;
        }

        #region Json Classes for video search

        [DataContract]
        private class PageInfo
        {
            [DataMember(Name = "totalResults")]
            public int totalResults { get; set; }
            [DataMember(Name = "resultsPerPage")]
            public int resultsPerPage { get; set; }
        }

        [DataContract]
        private class VideoId
        {
            [IgnoreDataMember]
            public string kind { get; set; }
            [DataMember(Name = "videoId")]
            public string videoId { get; set; }
        }

        [DataContract]
        private class VideoThumbnail
        {
            [DataMember(Name = "url")]
            public string url { get; set; }
        }

        [DataContract]
        private class VideoThumbnails
        {
            [DataMember(Name = "default")]
            public VideoThumbnail @default { get; set; }
            [DataMember(Name = "medium")]
            public VideoThumbnail medium { get; set; }
            [DataMember(Name = "high")]
            public VideoThumbnail high { get; set; }
        }

        [DataContract]
        private class VideoSnippet
        {
            [DataMember(Name = "publishedAt")]
            public string publishedAt { get; set; }
            [DataMember(Name = "channelId")]
            public string channelId { get; set; }
            [DataMember(Name = "title")]
            public string title { get; set; }
            [DataMember(Name = "description")]
            public string description { get; set; }
            [DataMember(Name = "thumbnails")]
            public VideoThumbnails thumbnails { get; set; }
            [DataMember(Name = "channelTitle")]
            public string channelTitle { get; set; }
            [IgnoreDataMember]
            public string liveBroadcastContent { get; set; }
        }

        [DataContract]
        private class VideoItem
        {
            [IgnoreDataMember]
            public string kind { get; set; }
            [IgnoreDataMember]
            public string etag { get; set; }
            [DataMember(Name = "id")]
            public VideoId id { get; set; }
            [DataMember(Name = "snippet")]
            public VideoSnippet snippet { get; set; }
        }

        [DataContract]
        private class RootVideoObject
        {
            [IgnoreDataMember]
            public string kind { get; set; }
            [IgnoreDataMember]
            public string etag { get; set; }
            [DataMember(Name = "nextPageToken")]
            public string nextPageToken { get; set; }
            [DataMember(Name = "pageInfo")]
            public PageInfo pageInfo { get; set; }
            [DataMember(Name = "items")]
            public List<VideoItem> items { get; set; }
        }

        #endregion

        #region Json classes for statistic search

        [DataContract]
        private class Statistics
        {
            [DataMember(Name = "viewCount")]
            public string viewCount { get; set; }
            [DataMember(Name = "likeCount")]
            public string likeCount { get; set; }
            [DataMember(Name = "dislikeCount")]
            public string dislikeCount { get; set; }
            [DataMember(Name = "favoriteCount")]
            public string favoriteCount { get; set; }
            [DataMember(Name = "commentCount")]
            public string commentCount { get; set; }
        }

        [DataContract]
        private class StatsItem
        {
            [IgnoreDataMember]
            public string kind { get; set; }
            [IgnoreDataMember]
            public string etag { get; set; }
            [DataMember(Name = "id")]
            public string id { get; set; }
            [DataMember(Name = "statistics")]
            public Statistics statistics { get; set; }
        }

        [DataContract]
        private class RootStatsObject
        {
            [IgnoreDataMember]
            public string kind { get; set; }
            [IgnoreDataMember]
            public string etag { get; set; }
            [IgnoreDataMember]
            public PageInfo pageInfo { get; set; }
            [DataMember(Name = "items")]
            public List<StatsItem> items { get; set; }
        }

        #endregion
    }

    #region XML Serialization classes

    public class SerializeableVideoClip
    {
        public string Id { get; set; }
        public string ResponseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string PictureLink { get; set; }
        public string SiteLink { get; set; }
        public int ViewsCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime Updated { get; set; }
    }

    public class SerializeableVideoClipRoot
    {
        public List<SerializeableVideoClip> VideoClips { get; set; }
    }

    #endregion
}
