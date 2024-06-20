using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class BookmarksMarginPresenter : Panel
    {
        private DocumentViewBase documentViewBase;
        private BookmarksMargin margin;
        private BitmapImage bookmarkImage;

        public BookmarksMarginPresenter(DocumentViewBase dvb, BookmarksMargin m)
        {
            this.documentViewBase = dvb;
            this.margin = m;

            // attach handler for the mouse down button (will toggle bookmarks)
            this.MouseLeftButtonDown += new MouseButtonEventHandler(BookmarksMarginPresenter_MouseLeftButtonDown);

            // attach handler to the layout change event (to force repain the margin when scrolling the editor)
            this.documentViewBase.LayoutChanged += new EventHandler<DocumentViewLayoutChangedEventArgs>(documentViewBase_LayoutChanged);

            // set light gray color as background of the margin
            this.Background = new SolidColorBrush(Colors.LightGray);

            // create the bookmark bitmap image
            this.bookmarkImage = new BitmapImage(new Uri("/IGSyntaxEditor;component/Images/Bookmark.png", UriKind.Relative));
        }

        // mouse down handler
        void BookmarksMarginPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // get mouse position relative to the bookmarks margin's canvas
            Point p = e.GetPosition(this);

            // get the editor line using the mouse position Y
            DocumentViewLine line = this.documentViewBase.ViewLineFromVerticalOffset(p.Y);

            // get the line index
            int lineIndex = line.LineFirstTextLocation.Line;

            // toggle this line index in the bookmarks index
            if (this.margin.Bookmarks.Contains(lineIndex))
            {
                this.margin.Bookmarks.Remove(lineIndex);
            }
            else
            {
                this.margin.Bookmarks.Add(lineIndex);
            }
            // (the bookmarks list is NOT sorted !)
            
            // update the images count and location in this panel and force arrange phase
            UpdateImagesBasedOnBookmarks();
        }

        // invoked when scrolling the editor
        void documentViewBase_LayoutChanged(object sender, DocumentViewLayoutChangedEventArgs e)
        {
            // update the images count and location in this panel and force arrange phase
            UpdateImagesBasedOnBookmarks();
        }

        private void UpdateImagesBasedOnBookmarks()
        {
            // clear all images
            this.Children.Clear();

            // clear bookmarks outside of the document content (this may happen if the user adds
            // bookmarks and then delete part of the document where the bookmarks were added)
            int docLines = this.documentViewBase.Document.CurrentSnapshot.LineCount;
            for (int i = this.margin.Bookmarks.Count - 1; i >= 0; i--)
            {
                if (this.margin.Bookmarks[i] >= docLines)
                {
                    this.margin.Bookmarks.RemoveAt(i);
                }
            }

            // add an image for each bookmark
            foreach (int lineIndex in this.margin.Bookmarks)
            {
                // obtain the line from the line index
                DocumentViewLine line = this.documentViewBase.ViewLineFromLineIndex(lineIndex);

                // check if this bookmark(line) is in the visible lines of the editor
                if (this.documentViewBase.VisibleLines.Contains(line))
                {
                    // create a new image and set position in the "Tag" property
                    // the Y of the images is takken from the line Y
                    Image img = new Image();
                    img.Source = this.bookmarkImage;
                    img.Tag = new Rect(0, line.Bounds.Top, 16, 16);
                    this.Children.Add(img);
                }
            }
            this.InvalidateArrange();
        }

        // layout:
        // - use the bookmark image width as the width of the whole margin
        // - use the height as proposed by the framework
        protected override Size MeasureOverride(Size availableSize)
        {
            Size resultSize = new Size();
            resultSize.Width = 16;
            if (availableSize.Height == Double.PositiveInfinity)
            {
                resultSize.Height = 16;
            }
            else
            {
                resultSize.Height = availableSize.Height;
            }
            return resultSize;
        }

        // arrange all images bases on their "Tag" property
        protected override sealed Size ArrangeOverride(Size arrangeSize)
        {
            Size resultSize = base.ArrangeOverride(arrangeSize);
            Image img;
            foreach (UIElement o in this.Children)
            {
                img = o as Image;
                if (img != null && img.Tag is Rect)
                {
                    Rect rect = (Rect)img.Tag;
                    img.Arrange(rect);
                    img.Tag = null;
                }
            }
            return resultSize;
        }
    }
}
