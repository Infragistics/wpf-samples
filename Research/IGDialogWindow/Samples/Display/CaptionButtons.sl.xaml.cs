using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Display
{
    /// <summary>
    /// Interaction logic for CaptionButtons.xaml
    /// </summary>
    public partial class CaptionButtons : SampleContainer
    {
        private EventHandler centerWindow;

        public CaptionButtons()
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
            xamWindow.Top = 0;
            xamWindow.Left = 0;
            this.xamWindow.LayoutUpdated -= centerWindow;
        }
    }
}
