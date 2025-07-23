using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;                           // HttpWebRequest
using System.Runtime.Serialization;         // DataContract, DataMember
using System.Runtime.Serialization.Json;    // DataContractJsonSerializer
using System.Windows;

namespace Infragistics.Samples.Services
{
    /// <summary>
    /// <para> Represents a connector class that sets up BingMaps REST imagery service </para>
    /// <para> and provides imagery tiles via Http web requests.</para>
    /// <remarks>Bing Maps REST Services: http://msdn.microsoft.com/en-us/library/ff701713.aspx </remarks>
    /// </summary>
    public class BingMapsConnector : DependencyObject, INotifyPropertyChanged
    {
        public BingMapsConnector()
        {
            this.IsInitialized = false;
        }
        #region Events
        public event EventHandler ImageryInitialized;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets an API key required by the Bing Maps imagery service.
        /// <remarks>This key must be obtained from the http://www.bingmapsportal.com website. </remarks>
        /// </summary>
        public string ApiKey
        {
            get { return (string)GetValue(ApiKeyProperty); }
            set { SetValue(ApiKeyProperty, value); }
        }
        public const string ApiKeyPropertyName = "ApiKey";
        public static readonly DependencyProperty ApiKeyProperty =
            DependencyProperty.Register(ApiKeyPropertyName, typeof(string), typeof(BingMapsConnector),
            new PropertyMetadata(string.Empty, (o, e) => (o as BingMapsConnector).OnApiKeyChanged((string)e.OldValue, (string)e.NewValue)));

        private void OnApiKeyChanged(string oldValue, string newValue)
        {
            this.Validate();
        }
        public const string ImageryStylePropertyName = "ImageryStyle";
        /// <summary>
        /// <para> Gets or sets a map style of the Bing Maps imagery tiles. </para>
        /// <para> For example: Aerial, AerialWithLabels, or Road map style. </para>
        /// </summary>
        public BingMapsImageryStyle ImageryStyle
        {
            get { return (BingMapsImageryStyle)GetValue(ImageryStyleProperty); }
            set { SetValue(ImageryStyleProperty, value); }
        }
        public static readonly DependencyProperty ImageryStyleProperty =
            DependencyProperty.Register(ImageryStylePropertyName, typeof(BingMapsImageryStyle), typeof(BingMapsConnector),
            new PropertyMetadata(BingMapsImageryStyle.AerialWithLabels, (o, e) =>
                (o as BingMapsConnector).OnImageryStylePropertyChanged((BingMapsImageryStyle)e.OldValue, (BingMapsImageryStyle)e.NewValue)));

        private void OnImageryStylePropertyChanged(BingMapsImageryStyle oldValue, BingMapsImageryStyle newValue)
        {
            this.Validate();
        }
        private string _tilePath;
        /// <summary>
        /// Gets an imagery tile path for the Bing Maps service.  
        /// </summary>
        public string TilePath
        {
            get { return _tilePath; }
            private set
            {
                _tilePath = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TilePath"));
                }
            }
        }
        private ObservableCollection<string> _subDomains;
        /// <summary>
        /// Gets a collection of image URI sub-domains for the Bing Maps service.
        /// </summary>
        public ObservableCollection<string> SubDomains
        {
            get
            {
                return _subDomains;
            }
            private set
            {
                _subDomains = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SubDomains"));
                }
            }
        }
        /// <summary>
        /// Gets a status whether the Bing Maps service is initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }
        /// <summary>
        /// Gets or sets whether the Bing Maps service should be auto-initialized upon valid property values.
        /// </summary>
        public bool IsAutoInitialized
        {
            get { return (bool)GetValue(IsAutoInitializedProperty); }
            set { SetValue(IsAutoInitializedProperty, value); }
        }
        public const string IsAutoInitializedPropertyName = "IsAutoInitialized";

        public static readonly DependencyProperty IsAutoInitializedProperty =
            DependencyProperty.Register(IsAutoInitializedPropertyName, typeof(bool), typeof(BingMapsConnector),
            new PropertyMetadata(false, (o, e) => (o as BingMapsConnector).OnAutoInitializedChanged((bool)e.OldValue, (bool)e.NewValue)));

        private void OnAutoInitializedChanged(bool oldValue, bool newValue)
        {
            this.Validate();
        }

        #endregion
        #region Methods
        private void Validate()
        {
            this.IsInitialized = false;
            if (!IsValidApiKey())
            {
                return;
            }
            if (this.IsAutoInitialized)
            {
                Initialize();
            }
        }
        private bool IsValidApiKey()
        {
            if (String.IsNullOrEmpty(this.ApiKey) || this.ApiKey.Length < 20)
            {
                return false;
            }
            return true;
        }
        public void Initialize()
        {
            if (!IsValidApiKey())
            {
                this.IsInitialized = false;
                System.Diagnostics.Debug.WriteLine("Detected Invalid BingMaps API key: " + this.ApiKey);
                return;
            }
            this.IsInitialized = true;
            // for more info on setting up web requests to BingMaps REST imagery service
            // refer to: http://msdn.microsoft.com/en-us/library/ff701716.aspx 

            var bingUrl = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/";
            var imagerySet = this.ImageryStyle;
            bingUrl += imagerySet;
            var parms = "key=" + this.ApiKey + "&include=ImageryProviders";
            var url = bingUrl + "?" + parms;
            var req = HttpWebRequest.Create(url);
            req.BeginGetResponse(GetResponseCompleted, req);
        }
        #endregion
        #region Event Handlers
        private void GetResponseCompleted(IAsyncResult res)
        {
            var req = (HttpWebRequest)res.AsyncState;
            var response = req.EndGetResponse(res);
            // alternatively, parsing of BingResponse can be performed using LINQ to XML 
            // instead of using JSON deserializer
            var json = new DataContractJsonSerializer(typeof(BingResponse));
            var resp = (BingResponse)json.ReadObject(response.GetResponseStream());
            if (resp.ResourceSets == null ||
                resp.ResourceSets.Count < 1 ||
                resp.ResourceSets[0].Resources == null ||
                resp.ResourceSets[0].Resources.Count < 1)
            {
                return;
            }
            var imageUrl = resp.ResourceSets[0].Resources[0].ImageUrl;
            var subDomains = resp.ResourceSets[0].Resources[0].ImageUrlSubdomains;
            if (imageUrl == null || subDomains == null)
            {
                return;
            }

            TilePath = imageUrl;
            SubDomains = new ObservableCollection<string>(subDomains);
            if (ImageryInitialized != null)
            {
                ImageryInitialized(this, new EventArgs());
            }
        }
        #endregion
    }
    /// <summary>
    /// Determines map style for the Bing Maps imagery.
    /// </summary>
    public enum BingMapsImageryStyle
    {
        /// <summary>
        /// Specifies the Aerial map style without road or labels overlay.
        /// </summary>
        Aerial,
        /// <summary>
        /// Specifies the Aerial map style with road and labels overlay.
        /// </summary>
        AerialWithLabels,
        /// <summary>
        /// Specifies the Roads map style without aerial overlay.
        /// </summary>
        Road,
        #region Not supported Bing Maps styles by the xamGeographicMap control
        ///// <summary>
        ///// Specifies the Bird’s eye (oblique-angle) map style
        ///// </summary>
        //Birdseye,
        ///// <summary>
        ///// Specifies the Bird’s eye map style with road and labels overlay.
        ///// </summary>
        //BirdseyeWithLabels, 
        #endregion
    }
    [DataContract]
    public class BingResponse
    {
        public BingResponse()
        {
            ResourceSets = new List<BingResourceSet>();
        }

        [DataMember(Name = "resourceSets")]
        public List<BingResourceSet> ResourceSets { get; set; }
    }
    [DataContract]
    public class BingResourceSet
    {
        public BingResourceSet()
        {
            Resources = new List<ImageryMetadata>();
        }
        [DataMember(Name = "resources")]
        public List<ImageryMetadata> Resources { get; set; }
    }
    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    public class ImageryMetadata
    {
        public ImageryMetadata()
        {
            ImageUrlSubdomains = new List<string>();
        }
        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }
        [DataMember(Name = "imageUrlSubdomains")]
        public List<string> ImageUrlSubdomains { get; set; }
    }

}