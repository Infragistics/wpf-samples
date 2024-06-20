using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace IGDragDropFramework.CustomControls
{
    public class XamMenuItemSeparator : ItemSeparatorBase
    {
        private Orientation _orientation = Orientation.Vertical;

        public XamMenuItemSeparator(Color separatorColor) :
            base(separatorColor)
        {
        }

        protected override bool ArrangePopup(ItemsControl itemsControl, Point mousePosition)
        {
            if (!base.ArrangePopup(itemsControl, mousePosition))
            {
                if (this.DropIndex > 0)
                {
                    this.DropIndex = 0;
                    OnPropertyChanged("DropIndex");
                    return false;
                }
            }

            Popup popup = GetVisualChild<Popup>(itemsControl);
            ItemsPresenter itemsPresenter = GetVisualChild<ItemsPresenter>(popup.Child);
            if (itemsPresenter == null)
            {
                if (this.DropIndex != 0)
                {
                    this.DropIndex = 0;
                    OnPropertyChanged("DropIndex");
                }

                this.Popup.IsOpen = false;
                return false;
            }

            Panel panel = GetVisualChild<VirtualizingStackPanel>(itemsPresenter);

            if (panel != null)
            {
                this._orientation = ((VirtualizingStackPanel)panel).Orientation;
            }
            else
            {
                panel = GetVisualChild<StackPanel>(itemsPresenter);
                if (panel != null)
                {
                    this._orientation = ((StackPanel)panel).Orientation;
                }
            }

            double zoomFactor = App.Current.Host.Content.ZoomFactor;

            if (ItemContainer != null)
            {
                if (this._orientation == Orientation.Vertical)
                {
                    if (mousePosition.Y >= ItemContainerPosition.Y + (((FrameworkElement)ItemContainer).ActualHeight / 2))
                    {
                        Popup.VerticalOffset = (ItemContainerPosition.Y + ((FrameworkElement)ItemContainer).ActualHeight) * zoomFactor;
                        DropIndex = itemsControl.ItemContainerGenerator.IndexFromContainer(ItemContainer) + 1;
                    }
                    else
                    {
                        Popup.VerticalOffset = ItemContainerPosition.Y * zoomFactor;
                        DropIndex = itemsControl.ItemContainerGenerator.IndexFromContainer(ItemContainer);
                    }

                    Popup.HorizontalOffset = ItemContainerPosition.X * zoomFactor;

                    PopupContent.Width = ((FrameworkElement)ItemContainer).ActualWidth;
                    PopupContent.Height = 2;

                    SeparatorMetrics.Width = ((FrameworkElement)ItemContainer).ActualWidth;
                    SeparatorMetrics.Height = ((FrameworkElement)ItemContainer).ActualHeight;
                }
                else
                {
                    if (mousePosition.X >= ItemContainerPosition.X + (((FrameworkElement)ItemContainer).ActualWidth / 2))
                    {
                        Popup.HorizontalOffset = (ItemContainerPosition.X + ((FrameworkElement)ItemContainer).ActualWidth) * zoomFactor;
                        DropIndex = itemsControl.ItemContainerGenerator.IndexFromContainer(ItemContainer) + 1;
                    }
                    else
                    {
                        Popup.HorizontalOffset = ItemContainerPosition.X * zoomFactor;
                        DropIndex = itemsControl.ItemContainerGenerator.IndexFromContainer(ItemContainer);
                    }

                    Popup.VerticalOffset = ItemContainerPosition.Y * zoomFactor;

                    PopupContent.Height = ((FrameworkElement)ItemContainer).ActualHeight;
                    PopupContent.Width = 2;

                    // switch the dimensions
                    SeparatorMetrics.Width = ((FrameworkElement)ItemContainer).ActualHeight;
                    SeparatorMetrics.Height = ((FrameworkElement)ItemContainer).ActualHeight;
                }

                this.Popup.Child.RenderTransform = new ScaleTransform() { ScaleX = zoomFactor, ScaleY = zoomFactor };

                OnPropertyChanged("DropIndex");
            }

            return true;
        }
    }

    public interface IItemSeparator
    {
        /// <summary>
        /// Arranges items separator popup element.
        /// </summary>
        /// <param name="parentControl">Tracked ItemsControl.</param>
        /// <param name="mousePosition">Mouse pointer position relative to visual root element.</param>
        void Show(ItemsControl parentControl, Point mousePosition);

        /// <summary>
        /// Gets popup element that visualizes separator
        /// </summary>
        Popup SeparatorPopup
        {
            get;
        }

        DataTemplate SeparatorTemplate
        {
            get;
            set;
        }

        int DropIndex
        {
            get;
        }
    }

    public static class ItemsSeparator
    {
        public static void ShowSeparator(ItemsControl parentControl, Point mousePosition, IItemSeparator separator)
        {
            ItemsSeparator.SetSeparator(parentControl, separator);
            separator.Show(parentControl, mousePosition);
        }

        public static void ShowSeparator(ItemsControl parentControl, Point mousePosition)
        {
            IItemSeparator separator = ItemsSeparator.GetSeparator(parentControl);
            if (separator != null)
            {
                separator.Show(parentControl, mousePosition);
            }
        }

        #region Properties

        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.RegisterAttached(
            "Separator",
            typeof(IItemSeparator),
            typeof(ItemsSeparator),
            new PropertyMetadata(new PropertyChangedCallback(OnSeparatorChanged)));

        private static void OnSeparatorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                IItemSeparator oldSeparator = e.OldValue as IItemSeparator;
                if (oldSeparator != null)
                {
                    oldSeparator.SeparatorPopup.IsOpen = false;
                }
            }
        }

        public static IItemSeparator GetSeparator(DependencyObject obj)
        {
            return (IItemSeparator)obj.GetValue(SeparatorProperty);
        }

        public static void SetSeparator(DependencyObject obj, IItemSeparator value)
        {
            obj.SetValue(SeparatorProperty, value);
        }

        #endregion // Properties
    }

    public class SeparatorMetrics : DependencyObject
    {
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(SeparatorMetrics), null);

        public double Width
        {
            get
            {
                return (double)GetValue(WidthProperty);
            }

            set
            {
                SetValue(WidthProperty, value);
            }
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(SeparatorMetrics), null);

        public double Height
        {
            get
            {
                return (double)GetValue(HeightProperty);
            }

            set
            {
                SetValue(HeightProperty, value);
            }
        }
    }

    public abstract class ItemSeparatorBase : IItemSeparator, INotifyPropertyChanged
    {
        #region Members

        #region Private Memebers

        private Color _separatorColor = Colors.Orange;
        private ContentControl _cc;
        private DependencyObject _itemContainer;
        private Point _itemContainerPosition;
        private Popup _popup;
        private System.Windows.Controls.Grid _content;
        private int _dropIndex = -1;
        private SeparatorMetrics _metrics = new SeparatorMetrics { Width = 0, Height = 0 };

        #endregion // Private Memebers

        #endregion // Members

        #region Properties

        #region Public Properties

        public Color SeparatorColor
        {
            get
            {
                return this._separatorColor;
            }

            set
            {
                this._separatorColor = value;
            }
        }

        #endregion // Public Properties

        #region Protected Properties

        protected Popup Popup
        {
            get
            {
                return this._popup;
            }

            set
            {
                this._popup = value;
            }
        }

        protected DependencyObject ItemContainer
        {
            get
            {
                return this._itemContainer;
            }

            set
            {
                this._itemContainer = value;
            }
        }

        protected Point ItemContainerPosition
        {
            get
            {
                return this._itemContainerPosition;
            }

            set
            {
                this._itemContainerPosition = value;
            }
        }

        protected int DropIndex
        {
            get
            {
                return this._dropIndex;
            }

            set
            {
                this._dropIndex = value;
            }
        }

        protected System.Windows.Controls.Grid PopupContent
        {
            get
            {
                return this._content;
            }
        }

        protected SeparatorMetrics SeparatorMetrics
        {
            get
            {
                return this._metrics;
            }
        }

        #endregion // Protected Properties

        #endregion // Properties

        #region Constructors

        protected ItemSeparatorBase(Color separatorColor)
        {
            this._separatorColor = separatorColor;

            this._popup = new Popup();

            this._cc = new ContentControl();

            this._content = new System.Windows.Controls.Grid { Background = new SolidColorBrush(this.SeparatorColor) };
            this._cc.Content = this._content;
            this._cc.DataContext = this._metrics;

            this._popup.Child = this._cc;
        }

        #endregion // Contructors

        #region Methods

        /// <summary>
        /// Returns whether is possible this separator to be arranged.
        /// </summary>
        /// <param name="itemsControl">The <see cref="ItemsControl"/> which items are going to be separated.</param>
        /// <param name="mousePosition">Mouse pointer position relative to Application.Current.RootVisual element.</param>
        /// <returns></returns>
        protected virtual bool ArrangePopup(ItemsControl itemsControl, Point mousePosition)
        {
            this._itemContainer = null;
            this._itemContainerPosition = new Point(0, 0);

            // get right root element because it is possible it to be into a Popup
            DependencyObject rootElement = VisualTreeHelper.GetParent(itemsControl);
            UIElement rootElementInstance = null;

            while (rootElement != null)
            {
                if (rootElement is UIElement)
                {
                    rootElementInstance = rootElement as UIElement;
                }

                rootElement = VisualTreeHelper.GetParent(rootElement);
            }

            if (rootElementInstance == null)
            {
                return false;
            }

            IEnumerable<UIElement> targets = VisualTreeHelper.FindElementsInHostCoordinates(mousePosition, rootElementInstance);
            List<UIElement> targetsList = new List<UIElement>(targets);

            List<DependencyObject> containers = new List<DependencyObject>();
            foreach (object item in itemsControl.Items)
            {
                DependencyObject container = itemsControl.ItemContainerGenerator.ContainerFromItem(item);
                containers.Add(container);
            }

            if (containers.Count > 0)
            {
                foreach (DependencyObject container in containers)
                {
                    if (targetsList.Contains(container as UIElement))
                    {
                        this._itemContainer = container;

                        GeneralTransform gt = ((UIElement)container).TransformToVisual(Application.Current.RootVisual);
                        this._itemContainerPosition = gt.Transform(new Point(0, 0));

                        break;
                    }
                }

                // it is very important to open or close popup just when it is needed
                if (this._itemContainer != null)
                {
                    this._popup.IsOpen = true;
                    return true;
                }

                this._popup.IsOpen = false;
            }

            return false;
        }

        public static T GetVisualChild<T>(DependencyObject parent) where T : class
        {
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null)
                {
                    if (child is T)
                    {
                        result = child as T;
                        break;
                    }

                    result = GetVisualChild<T>(child);
                    if (result != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        #endregion // Methods

        #region ISeparator Members

        public void Show(ItemsControl parentControl, Point mousePosition)
        {
            if (parentControl == null)
            {
                Debug.WriteLine("FAIL: ListBoxSeparator.Arrange(parentControl, mousePosition), parentControl cannot be null.");
                throw new ArgumentNullException("parentControl");
            }

            this.ArrangePopup(parentControl, mousePosition);
        }

        public Popup SeparatorPopup
        {
            get
            {
                return this._popup;
            }
        }

        public DataTemplate SeparatorTemplate
        {
            get
            {
                return this._cc.ContentTemplate;
            }

            set
            {
                this._cc.ContentTemplate = value;
            }
        }

        int IItemSeparator.DropIndex
        {
            get
            {
                return this.DropIndex;
            }
        }

        #endregion // ISeparator Members

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged Members
    }
}