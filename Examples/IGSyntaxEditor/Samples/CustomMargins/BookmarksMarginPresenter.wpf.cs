using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class BookmarksMarginPresenter : Canvas
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

            // create the bookmark image
            this.bookmarkImage = new BitmapImage();
            this.bookmarkImage.BeginInit();
            this.bookmarkImage.UriSource = new Uri("pack://application:,,,/IGSyntaxEditor;component/Images/Bookmark.png");
            this.bookmarkImage.EndInit();
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

            // force redraw the margin
            this.InvalidateVisual();
        }

        // invoked to draw the margin
        protected override void OnRender(DrawingContext dc)
        {
            // get actual size of the margin
            double thisW = this.ActualWidth;
            double thisH = this.ActualHeight;

            // fill the background (using the actual size)
            dc.DrawRectangle(
                new SolidColorBrush(Colors.LightGray),
                null, // no Pen
                new Rect(0, 0, thisW, thisH)
            );

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

            // draw each bookmark
            foreach (int lineIndex in this.margin.Bookmarks)
            {
                // obtain the line from the line index
                DocumentViewLine line = this.documentViewBase.ViewLineFromLineIndex(lineIndex);

                // check if this bookmark(line) is in the visible lines of the editor
                if (this.documentViewBase.VisibleLines.Contains(line))
                {
                    // draw the bookmark image using the line Y
                    dc.DrawImage(
                        this.bookmarkImage,
                        new Rect(0, line.Bounds.Top, this.bookmarkImage.PixelWidth, this.bookmarkImage.PixelHeight)
                    );
                }
            }
        }

        // invoked when scrolling the editor
        void documentViewBase_LayoutChanged(object sender, DocumentViewLayoutChangedEventArgs e)
        {
            // force repaint of the bookmarks margin
            this.InvalidateVisual();
        }

        // layout:
        // - use the bookmark image width as the width of the whole margin
        // - use the height as proposed by the framework
        protected override Size MeasureOverride(Size availableSize)
        {
            var resultSize = new Size();
            resultSize.Width = this.bookmarkImage.PixelWidth;
            if (availableSize.Height == Double.PositiveInfinity)
            {
                resultSize.Height = this.bookmarkImage.PixelWidth;
            }
            else
            {
                resultSize.Height = availableSize.Height;
            }
            return resultSize;
        }
    }
}
