using System.Windows;
using System.Windows.Controls;
using IGDialogWindow.Resources;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Display
{
    /// <summary>
    /// Interaction logic for ShowingTheDialog.xaml
    /// </summary>
    public partial class ShowingTheDialog : SampleContainer
    {
        XamDialogWindow winMB;

        public ShowingTheDialog()
        {
            InitializeComponent();
  
            
        }

        private XamDialogWindow CreateDialogWindow()
        {
            return new XamDialogWindow()
            {
                Width = 350, Height = 300,
                StartupPosition = StartupPosition.Center
            };
        }

        private void NoModalWindow_Click(object sender, RoutedEventArgs e)
        {
            XamDialogWindow win = CreateDialogWindow();
            this.windowContainer.Children.Add(win);
        }

        private void NoModalWindowMessageBox_Click(object sender, RoutedEventArgs e)
        {
            winMB = new Infragistics.Controls.Interactions.XamDialogWindow() { Width = 350, Height = 300, Header = DialogWindowStrings.XDW_ShowDialog_Prompt, StartupPosition = StartupPosition.Center };

            Button ok = new Button() { Content = DialogWindowStrings.XDW_ShowDialog_ButtonOK, Width = 100, Height = 25 };
            Button cancel = new Button() { Content = DialogWindowStrings.XDW_ShowDialog_ButtonCancel, Width = 100, Height = 25 };
            ok.Click += new RoutedEventHandler(ok_Click);
            cancel.Click += new RoutedEventHandler(cancel_Click);

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(ok);
            panel.Children.Add(cancel);
            panel.VerticalAlignment = VerticalAlignment.Bottom;
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.Margin = new Thickness(5);

            winMB.Content = panel;
            winMB.IsModal = true;
            winMB.StartupPosition = StartupPosition.Center;
            this.windowContainer.Children.Add(winMB);
        }

        void cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DialogWindowStrings.XDW_MessageBox_Closed);
            this.winMB.Close();
        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DialogWindowStrings.XDW_MessageBox_OK);
            this.winMB.Close();
        }
    }
}
