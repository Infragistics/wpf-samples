using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDialogWindow.Samples.Style
{
    /// <summary>
    /// Interaction logic for CustomTemplate.xaml
    /// </summary>
    public partial class CustomTemplate : SampleContainer
    {
        private EventHandler centerWindow;

        public CustomTemplate()
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
