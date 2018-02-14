//-------------------------------------------------------------------------
// <copyright file="MediaTimelineView.xaml.cs" company="Infragistics">
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
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using MediaTimeline.ViewModels;
using MediaTimeline.Assets.Resources;

namespace MediaTimeline.Views
{
    public partial class MediaTimelineView : UserControl
    {
        #region Constants
        /// <summary>
        /// Url that bring back XML with suggestions.
        /// </summary>
        private const string SUGESSTIONS_URL = "http://suggestqueries.google.com/complete/search?hl={0}&ds=yt&output=toolbar&q={1}";
        #endregion Constants

        #region Private Member Variables
        private readonly MediaTimelineViewModel _vm;
        //private VideoWindowView _videoVindow;
        #endregion Private Member Variables

        #region Public Member Variables
        public List<string> NumberOfResultsCollection = new List<string>() { "25", "50", "100", "200", "500" };
        public List<string> SortByCollection = new List<string>() { AppStrings.Relevance, AppStrings.Published, AppStrings.View_Count };
        #endregion Public Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTimelineView"/> class.
        /// </summary>
        public MediaTimelineView()
        {
            InitializeComponent();

            // Hold View Model as member varible
            _vm = (MediaTimelineViewModel)DataContext;

            // Create hidden preview axis to use with the timeline
            Infragistics.Controls.Timelines.PreviewAxis hiddenPreviewAxis = new Infragistics.Controls.Timelines.PreviewAxis();
            hiddenPreviewAxis.Visibility = System.Windows.Visibility.Collapsed;

            // Set the PreviewAxis, otherwise the labels are visible in the zoombar
            timeLine.PreviewAxis = hiddenPreviewAxis;

            //this.timeLineAxis.PaneStyle = this.LayoutRoot.FindResource("HiddenPaneStyle") as Style;

            this.NumberOfResultsComboBox.ItemsSource = this.NumberOfResultsCollection;
            this.SortByComboBox.ItemsSource = this.SortByCollection;
        }
        #endregion Constructors

        #region Event Handlers
        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetYouTubeVideos();
        }
        /// <summary>
        /// Handles the MouseLeftButtonDown event of the DateTimeSeries control (Title Click).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void DateTimeSeries_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (VideoClipViewModel)((TitleView)sender).DataContext;

            // Create new window
            var videoVindow = new VideoWindowView();
            videoVindow.DataContext = vm;
            videoVindow.Show();
        }
        /// <summary>
        /// Handles the Populating event of the AutoCompleteBox control. 
        /// On start typeing.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.PopulatingEventArgs"/> instance containing the event data.</param>
        private void AutoCompleteBox_Populating(object sender, PopulatingEventArgs e)
        {
            e.Cancel = true;

            string url = String.Format(SUGESSTIONS_URL, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName, ((AutoCompleteBox)sender).Text);

            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            try
            {
                string s = client.DownloadString(url);


                if (s == string.Empty) return;

                var doc = new XmlDocument();
                doc.LoadXml(s);
                var nodeList = doc.SelectNodes("//CompleteSuggestion");
                List<KeyValuePair<string, string>> list = null;

                try
                {
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        list = new List<KeyValuePair<string, string>>(nodeList.Count);
                        foreach (XmlNode node in nodeList)
                        {
                            if (node.ChildNodes.Count < 2) continue;
                            if (node.ChildNodes[0].Attributes.Count < 1) continue;
                            var key = node.ChildNodes[0].Attributes[0].Value;
                            var value = node.ChildNodes[1].Attributes[0].Value;

                            list.Add(new KeyValuePair<string, string>(key, String.Format("{0} {1}", value, AppStrings.Results)));
                        }
                        //list.AddRange(from XmlNode node in nodeList
                        //              select new KeyValuePair<string, string>(node.ChildNodes[0].Attributes[0].Value,
                        //                        String.Format("{0} {1}", node.ChildNodes[1].Attributes[0].Value, AppStrings.Results)));

                    }
                    if (list == null) return;

                    autoCompleteBox.ItemsSource = list;
                    autoCompleteBox.PopulateComplete();
                }
                catch (Exception inner)
                {
                    var error = "MediaTimeline failed while parsing suggestions...";
                    System.Diagnostics.Debug.WriteLine(error + inner);
                    throw new Exception(error, inner);
                }
            }
            catch (WebException webEx)
            {               
                System.Diagnostics.Debug.WriteLine(webEx);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the AutoCompleteBox control.
        /// On Enter pressed, get videos from YouTube.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void AutoCompleteBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) _vm.GetYouTubeVideos();
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var win = new About { Owner = Application.Current.MainWindow };
            //win.ShowDialog();
        }
        #endregion Event Handlers
    }
}
