using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class BookmarksMargin : MarginBase
    {
        #region Constants

        private readonly string[] _afterKeys = { MarginKeys.LineNumberMargin };
        private readonly string[] _beforeKeys = {  };

        #endregion //Constants
        
        #region Fields and Properties

        // will hold the bookmarked lines' indexes
        private readonly ObservableCollection<int> _bookmarks = new ObservableCollection<int>();

        // a reference to the document view base (used to obtain visible lines and scroll to a specific line)
        private DocumentViewBase _dvb;

        // property to obtain the bookmarks (used by the sample)
        public ObservableCollection<int> Bookmarks
        {
            get
            {
                return _bookmarks;
            }
        }

        #endregion Fields and Properties

        #region Base Class Overrides

        #region Properties

        /// <summary>
        /// Returns the location of the margin within the <see cref="EditorDocumentView"/> (read only).
        /// </summary>
        /// <seealso cref="EditorDocumentViewMarginLocation"/>
        public override EditorDocumentViewMarginLocation Location
        {
            get { return EditorDocumentViewMarginLocation.Left; }
        }

        /// <summary>
        /// An array of string keys that represent the items after which the current item should be sorted.
        /// </summary>
        public override IEnumerable<string> After
        {
            get { return _afterKeys; }
        }

        /// <summary>
        /// An array of string keys that represent the items before which the current item should be sorted.
        /// </summary>
        public override IEnumerable<string> Before
        {
            get { return _beforeKeys; }
        }

        /// <summary>
        /// The string key of the current item.  This key will be referenced in the <see cref="Before"/> and <see cref="After"/> lists of other items.
        /// </summary>
        public override string Key
        {
            get { return "BookmarksMargin"; }
        }

        #endregion //Properties

        #region Methods

        /// <summary>
        /// Returns an instance of the element that represents the margin within the <see cref="EditorDocumentView"/>.
        /// </summary>
        /// <param name="documentViewBase">The <see cref="EditorDocumentView"/> for which the margin presenter should be created.</param>
        /// <returns>An instance of a FrameworkElement derived class the represents the margin in the UI.</returns>
        public override FrameworkElement CreatePresenter(DocumentViewBase documentViewBase)
        {
            _dvb = documentViewBase;
            
            return new BookmarksMarginPresenter(documentViewBase, this);
        }

        #endregion //Methods

        #endregion //Base Class Overrides

        #region Methods

        // used by the sample to scroll the editor to a specific bookmark
        public void FocusBookmarkIndex(int index)
        {
            if (_dvb == null) return;
            if (index < 0 || index >= _bookmarks.Count) return;
            _dvb.GoToLineNumber(_bookmarks[index] + 1);
        }

        #endregion //Methods
    }
}
