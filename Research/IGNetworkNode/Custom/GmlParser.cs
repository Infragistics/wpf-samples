using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace IGNetworkNode.Custom
{
    public class GmlNode
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public ObservableCollection<GmlConnection> Connections { get; set; }

        public GmlNode()
        {
            Connections = new ObservableCollection<GmlConnection>();
        }
    }

    public class GmlConnection : IEquatable<GmlConnection>
    {
        public GmlNode Target { get; set; }
        public string Label { get; set; }

        #region IEquatable<GmlConnection> Members

        public bool Equals(GmlConnection other)
        {
            return this.Target.Equals(other.Target);
        }

        #endregion
    }

    public class GmlParser
    {
        private List<GmlNode> _nodes;

        public IEnumerable<GmlNode> Parse(TextReader reader)
        {
            var scanner = new GmlScanner();
            string propertyKey = null;
            GmlConnection connection = null;
            var lastElement = 0;

            _nodes = new List<GmlNode>();

            foreach (var token in scanner.GetTokens(reader))
            {
                switch (token.Type)
                {
                    case GmlTokenType.Key:
                        switch (token.Data)
                        {
                            case "node":
                                _nodes.Add(new GmlNode());
                                lastElement = 1;
                                break;
                            case "edge":
                                connection = new GmlConnection();
                                lastElement = 2;
                                break;
                            case "id":
                            case "label":
                            case "source":
                            case "target":
                                if (lastElement > 0)
                                {
                                    propertyKey = token.Data;
                                }
                                else
                                {
                                    propertyKey = null;
                                }
                                break;
                            default:
                                lastElement = 0;
                                break;
                        }
                        break;
                    case GmlTokenType.Int:
                        if (propertyKey != null)
                        {
                            switch (propertyKey)
                            {
                                case "id":
                                    if (lastElement == 1)
                                    {
                                        _nodes[_nodes.Count - 1].Id = Convert.ToInt32(token.Data);
                                    }
                                    break;
                                case "source":
                                    var sourceId = Convert.ToInt32(token.Data);
                                    FindNodeById(sourceId).Connections.Add(connection);
                                    break;
                                case "target":
                                    var targetId = Convert.ToInt32(token.Data);
                                    connection.Target = FindNodeById(targetId);
                                    break;
                            }
                        }
                        break;
                    case GmlTokenType.String:
                        if (propertyKey == "label")
                        {
                            if (lastElement == 1)
                            {
                                _nodes[_nodes.Count - 1].Label = token.Data;
                            }
                            else if (lastElement == 2)
                            {
                                connection.Label = token.Data;
                            }
                        }
                        break;
                }
            }

            return _nodes;
        }

        private GmlNode FindNodeById(int id)
        {
            return _nodes.First(node => node.Id == id);
        }
    }

    public class GmlScanner
    {
        private static readonly string[] IsoTable = { "&nbsp;", "&iexcl;", "&cent;", "&pound;", "&curren;", "&yen;", "&brvbar;", "&sect;", "&uml;", "&copy;", "&ordf;", "&laquo;", "&not;", "&shy;", "&reg;", "&macr;", "&deg;", "&plusmn;", "&sup2;", "&sup3;", "&acute;", "&micro;", "&para;", "&middot;", "&cedil;", "&sup1;", "&ordm;", "&raquo;", "&frac14;", "&frac12;", "&frac34;", "&iquest;", "&Agrave;", "&Aacute;", "&Acirc;", "&Atilde;", "&Auml;", "&Aring;", "&AElig;", "&Ccedil;", "&Egrave;", "&Eacute;", "&Ecirc;", "&Euml;", "&Igrave;", "&Iacute;", "&Icirc;", "&Iuml;", "&ETH;", "&Ntilde;", "&Ograve;", "&Oacute;", "&Ocirc;", "&Otilde;", "&Ouml;", "&times;", "&Oslash;", "&Ugrave;", "&Uacute;", "&Ucirc;", "&Uuml;", "&Yacute;", "&THORN;", "&szlig;", "&agrave;", "&aacute;", "&acirc;", "&atilde;", "&auml;", "&aring;", "&aelig;", "&ccedil;", "&egrave;", "&eacute;", "&ecirc;", "&euml;", "&igrave;", "&iacute;", "&icirc;", "&iuml;", "&eth;", "&ntilde;", "&ograve;", "&oacute;", "&ocirc;", "&otilde;", "&ouml;", "&divide;", "&oslash;", "&ugrave;", "&uacute;", "&ucirc;", "&uuml;", "&yacute;", "&thorn;", "&yuml;" };

        private static int DecodeIsoChar(string str, int len)
        {
            int i;
            int ret = '&';

            if (string.Compare(str, 0, "&quot;", 0, len) == 0)
            {
                return 34;
            }

            if (string.Compare(str, 0, "&amp;", 0, len) == 0)
            {
                return 38;
            }

            if (string.Compare(str, 0, "&lt;", 0, len) == 0)
            {
                return 60;
            }

            if (string.Compare(str, 0, "&gt;", 0, len) == 0)
            {
                return 62;
            }

            for (i = 0; i < 96; i++)
            {
                if (string.Compare(str, 0, IsoTable[i], 0, len) == 0)
                {
                    ret = i + 160;
                    break;
                }
            }

            return ret;
        }

        public IEnumerable<GmlToken> GetTokens(TextReader reader)
        {
            var line = 1;
            var column = 1;

            var sb = new StringBuilder();

            var i = reader.Peek();

            while (i > -1)
            {
                var c = (char)i;

                if (char.IsWhiteSpace(c))
                {
                    if (c == '\n')
                    {
                        line++;
                        column = 1;
                    }
                    else
                    {
                        column++;
                    }

                    reader.Read();
                }
                else
                {
                    int previousLine;
                    int previousColumn;

                    if (char.IsDigit(c) || c == '.' || c == '+' || c == '-')
                    {
                        var isDouble = false;
                        previousLine = line;
                        previousColumn = column;

                        do
                        {
                            if (c == '.')
                            {
                                if (isDouble)
                                {
                                    throw new GmlParseException(line, column);
                                }

                                isDouble = true;
                            }

                            sb.Append(c);
                            column++;

                            reader.Read();
                            c = (char)reader.Peek();

                        } while (char.IsDigit(c) || c == '.');

                        if (!char.IsWhiteSpace(c))
                        {
                            throw new GmlParseException(line, column);
                        }

                        if (c == '\r' || c == '\n')
                        {
                            line++;
                            column = 1;
                        }

                        if (isDouble)
                        {
                            yield return new GmlToken(GmlTokenType.Double, previousLine, previousColumn, sb.ToString());
                        }
                        else
                        {
                            yield return new GmlToken(GmlTokenType.Int, previousLine, previousColumn, sb.ToString());
                        }
                    }
                    else if (char.IsLetter(c) || c == '_')
                    {
                        previousLine = line;
                        previousColumn = column;

                        do
                        {
                            sb.Append(c);
                            column++;

                            reader.Read();
                            c = (char)reader.Peek();
                        } while (char.IsLetterOrDigit(c) || c == '_');

                        if (c == '\r' || c == '\n')
                        {
                            line++;
                            column = 1;
                        }

                        if (!char.IsWhiteSpace(c))
                        {
                            throw new GmlParseException(line, column);
                        }

                        yield return new GmlToken(GmlTokenType.Key, previousLine, previousColumn, sb.ToString());
                    }
                    else
                    {
                        previousLine = line;
                        previousColumn = column;

                        switch (c)
                        {
                            case '#':
                                do
                                {
                                    reader.Read();
                                    c = (char)reader.Peek();
                                } while (c != '\r' && c != '\n');

                                line++;
                                column = 1;
                                break;
                            case '[':
                                line++;
                                reader.Read();

                                yield return new GmlToken(GmlTokenType.LBracket, previousLine, previousColumn);
                                break;
                            case ']':
                                line++;
                                reader.Read();

                                yield return new GmlToken(GmlTokenType.RBracket, previousLine, previousColumn);
                                break;
                            case '"':
                                column++;

                                reader.Read();
                                c = (char)reader.Peek();

                                while (c != '"')
                                {
                                    if (c == '&')
                                    {
                                        var iso = new StringBuilder(8);

                                        while (c != ';')
                                        {
                                            if (c == '"')
                                            {
                                                iso.Clear();
                                                break;
                                            }

                                            if (iso.Length < 8)
                                            {
                                                iso.Append(c);
                                            }

                                            reader.Read();
                                            c = (char)reader.Peek();
                                        }

                                        if (iso.Length == 8)
                                        {
                                            c = '&';
                                        }
                                        else
                                        {
                                            iso.Append(";");
                                            c = (char)DecodeIsoChar(iso.ToString(), iso.Length);
                                        }
                                    }

                                    sb.Append(c);
                                    column++;

                                    reader.Read();
                                    c = (char)reader.Peek();

                                    if (c == '\r' || c == '\n')
                                    {
                                        line++;
                                        column = 1;
                                    }
                                }
                                reader.Read();

                                yield return new GmlToken(GmlTokenType.String, previousLine, previousColumn, sb.ToString());
                                break;
                            case (char)65279:
                                column++;
                                reader.Read();
                                break;
                            default:
                                throw new GmlParseException(line, column);
                        }
                    }
                }

                i = reader.Peek();
                sb.Clear();
            }
        }
    }

    public class GmlParseException : Exception
    {
        private readonly int _line;
        private readonly int _column;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"/> class.
        /// </summary>
        public GmlParseException(int line, int column)
        {
            _line = line;
            _column = column;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        /// <param name="line"></param>
        /// <param name="column"></param>
        public GmlParseException(string message, int line, int column)
            : base(message)
        {
            _line = line;
            _column = column;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param><param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        /// <param name="line"></param>
        /// <param name="column"></param>
        public GmlParseException(string message, Exception innerException, int line, int column)
            : base(message, innerException)
        {
            _line = line;
            _column = column;
        }

        public int Line
        {
            get { return _line; }
        }

        public int Column
        {
            get { return _column; }
        }
    }

    public enum GmlTokenType
    {
        Key,
        Int,
        Double,
        String,
        LBracket,
        RBracket
    }

    public class GmlToken
    {
        public GmlTokenType Type { get; private set; }
        public string Data { get; private set; }
        public int Line { get; private set; }
        public int Column { get; private set; }

        public GmlToken(GmlTokenType type, int line, int column, string data)
        {
            Type = type;
            Data = data;
            Line = line;
            Column = column;
        }

        public GmlToken(GmlTokenType type, int line, int column)
            : this(type, line, column, null)
        {
        }
    }
}
