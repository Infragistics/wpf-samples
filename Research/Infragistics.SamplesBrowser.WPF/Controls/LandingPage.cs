using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Infragistics.SamplesBrowser.Controls
{
    public class LandingPage : ItemsControl
    {
        protected internal string StylePath = "/Infragistics.SamplesBrowser;component/Controls/LandingPage.xaml";

        public LandingPage()
        {
            base.DefaultStyleKey = typeof(LandingPage);



            // Add the default style to the control's resources
            //ResourceDictionary style = new ResourceDictionary();
            //style.Source = new System.Uri(StylePath, System.UriKind.Relative);
            //this.Resources.MergedDictionaries.Add(style);
        }


        #region ControlImagePath
        /// <summary>
        /// Identifies the 'ControlImagePath' dependency property
        /// </summary>
        public static readonly DependencyProperty ControlImagePathProperty =
            DependencyProperty.Register("ControlImagePath",
            typeof(string), typeof(LandingPage), new FrameworkPropertyMetadata());

        /// <summary>
        /// The path to an image that represents a control
        /// </summary>
        [Description(" .")]
        [Category("Text")]
        public string ControlImagePath
        {
            get
            {
                return (string)this.GetValue(LandingPage.ControlImagePathProperty);
            }
            set
            {
                this.SetValue(LandingPage.ControlImagePathProperty, value);
            }
        } 
        #endregion

        #region ControlImageSource

        //public ImageSource ControlImageSource { get { return new BitmapImage(new Uri(this.ControlImagePath, UriKind.RelativeOrAbsolute)); } }

        /// <summary>
        /// Identifies the 'ControlImagePath' dependency property
        /// </summary>
        public static readonly DependencyProperty ControlImageSourceProperty =
            DependencyProperty.Register("ControlImageSource",
            typeof(ImageSource), typeof(LandingPage), new FrameworkPropertyMetadata());

        /// <summary>
        /// The path to an image that represents a control
        /// </summary>
        [Description(" .")]
        [Category("Layout")]
        public ImageSource ControlImageSource
        {
            get
            {
                return (ImageSource)this.GetValue(LandingPage.ControlImageSourceProperty);
            }
            set
            {
                this.SetValue(LandingPage.ControlImageSourceProperty, value);
            }
        }
        #endregion

        #region ControlNamePrefix
        /// <summary>
        /// Identifies the 'ControlNamePrefix' dependency property
        /// </summary>
        public static readonly DependencyProperty ControlNamePrefixProperty =
            DependencyProperty.Register("ControlNamePrefix",
            typeof(string), typeof(LandingPage), new FrameworkPropertyMetadata());

        /// <summary>
        ///  
        /// </summary>
        [Description(" .")]
        [Category("Layout")]
        public string ControlNamePrefix
        {
            get
            {
                return (string)this.GetValue(LandingPage.ControlNamePrefixProperty);
            }
            set
            {
                this.SetValue(LandingPage.ControlNamePrefixProperty, value);
            }
        } 
        #endregion

        #region ControlName
        /// <summary>
        /// Identifies the 'ControlName' dependency property
        /// </summary>
        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName",
            typeof(string), typeof(LandingPage), new FrameworkPropertyMetadata());

        /// <summary>
        ///  
        /// </summary>
        [Description(" .")]
        [Category("Layout")]
        public string ControlName
        {
            get
            {
                return (string)this.GetValue(LandingPage.ControlNameProperty);
            }
            set
            {
                this.SetValue(LandingPage.ControlNameProperty, value);
            }
        } 
        #endregion

        #region ControlDescription
        /// <summary>
        /// Identifies the 'ControlDescription' dependency property
        /// </summary>
        public static readonly DependencyProperty ControlDescriptionProperty =
            DependencyProperty.Register("ControlDescription",
            typeof(string), typeof(LandingPage), new FrameworkPropertyMetadata());

        /// <summary>
        ///  
        /// </summary>
        [Description(" .")]
        [Category("Layout")]
        public string ControlDescription
        {
            get
            {
                return (string)this.GetValue(LandingPage.ControlDescriptionProperty);
            }
            set
            {
                this.SetValue(LandingPage.ControlDescriptionProperty, value);
            }
        } 
        #endregion
    }
}