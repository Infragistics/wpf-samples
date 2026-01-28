using System;
using System.Windows;
using System.Windows.Media.Effects;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Display
{
    /// <summary>
    /// Interaction logic for ModalDialogs.xaml
    /// </summary>
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

        private void ModalWindow_Click(object sender, EventArgs e)
        {
            XamDialogWindow win = CreateDialogWindow();
            win.IsModal = true;
            this.windowContainer.Children.Add(win);
        }

        private void ModalWindowContainerPanel_Click(object sender, EventArgs e)
        {
            // blur the other content except XamDialogWindows
            var blurEffect = new BlurEffect() { Radius = 10 };
            blurContentContainer.Effect = blurEffect;

            XamDialogWindow win = CreateDialogWindow();
            win.IsModal = true;
            this.windowContainer.Children.Add(win);
            win.IsVisibleChanged += new DependencyPropertyChangedEventHandler(win_IsVisibleChanged);
        }

        void win_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            blurContentContainer.Effect = null;
        }
    }
}
