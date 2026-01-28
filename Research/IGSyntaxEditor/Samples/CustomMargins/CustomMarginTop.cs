using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class CustomMarginTop : MarginBase
    {
        #region Constants

        private readonly string[] AfterKeys = { };
        private readonly string[] BeforeKeys = { };

        #endregion //Constants

        #region Base Class Overrides

        #region Properties

        /// <summary>
        /// Returns the location of the margin within the <see cref="EditorDocumentView"/> (read only).
        /// </summary>
        /// <seealso cref="EditorDocumentViewMarginLocation"/>
        public override EditorDocumentViewMarginLocation Location
        {
            get { return EditorDocumentViewMarginLocation.Top; }
        }

        /// <summary>
        /// An array of string keys that represent the items after which the current item should be sorted.
        /// </summary>
        public override IEnumerable<string> After
        {
            get { return AfterKeys; }
        }

        /// <summary>
        /// An array of string keys that represent the items before which the current item should be sorted.
        /// </summary>
        public override IEnumerable<string> Before
        {
            get { return BeforeKeys; }
        }

        /// <summary>
        /// The string key of the current item.  This key will be referenced in the <see cref="Before"/> and <see cref="After"/> lists of other items.
        /// </summary>
        public override string Key
        {
            get { return "MyCustomMarginTop"; }
        }

        #endregion //Properties

        #region Methods

        /// <summary>
        /// Returns an instance of the element that represents the margin within the <see cref="EditorDocumentView"/>.
        /// </summary>
        /// <param name="editorDocumentView">The <see cref="EditorDocumentView"/> for which the margin presenter should be created.</param>
        /// <returns>An instance of a FrameworkElement derived class the represents the margin in the UI.</returns>
        public override FrameworkElement CreatePresenter(DocumentViewBase documentViewBase)
        {
            return new CustomMarginTopPresenter(documentViewBase, this);
        }

        #endregion //Methods

        #endregion //Base Class Overrides
    }
}
