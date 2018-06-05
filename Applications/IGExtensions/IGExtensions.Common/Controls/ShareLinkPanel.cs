using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using IGExtensions.Framework.Controls;      // FrameworkElementEx.ApplyStyle()

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// Represents the ShareLinkPanel
    /// </summary>
    [TemplatePart(Name = "RootElement", Type = typeof(Grid))]
    [TemplatePart(Name = "stackPanel", Type = typeof(StackPanel))]
    [TemplatePart(Name = "shareFacebookButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareTwitterButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareGooglePlusButton", Type = typeof(ShareLinkButton))]
   // [TemplatePart(Name = "shareGoogleBookmarkButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareDeliciousButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareStumbleUponButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "sharePinterestButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareRedditButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareLinkedInButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareBloggerButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "shareFriendFeedButton", Type = typeof(ShareLinkButton))]
    //[TemplatePart(Name = "shareMySpaceButton", Type = typeof(ShareLinkButton))]
    [TemplatePart(Name = "sharePrintFriendlyButton", Type = typeof(ShareLinkButton))]
    public class ShareLinkPanel : Control
    {
        /// <summary>
        /// Constructs an instance of the ShareLinkPanel
        /// </summary>
        public ShareLinkPanel()
        {
           // this.ShareLink = new ShareLink() { Link = "http://labs.infragistics.com/blog", Title = "IGExtensions" };
             
            this.ShareLinksVisibility = new ShareLinksVisibility();

            this.Loaded += new RoutedEventHandler(ShareLinkPanel_Loaded);

            //this.DefaultStyleKey = typeof(ShareLinkPanel);
            
            // add the generic style to the control's resources
            const string stylePath = "/IGExtensions.Common;component/Controls/ShareLinkPanel.xaml";
            this.ApplyStyle(stylePath);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (ShareLinkRequested != null)
            {
                ShareLinkRequested(this, e);
            }
        }
        private StackPanel _stackPanel;
        public event EventHandler ShareLinkRequested;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var rootElement = this.GetTemplateChild("RootElement") as Grid;
            if (rootElement != null)
            {
                _stackPanel = rootElement.FindName("stackPanel") as StackPanel;

                // get share buttons from generic style
                var shareFacebookButton = _stackPanel.FindName("shareFacebookButton") as ShareLinkButton;
                var shareTweetButton = _stackPanel.FindName("shareTwitterButton") as ShareLinkButton;
                var shareGooglePlusButton = _stackPanel.FindName("shareGooglePlusButton") as ShareLinkButton;
                var shareGoogleBookmarkButton = _stackPanel.FindName("shareGoogleBookmarkButton") as ShareLinkButton;
                var shareDeliciousButton = _stackPanel.FindName("shareDeliciousButton") as ShareLinkButton;
                var shareStumbleUponButton = _stackPanel.FindName("shareStumbleUponButton") as ShareLinkButton;
                var sharePinItButton = _stackPanel.FindName("sharePinterestButton") as ShareLinkButton;
                var shareRedditButton = _stackPanel.FindName("shareRedditButton") as ShareLinkButton;
                var shareLinkedInButton = _stackPanel.FindName("shareLinkedInButton") as ShareLinkButton;
                var shareBloggerButton = _stackPanel.FindName("shareBloggerButton") as ShareLinkButton;
                var shareFriendFeedButton = _stackPanel.FindName("shareFriendFeedButton") as ShareLinkButton;
                var shareMySpaceButton = _stackPanel.FindName("shareMySpaceButton") as ShareLinkButton;
                var sharePrintFriendlyButton = _stackPanel.FindName("sharePrintFriendlyButton") as ShareLinkButton;

                // add an event handler for requesting update of the share link from all share buttons
                if (shareFacebookButton != null) ConfigureShareButton(shareFacebookButton);
                if (shareTweetButton != null) ConfigureShareButton(shareTweetButton);
                if (shareGooglePlusButton != null) ConfigureShareButton(shareGooglePlusButton);
                if (shareGoogleBookmarkButton != null) ConfigureShareButton(shareGoogleBookmarkButton);
                if (shareDeliciousButton != null) ConfigureShareButton(shareDeliciousButton);
                if (shareStumbleUponButton != null) ConfigureShareButton(shareStumbleUponButton);
                if (sharePinItButton != null) ConfigureShareButton(sharePinItButton);
                if (shareRedditButton != null) ConfigureShareButton(shareRedditButton);
                if (shareLinkedInButton != null) ConfigureShareButton(shareLinkedInButton);
                if (shareBloggerButton != null) ConfigureShareButton(shareBloggerButton);
                if (shareFriendFeedButton != null) ConfigureShareButton(shareFriendFeedButton);
                if (shareMySpaceButton != null) ConfigureShareButton(shareMySpaceButton);
                if (sharePrintFriendlyButton != null) ConfigureShareButton(sharePrintFriendlyButton);
                 
            }
        }
       
        void ShareLinkPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (VisualTreeHelper.GetChildrenCount(this) > 0)
            {
                var rootElement = VisualTreeHelper.GetChild(this, 0) as Grid;
                if (rootElement != null)
                {
                    //_stackPanel = rootElement.FindName("stackPanel") as StackPanel;
                 }
            }

        }
        void ConfigureShareButton(ShareLinkButton button)
        {
            button.HyperlinkTarget = "_blank";
            //button.HyperlinkLinkUpdateRequested += OnHyperlinkLinkUpdateRequested;
            button.MouseLeftButtonDown += OnShareButtonMouseLeftButtonDown;
            button.MouseLeftButtonUp += OnShareButtonMouseLeftButtonUp;
        
        }

        private string link;
        private void OnHyperlinkLinkUpdateRequested(object sender, EventArgs e)
        {
            if (ShareLinkRequested != null)
            {
                ShareLinkRequested(this, e);
            }
        }
        private void OnShareButtonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseLeftButtonDown(e);
            link = this.ShareLink.Link;
            if (ShareLinkRequested != null)
            {
                if (this.ShareLinksRequestable) ShareLinkRequested(this, e);
            }
        }
        private void OnShareButtonMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseLeftButtonUp(e);

            var btn = (ShareLinkButton)sender;
            var sharePortal = btn.Name.Replace("share", "").Replace("Button", "");
            // create share link from the current navigation address
            this.ShareLink.Link = NavigationFramework.CurrentNavigationAddress;
            var shareLink = this.ShareLink;
            var uri = shareLink.ConvertToUri(sharePortal);
            NavigationFramework.Navigate(uri, "_blank");
        }

        #region CornerRadius
        public const string CornerRadiusPropertyName = "CornerRadius";
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
                  CornerRadiusPropertyName, typeof(CornerRadius), typeof(ShareLinkPanel), null);
        
        /// <summary>
        /// Gets or sets CornerRadius of the ShareLinkPanel
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #endregion

        #region ShareLinkOrientation 

        public const string ShareLinkOrientationPropertyName = "ShareLinkOrientation";

        public static DependencyProperty ShareLinkOrientationProperty = DependencyProperty.Register(
            ShareLinkOrientationPropertyName, typeof(Orientation), typeof(ShareLinkPanel), null);

        /// <summary>
        /// Gets or sets Orientation of the ShareLinkPanel
        /// </summary>
        public Orientation ShareLinkOrientation
        {
            get { return (Orientation)GetValue(ShareLinkOrientationProperty); }
            set { SetValue(ShareLinkOrientationProperty, value); }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case ShareLinkOrientationPropertyName:
                    {
                        //UpdatePaletteBrush();
                        //_stackPanel.Orientation = this.Orientation;
                        break;
                    }
            }
        } 
        #endregion

        #region ShareLink 
        public const string ShareLinkPropertyName = "ShareLink";
        public static DependencyProperty ShareLinkProperty = DependencyProperty.Register(
            ShareLinkPropertyName, typeof(ShareLink), typeof(ShareLinkPanel),
            new PropertyMetadata(ShareLink.ShowcaseLink, (sender, e) =>
            {
                (sender as ShareLinkPanel).OnPropertyChanged(new PropertyChangedEventArgs(ShareLinkPropertyName));
            }));
         
        /// <summary>
        /// Gets or sets ShareLink of the ShareLinkPanel
        /// </summary>
        public ShareLink ShareLink
        {
            get { return (ShareLink)GetValue(ShareLinkProperty); }
            set { SetValue(ShareLinkProperty, value); }
        }

        #endregion
        
        #region ShareLinkSize 
        public const string ShareLinkSizePropertyName = "ShareLinkSize";
        public static DependencyProperty ShareLinkSizeProperty = DependencyProperty.Register(
            ShareLinkSizePropertyName, typeof(int), typeof(ShareLinkPanel),
            new PropertyMetadata(15, (sender, e) =>
            {
                (sender as ShareLinkPanel).OnPropertyChanged(new PropertyChangedEventArgs(ShareLinkSizePropertyName));
            }));

        /// <summary>
        /// Gets or sets ShareLink of the ShareLinkPanel
        /// </summary>
        public int ShareLinkSize
        {
            get { return (int)GetValue(ShareLinkSizeProperty); }
            set { SetValue(ShareLinkSizeProperty, value); }
        }

        #endregion

        #region ShareLinksRequestable
        public const string ShareLinksRequestablePropertyName = "ShareLinksRequestable";
        public static DependencyProperty ShareLinksRequestableProperty = DependencyProperty.Register(
                ShareLinksRequestablePropertyName, typeof(bool), typeof(ShareLinkPanel),
            new PropertyMetadata(true, (sender, e) =>
            {
                (sender as ShareLinkPanel).OnPropertyChanged(new PropertyChangedEventArgs(ShareLinksRequestablePropertyName));
            }));

        /// <summary>
        /// Gets or sets whether share link should be request-able 
        /// </summary>
        public bool ShareLinksRequestable
        {
            get { return (bool)GetValue(ShareLinksRequestableProperty); }
            set { SetValue(ShareLinksRequestableProperty, value); }
        } 
        #endregion

        #region ShareLinksVisibility
        public const string ShareLinksVisibilityPropertyName = "ShareLinksVisibility";
        public static DependencyProperty ShareLinksVisibilityProperty = DependencyProperty.Register(
          ShareLinksVisibilityPropertyName, typeof(ShareLinksVisibility), typeof(ShareLinkPanel), null);

        /// <summary>
        /// Gets or sets Visibility of the share buttons
        /// </summary>
        public ShareLinksVisibility ShareLinksVisibility
        {
            get { return (ShareLinksVisibility)GetValue(ShareLinksVisibilityProperty); }
            set { SetValue(ShareLinksVisibilityProperty, value); }
        } 
        #endregion
         
    }
    public class ShareLink : ObservableModel
    {
        #region Constructors
        public ShareLink()
            : this(AppWebsite, AppImage, AppName)
        { }
        public ShareLink(string uri)
            : this(uri, AppImage, AppName)
        { }
        public ShareLink(string uri, string image)
            : this(uri, image, AppName)
        { }

        public ShareLink(string uri, string image, string title)
        {
            _link = uri;
            _title = title;
            _image = image;
        }
        #endregion
        #region Properties

        private string _link;
        /// <summary>
        /// Gets or sets absolute Uri string to the shared link
        /// </summary>
        public string Link
        {
            get { return _link; }
            set { if (_link == value) return; _link = value; OnPropertyChanged("Link"); }
        }

        private string _image;
        /// <summary>
        /// Gets or sets absolute Uri string to an image that represents the shared link
        /// </summary>
        public string Image
        {
            get { return _image; }
            set { if (_image == value) return; _image = value; OnPropertyChanged("Image"); }
        }

        private string _description;
        /// <summary>
        /// Gets or sets description for the shared link
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { if (_description == value) return; _description = value; OnPropertyChanged("Description"); }
        }

        private string _title;
        /// <summary>
        /// Gets or sets title for the shared link
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { if (_title == value) return; _title = value; OnPropertyChanged("Title"); }
        }

        #endregion
        #region Methods
        public Uri ConvertToUri(string sharePortal)
        {
            return ConvertToUri(this, sharePortal);
        }

        public static Uri ConvertToUri(ShareLink shareLink, string sharePortal)
        {
            string uri = "http://labs.infragistics.com";
            switch (sharePortal)
            {
                case "Facebook":
                    {
                        //uri = "http://www.facebook.com/sharer.php?u=" + shareLink.Link + "&t=" + shareLink.Title;
                        uri = "http://www.facebook.com/sharer.php?u=" + shareLink.Link;
                        break;
                    }
                case "Twitter":
                    {
                        //uri = "http://twitter.com/intent/tweet?url=" + shareLink.Link + "&text=" + shareLink.Title;
                        uri = "http://twitter.com/intent/tweet?url=" + shareLink.Link;
                        break;
                    }
                case "Delicious":
                    {
                        //uri = "http://del.icio.us/post?url=" + obj.Link + "&title" + obj.Link;
                        uri = "http://delicious/post?url=" + shareLink.Link + "&title" + shareLink.Link;
                        break;
                    }
                case "GooglePlus":
                    {
                        //uri = "https://plusone.google.com/_/+1/confirm?hl=en&url=" + shareLink.Link;
                        uri = "http://plus.google.com/share?url=" + shareLink.Link;
                        break;
                    }
                case "StumbleUpon":
                    {
                        uri = "http://www.stumbleupon.com/submit?url=" + shareLink.Link + "&title=" + shareLink.Title;
                        break;
                    }
                case "Digg":
                    {
                        uri = "http://digg.com/submit?phase=2&url=" + shareLink.Link + "&title=" + shareLink.Title;
                        break;
                    }
                case "Pinterest":
                    {
                        //uri = "http://pinterest.com/pin/create/button/?url=" + obj.Link + "&title=" + obj.Title + "&is_video=";
                        //     http://pinterest.com/pin/create/bookmarklet/?media=http%3A%2F%2Flabs.infragistics.com%2Fbackground_backup.png&url=http%3A%2F%2Flabs.infragistics.com%2F&title=IG%20Labs%20-%20Exploring%20Tomorrow%20Today&is_video=false&description=IG%20Labs%20-%20Exploring%20Tomorrow%20Today
                        uri = "http://pinterest.com/pin/create/bookmarklet/?media=" + shareLink.Image + "&url=" + shareLink.Link + "&title=" + shareLink.Title + "&is_video=false&description=" + shareLink.Description;
                        break;
                    }
                case "Reddit":
                    {
                        uri = "http://reddit.com/submit?url=" + shareLink.Link + "&title=" + shareLink.Title;
                        break;
                    }
                case "LinkedIn":
                    {
                        //uri = "http://www.linkedin.com/shareArticle?mini=true&url=" + shareLink.Link + "&title=" + shareLink.Title;
                        uri = "http://www.linkedin.com/shareArticle?mini=true&url=" + shareLink.Link;
                        break;
                    }
                case "Blogger":
                    {
                        uri = "http://www.blogger.com/blog_this.pyra?t&u=" + shareLink.Link + "&n=" + shareLink.Title + "&pli=1";
                        break;
                    }
                case "MySpace":
                    {
                        //uri = "http://www.myspace.com/Modules/PostTo/Pages/?u=" + obj.Link + "&t=" + obj.Title;
                        uri = "http://www.myspace.com/index.cfm?fuseaction=postto&t=" + shareLink.Title + "&c=CONTENTS&u=" + shareLink.Link + "&l=";
                        break;
                    }
                case "GoogleBookmark":
                    {
                        uri = "http://www.google.com/bookmarks/mark?op=add&bkmk=" + shareLink.Link + "&title=" + shareLink.Title;
                        break;
                    }
                case "PrintFriendly":
                    {
                        uri = "http://www.printfriendly.com/print/v2?url=" + shareLink.Link;
                        break;
                    }
                case "FriendFeed":
                    {
                        uri = "http://www.friendfeed.com/share?link=" + shareLink.Link + "&title=" + shareLink.Title;
                        break;
                    }
                default:
                    {
                        uri = "http://www.facebook.com/sharer.php?u=" + shareLink.Link + "&t=" + shareLink.Title;
                        break;
                    }
            }
            return new Uri(uri);
        }

        #endregion
        public static string AppName = "IG Showcase Application";
        public static string AppWebsite = "http://labs.infragistics.com";
        //public static string AppImage = "http://labs.infragistics.com/preview.png";
        public static string AppImage = "http://www.infragistics.com/App_Themes/Global/images/logo.gif";


        public static ShareLink ShowcaseLink = new ShareLink { Link = AppWebsite, Image = AppImage, Title = AppName };
        public static ShareLink EmptyLink = new ShareLink { Link = "", Image = "", Title = "" };


    }
    
    public class ShareLinkPanelItem : HyperlinkButton
    {
        #region ShareLink Property
        public const string ShareLinkPropertyName = "ShareImage";
        public static DependencyProperty ShareLinkProperty = DependencyProperty.Register(
            ShareLinkPropertyName, typeof(ShareLink), typeof(ShareLinkPanel),
            new PropertyMetadata(ShareLink.ShowcaseLink, (sender, e) =>
            {
                (sender as ShareLinkPanel).OnPropertyChanged(new PropertyChangedEventArgs(ShareLinkPropertyName));
            }));

        /// <summary>
        /// Gets or sets ShareLink of the ShareLinkPanel
        /// </summary>
        public ShareLink ShareLink
        {
            get { return (ShareLink)GetValue(ShareLinkProperty); }
            set { SetValue(ShareLinkProperty, value); }
        }

        #endregion


        //public const string VisibilityPropertyName = "Visibility";
        //public static DependencyProperty VisibilityProperty = DependencyProperty.Register(
        // VisibilityPropertyName, typeof(Visibility), typeof(ShareLinkPanelItem),
        //  new PropertyMetadata(Visibility.Visible, (sender, e) =>
        //  {
        //      (sender as ShareLinkPanelItem).OnPropertyChanged(new PropertyChangedEventArgs(VisibilityPropertyName));
        //  }));

        ///// <summary>
        ///// Gets or sets Visibility of the share Facebook button
        ///// </summary>
        //public Visibility ShareFacebookVisibility
        //{
        //    get { return (Visibility)GetValue(VisibilityProperty); }
        //    set { SetValue(VisibilityProperty, value); }
        //}

        //public void OnPropertyChanged(PropertyChangedEventArgs ea)
        //{
        //    switch (ea.PropertyName)
        //    {
        //        case VisibilityPropertyName:
        //            {
        //                //UpdatePaletteBrush();
        //                //_stackPanel.Orientation = this.Orientation;
        //                break;
        //            }
        //    }
            
        //} 
    }

    public enum ShareTargetMedia
    {
        Facebook,
        GoogleBookmark,
        GooglePlus,
        Twitter,
        PrintFriendly,
        Myspace,
        Blogger,
        Friendfeed,
        Delicious,
        Pinterest,
        Linkedin,
        Reddit,
        Digg,
        Sstumbleupon,
    }

    public class ShareLinksVisibility : ObservableModel 
    {
        public ShareLinksVisibility()
        {
            this.BloggerVisibility = Visibility.Collapsed;
            this.FacebookVisibility = Visibility.Visible;
            this.GooglePlusVisibility = Visibility.Visible;
            this.TwitterVisibility = Visibility.Visible;
            this.PrintFriendlyVisibility = Visibility.Collapsed;
            this.MyspaceVisibility = Visibility.Collapsed;
            this.FriendFeedVisibility = Visibility.Collapsed;
            this.DeliciousVisibility = Visibility.Collapsed;
            this.PinterestVisibility = Visibility.Collapsed;
            this.LinkedInVisibility = Visibility.Visible;
            this.RedditVisibility = Visibility.Collapsed;
            this.DiggVisibility = Visibility.Visible;
            this.StumbleUponVisibility = Visibility.Collapsed;
        }
        #region Properties
        private Visibility _visibilityFacebook;
        /// <summary>
        /// Gets or sets visibility of the Facebook shared link
        /// </summary>
        public Visibility FacebookVisibility
        {
            get { return _visibilityFacebook; }
            set { if (_visibilityFacebook == value) return; _visibilityFacebook = value; OnPropertyChanged("FacebookVisibility"); }
        }
        private Visibility _visibilityGooglePlus;
        /// <summary>
        /// Gets or sets visibility of the GooglePlus shared link
        /// </summary>
        public Visibility GooglePlusVisibility
        {
            get { return _visibilityGooglePlus; }
            set { if (_visibilityGooglePlus == value) return; _visibilityGooglePlus = value; OnPropertyChanged("GooglePlusVisibility"); }
        }
        private Visibility _visibilityTwitter;
        /// <summary>
        /// Gets or sets visibility of the Twitter shared link
        /// </summary>
        public Visibility TwitterVisibility
        {
            get { return _visibilityTwitter; }
            set { if (_visibilityTwitter == value) return; _visibilityTwitter = value; OnPropertyChanged("TwitterVisibility"); }
        }
        private Visibility _visibilityPrintFriendly;
        /// <summary>
        /// Gets or sets visibility of the PrintFriendly shared link
        /// </summary>
        public Visibility PrintFriendlyVisibility
        {
            get { return _visibilityPrintFriendly; }
            set { if (_visibilityPrintFriendly == value) return; _visibilityPrintFriendly = value; OnPropertyChanged("PrintFriendlyVisibility"); }
        }
        private Visibility _visibilityMyspace;
        /// <summary>
        /// Gets or sets visibility of the Myspace shared link
        /// </summary>
        public Visibility MyspaceVisibility
        {
            get { return _visibilityMyspace; }
            set { if (_visibilityMyspace == value) return; _visibilityMyspace = value; OnPropertyChanged("MyspaceVisibility"); }
        }
        private Visibility _visibilityBlogger;
        /// <summary>
        /// Gets or sets visibility of the Blogger shared link
        /// </summary>
        public Visibility BloggerVisibility
        {
            get { return _visibilityBlogger; }
            set { if (_visibilityBlogger == value) return; _visibilityBlogger = value; OnPropertyChanged("BloggerVisibility"); }
        }
        private Visibility _visibilityFriendfeed;
        /// <summary>
        /// Gets or sets visibility of the Friend feed shared link
        /// </summary>
        public Visibility FriendFeedVisibility
        {
            get { return _visibilityFriendfeed; }
            set { if (_visibilityFriendfeed == value) return; _visibilityFriendfeed = value; OnPropertyChanged("FriendFeedVisibility"); }
        }
        private Visibility _visibilityDelicious;
        /// <summary>
        /// Gets or sets visibility of the Delicious shared link
        /// </summary>
        public Visibility DeliciousVisibility
        {
            get { return _visibilityDelicious; }
            set { if (_visibilityDelicious == value) return; _visibilityDelicious = value; OnPropertyChanged("DeliciousVisibility"); }
        }
        private Visibility _visibilityPinterest;
        /// <summary>
        /// Gets or sets visibility of the Pinterest shared link
        /// </summary>
        public Visibility PinterestVisibility
        {
            get { return _visibilityPinterest; }
            set { if (_visibilityPinterest == value) return; _visibilityPinterest = value; OnPropertyChanged("PinterestVisibility"); }
        }
        private Visibility _visibilityLinkedin;
        /// <summary>
        /// Gets or sets visibility of the LinkedIn shared link
        /// </summary>
        public Visibility LinkedInVisibility
        {
            get { return _visibilityLinkedin; }
            set { if (_visibilityLinkedin == value) return; _visibilityLinkedin = value; OnPropertyChanged("LinkedInVisibility"); }
        } 
        private Visibility _visibilityReddit;
        /// <summary>
        /// Gets or sets visibility of the Reddit shared link
        /// </summary>
        public Visibility RedditVisibility
        {
            get { return _visibilityReddit; }
            set { if (_visibilityReddit == value) return; _visibilityReddit = value; OnPropertyChanged("RedditVisibility"); }
        }
        private Visibility _visibilityDigg;
        /// <summary>
        /// Gets or sets visibility of the Digg shared link
        /// </summary>
        public Visibility DiggVisibility
        {
            get { return _visibilityDigg; }
            set { if (_visibilityDigg == value) return; _visibilityDigg = value; OnPropertyChanged("DiggVisibility"); }
        }
        private Visibility _visibilityStumbleupon;
        /// <summary>
        /// Gets or sets visibility of the SstumbleUpon shared link
        /// </summary>
        public Visibility StumbleUponVisibility
        {
            get { return _visibilityStumbleupon; }
            set { if (_visibilityStumbleupon == value) return; _visibilityStumbleupon = value; OnPropertyChanged("StumbleUponVisibility"); }
        }
        #endregion

        //private Visibility _visibility;
        ///// <summary>
        ///// Gets or sets visibility of the  shared link
        ///// </summary>
        //public Visibility Visibility
        //{
        //    get { return _visibility; }
        //    set { if (_visibility == value) return; _visibility = value; OnPropertyChanged("Visibility"); }
        //}
    }
}