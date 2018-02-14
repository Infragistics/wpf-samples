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
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using Infragistics.Controls.Interactions;
using MediaTimeline.ViewModels;
using System.Windows.Data;
using MediaTimeline.Converters;
using MediaTimeline.Assets.Resources;

namespace MediaTimeline.Views
{
	public partial class MediaTimelineView : UserControl
	{
		#region Constants
		/// <summary>
		/// Url that bring back Jason with suggestions.
		/// </summary>
		private const string SUGESSTIONS_URL = "http://suggestqueries.google.com/complete/search?hl={0}&ds=yt&json=t&jsonp=myCallbackFunction&q={1}";
		#endregion Constants

		#region Private Member Variables
		private readonly MediaTimelineViewModel _vm;
		private VideoWindowView _videoVindow;
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

			// Register scriptable object 
			HtmlPage.RegisterScriptableObject("MediaTimeLineScriptable", this);

			// Hold View Model as member varible
			_vm = (MediaTimelineViewModel)DataContext;

			// Create hidden preview axis to use with the timeline
			Infragistics.Controls.Timelines.PreviewAxis hiddenPreviewAxis = new Infragistics.Controls.Timelines.PreviewAxis();
			hiddenPreviewAxis.Visibility = System.Windows.Visibility.Collapsed;

			// Set the PreviewAxis, otherwise the labels are visible in the zoombar
			timeLine.PreviewAxis = hiddenPreviewAxis;

			this.NumberOfResultsComboBox.ItemsSource = this.NumberOfResultsCollection;
			this.SortByComboBox.ItemsSource = this.SortByCollection;
		}

		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Handles suggestion received from java script
		/// </summary>
		/// <param name="data">The data.</param>
		[ScriptableMember]
		public void PassData(ScriptObject data)
		{
			var tips = ((ScriptObject)data.GetProperty(1)).ConvertTo<string[]>();
			var resultsCount = ((ScriptObject)data.GetProperty(2)).ConvertTo<string[]>();

			// Get min of both arrays
			int count = tips.Length > resultsCount.Length ? resultsCount.Length : tips.Length;
			if (count <= 0) return;

			var list = new List<KeyValuePair<string, string>>(count);

			// Loop the elements and place them in list collection.
			for (int i = 0; i < count; i++)
			{
				list.Add(new KeyValuePair<string, string>(tips[i], resultsCount[i]));
			}

			// Populate suggestions
			autoCompleteBox.ItemsSource = list;
			autoCompleteBox.PopulateComplete();
		}
		#endregion Public Methods

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
			if (_videoVindow == null)
			{
				_videoVindow = new VideoWindowView();
				_videoVindow.MainWindow.WindowStateChanged += WindowStateChanged;
				LayoutRoot.Children.Add(_videoVindow);
			}

			//Set the data context for the video window asynchronously.
			this.Dispatcher.BeginInvoke(
				() =>
				{
					_videoVindow.SetDataContext(vm);
				});
		}


		/// <summary>
		/// Windows the state changed. If state is Hidden then remove window from visual tree.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="WindowStateChangedEventArgs"/> instance containing the event data.</param>
		private void WindowStateChanged(object sender, WindowStateChangedEventArgs e)
		{
			if (e.NewWindowState == Infragistics.Controls.Interactions.WindowState.Hidden)
			{
				LayoutRoot.Children.Remove(_videoVindow);
				_videoVindow.MainWindow.WindowStateChanged -= WindowStateChanged;
				_videoVindow = null;
			}
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
			HtmlPage.Window.Invoke("injectScript", String.Format(SUGESSTIONS_URL, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, ((AutoCompleteBox)sender).Text));
		}

		/// <summary>
		/// Handles the KeyUp event of the AutoCompleteBox control.
		/// On Enter pressed, get videos from YouTube.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void AutoCompleteBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.PlatformKeyCode == 13) _vm.GetYouTubeVideos();
		}

		/// <summary>
		/// Handles the Click event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			// TODO: add code to open XamDialogWindow
			//var win = new About();
			//win.Show();
		}
		#endregion Event Handlers
	}
}