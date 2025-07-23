using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGDragDropFramework.CustomControls;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;

namespace IGDragDropFramework.Samples.Editing
{
    public partial class DragAndDropEvents : SampleContainer
    {
        public DragAndDropEvents()
        {
            InitializeComponent();
        }

        private void DragSource_DragStart(object sender, DragDropStartEventArgs e)
        {
            UpdateOutput("DragStart");
        }

        private void DragSource_DragEnter(object sender, DragDropCancelEventArgs e)
        {
            UpdateOutput("DragEnter");
        }

        private void DragSource_DragOver(object sender, DragDropMoveEventArgs e)
        {
            UpdateOutput("DragOver");
        }

        private void DragSource_DragLeave(object sender, DragDropEventArgs e)
        {
            UpdateOutput("DragLeave");
        }

        private void DragSource_Drop(object sender, DropEventArgs e)
        {
            UpdateOutput("Drop");

            UniformStackPanel targetContainer = e.DropTarget as UniformStackPanel;
            Image draggedElement = e.OriginalDragSource as Image;

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

        private void DragSource_DragEnd(object sender, DragDropEventArgs e)
        {
            UpdateOutput("DragEnd");
        }

        private void DragSource_DragCancel(object sender, DragDropEventArgs e)
        {
            UpdateOutput("DragCancel");
        }

        #region AuxiliaryMethods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        private void UpdateOutput(string text)
        {
            TextBlockOutput.Text += text + Environment.NewLine;
            ScrollViewerOutput.UpdateLayout();
            ScrollViewerOutput.ScrollToVerticalOffset(Double.MaxValue);
        }

        private void ClearOutput()
        {
            TextBlockOutput.Text = string.Empty;
        }

        #endregion
    }
}