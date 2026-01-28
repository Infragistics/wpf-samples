using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;

namespace IGDragDropFramework.Samples.Editing
{
    public partial class DropChannels : SampleContainer
    {
        public DropChannels()
        {
            InitializeComponent();
        }

        #region Event Handlers
        #region Shared DragDrop Event Handlers
        private void OnDragStart(object sender, DragDropStartEventArgs e)
        {
            if (e.OriginalDragSource is Image)
            {
                e.Data = (e.OriginalDragSource as Image).Source;
                e.DragSnapshotElement = e.OriginalDragSource;
            }
            else
            {
                e.Cancel = true;
            }

        }
        #endregion Shared DragDrop Event Handlers

        #region SearchResults DragDrop

        private void SearchResults_Drop(object sender, DropEventArgs e)
        {
            StackPanel targetContainer = (e.DropTarget as StackPanel);
            Image draggedElement = (e.OriginalDragSource as Image);

            Border containerBorder = new Border();

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = 0.7;
            scaleTransform.ScaleY = 0.7;

            Border outerBorder = new Border()
            {
                BorderBrush = new SolidColorBrush(new Color() { A = 255, B = 86, G = 85, R = 85 }),
                BorderThickness = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2, 2, 2, 2),
                RenderTransform = scaleTransform
            };

            WriteableBitmap bitmap = new WriteableBitmap(draggedElement, null);

            Image image = new Image()
            {
                Source = bitmap,
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.None
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

        #endregion SearchResults DragDrop

        #region Lightbox DragDrop
        private void Lightbox_Drop(object sender, DropEventArgs e)
        {
            foreach (Border item in this.lightbox.Children)
            {
                Image image = (item.Child as Border).Child as Image;
                if (image == e.OriginalDragSource)
                {
                    bool stop = false;
                    foreach (StackPanel row in this.searchResults.Children)
                    {
                        if (stop) break;
                        foreach (ContentControl imgContainer in row.Children)
                        {
                            if (stop) break;
                            if (imgContainer.Content == null)
                            {
                                this.lightbox.Children.Remove(item);
                                imgContainer.Content = new Image() { Source = image.Source };
                                stop = true;
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void Trash_DragEnter(object sender, DragDropCancelEventArgs e)
        {
            e.DragTemplate = (DataTemplate)this.Resources["DraggedElementOverTarget"];
        }

        private void Trash_DragLeave(object sender, DragDropEventArgs e)
        {
            e.DragTemplate = (DataTemplate)this.Resources["DraggedElementOutOfTarget"];
        }
        #endregion Lightbox DragDrop
        #endregion Event Handlers
    }
}