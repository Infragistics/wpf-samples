using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;

namespace IGDragDropFramework.Samples.Display
{
    public partial class AnimatedDragDrop : SampleContainer
    {
        private Point _targetLocation;
        private bool _animationAllowed = true;
        private bool _isGoingToTrash;

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
                _targetLocation =
                    ((Image)e.OriginalDragSource).TransformToVisual(App.Current.RootVisual).Transform(new Point(0, 0));
                e.OriginalDragSource.Opacity = 0;
                _isGoingToTrash = false;
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
                if (_animationAllowed && !_isGoingToTrash)
                {
                    _animationAllowed = false;
                    Animate(e, SourceContainer.SearchedImagesContainer);
                }
            }
        }

        private void OnLightboxDrop(object sender, DropEventArgs e)
        {
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
                RenderTransform = scaleTransform
            };

            Image image = new Image()
            {
                Source = e.Data as ImageSource,
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.None
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
            _targetLocation = containerBorder.TransformToVisual(App.Current.RootVisual).Transform(new Point(0, 0));
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
                _targetLocation =
                    ((Image)e.OriginalDragSource).TransformToVisual(App.Current.RootVisual).Transform(new Point(0, 0));
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
                if (_animationAllowed && !_isGoingToTrash)
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
            _isGoingToTrash = true;
        }

        private void InsertBackInSearchItems(ImageSource imageSource)
        {
            foreach (StackPanel row in this.searchResults.Children)
            {
                foreach (ContentControl item in row.Children)
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

        private object _popupContent;
        private System.Windows.Controls.Primitives.Popup _popup;
        private object _dragDropDataContext;
        private DataTemplate _dragDataTemplate;

        private void Animate(DragDropEventArgs e, SourceContainer source)
        {
            if (this._dragSourceElement != null)
                return;
           
            this._dragSourceElement = e.OriginalDragSource;
            this._sourceContainer = source;

            this._popup = DragDropManager.DragPopup;
            ContentControl contentControl = (ContentControl)DragDropManager.DragPopup.Child;
            this._popupContent = contentControl.Content;
            this._dragDropDataContext = contentControl.DataContext;
            this._dragDataTemplate = contentControl.ContentTemplate;

            DragDropManager.DragPopup.Closed += this.DragPopup_Closed;
        }

        private void DragPopup_Closed(object sender, EventArgs e)
        {
            this._popup.Closed -= this.DragPopup_Closed;

            ContentControl contentControl = new ContentControl();
            contentControl.Content = this._popupContent;
            contentControl.DataContext = this._dragDropDataContext;
            contentControl.ContentTemplate = this._dragDataTemplate;

            this._popup.Child = contentControl;
            this._popup.IsOpen = true;

            Point popupLocation = new Point(this._popup.HorizontalOffset, this._popup.VerticalOffset);

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

            Storyboard.SetTarget(AnimateX, this._popup);
            Storyboard.SetTarget(AnimateY, this._popup);

            Storyboard.SetTargetProperty(AnimateX, new PropertyPath("(HorizontalOffset)"));
            Storyboard.SetTargetProperty(AnimateY, new PropertyPath("(VerticalOffset)"));

            // The _targetLocation value is set in the DropInMyLightBox Event Handler method
            AnimateX.From = popupLocation.X;
            AnimateX.To = _targetLocation.X;
            AnimateY.From = popupLocation.Y;
            AnimateY.To = _targetLocation.Y;

            sb.Begin();

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

                this._popup.IsOpen = false;
                this._popupContent = null;
                this._popup = null;
                this._dragSourceElement = null;

                _animationAllowed = true;
            };
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