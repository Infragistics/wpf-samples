using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGDragDropFramework.CustomControls;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;

namespace IGDragDropFramework.Samples.Editing
{
    public partial class Cursors : SampleContainer
    {
        public Cursors()
        {
            InitializeComponent();
        }

        private void DragDropEventsInfo_Drop(object sender, DropEventArgs e)
        {
            UniformStackPanel targetContainer = (e.DropTarget as UniformStackPanel);
            Image draggedElement = (e.OriginalDragSource as Image);

            Border containerBorder = new Border();

            Border outerBorder = new Border()
            {
                BorderBrush = new SolidColorBrush(new Color() { A = 255, B = 86, G = 85, R = 85 }),
                BorderThickness = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5, 5, 5, 5)
            };

            Image image = new Image()
            {
                Source = draggedElement.Source,
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Stretch = Stretch.Uniform
            };

            outerBorder.Child = image;
            containerBorder.Child = outerBorder;
            targetContainer.Children.Add(containerBorder);
            ContentControl parentContentControl = draggedElement.Parent as ContentControl;
            if (parentContentControl != null)
            {
                parentContentControl.Content = null;
            }
        }

        private void DragSource_DragStart(object sender, DragDropStartEventArgs e)
        {
            if (e.OriginalDragSource is Image)
            {
                e.DragSnapshotElement = e.OriginalDragSource;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}