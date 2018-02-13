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
using IGExtensions.Framework.Controls;
using MediaTimeline.Assets.Resources;
using MediaTimeline.Views;

namespace MediaTimeline
{
    public partial class App : NavigationApp //Application
	{
		
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
			Startup += Application_Startup;

          	//SetupLocalization();
			InitializeComponent();
            this.InitializeCulture(AppStrings.Language);
        }
		#endregion Constructor
        
		#region Event Handlers
		/// <summary>
		/// Handles the Startup event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
		private void Application_Startup(object sender, StartupEventArgs e)
		{
            
#if SILVERLIGHT 
            var mainPage = new MainPage();
            //mainPage.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.RootVisual = mainPage;
#else  // WPF
            var mainWindow = new MainWindow();
            mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
#endif
		}
    
		#endregion Event Handlers
	}
}