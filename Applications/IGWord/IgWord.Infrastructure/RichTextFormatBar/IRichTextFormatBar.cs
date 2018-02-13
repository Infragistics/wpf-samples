using Infragistics.Controls.Editors;

namespace IgWord.Infrastructure.RichTextFormatBar
{
    public interface IRichTextFormatBar
    {
        /// <summary>
        /// Represents the RichTextBox that will be the target for all text manipulations in the format bar.
        /// </summary>
        XamRichTextEditor Target { get; set; }

        /// <summary>
        /// Represents the method that will be used to update the format bar values based on the current selection.
        /// </summary>
        void UpdateVisualState();
    }
}
