using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;

namespace IGDragDropFramework.Samples.Display
{
    public partial class AnimatedDragDrop : SampleContainer
    {
        private Point _targetLocation;
        private Boolean _animationAllowed = true;
       
        public AnimatedDragDrop()
        {
            InitializeComponent();
        }

        #region SearchedImagesDragHandlers

        private void OnSearchedResultsDragStart(object sender, DragDropStartEventArgs e)
        {
            if (e.OriginalDragSource is Image && _animationAllowed)
            {
                e.Data = (e.OriginalDragSource as Image).Source;
                e.DragTemplate = this.Resources["DraggedImage"] as DataTemplate;
                _targetLocation = e.OriginalDragSource.PointToScreen(new Point(0, 0));
                e.OriginalDragSource.Opacity = 0;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OnSearchResultsDragEnd(object sender, DragDropEventArgs e)
        {
            if (e.OriginalDragSource is Image)
            {
                if (_animationAllowed)
                {
                    _animationAllowed = false;
                    Animate(e, SourceContainer.SearchedImagesContainer);
                }
            }
        }

        private void OnLightboxDrop(object sender, DropEventArgs e)
        {
            Image draggedElement = e.OriginalDragSource as Image;
            // Create a Container for the Item
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
                LayoutTransform = scaleTransform
            };

            Image image = new Image()
            {
                Source = e.Data as ImageSource,
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform
            };

            // Build the Container Hierarchy and Add to the DropTarget
            outerBorder.Child = image;
            containerBorder.Child = outerBorder;
            (e.DropTarget as StackPanel).Children.Add(containerBorder);

            // Get the Index of the Newly Added Item
            int index = (e.DropTarget as StackPanel).Children.Count - 1;

            // Hide the Current Element
            containerBorder.Opacity = 0;

            // Define the Target Location to Animate this Item To
            //   --> (used by the Animate() method)

            _targetLocation = containerBorder.PointToScreen(new Point(0, 0));
            _targetLocation.X += index * ((e.DropTarget as StackPanel).Children[0] as Border).ActualWidth;


            // Remove Element from Search Results
            ContentControl parentContentControl = (e.OriginalDragSource as Image).Parent as ContentControl;
            if (parentContentControl != null)
            {
                parentContentControl.Content = null;
            }
        }

        #endregion

        #region MyLightboxImages

        private void OnLightboxDragStart(object sender, DragDropStartEventArgs e)
        {
            if (e.OriginalDragSource is Image)
            {
                e.Data = (e.OriginalDragSource as Image).Source;
                e.DragTemplate = this.Resources["DraggedImage"] as DataTemplate;
                _targetLocation = e.OriginalDragSource.PointToScreen(new Point(0, 0));
                Border internalBorder = VisualTreeHelper.GetParent(e.OriginalDragSource as Image) as Border;
                Border outerborder = VisualTreeHelper.GetParent(internalBorder) as Border;
                outerborder.Opacity = 0;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OnLightboxDragEnd(object sender, DragDropEventArgs e)
        {
            if (e.OriginalDragSource is Image)
            {
                if (_animationAllowed)
                {
                    _animationAllowed = false;
                    Animate(e, SourceContainer.MyLightboxContainer);
                }
            }
        }

        private void OnTrashDrop(object sender, DropEventArgs e)
        {
            if (e.OriginalDragSource is Image)
            {
                foreach (Border item in this.lightbox.Children)
                {
                    if (((item.Child as Border).Child as Image) == e.OriginalDragSource)
                    {
                        InsertBackInSearchItems(e.Data as ImageSource);
                        this.lightbox.Children.Remove(item);
                        break;
                    }
                }
            }
        }

        private void InsertBackInSearchItems(ImageSource imageSource)
        {
            foreach (WrapPanel wrapPanel in this.searchResults.Children)
            {
                foreach (ContentControl item in wrapPanel.Children)
                {
                    if (item.Content == null)
                    {
                        item.Content = new Image() { Source = imageSource };
                        return;
                    }
                }
            }
        }

        #endregion

        #region Movement Animation

        private UIElement _dragSourceElement;
        private SourceContainer _sourceContainer;
        private System.Windows.Controls.Primitives.Popup _popup;
        private UIElement _placementTarget;
        private object _popupContent;
        private object _dragDropDataContext;
        private DataTemplate _dragDataTemplate;

        private void Animate(DragDropEventArgs e, SourceContainer source)
        {
            if (this._dragSourceElement != null)
                return;

            this._dragSourceElement = e.OriginalDragSource;
            this._sourceContainer = source;

            this._popup = (Popup)DragDropManager.DragPopup;
            this._placementTarget = ((Popup)DragDropManager.DragPopup).PlacementTarget;

            ContentControl contentControl = (ContentControl)((Popup)DragDropManager.DragPopup).Child;
            this._popupContent = contentControl.Content;
            this._dragDropDataContext = contentControl.DataContext;
            this._dragDataTemplate = contentControl.ContentTemplate;

            ((Popup)DragDropManager.DragPopup).Closed += this.DragPopup_Closed;
        }

        private void DragPopup_Closed(object sender, EventArgs e)
        {
            this._popup.Closed -= this.DragPopup_Closed;

            ContentControl contentControl = new ContentControl();
            contentControl.Content = this._popupContent;
            contentControl.DataContext = this._dragDropDataContext;
            contentControl.ContentTemplate = this._dragDataTemplate;

            // In WPF, reopening a popup in response to its Closed event will throw an exception.
            // Instead, we create a new popup with the same characteristics.
            System.Windows.Controls.Primitives.Popup popup = new System.Windows.Controls.Primitives.Popup();

            Popup dragPopup = this._popup;
            dragPopup.PlacementTarget = this._placementTarget;
            popup.HorizontalOffset = dragPopup.HorizontalOffset;
            popup.VerticalOffset = dragPopup.VerticalOffset;

            popup.Child = contentControl;
            popup.IsOpen = true;

            Point popupLocation = dragPopup.PlacementTarget.PointToScreen(
                new Point(
                    dragPopup.HorizontalOffset,
                    dragPopup.VerticalOffset));

            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));

            DoubleAnimation AnimateX = new DoubleAnimation();
            DoubleAnimation AnimateY = new DoubleAnimation();

            AnimateX.Duration = duration;
            AnimateX.EasingFunction = new ExponentialEase() { Exponent = 5 };
            AnimateY.Duration = duration;
            AnimateY.EasingFunction = new ExponentialEase() { Exponent = 5 };

            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(AnimateX);
            sb.Children.Add(AnimateY);

            Storyboard.SetTarget(AnimateX, popup);
            Storyboard.SetTarget(AnimateY, popup);

            Storyboard.SetTargetProperty(AnimateX, new PropertyPath("(HorizontalOffset)"));
            Storyboard.SetTargetProperty(AnimateY, new PropertyPath("(VerticalOffset)"));

            // The _targetLocation value is set in the DropInMyLightBox Event Handler method
            AnimateX.From = popupLocation.X;
            AnimateX.To = _targetLocation.X;
            AnimateY.From = popupLocation.Y;
            AnimateY.To = _targetLocation.Y;

            sb.Completed += (object sender2, EventArgs ee) =>
            {
                if (this._sourceContainer == SourceContainer.MyLightboxContainer)
                {
                    Border internalBorder = VisualTreeHelper.GetParent(this._dragSourceElement as Image) as Border;
                    Border outerborder = VisualTreeHelper.GetParent(internalBorder) as Border;
                    outerborder.Opacity = 1;
                }

                if (this._sourceContainer == SourceContainer.SearchedImagesContainer)
                {
                    this._dragSourceElement.Opacity = 1;
                    for (int i = 0; i < lightbox.Children.Count; i++)
                    {
                        Border item = (lightbox.Children[i] as Border);
                        if (((item.Child as Border).Child as Image).Source == (this._dragSourceElement as Image).Source)
                        {
                            item.Opacity = 1;
                            break;
                        }
                    }
                }

                popup.IsOpen = false;
                this._dragSourceElement = null;

                _animationAllowed = true;
            };

            sb.Begin();
        }

        #endregion

        #region Trash events

        private void OnTrashDragEnter(object sender, DragDropCancelEventArgs e)
        {
            e.DragTemplate = (DataTemplate)this.Resources["DraggedElementOverTarget"];
        }

        private void OnTrashDragLeave(object sender, DragDropEventArgs e)
        {
            e.DragTemplate = (DataTemplate)this.Resources["DraggedElementOutOfTarget"];
        }

        #endregion
    }

    public enum SourceContainer
    {
        MyLightboxContainer,
        SearchedImagesContainer
    }
}