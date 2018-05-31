//-------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Infragistics">
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
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using IGExtensions.Common.Controls;
using IGShowcase.EarthQuake.Resources;
using IGShowcase.EarthQuake.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.EarthQuake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnMainWindowLoaded;
            this.ContentFrame.Navigating += OnContentFrameNavigating;
		}
        protected ColorWasher ResourceColorWasher;
        
        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.ResourceColorWasher = this.Resources["ResourceWasher2"] as ColorWasher;
        }

        private void OnContentFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void NavAppInfoDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var winInfo = new AboutView();
            var win = new XamDialogWindow
            {
                Width = winInfo.GetDesiredSize().Width,
                Height = winInfo.GetDesiredSize().Height + 80,
                StartupPosition = StartupPosition.Center,
                Content = winInfo,
                IsModal = true,
                IsResizable = false,
                Header = AppStrings.About,
            };
            this.AppInfoContainer.Children.Add(win);
        }
        private void NavColorWasherDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorWashEditor
            {
                StartupPosition = StartupPosition.Center,
                IsModal = true,
                ModalBackground = new SolidColorBrush(Colors.Transparent),
                Style = Application.Current.Resources["XamDialogWindowStyle"] as Style,
            };
            this.AppWashContainer.Children.Add(win);
            win.WashSettingsChanged += OnColorWashSettingsChanged;
        }

        private void OnColorWashSettingsChanged(object sender, ColorWashSettingsChangedEventArgs e)
        {
            var washSettings = e.ColorWashSettings;
            //if (this.ResourceWasher != null)
            //{
            //    this.ResourceWasher.WashColor = washSettings.WashColor;
            //    this.ResourceWasher.WashGroups[0].WashColor = washSettings.WashColor;
            //    this.ResourceWasher.WashMode = washSettings.WashMode;
            //    this.ResourceWasher.SourceDictionary = Application.Current.Resources;
            //    this.ResourceWasher.WashResources();
            //}

            if (this.ResourceColorWasher != null)
            {
                //this.ResourceColorWasher.WashResources(Application.Current.Resources, e.ColorWashSettings);
                this.ResourceColorWasher.WashResources(e.ColorWashSettings);
            }
        }

    }
}
