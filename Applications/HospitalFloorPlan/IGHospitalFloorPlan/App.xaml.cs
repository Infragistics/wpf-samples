//-------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Infragistics">
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

using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using IGExtensions.Framework.Controls;  // NavigationApp with error handling and culture initialization
using IGShowcase.HospitalFloorPlan.Assets.Resources;
using IGShowcase.HospitalFloorPlan.Views;

namespace IGShowcase.HospitalFloorPlan
{
    public partial class App : NavigationApp
	{
		 
		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
			Startup += ApplicationStartup;
			 
            this.InitializeComponent();
            this.InitializeCulture(Strings.Language);

            // NOTE: comment out the following code when testing Japanese culture
            //this.InitializeCulture(SupportedCultures.Japanese);
		}

		/// <summary>
		/// Handles the Startup event of the Application control.
		/// </summary>
		private void ApplicationStartup(object sender, StartupEventArgs e)
		{

#if SILVERLIGHT
            var mainPage = new MainPage();
            this.RootVisual = mainPage;

#else  // WPF
            var mainWindow = new MainWindow();
            //mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
#endif
		}
 
	}
}