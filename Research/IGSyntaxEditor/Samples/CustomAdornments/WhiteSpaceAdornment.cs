using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Editors;
using Infragistics.Documents;

namespace IGSyntaxEditor.Samples.CustomAdornments
{
    //The adornment's generator provider will be used by the Syntax Editor's infrastructure to create the custom adornment.
    //Also it will pass an arguments provider to the adornment so that the host application can control the behavior of the
    //custom adornment.
    public class WhiteSpaceAdornmentProvider : AdornmentGeneratorProvider
    {
        private readonly WhiteSpaceAdornmentArgumentsProvider _argumentsProvider;

        public WhiteSpaceAdornmentProvider(WhiteSpaceAdornmentArgumentsProvider ap)
        {
            this._argumentsProvider = ap;
        }

        public override SyntaxEditorAdornmentGeneratorBase CreateAdornmentGenerator(DocumentViewBase documentView)
        {
            var adornment = new WhiteSpaceAdornment(documentView, _argumentsProvider);
            return adornment;
        }
    }

    // this adornment will draw symbols to indicate tabs and spaces
    public class WhiteSpaceAdornment : SyntaxEditorAdornmentGeneratorBase
    {
        private WhiteSpaceAdornmentArgumentsProvider _argumentsProvider;
        private AdornmentInfo _adornmentInfo;
        private Canvas _adornmentCanvas;
        private bool _adornmentsInitialized;

        // the adornment will draw symbols in its own layer defined between the Caret layer and the Text Foreground layer
        public WhiteSpaceAdornment(DocumentViewBase dv, WhiteSpaceAdornmentArgumentsProvider ap) :
            base(dv, new AdornmentLayerInfo("WhiteSpaceLayer",
                new string[] { AdornmentLayerKeys.CaretLayer },
                new string[] { AdornmentLayerKeys.TextForegroundLayer }))
        {
            // save a reference for the arguments provider
            this._argumentsProvider = ap;

            // listen for changes in the arguments, used to configure this adornment
            this._argumentsProvider.HighlightingChanged += UpdateWhiteSpaces;

            // listen for layout changed so that the whitespace marks will be redrawn when scrolling the docuemnt
            this.DocumentView.LayoutChanged += UpdateWhiteSpaces;

            InitializeAdornments();
        }

        private void InitializeAdornments()
        {
            if (this._adornmentsInitialized || this.AdornmentLayer == null) return;

            // create a canvas for showing the whitespace marks
            this._adornmentCanvas = new Canvas();

            // obtain document view text area bounds
            double TAWidth = double.NaN;
            double TAHeight = double.NaN;
            try
            {
                TAWidth = this.DocumentView.TextAreaBounds.Width;
                TAHeight = this.DocumentView.TextAreaBounds.Height;
            }
            catch (Exception) { }

            // set bounds only if it is available (different than NAN and positive)
            if (TAWidth != double.NaN && TAWidth > 0)
            {
                this._adornmentCanvas.Width = TAWidth;
            }
            if (TAHeight != double.NaN && TAHeight > 0)
            {
                this._adornmentCanvas.Height = TAHeight;
            }

            // add the adornment and position the canvas at 0,0 with respect to the editing area
            this._adornmentInfo = this.AdornmentLayer.AddAdornment(this._adornmentCanvas, new Point(0, 0), null);

            this._adornmentsInitialized = true;
        }

        protected override void OnTextAreaInitialized()
        {
            base.OnTextAreaInitialized();

            // initialize the adornment after the text area of the editor is initialized
            this.InitializeAdornments();
        }

        // create new geometries to update the whitespace marks
        private void UpdateWhiteSpaces(object sender, EventArgs e)
        {
            if (!this._adornmentsInitialized) return;

            // obtain all visible lines
            DocumentViewLineCollection visLines = this.DocumentView.VisibleLines;

            // clear old geometry
            this._adornmentCanvas.Children.Clear();

            // iterate over all visible lines
            foreach (DocumentViewLine visLine in visLines)
            {
                SnapshotLineInfo sli = visLine.SnapshotLineInfo;
                // iterate over the characters in a single line
                for (int charIndex = 0; charIndex < sli.Length; charIndex++)
                {
                    char ch = sli.GetCharacter(charIndex);
                    if (ch.Equals('\t') && this._argumentsProvider.MarkTabs)
                    {
                        // if the adornment encounter a tab - create the tab mark
                        Rect bounds = GetCharBounds(charIndex, visLine, sli);
                        Path path = CreateTabMarker(Colors.Blue, Colors.Blue, bounds);
                        this._adornmentCanvas.Children.Add(path);
                    }
                    else if (ch.Equals(' ') && this._argumentsProvider.MarkSpaces)
                    {
                        // if the adornment encounter a space - create the space mark
                        Rect bounds = GetCharBounds(charIndex, visLine, sli);
                        Path path = CreateSpaceMarker(Colors.Red, Colors.Red, bounds);
                        this._adornmentCanvas.Children.Add(path);
                    }
                }
            }

            // force repaint of the canvas
            _adornmentCanvas.InvalidateMeasure();
        }

        // calculate the bounds of a given character
        private Rect GetCharBounds(int charIndex, DocumentViewLine visLine, SnapshotLineInfo sli)
        {
            Rect result = new Rect();
            Point startPoint = visLine.PointFromCharacterIndex(charIndex);
            result.X = startPoint.X;
            result.Y = startPoint.Y;
            Point endPoint;
            if (charIndex == sli.Length - 1)
            {
                // last line character
                endPoint = new Point(visLine.Bounds.Right, visLine.Bounds.Bottom);
            }
            else
            {
                // not last line character
                endPoint = visLine.PointFromCharacterIndex(charIndex + 1);
                endPoint.X--;
                endPoint.Y = visLine.Bounds.Bottom;
            }
            double calcWidth = endPoint.X - startPoint.X + 1;
            if (calcWidth > 0) result.Width = calcWidth;
            double calcHeight = endPoint.Y - startPoint.Y + 1;
            if (calcHeight > 0) result.Height = calcHeight;
            return result;
        }

        // create the geometries for a tab mark
        private Path CreateTabMarker(Color stroke, Color fill, Rect bounds)
        {
            GeometryGroup geoGroup = new GeometryGroup();

            // Draw the left line
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(bounds.Left + 2, bounds.Top + 2);
            line.EndPoint = new Point(bounds.Left + 2, bounds.Bottom - 7);
            geoGroup.Children.Add(line);

            // Draw the right line
            line = new LineGeometry();
            line.StartPoint = new Point(bounds.Right - 2, bounds.Top + 2);
            line.EndPoint = new Point(bounds.Right - 2, bounds.Bottom - 7);
            geoGroup.Children.Add(line);

            // Draw the top line
            line = new LineGeometry();
            line.StartPoint = new Point(bounds.Left + 2, bounds.Top + 2);
            line.EndPoint = new Point(bounds.Right - 2, bounds.Top + 2);
            geoGroup.Children.Add(line);

            // Draw the bottom line
            line = new LineGeometry();
            line.StartPoint = new Point(bounds.Left + 2, bounds.Bottom - 7);
            line.EndPoint = new Point(bounds.Right - 2, bounds.Bottom - 7);
            geoGroup.Children.Add(line);

            Path path = new Path();
            path.Fill = new SolidColorBrush(fill);
            path.Stroke = new SolidColorBrush(stroke);
            path.Data = geoGroup;

            return path;
        }

        // create the geometries for a space mark
        private Path CreateSpaceMarker(Color stroke, Color fill, Rect bounds)
        {
            GeometryGroup geoGroup = new GeometryGroup();

            // Draw left vertical line
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(bounds.Left + 2, bounds.Bottom - 10);
            line.EndPoint = new Point(bounds.Left + 2, bounds.Bottom - 7);
            geoGroup.Children.Add(line);

            // Draw right vertical line
            line = new LineGeometry();
            line.StartPoint = new Point(bounds.Right - 1, bounds.Bottom - 10);
            line.EndPoint = new Point(bounds.Right - 1, bounds.Bottom - 7);
            geoGroup.Children.Add(line);

            // Draw horizontal line
            line = new LineGeometry();
            line.StartPoint = new Point(bounds.Left + 2, bounds.Bottom - 7);
            line.EndPoint = new Point(bounds.Right - 1, bounds.Bottom - 7);
            geoGroup.Children.Add(line);

            Path path = new Path();
            path.Fill = new SolidColorBrush(fill);
            path.Stroke = new SolidColorBrush(stroke);
            path.Data = geoGroup;

            return path;
        }

        // invoked from the Syntax Editor, when there are changes and update is needed
        protected override void OnRefreshAdornments()
        {
            UpdateWhiteSpaces(null, null);
        }

        // unregister event handlers on unload
        protected override void OnUnloaded()
        {
            this.DocumentView.LayoutChanged -= UpdateWhiteSpaces;
            this._argumentsProvider.HighlightingChanged -= UpdateWhiteSpaces;

            if (this._adornmentsInitialized)
            {
                bool removed = this.AdornmentLayer.RemoveAdornment(_adornmentInfo);
                this._adornmentsInitialized = false;
            }
        }
    }
}
