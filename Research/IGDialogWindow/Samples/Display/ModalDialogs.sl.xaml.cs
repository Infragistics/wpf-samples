using System.Windows;
using System.Windows.Media.Effects;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Display
{
    public partial class ModalDialogs : SampleContainer
    {
        public ModalDialogs()
        {
            InitializeComponent();
        }

        private XamDialogWindow CreateDialogWindow()
        {
            return new XamDialogWindow() { Width = 350, Height = 300, StartupPosition = StartupPosition.Center };
        }

        private void ModalWindow_Click(object sender, RoutedEventArgs e)
        {
            XamDialogWindow win = CreateDialogWindow();
            win.ModalBackgroundEffect = null;
            win.IsModal = true;
            this.windowContainer.Children.Add(win);
        }

        private void ModalWindowContainerPanel_Click(object sender, RoutedEventArgs e)
        {
            XamDialogWindow win = CreateDialogWindow();
            win.ModalBackgroundEffect = new BlurEffect() { Radius = 10 };
            win.IsModal = true;
            this.windowContainer.Children.Add(win);
        }

    }
}
