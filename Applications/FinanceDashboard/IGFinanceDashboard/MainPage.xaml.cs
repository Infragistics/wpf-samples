//-------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="Infragistics">
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
using IGShowcase.FinanceDashboard.Resources;
using IGShowcase.FinanceDashboard.ViewModels;
using IGShowcase.FinanceDashboard.Views;
using IGExtensions.Framework;
using Infragistics.Controls.Interactions;

namespace IGShowcase.FinanceDashboard
{

    public partial class MainPage : IGExtensions.Framework.Controls.NavigationPage 
    {
        #region Dependency Properties
        #region IsStockDataLoading
        public bool IsStockDataLoading
        {
            get { return (bool)GetValue(IsStockDataLoadingProperty); }
            set { SetValue(IsStockDataLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStockDataLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStockDataLoadingProperty =
            DependencyProperty.Register("IsStockDataLoading", typeof(bool), typeof(MainPage),
                                        new PropertyMetadata(true, OnDataLoadingChanged));
        #endregion

        #region IsHeatmapDataLoading
        public bool IsHeatmapDataLoading
        {
            get { return (bool)GetValue(IsHeatmapDataLoadingProperty); }
            set { SetValue(IsHeatmapDataLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHeatmapDataLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHeatmapDataLoadingProperty =
            DependencyProperty.Register("IsHeatmapDataLoading", typeof(bool), typeof(MainPage),
                                        new PropertyMetadata(true, OnDataLoadingChanged));
        #endregion

        private static void OnDataLoadingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var mainPage = dependencyObject as MainPage;
            if (mainPage != null) 
                mainPage.OnDataLoadingChanged(e);
        }

        private void OnDataLoadingChanged(DependencyPropertyChangedEventArgs e)
        {
            //LayoutRoot.IsBusy = IsStockDataLoading || IsHeatmapDataLoading;
           // this.PageBusyIndicator.IndicatorIsBusy = this.IsStockDataLoading;
            //this.PageBusyIndicator.IndicatorIsBusy = false;
            //this.PageBusyIndicator.IndicatorIsBusy = IsStockDataLoading || IsHeatmapDataLoading;
             
        }
        #endregion

        public MainPage()
        {
            InitializeComponent();
            this.Title = AppStrings.AppName;
      
            this.Loaded += OnMainPageLoaded;
           // this.PageBusyIndicator.IndicatorIsBusy = true;
       
            ViewModel = Application.Current.Resources["stockViewModel"] as StockViewModel;
            if (ViewModel != null) ViewModel.InitialDataLoaded += OnViewModelInitialDataLoaded;
        }

        void OnMainPageLoaded(object sender, RoutedEventArgs e)
        { 
        }

        protected StockViewModel ViewModel;
        void OnViewModelInitialDataLoaded(object sender, System.EventArgs e)
        {
            this.IsStockDataLoading = false;
        }

        private void ContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            DebugManager.LogError(e.Exception);
            e.Handled = true;
            //ErrorWindow.CreateNew(e.Exception, e.Uri);
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
                              RestrictInContainer = true,
                              Style = Application.Current.Resources["AppDialogWindowStyle"] as Style
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
                              Style = Application.Current.Resources["AppDialogWindowStyle"] as Style,
                              RestrictInContainer = true
                          };
            this.AppWashContainer.Children.Add(win);
            win.WashSettingsChanged += OnColorWashSettingsChanged;
        }
        private void OnColorWashSettingsChanged(object sender, ColorWashSettingsChangedEventArgs e)
        {
            var washSettings = e.ColorWashSettings;
            if (this.ResourceWasher != null)
            {
                this.ResourceWasher.WashColor = washSettings.WashColor;
                this.ResourceWasher.WashGroups[0].WashColor = washSettings.WashColor;
                this.ResourceWasher.WashMode = washSettings.WashMode;
                this.ResourceWasher.SourceDictionary = Application.Current.Resources;
                this.ResourceWasher.WashResources();
            }
        }
        private void ShareLinkPanel_ShareLinkRequested(object sender, EventArgs e)
        {

        }
        private void NavShareButton_Click(object sender, RoutedEventArgs e)
        {
        }


    }
}
