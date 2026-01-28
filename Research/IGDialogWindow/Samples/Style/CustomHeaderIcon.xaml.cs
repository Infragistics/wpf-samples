using System;
using System.Windows;
using System.Windows.Media.Effects;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Style
{
    /// <summary>
    /// Interaction logic for CustomHeaderIcon.xaml
    /// </summary>
    public partial class CustomHeaderIcon : SampleContainer
    {
        private EventHandler centerWindow;

        public CustomHeaderIcon()
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

        private Infragistics.Controls.Interactions.XamDialogWindow CreateDialogWindow()
        {
            return new Infragistics.Controls.Interactions.XamDialogWindow() { Width = 350, Height = 300, StartupPosition = StartupPosition.Center };
        }

        private void ModalWindow_Click(object sender, EventArgs e)
        {
            Infragistics.Controls.Interactions.XamDialogWindow win = CreateDialogWindow();
            win.ModalBackgroundEffect = null;
            win.IsModal = true;
            this.windowContainer.Children.Add(win);
        }

        private void ModalWindowContainerPanel_Click(object sender, EventArgs e)
        {
            Infragistics.Controls.Interactions.XamDialogWindow win = CreateDialogWindow();
            win.ModalBackgroundEffect = new BlurEffect() { Radius = 10 };
            win.IsModal = true;
            this.windowContainer.Children.Add(win);
        }
    }
}
