using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using IGRichTextEditor.Resources;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGRichTextEditor.Samples.Data
{
    public partial class DefiningTables : SampleContainer
    {
        public DefiningTables()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            XmlDataProvider xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Books.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;
            List<Book> books = (from d in doc.Descendants("book")
                                select new Book
                                {
                                    Author = d.Attribute("Author").Value,
                                    Title = d.Attribute("Title").Value,
                                    ReleaseDate = d.Attribute("ReleaseDate").Value,
                                    UnitPrice = Decimal.Parse(d.Attribute("UnitPrice").Value, CultureInfo.InvariantCulture),
                                    Url = d.Attribute("Url").Value
                                }).ToList();
            CreateTable(books);
        }

        private void CreateTable(List<Book> books)
        {
            int rowCounter = 0;
            string error = null;

            // text character settings
            CharacterSettings charSettings = new CharacterSettings()
            {
                Color = new ColorInfo(Color.FromArgb(255, 85, 85, 85))
            };

            // setting for headers
            TableCellSettings headerSettings = new TableCellSettings();
            headerSettings.Shading = new Shading(new ColorInfo(Color.FromArgb(255, 232, 234, 235)));

            // settings for non-highlighted rows
            TableCellSettings rowSettings = new TableCellSettings();
            rowSettings.Shading = new Shading(new ColorInfo(Color.FromArgb(255, 252, 252, 252)));

            // settings for highlighted rows
            TableCellSettings rowAlternatingSettings = new TableCellSettings();
            rowAlternatingSettings.Shading = new Shading(new ColorInfo(Color.FromArgb(255, 246, 246, 246)));

            // create table settings
            RichTextBorder tableBorder = new RichTextBorder()
            {
                Color = Color.FromArgb(255, 224, 224, 224),
                BorderType = RichTextBorderType.Single,
                Size = new Extent(1, ExtentUnitType.LogicalPixels),
            };
            TableSettings tableSettings = new TableSettings();
            tableSettings.Borders = new TableBorderSettings();
            tableSettings.Borders.Top = tableBorder;
            tableSettings.Borders.Bottom = tableBorder;
            tableSettings.Borders.Start = tableBorder;
            tableSettings.Borders.End = tableBorder;
            
            // create a table with four columns and rows equals to the books count + 1, because of the headers on top
            TableNode tn = this.xamRichTextEditor1.Document.InsertTable(
                this.xamRichTextEditor1.Document.AggregateOffsetCount, 4, books.Count + 1, out error);
            tn.Settings = tableSettings;

            foreach (TableRowNode trn in tn.ChildNodes)
            {
                if (rowCounter == 0)
                {
                    // populating headers
                    TableCellNode tcn = trn.ChildNodes[0] as TableCellNode;
                    tcn.Settings = headerSettings;
                    ParagraphNode pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, RichTextEditorStrings.txtTitle));

                    tcn = trn.ChildNodes[1] as TableCellNode;
                    tcn.Settings = headerSettings;
                    pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, RichTextEditorStrings.txtAuthor));

                    tcn = trn.ChildNodes[2] as TableCellNode;
                    tcn.Settings = headerSettings;
                    pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, RichTextEditorStrings.txtReleaseDate));

                    tcn = trn.ChildNodes[3] as TableCellNode;
                    tcn.Settings = headerSettings;
                    pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, RichTextEditorStrings.txtUnitPrice));
                }
                else
                {
                    // populating data
                    Book b = books[rowCounter - 1];

                    TableCellNode tcn = trn.ChildNodes[0] as TableCellNode;
                    if (rowCounter % 2 != 0) tcn.Settings = rowAlternatingSettings;
                    else tcn.Settings = rowSettings;
                    ParagraphNode pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, b.Title));

                    tcn = trn.ChildNodes[1] as TableCellNode;
                    if (rowCounter % 2 != 0) tcn.Settings = rowAlternatingSettings;
                    else tcn.Settings = rowSettings;
                    pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, b.Author));

                    tcn = trn.ChildNodes[2] as TableCellNode;
                    if (rowCounter % 2 != 0) tcn.Settings = rowAlternatingSettings;
                    else tcn.Settings = rowSettings;
                    pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, b.ReleaseDate));

                    tcn = trn.ChildNodes[3] as TableCellNode;
                    if (rowCounter % 2 != 0) tcn.Settings = rowAlternatingSettings;
                    else tcn.Settings = rowSettings;
                    pn = tcn.ChildNodes[0] as ParagraphNode;
                    pn.ChildNodes.Add(CreateRunNodeWithContent(charSettings, b.UnitPrice.ToString()));
                }

                rowCounter++;
            }
        }

        private RunNode CreateRunNodeWithContent(CharacterSettings cs, string content)
        {
            RunNode rn = new RunNode();
            if (cs != null) rn.Settings = cs;
            if (content != null) rn.SetText(content);
            return rn;
        }
    }
}
