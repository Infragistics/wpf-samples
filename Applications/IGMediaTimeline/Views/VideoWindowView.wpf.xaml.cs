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


using System;
using System.Windows;
using MediaTimeline.ViewModels;
using System.Windows.Controls;
using System.Text;

namespace MediaTimeline.Views
{
    public partial class VideoWindowView : Window
    {
        private string player = "<object > <param name=\"movie\" value=\"{0}\" <param name=\"allowFullScreen\" value=\"true\" <param name=\"allowScriptAccess\" value=\"always\"> <embed src=\"{0}\" type=\"application/x-shockwave-flash\" allowfullscreen=\"true\" allowScriptAccess=\"always\" width=\"100%\" height=\"100%\"></object>";

        public VideoWindowView()
        {
            InitializeComponent();
            DataContextChanged += VideoWindowView_DataContextChanged;
        }

        void VideoWindowView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {          
            VideoClipViewModel vm = (VideoClipViewModel)e.NewValue;

            string str = vm.VideoLink.ToString();

            var x = System.Environment.OSVersion.Version;

            if (x.Major == 6 && x.Minor == 1)
            {
                str = str.Replace("/v/", "/watch?v=");
                str = str.Replace(vm.VideoLink.Query.ToString(), "");

                this.htmlViewer.Navigate(str);

                this.theExpander.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.htmlViewer.NavigateToString(String.Format(player, str));

                if (vm.HtmlCode != string.Empty)
                {
                    string htmlString = "<html> <head> <meta charset='UTF-8'> </head> <body> <p>" + vm.HtmlCode + "</p> </body> </html>";
                    this.detailViewer.NavigateToString(htmlString);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.htmlViewer.NavigateToString("<html />");
            this.detailViewer.NavigateToString("<html />");
        }
    }
}
