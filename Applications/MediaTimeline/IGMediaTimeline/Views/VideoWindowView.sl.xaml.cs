//-------------------------------------------------------------------------
// <copyright file="VideoWindowView.xaml.cs" company="Infragistics">
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

using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using MediaTimeline.ViewModels;

namespace MediaTimeline.Views
{
    public partial class VideoWindowView : UserControl
	{
		#region Constructor
		public VideoWindowView()
        {
            InitializeComponent();
                        
            SilverlightHost currentHost = Application.Current.Host;
            
            //The XamHtmlViewer works with Windowless set to false.
            if (!currentHost.Settings.Windowless)
            {
                ExpanderDetails.Visibility = Visibility.Collapsed;
            }
        }
		#endregion Constructor

        /// <summary>
        /// Sets the data context for the video window.
        /// </summary>
        /// <param name="dataContext">The data context</param>
        public void SetDataContext(VideoClipViewModel dataContext)
        {
            this.DataContext = dataContext;

            HtmlViewer.SourceUri = dataContext.VideoLink;
            HtmlViewerDetails.HtmlCode = dataContext.HtmlCode;
        }
	}
}
