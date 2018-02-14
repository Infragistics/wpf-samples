using Infragistics.Controls.Editors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace IgWord.Infrastructure.RichTextFormatBar
{
    public class RichTextBoxFormatBarManager : DependencyObject
    {
        #region Members

        private XamRichTextEditor _richTextBox;
        private UIElementAdorner<Control> _adorner;
        private IRichTextFormatBar _toolbar;

        #endregion //Members

        #region Properties

        public static readonly DependencyProperty FormatBarProperty = DependencyProperty.RegisterAttached("FormatBar", typeof(IRichTextFormatBar), typeof(RichTextBoxFormatBarManager), new PropertyMetadata(null, OnFormatBarPropertyChanged));
        public static void SetFormatBar(UIElement element, IRichTextFormatBar value)
        {
            element.SetValue(FormatBarProperty, value);
        }
        public static IRichTextFormatBar GetFormatBar(UIElement element)
        {
            return (IRichTextFormatBar)element.GetValue(FormatBarProperty);
        }

        private static void OnFormatBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rtb = d as XamRichTextEditor;
            if (rtb == null)
                throw new Exception("A FormatBar can only be applied to a xamRichTextEditor.");

            RichTextBoxFormatBarManager manager = new RichTextBoxFormatBarManager();
            manager.AttachFormatBarToRichtextBox(rtb, e.NewValue as IRichTextFormatBar);
        }

        public bool IsAdornerVisible
        {
            get { return _adorner.Visibility == Visibility.Visible; }
        }

        #endregion //Properties

        #region Event Handlers

        void RichTextBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Released)
            {
                if (_richTextBox.Selection.Text.Length > 0 && !String.IsNullOrWhiteSpace(_richTextBox.Selection.Text))
                    ShowAdorner();
                else
                    HideAdorner();

                e.Handled = true;
            }
            else
                HideAdorner();
        }

        void RichTextBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //if the mouse moves outside the richtextbox bounds hide the adorner
            //though this deosn't always work, especially if the user moves the mouse very quickly.
            //need to find a better solution, but this will work for now.
            Point p = e.GetPosition(_richTextBox);
            if (p.X <= 5.0 || p.X >= _richTextBox.ActualWidth - 5 || p.Y <= 3.0 || p.Y >= _richTextBox.ActualHeight - 3)
                HideAdorner();
        }

        private void RichTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                HideAdorner();
            }
        }

        #endregion //Event Handlers

        #region Methods

        /// <summary>
        /// Attaches a FormatBar to a RichtextBox
        /// </summary>
        /// <param name="richTextBox">The RichtextBox to attach to.</param>
        /// <param name="formatBar">The Formatbar to attach.</param>
        private void AttachFormatBarToRichtextBox(XamRichTextEditor richTextBox, IRichTextFormatBar formatBar)
        {
            _richTextBox = richTextBox;
            _richTextBox.MouseLeftButtonUp += RichTextBox_MouseLeftButtonUp;
            _richTextBox.PreviewMouseMove += RichTextBox_PreviewMouseMove;
            _richTextBox.IsVisibleChanged += RichTextBox_IsVisibleChanged;

            _adorner = new UIElementAdorner<Control>(_richTextBox);
            formatBar.Target = _richTextBox;
            _toolbar = formatBar;
        }

        /// <summary>
        /// Shows the FormatBar
        /// </summary>
        void ShowAdorner()
        {
            if (_adorner.Visibility == Visibility.Visible)
                return;

            VerifyAdornerLayer();

            Control adorningEditor = _toolbar as Control;

            if (_adorner.Child == null)
                _adorner.Child = adorningEditor;

            _toolbar.UpdateVisualState(); //let's tell the toolbar to update it's visual state

            _adorner.Visibility = Visibility.Visible;

            PositionFormatBar(adorningEditor);
        }

        /// <summary>
        /// Positions the FormatBar so that is does not go outside the bounds of the RichTextBox or covers the selected text
        /// </summary>
        /// <param name="adorningEditor"></param>
        private void PositionFormatBar(Control adorningEditor)
        {
            Point mousePosition = Mouse.GetPosition(_richTextBox);

            double left = mousePosition.X;
            double top = (mousePosition.Y - 15) - adorningEditor.ActualHeight;

            //top
            if (top < 0)
            {
                top = mousePosition.Y + 10;
            }

            //right boundary
            if (left + adorningEditor.ActualWidth > _richTextBox.ActualWidth - 20)
            {
                left = left - (adorningEditor.ActualWidth - (_richTextBox.ActualWidth - left));
            }

            _adorner.SetOffsets(left, top);
        }

        /// <summary>
        /// Ensures that the IRichTextFormatBar is in the adorner layer.
        /// </summary>
        /// <returns>True if the IRichTextFormatBar is in the adorner layer, else false.</returns>
        bool VerifyAdornerLayer()
        {
            if (_adorner.Parent != null)
                return true;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(_richTextBox);
            if (layer == null)
                return false;

            layer.Add(_adorner);
            return true;
        }

        /// <summary>
        /// Hides the IRichTextFormatBar that is in the adornor layer.
        /// </summary>
        void HideAdorner()
        {
            if (IsAdornerVisible)
            {
                _adorner.Visibility = Visibility.Collapsed;
            }
        }

        #endregion //Methods
    }
}
