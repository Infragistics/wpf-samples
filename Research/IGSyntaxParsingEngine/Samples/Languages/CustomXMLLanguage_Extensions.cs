using System.Windows.Media;
using Infragistics.Documents.Parsing;

namespace IGSyntaxParsingEngine.Samples.Languages
{
    public partial class CustomXMLLanguage
    {
        #region GetColor
        static public Color GetColor(TerminalSymbol symbol)
        {
            string symbolName = symbol.Name;
            Color color = Colors.Black;

            switch (symbolName)
            {
                // attribute values
                case "LITERAL_OPEN":
                case "LITERAL_CLOSE":
                case "LITERAL_CONTENT":
                    color = Colors.Red;
                    break;

                // open and closing braces for the tags, equal and questions signs
                case "TAG_OPEN":
                case "TAG_CLOSE":
                case "QuestionToken":
                case "EqualsToken":
                case "SLASH":
                case "CDATA_DEC_OPEN":
                case "CDATA_DEC_CLOSE":
                    color = Colors.Blue;
                    break;
                
                // text between tags
                case "TAG_VALUE_CONTENT":
                    color = Colors.Black;
                    break;

                // tags, attributes, colon and dot in the tag ot attrbutes
                case "XML_DEC":
                case "IdentifierToken":
                case "ColonToken":
                case "DotToken":
                    color = Colors.Brown;
                    break;

                // cdata content
                case "CDATA_TEXT":
                    color = Colors.DarkGray;
                    break;

                // string entities
                case "ENTITY_START":
                case "ENTITY_END":
                case "ENTITY_AMP":
                case "ENTITY_LT":
                case "ENTITY_GT":
                case "ENTITY_QUOT":
                case "ENTITY_APOS":
                case "ENTITY_TEXT":
                    color = Colors.Orange;
                    break;

                // comment start and end tags, comment content
                case "COMMENT_START":
                case "COMMENT_END":
                case "COMMENT_TEXT":
                    color = Colors.Green;
                    break;
            }

            return color;
        }
        #endregion GetColor
    }
}
