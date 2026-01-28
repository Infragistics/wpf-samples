using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Style
{
    /// <summary>
    /// Interaction logic for StylingCaptionButtons.xaml
    /// </summary>
    public partial class StylingCaptionButtons : SampleContainer
    {
        private EventHandler centerWindow;

        public StylingCaptionButtons()
        {
            InitializeComponent();
            this.centerWindow = new EventHandler(xamWindow_LayoutUpdated);
  
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.xamWindow.WindowState = Infragistics.Controls.Interactions.WindowState.Normal;
            this.xamWindow.LayoutUpdated += centerWindow;
        }

        void xamWindow_LayoutUpdated(object sender, EventArgs e)
        {
            this.xamWindow.CenterDialogWindow();
            this.xamWindow.LayoutUpdated -= centerWindow;
        }
    }
}
