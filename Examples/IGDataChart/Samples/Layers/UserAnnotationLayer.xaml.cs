using IGDataChart.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
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
using System.Windows.Shapes;

namespace IGDataChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for UserAnnotationLayer.xaml
    /// </summary>
    public partial class UserAnnotationLayer : SampleContainer
    {
        public UserAnnotationLayer()
        {
            InitializeComponent();
        }

        private void Chart_UserAnnotationInformationRequested(object sender, Infragistics.Controls.Charts.UserAnnotationInformationEventArgs args)
        {
            var loc = new Point(args.AnnotationInfo.DialogSuggestedXLocation, args.AnnotationInfo.DialogSuggestedYLocation);
            var dialog = new AnnotationDialog()
            {             
                WindowStartupLocation = WindowStartupLocation.Manual,
                Width = 400,
                Height = 400,
                Left = loc.X,
                Top = loc.Y,
                AnnotationInformation = args.AnnotationInfo
            };
            dialog.Closed += Dialog_Closed;

            dialog.Show();
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var d = (AnnotationDialog)sender;
            if (d.ShouldCommit)
            {
                Chart.FinishAnnotationFlow(d.AnnotationInformation);
            }
            else
            {
                Chart.CancelAnnotationFlow(d.AnnotationInformation.AnnotationId);
            }
        }

        private void Chart_UserAnnotationToolTipContentUpdating(object sender, Infragistics.Controls.Charts.UserAnnotationToolTipContentUpdatingEventArgs args)
        {
            UserAnnotationInformation info = args.AnnotationInfo as UserAnnotationInformation;
            ContentControl container = (ContentControl)args.Content;
            container.Content = info.AnnotationData;
        }
    }
}
