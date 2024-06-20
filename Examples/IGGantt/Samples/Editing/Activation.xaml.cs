using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGGantt.Samples.Editing
{
    public partial class Activation : SampleContainer
    {
        private BitmapImage bmiCritical;
        private BitmapImage bmiNormal;

        public Activation()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            ImageFilePathProvider provider = new ImageFilePathProvider();

            string imageResourcePathCritical = ImageFilePathProvider.GetImageLocalPath("/Icons/red.png");
            string imageResourcePathNormal = ImageFilePathProvider.GetImageLocalPath("/Icons/green.png");

            bmiCritical = new BitmapImage(new Uri(imageResourcePathCritical, UriKind.Absolute));
            bmiNormal = new BitmapImage(new Uri(imageResourcePathNormal, UriKind.Absolute));
        }

        private void gantt_ActiveCellChanged(object sender, EventArgs e)
        {
            if (this.gantt.ActiveRow.HasValue)
            {
                this.txt_TaskName.Text = this.gantt.ActiveRow.Value.Task.TaskName;
                this.txt_CellRowId.Text = this.gantt.ActiveRow.Value.Task.Id.ToString();

                this.SetCriticalImage(this.gantt.ActiveRow.Value.Task.IsCritical);
            }

            if (this.gantt.ActiveColumn.HasValue)
            {
                this.txt_CellColumn.Text = this.gantt.ActiveColumn.Value.Key;
            }
        }

        private void SetCriticalImage(bool isCritical)
        {
            this.Img_Critical.Source = isCritical ? bmiCritical : bmiNormal;
        }
    }
}
