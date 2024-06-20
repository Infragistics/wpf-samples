using System.Windows;
using System.Windows.Controls;
using System;

namespace IGDockManager.CustomControls
{
    public class DescriptionNote : ContentControl
    {
        static DescriptionNote()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionNote), new FrameworkPropertyMetadata(typeof(DescriptionNote)));
        }

        #region Dependency Properties
        #region NoteCaption (Dependency Property)

        public static readonly DependencyProperty DescNoteCaptionProperty =
            DependencyProperty.Register("DescNoteCaption", typeof(string), typeof(DescriptionNote),
               new FrameworkPropertyMetadata((string)String.Empty));

        public string DescNoteCaption
        {
            get { return (string)GetValue(DescNoteCaptionProperty); }
            set { SetValue(DescNoteCaptionProperty, value); }
        }

        #endregion NoteCaption

        #endregion Dependency Properties
    }
}
