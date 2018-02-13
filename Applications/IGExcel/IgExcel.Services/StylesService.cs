using System.Collections.Generic;

namespace IgExcel.Services
{
    public class StyleService : IStyleService
    {
        //Table Styles
        public List<string> GetAllTableStyles()
        {
            return new List<string>
            {
                 "TableStyleMedium28",
                 "TableStyleMedium27",
                 "TableStyleMedium26",
                 "TableStyleMedium25",
                 "TableStyleMedium24",
                 "TableStyleMedium23",
                 "TableStyleMedium22",
                 "TableStyleMedium21",
                 "TableStyleMedium20",
                 "TableStyleMedium19",
                 "TableStyleMedium18",
                 "TableStyleMedium17",
                 "TableStyleMedium16",
                 "TableStyleMedium15",
                 "TableStyleMedium14",
                 "TableStyleMedium13",
                 "TableStyleMedium12",
                 "TableStyleMedium11",
                 "TableStyleMedium10",
                 "TableStyleMedium9",
                 "TableStyleMedium8",
                 "TableStyleMedium7",
                 "TableStyleMedium6",
                 "TableStyleMedium5",
                 "TableStyleMedium4",
                 "TableStyleMedium3",
                 "TableStyleMedium2",
                 "TableStyleMedium1",
                 "TableStyleLight21",
                 "TableStyleLight20",
                 "TableStyleLight19",
                 "TableStyleLight18",
                 "TableStyleLight17",
                 "TableStyleLight16",
                 "TableStyleLight15",
                 "TableStyleLight14",
                 "TableStyleLight13",
                 "TableStyleLight12",
                 "TableStyleLight11",
                 "TableStyleLight10",
                 "TableStyleLight9",
                 "TableStyleLight8",
                 "TableStyleLight7",
                 "TableStyleLight6",
                 "TableStyleLight5",
                 "TableStyleLight4",
                 "TableStyleLight3",
                 "TableStyleLight2",
                 "TableStyleLight1",
                 "TableStyleDark11",
                 "TableStyleDark10",
                 "TableStyleDark9",
                 "TableStyleDark8",
                 "TableStyleDark7",
                 "TableStyleDark6",
                 "TableStyleDark5",
                 "TableStyleDark4",
                 "TableStyleDark3",
                 "TableStyleDark2",
                 "TableStyleDark1"
            };
        }

        public List<string> GetDarkTableStyles()
        {
            return new List<string>
            {
                 "TableStyleMedium28",
                 "TableStyleMedium27",
                 "TableStyleMedium26",
                 "TableStyleMedium25",
                 "TableStyleMedium24",
                 "TableStyleMedium23",
                 "TableStyleMedium22",
                 "TableStyleMedium21",
                 "TableStyleMedium20",
                 "TableStyleMedium19",
                 "TableStyleMedium18",
                 "TableStyleMedium17",
                 "TableStyleMedium16",
                 "TableStyleMedium15",
                 "TableStyleMedium14",
                 "TableStyleMedium13",
                 "TableStyleMedium12",
                 "TableStyleMedium11",
                 "TableStyleMedium10",
                 "TableStyleMedium9",
                 "TableStyleMedium8",
                 "TableStyleMedium7",
                 "TableStyleMedium6",
                 "TableStyleMedium5",
                 "TableStyleMedium4",
                 "TableStyleMedium3",
                 "TableStyleMedium2",
                 "TableStyleMedium1"            
            };
        }

        public List<string> GetLightTableStyles()
        {
            return new List<string>
            {
                 "TableStyleLight21",
                 "TableStyleLight20",
                 "TableStyleLight19",
                 "TableStyleLight18",
                 "TableStyleLight17",
                 "TableStyleLight16",
                 "TableStyleLight15",
                 "TableStyleLight14",
                 "TableStyleLight13",
                 "TableStyleLight12",
                 "TableStyleLight11",
                 "TableStyleLight10",
                 "TableStyleLight9",
                 "TableStyleLight8",
                 "TableStyleLight7",
                 "TableStyleLight6",
                 "TableStyleLight5",
                 "TableStyleLight4",
                 "TableStyleLight3",
                 "TableStyleLight2",
                 "TableStyleLight1"
            };
        }

        public List<string> GetMediumTableStyles()
        {
            return new List<string>
            {
                 "TableStyleDark11",
                 "TableStyleDark10",
                 "TableStyleDark9",
                 "TableStyleDark8",
                 "TableStyleDark7",
                 "TableStyleDark6",
                 "TableStyleDark5",
                 "TableStyleDark4",
                 "TableStyleDark3",
                 "TableStyleDark2",
                 "TableStyleDark1"
            };
        }

        //Cell Styles
        public List<string> GetAllCellStyles()
        {
            return new List<string>
            {
                "Normal",
                "Good",
                "Bad",
                "Neutral",
                 "Calculation",
                 "Check Cell",
                 "Explanatory Text",
                 "Input",
                 "Linked Cell",
                 "Note",
                 "Output",
                 "Warning Text",
                 "Heading 1",
                 "Heading 2",
                 "Heading 3",
                 "Heading 4",
                 "Title",
                 "Total",
                 "20% - Accent1",
                 "20% - Accent2",
                 "20% - Accent3",
                 "20% - Accent4",
                 "20% - Accent5",
                 "20% - Accent6",
                 "40% - Accent1",
                 "40% - Accent2",
                 "40% - Accent3",
                 "40% - Accent4",
                 "40% - Accent5",
                 "40% - Accent6",
                 "60% - Accent1",
                 "60% - Accent2",
                 "60% - Accent3",
                 "60% - Accent4",
                 "60% - Accent5",
                 "60% - Accent6",
                 "Accent1",
                 "Accent2",
                 "Accent3",
                 "Accent4",
                 "Accent5",
                 "Accent6",
                 "Comma",
                 "Comma [0]",
                 "Currency",
                 "Currency [0]",
                 "Percent"
            };
        }

        public List<string> GetGoodBadAndNeutralCellStyles()
        {
            return new List<string>
            {
                "Normal",
                "Good",
                "Bad",
                "Neutral"
            };
        }

        public List<string> GetDataAndModelCellStyles()
        {
            return new List<string>
            {
                 "Calculation",
                 "Check Cell",
                 "Explanatory Text",
                 "Input",
                 "Linked Cell",
                 "Note",
                 "Output",
                 "Warning Text"
            };
        }

        public List<string> GetTitlesAndHeadingsCellStyles()
        {
            return new List<string>
            {
                 "Heading 1",
                 "Heading 2",
                 "Heading 3",
                 "Heading 4",
                 "Title",
                 "Total"
            };
        }

        public List<string> GetThemedCellStyles()
        {
            return new List<string>
            {
                 "20% - Accent1",
                 "20% - Accent2",
                 "20% - Accent3",
                 "20% - Accent4",
                 "20% - Accent5",
                 "20% - Accent6",
                 "40% - Accent1",
                 "40% - Accent2",
                 "40% - Accent3",
                 "40% - Accent4",
                 "40% - Accent5",
                 "40% - Accent6",
                 "60% - Accent1",
                 "60% - Accent2",
                 "60% - Accent3",
                 "60% - Accent4",
                 "60% - Accent5",
                 "60% - Accent6",
                 "Accent1",
                 "Accent2",
                 "Accent3",
                 "Accent4",
                 "Accent5",
                 "Accent6"
            };
        }

        public List<string> GetNumberFormatCellStyles()
        {
            return new List<string>
            {
                 "Comma",
                 "Comma [0]",
                 "Currency",
                 "Currency [0]",
                 "Percent"
            };
        }
    }
}
