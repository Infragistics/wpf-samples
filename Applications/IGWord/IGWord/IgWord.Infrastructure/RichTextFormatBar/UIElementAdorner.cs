using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace IgWord.Infrastructure.RichTextFormatBar
{
    /// <summary>
    /// An adorner that can display one and only one UIElement.  
    /// That element can be a panel, which contains multiple other elements.
    /// The element is added to the adorner's visual and logical trees, enabling it to 
    /// particpate in dependency property value inheritance, amongst other things.
    /// </summary>
    public class UIElementAdorner<T> : Adorner where T : UIElement
    {
        #region Fields

        VisualCollection _visuals;
        T _child = null;
        double _offsetLeft = 0;
        double _offsetTop = 0;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="adornedElement">The element to which the adorner will be bound.</param>
        public UIElementAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            _visuals = new VisualCollection(this);
        }

        #endregion // Constructor

        #region Properties

        #region Child

        /// <summary>
        /// Gets/sets the child element hosted in the adorner.
        /// </summary>
        public T Child
        {
            get { return _child; }
            set
            {
                if (value == _child)
                    return;

                if (_child != null)
                {
                    if (_visuals.Contains(_child))
                        _visuals.Remove(_child);
                }

                _child = value;

                if (_child != null)
                {
                    var visualParentAdorner = VisualTreeHelper.GetParent(_child) as UIElementAdorner<T>;
                    if (visualParentAdorner != null)
                        visualParentAdorner._visuals.Remove(_child);

                    if (!_visuals.Contains(_child))
                        _visuals.Add(_child); ;
                }
            }
        }

        #endregion // Properties

        #region GetDesiredTransform

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(_offsetLeft, _offsetTop));
            return result;
        }

        #endregion // GetDesiredTransform

        #region OffsetLeft

        /// <summary>
        /// Gets/sets the horizontal offset of the adorner.
        /// </summary>
        public double OffsetLeft
        {
            get { return _offsetLeft; }
            set
            {
                _offsetLeft = value;
                UpdateLocation();
            }
        }

        #endregion // OffsetLeft

        #region SetOffsets

        /// <summary>
        /// Updates the location of the adorner in one atomic operation.
        /// </summary>
        public void SetOffsets(double left, double top)
        {
            _offsetLeft = left;
            _offsetTop = top;
            this.UpdateLocation();
        }

        #endregion // SetOffsets

        #region OffsetTop

        /// <summary>
        /// Gets/sets the vertical offset of the adorner.
        /// </summary>
        public double OffsetTop
        {
            get { return _offsetTop; }
            set
            {
                _offsetTop = value;
                UpdateLocation();
            }
        }

        #endregion // OffsetTop

        #endregion // Public Interface

        #region Protected Overrides

        protected override Size MeasureOverride(Size constraint)
        {
            if (_child == null)
                return base.MeasureOverride(constraint);

            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_child == null)
                return base.ArrangeOverride(finalSize);

            _child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }

        #endregion // Protected Overrides

        #region Private Helpers

        void UpdateLocation()
        {
            AdornerLayer adornerLayer = base.Parent as AdornerLayer;
            if (adornerLayer != null)
                adornerLayer.Update(base.AdornedElement);
        }

        #endregion // Private Helpers
    }
}
