using System.Windows.Controls;
using Infragistics.Samples.Framework;
using IGControls = Infragistics.Controls.Interactions;

namespace IGDialogWindow.Samples.Display
{
    /// <summary>
    /// Interaction logic for DialogWindowLocation.xaml
    /// </summary>
    public partial class DialogWindowLocation : SampleContainer
    {
        private IGControls::XamDialogWindow DialogWindow = null;

        public DialogWindowLocation()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string btnName = (sender as Button).Name;

            if (DialogWindow == null)
            {
                // Create new XamDialogWindow and add it in the specified container
                DialogWindow = CreateDialogWindow();

                if (btnName.Contains("Left"))
                {
                    LeftContainer.Children.Add(DialogWindow);
                }
                else
                {
                    RightContainer.Children.Add(DialogWindow);
                }
            }
            else
            {
                if (btnName.Contains("Left"))
                {
                    if (RightContainer.Children.Contains(DialogWindow))
                    {
                        // Move the XamDialogWindow to the other container
                        RightContainer.Children.Remove(DialogWindow);
                        LeftContainer.Children.Add(DialogWindow);
                    }
                }
                else
                {
                    if (LeftContainer.Children.Contains(DialogWindow))
                    {
                        // Move the XamDialogWindow to the other container
                        LeftContainer.Children.Remove(DialogWindow);
                        RightContainer.Children.Add(DialogWindow);
                    }
                }
            }
        }

        private IGControls::XamDialogWindow CreateDialogWindow()
        {
            IGControls::XamDialogWindow dialog = new IGControls::XamDialogWindow();

            dialog.Name = "dialogWindow";
            dialog.Height = 150;
            dialog.Width = 200;
            dialog.RestrictInContainer = true;
            dialog.CloseButtonVisibility = System.Windows.Visibility.Collapsed;

            return dialog;
        }

        private void MoveWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DialogWindow != null)
            {
                double left = DialogWindow.Left;
                double up = DialogWindow.Top;

                double incrementalStep = 20;

                string btn_Name = (sender as Button).Name as string;

                if (btn_Name == "Up")
                {
                    up -= incrementalStep;
                }
                if (btn_Name == "Down")
                {
                    up += incrementalStep;
                }
                if (btn_Name == "Left")
                {
                    left -= incrementalStep;
                }
                if (btn_Name == "Right")
                {
                    left += incrementalStep;
                }

                DialogWindow.Top = up;
                DialogWindow.Left = left;
            }
        }
    }
}
