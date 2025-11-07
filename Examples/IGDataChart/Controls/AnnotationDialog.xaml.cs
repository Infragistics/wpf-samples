using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGDataChart.Controls
{
    /// <summary>
    /// Interaction logic for AnnotationDialog.xaml
    /// </summary>
    public partial class AnnotationDialog : Window
    {
        public bool ShouldCommit { get; set; }
        public UserAnnotationInformation AnnotationInformation { get; internal set; }

        public AnnotationDialog()
        {
            InitializeComponent();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            ShouldCommit = true;
            AnnotationInformation.Label = LabelTextBox.Text;
            AnnotationInformation.AnnotationData = InputTextBox.Text;
            AnnotationInformation.MainColor = mainColor.Value;
            AnnotationInformation.BadgeColor = badgeColor.Value;

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
