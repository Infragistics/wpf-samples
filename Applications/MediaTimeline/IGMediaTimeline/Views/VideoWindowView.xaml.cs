using System;
using System.Windows;
using MediaTimeline.ViewModels;

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

                this.theExpander.Visibility = Visibility.Collapsed;
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
