using System;
using System.IO;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGWord.Resources;
using Infragistics.Documents.Word;
using Infragistics.Samples.Framework;

namespace IGWord.Samples.Data
{
    /// <summary>
    /// Interaction logic for ExportToWordUsingStreamerObject.xaml
    /// </summary>
    public partial class ExportToWordUsingStreamerObject : SampleContainer
    {
        public ExportToWordUsingStreamerObject()
        {
            InitializeComponent();
        }

        void Sample_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeStrings();
        }

        #region Private Members
        MemoryStream documentStream;
        // Define Resource string variable     
        ResourceManager rm = WordStrings.ResourceManager;
        string strDocTitle = null;
        string strSimpleWordHeading = null;
        string strSimpleWordDocSampleText = null;
        string strError = null;
        string strFormattedContentHeading = null;
        string strFormattedContentSampleText1 = null;
        string strFormattedContentSampleText2 = null;
        string strFormattedContentSampleText3 = null;
        string strHyperlink = null;
        string strHyperlinkTooltip = null;
        string strHyperlinkAddress = null;
        string strHyperlinkText = null;
        string strImagesHeading = null;
        string strImagesSampleText1 = null;
        string strImagesAltTextDesc = null;
        string strImagesSampleText2 = null;
        string strTableHeading = null;
        string strTableSampleText1 = null;
        string strTableSampleText2 = null;
        string strTableSampleText3 = null;
        string strTableSampleText4 = null;
        string strTableHyperlinkAdd = null;
        string strTableHyperlinkText = null;
        string strTableImgAltTextDesc = null;
        string strNestedTableHeading1 = null;
        string strNestedTableHeading2 = null;
        string strNestedTableSampleTxt1 = null;
        string strNestedTableSampleTxt2 = null;
        string strNestedTableSampleTxt3 = null;
        string strNestedTableSampleTxt4 = null;
        string strNestedTableSampleTxt5 = null;
        string strNestedTableSampleTxt6 = null;
        string strNestedTableSampleTxt7 = null;
        string strNestedTableSampleTxt8 = null;
        string strHeaderFooterHeading = null;
        string strHeaderText = null;
        string strFooterText = null;
        string strHeaderFooterSampleTxt1 = null;
        string strHeaderFooterSampleTxt2 = null;
        string strShapesHeading = null;
        string strLineShape = null;
        string strEllipseShape = null;
        string strRectShape = null;
        string strIsocelesShape = null;
        string strRightTriangleShape = null;
        #endregion

        #region Initializing string variables for the sample
        private void InitializeStrings()
        {
            strDocTitle = rm.GetString("SimpleWordDocTitle");
            strSimpleWordHeading = rm.GetString("SimpleWordDocHeading");
            strSimpleWordDocSampleText = rm.GetString("SimpleWordDocSampleText");
            strError = rm.GetString("Error");
            strFormattedContentHeading = rm.GetString("FormattedContentHeading");
            strFormattedContentSampleText1 = rm.GetString("FormattedContentSampleText1");
            strFormattedContentSampleText2 = rm.GetString("FormattedContentSampleText2");
            strFormattedContentSampleText3 = rm.GetString("FormattedContentSampleText3");
            strHyperlink = rm.GetString("Hyperlink");
            strHyperlinkTooltip = rm.GetString("HyperlinkTooltip");
            strHyperlinkAddress = rm.GetString("HyperlinkAddress");
            strHyperlinkText = rm.GetString("HyperlinkText");
            strImagesHeading = rm.GetString("ImagesHeading");
            strImagesSampleText1 = rm.GetString("ImagesSampleText1");
            strImagesAltTextDesc = rm.GetString("ImagesAlternateTextDesc");
            strImagesSampleText2 = rm.GetString("ImagesSampleText2");
            strTableHeading = rm.GetString("TableHeading");
            strTableSampleText1 = rm.GetString("TableSampleText1");
            strTableSampleText2 = rm.GetString("TableSampleText2");
            strTableSampleText3 = rm.GetString("TableSampleText3");
            strTableSampleText4 = rm.GetString("TableSampleText4");
            strTableHyperlinkAdd = rm.GetString("TableHyperlinkAddress");
            strTableHyperlinkText = rm.GetString("TableHyperlinkText");
            strTableImgAltTextDesc = rm.GetString("TableImgAltTextDesc");
            strNestedTableHeading1 = rm.GetString("NestedTableHeading1");
            strNestedTableHeading2 = rm.GetString("NestedTableHeading2");
            strNestedTableSampleTxt1 = rm.GetString("NestedTableSampleText1");
            strNestedTableSampleTxt2 = rm.GetString("NestedTableSampleText2");
            strNestedTableSampleTxt3 = rm.GetString("NestedTableSampleText3");
            strNestedTableSampleTxt4 = rm.GetString("NestedTableSampleText4");
            strNestedTableSampleTxt5 = rm.GetString("NestedTableSampleText5");
            strNestedTableSampleTxt6 = rm.GetString("NestedTableSampleText6");
            strNestedTableSampleTxt7 = rm.GetString("NestedTableSampleText7");
            strNestedTableSampleTxt8 = rm.GetString("NestedTableSampleText8");
            strHeaderFooterHeading = rm.GetString("HeadersHeading");
            strHeaderText = rm.GetString("HeaderText");
            strFooterText = rm.GetString("FooterText");
            strHeaderFooterSampleTxt1 = rm.GetString("HeaderFooterSampleText1");
            strHeaderFooterSampleTxt2 = rm.GetString("HeaderFooterSampleText2");
            strShapesHeading = rm.GetString("ShapesHeading");
            strLineShape = rm.GetString("LineShapeText");
            strEllipseShape = rm.GetString("EllipseShapeText");
            strRectShape = rm.GetString("RectangleShapeText");
            strIsocelesShape = rm.GetString("IsocelesShapeText");
            strRightTriangleShape = rm.GetString("RightTrianglShapeText");
        }
        #endregion

        private void btnWordDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter     
                //  class using the static 'Create' method.      
                //  This instance must be closed once content is written into Word.  
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);
                //  Set the document properties, such as title, author, etc.          
                docWriter.DocumentProperties.Title = strDocTitle;
                docWriter.DocumentProperties.Author = "Infragistics.WordDocumentWriter";
                //  Start the document...note that each call to StartDocument must   
                //  be balanced with a corresponding call to EndDocument.     
                docWriter.StartDocument();
                //  Create a font, which we can reuse in content creation          
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                font.Reset();
                font.Bold = true;
                font.Size = 15;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                //  Start a paragraph           
                docWriter.StartParagraph();
                //  Add a text run for the title   
                docWriter.AddTextRun(strSimpleWordHeading, font);
                // Add a new line         
                docWriter.AddNewLine();
                //End the paragraph      
                docWriter.EndParagraph();
                //  Start a paragraph        
                docWriter.StartParagraph();
                font.Reset();
                //  Add a text run for the title   
                docWriter.AddTextRun(strSimpleWordDocSampleText);
                //End the paragraph    
                docWriter.EndParagraph();
                docWriter.EndDocument();
                // Close the writer     
                docWriter.Close();
                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }
        }

        private void btnFormattedContent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter 
                //  class using the static 'Create' method.
                //  This instance must be closed once content is written into Word.
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);
                //  Use inches as the unit of measure
                docWriter.Unit = UnitOfMeasurement.Inch;
                //  Create a font, which we can reuse in content creation
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                //Start the document...note that each call to StartDocument must
                //be balanced with a corresponding call to EndDocument.
                docWriter.StartDocument();
                //  Add a text run for the title, bolded and a little bigger
                font.Reset();
                font.Bold = true;
                font.Size = .23f;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                font.UnderlineColor = Colors.Blue;
                font.Effects.Capitalization = Capitalization.CapsOn;
                //  Start a paragraph
                docWriter.StartParagraph();
                //  Add a text run for the title
                docWriter.AddTextRun(strFormattedContentHeading, font);
                // Add a new line
                docWriter.AddNewLine();
                //End the paragraph
                docWriter.EndParagraph();

                // Paragraph Properties
                ParagraphProperties paraformat = docWriter.CreateParagraphProperties();
                paraformat.Alignment = ParagraphAlignment.Right;

                // Start another Paragraph
                // and apply the ParagraphProperties Object
                docWriter.StartParagraph(paraformat);
                docWriter.AddNewLine();
                // Reset font, and apply different font settings for this paragraph.
                font.Reset();
                font.Italic = true;
                font.ForeColor = Colors.Red;
                font.Effects.TextEffect = FontTextEffect.EngravingOn;
                docWriter.AddTextRun(strFormattedContentSampleText1, font);
                docWriter.AddNewLine();
                font.Reset();
                font.Italic = true;
                font.ForeColor = Colors.Blue;
                font.Effects.TextEffect = FontTextEffect.OutliningOn;
                docWriter.AddTextRun(strFormattedContentSampleText2, font);
                // End the paragraph
                docWriter.EndParagraph();

                //Add an Empty paragraph
                docWriter.AddEmptyParagraph();
                paraformat.Reset();
                paraformat.PageBreakBefore = true;
                docWriter.StartParagraph(paraformat);
                font.Reset();
                font.ForeColor = Colors.Red;
                docWriter.AddTextRun(strFormattedContentSampleText3, font);
                docWriter.EndParagraph();

                // Set page attribute for all pages in the document.
                // If SectionProperties is defined (see commented code below), only the last page gets the FinalSectionProperties settings.
                docWriter.FinalSectionProperties.PageSize = new Size(7, 5);
                docWriter.FinalSectionProperties.PageOrientation = PageOrientation.Landscape;

                //// Set page attributes for specific pages in the document using SectionProperties object
                //SectionProperties secProperties = docWriter.CreateSectionProperties();
                //secProperties.PageSize = new Size(7, 5);
                //secProperties.PageOrientation = PageOrientation.Portrait;
                //// Applies the section properties(PageSize and Orientation) for the above added paragraphs
                //docWriter.DefineSection(secProperties);

                // End the Document       
                docWriter.EndDocument();
                // Close the writer
                docWriter.Close();

                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }

        }

        private void OpenDocument()
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = "Word files|*.docx", DefaultExt = "docx" };
            bool? showDialog = dialog.ShowDialog();
            if (showDialog == true)
            {
                using (Stream exportStream = dialog.OpenFile())
                {
                    byte[] data = documentStream.ToArray();
                    exportStream.Write(data, 0, data.Length);
                    exportStream.Close();
                }
            }
        }

        private void btnHyperlinks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter 
                //  class using the static 'Create' method.
                //  This instance must be closed once content is written into Word.
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);

                //  Start the document...note that each call to StartDocument must
                //  be balanced with a corresponding call to EndDocument.
                docWriter.StartDocument();
                //  Create a font, which we can reuse in content creation
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                font.Reset();
                font.Bold = true;
                font.Size = 15;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                //  Start a paragraph
                docWriter.StartParagraph();
                //  Add a text run for the title
                docWriter.AddTextRun(strHyperlink, font);
                // Add a new line
                docWriter.AddNewLine();

                // Add a Hyperlink
                docWriter.AddHyperlink(strHyperlinkAddress, strHyperlinkText, strHyperlinkTooltip);

                //End the paragraph
                docWriter.EndParagraph();

                docWriter.EndDocument();
                // Close the writer
                docWriter.Close();
                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }
        }

        private void btnNestedTable_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter 
                //  class using the static 'Create' method.
                //  This instance must be closed once content is written into Word.
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);
                // Start the Document
                docWriter.StartDocument();
                //  Create a font, which we can reuse in content creation
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                font.Reset();
                font.Bold = true;
                font.Size = 15;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                //  Start a paragraph
                docWriter.StartParagraph();
                //  Add a text run for the title
                docWriter.AddTextRun(strNestedTableHeading1, font);
                docWriter.AddNewLine();
                docWriter.EndParagraph();

                // Create properties for table cell
                TableCellProperties cellProps = docWriter.CreateTableCellProperties();
                cellProps.BackColor = Colors.DarkGray;

                // Begin a Table with 2 columns
                docWriter.StartTable(2);
                // Begin a table row
                docWriter.StartTableRow();
                // Begin Table cell for first row, first column
                docWriter.StartTableCell(cellProps);

                // Add this paragraph to the first row, first column
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt1);
                docWriter.EndParagraph();
                docWriter.EndTableCell();

                // Begin Table cell for first row second column
                docWriter.StartTableCell(cellProps);

                #region
                // Nested Table within first row, second column
                // Create paragraph properties for the paragraph added within the Nested table
                ParagraphProperties nestedTableParaProperties = docWriter.CreateParagraphProperties();
                nestedTableParaProperties.Alignment = ParagraphAlignment.Center;

                // Start a paragraph. 
                // This text is added within the first row, second column.
                docWriter.StartParagraph(nestedTableParaProperties);
                docWriter.AddTextRun(strNestedTableHeading2);
                nestedTableParaProperties.SpacingAfter = 10;
                docWriter.EndParagraph();

                // Start a table with two columns within first row, second column
                docWriter.StartTable(2);
                docWriter.StartTableRow();
                cellProps.Reset();
                cellProps.BackColor = Color.FromArgb(255, 255, 250, 250);
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt2);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt3);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.EndTableRow();

                docWriter.StartTableRow();
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt4);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt5);
                docWriter.AddNewLine();
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();

                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt6);

                // AddNewline method within a table cell will just add a space, instead of a new line
                docWriter.AddNewLine();
                docWriter.AddHyperlink(strHyperlinkAddress, strHyperlinkText);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.EndTableRow();
                docWriter.EndTable();
                docWriter.AddEmptyParagraph();
                #endregion //Nested Table

                docWriter.EndTableCell();
                docWriter.EndTableRow();
                docWriter.StartTableRow();
                cellProps.Reset();
                cellProps.BackColor = Colors.DarkGray;
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt7);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strNestedTableSampleTxt8);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.EndTableRow();
                docWriter.EndTable();
                docWriter.EndDocument();
                // Close the WordDocumentWriter instance.
                docWriter.Close();
                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }

        }

        private void btnHeaderFooter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter 
                //  class using the static 'Create' method.
                //  This instance must be closed once content is written into Word.
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);
                //Start the document...note that each call to StartDocument must
                //be balanced with a corresponding call to EndDocument.
                docWriter.StartDocument();
                //  Create a font instance, which we can use in content creation
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                font.Reset();
                font.Bold = true;
                font.Size = 15;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                //  Start a paragraph
                docWriter.StartParagraph();
                //  Add a text run for the title
                docWriter.AddTextRun(strHeaderFooterHeading, font);
                docWriter.AddNewLine();
                docWriter.EndParagraph();
                // Formatting for Paragraphs
                ParagraphProperties paraformat = docWriter.CreateParagraphProperties();

                // Header and Footer
                //  Specify the default parts for header and footer.
                SectionHeaderFooterParts parts = SectionHeaderFooterParts.HeaderAllPages | SectionHeaderFooterParts.FooterAllPages;
                SectionHeaderFooterWriterSet writerSet = docWriter.AddSectionHeaderFooter(parts);


                // Header
                writerSet.HeaderWriterAllPages.Open();
                // The header text alignment is set to right
                // by passing in the ParagraphProperties instance
                // Paragraph Properties
                paraformat.Alignment = ParagraphAlignment.Right;
                writerSet.HeaderWriterAllPages.StartParagraph(paraformat);
                writerSet.HeaderWriterAllPages.AddTextRun(strHeaderText);
                writerSet.HeaderWriterAllPages.EndParagraph();
                writerSet.HeaderWriterAllPages.Close();

                //Footer
                writerSet.FooterWriterAllPages.Open();
                // The footer text alignment is set to right
                // by passing in the ParagraphProperties instance
                writerSet.FooterWriterAllPages.StartParagraph(paraformat);
                writerSet.FooterWriterAllPages.AddTextRun(strFooterText);

                // Add Page numbers to the Footer
                writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Ordinal, font);
                writerSet.FooterWriterAllPages.EndParagraph();
                writerSet.FooterWriterAllPages.Close();

                // Some paragraphs for the page
                //  Add a text run for the title, bolded
                docWriter.StartParagraph();
                font.Reset();
                font.Bold = true;
                docWriter.AddTextRun(strHeaderFooterSampleTxt1, font);
                docWriter.EndParagraph();
                font.Reset();
                docWriter.StartParagraph();
                docWriter.AddTextRun(strHeaderFooterSampleTxt2, font);
                docWriter.EndParagraph();
                paraformat.Reset();
                paraformat.PageBreakBefore = true;
                //  Add a text run for the title, bolded
                font.Bold = true;
                docWriter.StartParagraph(paraformat);
                docWriter.AddTextRun(strHeaderFooterSampleTxt1, font);
                docWriter.EndParagraph();
                font.Reset();
                docWriter.StartParagraph();
                docWriter.AddTextRun(strHeaderFooterSampleTxt2, font);
                docWriter.EndParagraph();

                //End Document
                docWriter.EndDocument();
                // Close the writer
                docWriter.Close();

                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }

        }

        private void btnShapes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter 
                //  class using the static 'Create' method.
                //  This instance must be closed once content is written into Word.
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);
                docWriter.StartDocument();
                //  Create a font, which we can use in content creation
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                font.Reset();
                font.Bold = true;
                font.Size = 20;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                //  Start a paragraph
                docWriter.StartParagraph();
                //  Add a text run for the title
                docWriter.AddTextRun(strShapesHeading, font);
                docWriter.AddNewLine();
                docWriter.EndParagraph();

                #region Line Shape
                VmlShape shape = null;
                AnchoredShape anchShape = null;
                // Create a VmlShape with the specified shape type
                shape = VmlShape.Create(docWriter, ShapeType.Line);
                // Set Shape Properties
                shape.LineColor = Colors.Red;
                shape.LineWidth = 3;
                shape.Size = new Size(70, 70);
                // Start a Paragraph
                docWriter.StartParagraph();
                // Write the shape to the document as Inline shape
                docWriter.AddInlineShape(shape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddTextRun(strLineShape);
                anchShape = AnchoredShape.Create(docWriter, shape);
                anchShape.HorizontalAlignment = AnchorHorizontalAlignment.Left;
                anchShape.TextWrapping = AnchorTextWrapping.Square;
                // Write the shape to the document as Anchored shape
                docWriter.AddAnchoredShape(anchShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                #endregion //Line Shape

                #region Ellipse Shape
                anchShape = AnchoredShape.Create(docWriter, ShapeType.Ellipse);
                //  Get the underlying shape and upcast to type VmlEllipse
                VmlEllipse ellipseShape = anchShape.Shape as VmlEllipse;
                ellipseShape.LineColor = Colors.Blue;
                ellipseShape.LineWidth = 3;
                ellipseShape.Size = new Size(75, 100);
                ellipseShape.FillColor = Color.FromArgb(255, 255, 192, 203);
                ellipseShape.Rotation = 30;
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddInlineShape(ellipseShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddTextRun(strEllipseShape);
                docWriter.AddAnchoredShape(anchShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                #endregion //Ellipse Shape

                #region Rectangle Shape
                // Create an Anchored shape instance with the specified shape type
                anchShape = AnchoredShape.Create(docWriter, ShapeType.Rectangle);
                //  Get the underlying shape and upcast to type VmlRectangle
                VmlRectangle rectangleShape = anchShape.Shape as VmlRectangle;
                // Set Shape Properties
                rectangleShape.LineColor = Colors.Green;
                rectangleShape.LineWidth = 3;
                rectangleShape.Size = new Size(120, 75);
                rectangleShape.FillColor = Color.FromArgb(255, 255, 255, 224);
                rectangleShape.Rotation = 20;
                // Start a Paragraph to add Inline Shape
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddInlineShape(rectangleShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                // Start a paragraph to add a shape anchored to text
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddTextRun(strRectShape);
                docWriter.AddAnchoredShape(anchShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                #endregion //Rectangle Shape

                #region Isoscelese Triangle Shape
                // Create an Anchored shape instance with the specified shape type
                anchShape = AnchoredShape.Create(docWriter, ShapeType.IsosceleseTriangle);
                //  Get the underlying shape and upcast to type VmlIsosceleseTriangle
                VmlIsosceleseTriangle isoceleseTriangleShape = anchShape.Shape as VmlIsosceleseTriangle;
                // Set Shape Properties
                isoceleseTriangleShape.LineColor = Color.FromArgb(255, 238, 130, 238);
                isoceleseTriangleShape.LineWidth = 3;
                isoceleseTriangleShape.Size = new Size(120, 75);
                isoceleseTriangleShape.FillColor = Color.FromArgb(255, 224, 255, 255);
                // Start a Paragraph to add Inline Shape
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddInlineShape(isoceleseTriangleShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                // Start a paragraph to add a shape anchored to text
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddTextRun(strIsocelesShape);
                docWriter.AddAnchoredShape(anchShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                #endregion // Isoscelese Triangle Shape

                #region Right Triangle Shape
                // Create an Anchored shape instance with the specified shape type
                anchShape = AnchoredShape.Create(docWriter, ShapeType.RightTriangle);
                //  Get the underlying shape and upcast to type VmlRightTriangle
                VmlRightTriangle rightTriangleShape = anchShape.Shape as VmlRightTriangle;
                // Set Shape Properties
                rightTriangleShape.LineColor = Colors.Orange;
                rightTriangleShape.LineWidth = 3;
                rightTriangleShape.Size = new Size(120, 75);
                rightTriangleShape.FillColor = Color.FromArgb(255, 152, 251, 152);
                rightTriangleShape.Rotation = 120;
                // Start a Paragraph to add Inline Shape
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddInlineShape(rightTriangleShape);
                docWriter.EndParagraph();
                docWriter.AddEmptyParagraph();
                // Start a paragraph to add a shape anchored to text
                docWriter.StartParagraph();
                docWriter.AddNewLine();
                docWriter.AddTextRun(strRightTriangleShape);
                docWriter.AddAnchoredShape(anchShape);
                docWriter.EndParagraph();
                #endregion // Right Triangle Shape

                docWriter.EndDocument();
                docWriter.Close();
                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }
        }

        private void btnTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documentStream = new MemoryStream();
                //  Create a new instance of the WordDocumentWriter 
                //  class using the static 'Create' method.
                //  This instance must be closed once content is written into Word.
                WordDocumentWriter docWriter = WordDocumentWriter.Create(documentStream);
                // Start the Document
                docWriter.StartDocument();
                //  Create a font, which we can reuse in content creation
                Infragistics.Documents.Word.Font font = docWriter.CreateFont();
                font.Reset();
                font.Bold = true;
                font.Size = 15;
                font.Underline = Infragistics.Documents.Word.Underline.Double;
                //  Start a paragraph
                docWriter.StartParagraph();
                //  Add a text run for the title
                docWriter.AddTextRun(strTableHeading, font);
                docWriter.AddNewLine();
                docWriter.EndParagraph();

                // Create border properties for Table
                TableBorderProperties borderProps = docWriter.CreateTableBorderProperties();
                borderProps.Color = Colors.Red;
                borderProps.Style = TableBorderStyle.Double;

                // Create table properties
                TableProperties tableProps = docWriter.CreateTableProperties();
                tableProps.Alignment = ParagraphAlignment.Center;
                tableProps.BorderProperties.Color = borderProps.Color;
                tableProps.BorderProperties.Style = borderProps.Style;

                // Create table row properties
                TableRowProperties rowProps = docWriter.CreateTableRowProperties();

                // Create table cell properties
                TableCellProperties cellProps = docWriter.CreateTableCellProperties();

                // Begin a table with 2 columns, and apply the table properties
                docWriter.StartTable(2, tableProps);
                // Begin a Row and apply table row properties
                // This row will be set as the Header row by the row properties
                //Make the row a Header
                rowProps.IsHeaderRow = true;
                docWriter.StartTableRow(rowProps);
                // Define back color for header cell
                cellProps.BackColor = Color.FromArgb(255, 112, 219, 147);
                cellProps.ColumnSpan = 2;
                // Cell Value for 1st row 1st column
                // Start a Paragraph and add a text run to the cell
                docWriter.StartTableCell(cellProps);
                // Set the alignment of the cell text using ParagraphProperties
                ParagraphProperties paraProps = docWriter.CreateParagraphProperties();
                paraProps.Alignment = ParagraphAlignment.Center;
                docWriter.StartParagraph(paraProps);
                docWriter.AddTextRun(strTableSampleText1);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                // End the Table Row
                docWriter.EndTableRow();

                // Reset the cell properties, so that the 
                // cell properties are different from the header cells.
                cellProps.Reset();
                cellProps.BackColor = Color.FromArgb(255, 255, 228, 196);
                cellProps.VerticalAlignment = TableCellVerticalAlignment.Center;
                // Reset the row properties
                rowProps.Reset();

                // DATA ROW
                docWriter.StartTableRow();
                // Cell Value for 2nd row 1st column
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strTableSampleText2);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                // Cell Value for 2nd row 2nd column
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strTableSampleText3);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                docWriter.EndTableRow();

                // DATA ROW
                docWriter.StartTableRow();
                // Cell Value for 3rd row 1st column
                docWriter.StartTableCell(cellProps);
                docWriter.StartParagraph();
                docWriter.AddTextRun(strTableSampleText4);
                docWriter.AddNewLine();
                docWriter.EndParagraph();
                docWriter.StartParagraph();
                docWriter.AddHyperlink(strTableHyperlinkAdd, strTableHyperlinkText);
                docWriter.EndParagraph();
                docWriter.EndTableCell();
                // Cell Value for 3rd row 2nd column
                docWriter.StartTableCell(cellProps);
                docWriter.EndTableCell();
                docWriter.EndTableRow();

                docWriter.EndTable();
                docWriter.EndDocument();
                docWriter.Close();
                OpenDocument();
            }
            catch (Exception errmsg)
            {
                string contentmsg = errmsg.Message.ToString();
                System.Windows.MessageBox.Show(contentmsg);
            }
        }
    }
}
