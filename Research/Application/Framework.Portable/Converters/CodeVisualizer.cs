using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Infragistics.Framework.Converters
{
    public enum CodeFile
    {
        Xaml,
        XamlCSharp,
        CSharp 
    }
    public static class CodeFileEx
    {
        public static string GetFileExt(this CodeFile codeType)
        {
            if (codeType == CodeFile.Xaml)
            {
                return ".xaml";
            }
            else if (codeType == CodeFile.XamlCSharp)
            {
                return ".xaml.cs";
            } 
            else if (codeType == CodeFile.CSharp)
            {
                return ".cs";           
            }
            return "";
        }
    }
    public abstract class CodeLanguage
    {
        protected CodeLanguage()
        {
            Font = 14;

            Colors = new CodeColors();
            Symbols = new List<string>();
            Keywords = new List<string>();
        }
        /// <summary>
        /// Gets or sets Colors
        /// </summary>
        public CodeColors Colors { get; set; }

        public List<string> Symbols { get; set; }
        public List<string> Keywords { get; set; }

        public double Font { get; set; }

        public void Parse(string code)
        {

        }
    }

    public class CodeXaml : CodeLanguage
    {
        public CodeXaml()
        {
            Colors.Default = Paints.Gray;
            //Colors.Keyword = Paints.Beige;
            //Colors.Type = Paint.FromString("#FF369C94");
            //Colors.Symbol = Paints.Gray;
            //Colors.PropertyName = Paint.FromString("#FF769CAE"); //Paints.LightBlue;
            //Colors.PropertyValue = Paints.DodgerBlue;

            Colors.Symbol = Paint.FromString("#FFA5A5A5"); //Paints.White;
            Colors.Keyword = Paints.Red;
            Colors.Type = Paint.FromString("#FF93C766");
            Colors.PropertyName = Paints.White;
            //Colors.PropertyName = Paint.FromString("#FF2690FC");
            Colors.PropertyValue = Paint.FromString("#FFEC7615");
       
            Symbols = new List<string> { "</", "<", "/>", ">", }; // {  = } 

            Keywords = new List<string>
                {
                    "StaticResource", "Binding", 
                    "x:Static", 
                    "x:Reference",
                };
        }
    }

    public class CodeCsharpLanguage : CodeLanguage
    {
        public CodeCsharpLanguage()
        {
            Assemblies = new List<Assembly>();
            Colors.Default = Paints.White;
            Colors.Keyword = Paint.FromString("#FF299ACB");
            Colors.Type = Paint.FromString("#FF5FC8A4");
            Colors.Symbol = Paints.Gray;
            Colors.PropertyName = Paints.White;
            Colors.PropertyValue = Paints.White;

            Symbols = new List<string> { "{", "}", "(", ")", ";", ",", ":", };
            Keywords = new List<string>
                {
                    "public", "protected", "internal", "private",
                    "static", "class", "interface", "operator",  "typeof",
                    "null", "params", "new", "Enum", "dDelegate", "single",
                    "void", "object", "int", "double", "string", "char", "long", "bool", "byte", "decimal", 
                };

            //Types = new List<string>
            //    {
            //        "List", "Collection", "Dictionary", "IList",
            //        "static", "class", "interface", "operator",  "typeof",
            //        "null", "params", "new", "Enum", "dDelegate", "single",
            //        "void", "object", "int", "double", "string", "char", "long", "bool", "byte", "decimal", 
            //    };
        }
        public List<Type> Types { get; set; }

        public List<Assembly> Assemblies { get; set; }

        public void ConvertToHtml(string code)
        {
            var types = new List<string>();
            foreach (var assembly in Assemblies)
            {
                var items = assembly.GetTypesList();
                foreach (var item in items)
                {
                    //types.Add(item.);
                }
            }

            var CodeSpan = new HtmlCodeSpan();
            var lines = code.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                 
                foreach (var keyword in Keywords)
                {
                    if (line.Contains(keyword))
                    {
                        CodeSpan = new HtmlCodeSpan(keyword, Colors.Keyword);
                        line = line.Replace(keyword, CodeSpan.ToString());
                    }
                }
            } 
        }

        public void ConvertToCodeSpans(string code)
        {
        }
    }

    public class HtmlCodeSpan
    {
        public HtmlCodeSpan()
        {

        }

        public HtmlCodeSpan(string content, Paint foreground)
        {
            Content = content;
            Foreground = foreground;
        }
        public string Content { get; set; }
        public Paint Foreground { get; set; }

        public new string ToString()
        {
            var str = string.Empty;
            str += "<CodeSpan";
            str += "style=";
            str += "\"color:" + Foreground.ToHex("RGB") + "\"";
            str += Content;
            str += "</CodeSpan>";
            return str;
        }
    }

    public class CodeColors
    {
        public CodeColors()
        {
            Default = Paints.Gray;
            Keyword = Paints.Beige;
            Type = Paints.White;
            Symbol = Paints.White;
            PropertyName = Paints.White;
            PropertyValue = Paints.White;
            Comment = Paint.FromString("#FF34D06A");
        }

        public Paint Default { get; set; }
        public Paint Keyword { get; set; }
        public Paint Symbol { get; set; }
        public Paint Type { get; set; }
        public Paint PropertyName { get; set; }
        public Paint PropertyValue { get; set; }
        public Paint Comment { get; set; }
    }

    public static class CodeVisualizer
    {   
        public static string FontName = "";

        #region Extensions
        //public static void AddSpan(this List<CodeSpan> CodeSpans, string text, Color foreground, double font)
        //{
        //    var CodeSpan = new CodeSpan { Font = font };
        //    CodeSpan.Text = text;
        //    CodeSpan.ForegroundColor = foreground;
        //    CodeSpans.Add(CodeSpan);
        //}
        //public static void AddSpan(this List<CodeSpan> CodeSpans, string text, Color foreground)
        //{
        //    CodeSpans.AddSpan(text, foreground, Font);
        //}
        public static void AddSpan(this List<CodeSpan> spans, string text, Paint foreground, double font)
        {
            var span = new CodeSpan();
            span.Text = text;
            span.Font = (double.IsNaN(font)) ? FontSize : font;
            span.Foreground = foreground ?? Language.Colors.Default;
            spans.Add(span);
        }

        public static void AddSpan(this List<CodeSpan> spans, string text, Paint foreground)
        {
            spans.AddSpan(text, foreground, FontSize);
        }
        public static void AddSpan(this List<CodeSpan> spans, string text, double font)
        {
            spans.AddSpan(text, Language.Colors.Default, font);
        }
        public static void AddSpan(this List<CodeSpan> spans, string text)
        {
            spans.AddSpan(text, FontSize);
        }
        //public static FormattedString AddRange(this FormattedString fs, List<CodeSpan> CodeSpans)
        //{
        //    foreach (var item in CodeSpans)
        //    {
        //        fs.CodeSpans.Add(item);
        //    }
        //    return fs;
        //}
   
        #endregion

        static CodeVisualizer()
        {
            FontSize = 30;
            Language = new CodeXaml();
            CodeCsharp = new CodeCsharp();
        }
        public static CodeLanguage Language { get; set; }

        public static CodeCsharp CodeCsharp { get; set; }

        //TODO move to CodeXaml class
        public static string PropertySeperator = " ";
        //TODO move to CodeXaml class
        public static List<CodeSpan> ParseProperty(string code)
        {
            var codeSpans = new List<CodeSpan>();
            var codeSpan = new CodeSpan { Font = Language.Font };

            if (!code.Contains("="))
            {
                codeSpans.AddSpan(code, Language.Colors.Type);
            }
            else
            {
                var items = Regex.Split(code, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*)=(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                codeSpans.AddSpan(items[0], Language.Colors.PropertyName);

                if (items.Count() > 1)
                {
                    codeSpans.AddSpan("=", Language.Colors.Symbol);

                    codeSpans.AddSpan(items[1], Language.Colors.PropertyValue);
                }
            }

            return codeSpans;
        }
        //TODO move to CodeXaml class
        public static List<CodeSpan> ParseLineContent(string code)
        {
            var codeSpans = new List<CodeSpan>();
            code = code.Trim();

            foreach (var symbol in Language.Symbols)
            {
                if (code.StartsWith(symbol))
                {
                    codeSpans.AddSpan(symbol, Language.Colors.Symbol);
                    code = code.Remove(symbol).Trim();
                    var propCodeSpans = ParseProperty(code);
                    codeSpans.AddRange(propCodeSpans);

                    break;
                }
                if (code.EndsWith(symbol))
                {
                    code = code.Remove(symbol).Trim();
                    var propCodeSpans = ParseProperty(code);
                    codeSpans.AddRange(propCodeSpans);

                    codeSpans.AddSpan(symbol, Language.Colors.Symbol);
               
                    break;
                }
            }
            if (codeSpans.Count == 0)
            {
                var propCodeSpans = ParseProperty(code);
                codeSpans.AddRange(propCodeSpans);
            }
            return codeSpans;
        }
        //TODO move to CodeXaml class
        public static List<CodeSpan> ParseLine(string code)
        {
            var codeSpans = new List<CodeSpan>();
            var items = Regex.Split(code, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            foreach (var item in items)
            {
                codeSpans.AddRange(ParseLineContent(item));
                codeSpans.AddSpan(PropertySeperator);
            }

            return codeSpans;
        }

        public static List<CodeSpan> ConvertToSpans(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new Exception("Cannot convert empty code");

            code = code.Trim();

            if (code.StartsWith("<?xml"))
            {
                Language = new CodeXaml();
                return FormatCode(code);
            }
            else if (code.StartsWith("using") ||
                     code.StartsWith("namespace") || 
                     code.StartsWith("class") )
            {
                //Language = new CodeCsharpLanguage();
                return CodeCsharp.FormatCode(code);
            }
            else
            {
                throw new Exception("Not supported language: \n" + code);
            }
        }

        #region Convert HTML
        private static string ToHtmlSpan(this string text, Paint foreground)
        {
            //var color = "color:" + foreground.ToHex("RGB");
            //var style = "style=|" + color + "|";
            //style = style.Replace("|", "\"");
            var span = @"<span " + foreground.ToHtmlStyle() + ">" + text.Trim() + "</span>";
            return span;
        }
        private static string ToHtmlPara(this string text, Paint foreground)
        {
            //var color = "color:" + foreground.ToHex("RGB");
            //var style = "style=|" + color + "|";
            //style = style.Replace("|", "\"");
            var span = @"<p " + foreground.ToHtmlStyle() + ">" + text.Trim() + "</p>";
            return span;
        }
        private static string ToHtmlStyle(this Paint foreground)
        {
            var color = "color:" + foreground.ToHex("RGB");
            //class=|nowrap| 
            var style = " style=|" + color + "|";
            style = style.Replace("|", "\"");
            return style;
        } 
        #endregion

        /// <summary> Gets or sets UseBlankLines </summary>
        public static bool ShowBlankLines { get; set; }

        /// <summary> Gets or sets ShowWhitespaces </summary>
        public static bool ShowWhitespaces = false;

        internal static string LineBreak = "\r\n";
        internal static string[] LineSeperator = { LineBreak };
        internal static string Whitespace = "&nbsp;";

        public static Double FontSize { get; set; }

        public static string ConvertToHtml(string code)
        {
            //'Courier New' font-size:70%;  font-size: 34px;
            var styles = @"<style>
                            
                            body {background-color:#15171A}
                            h1   {color:white}
                            p    {
                                    font-family: 'Courier New', Consolas, monaco, monospace;
	                                font-size: " + (int)FontSize + @"px;
                                    color:white; 
                                    white-space: nowrap;
                                 }
                           </style>";
            var head = @"<head>" + styles + "</head>";
            
            if (code == null || code.Trim() == "")
            {
                code = @"<p> =======================  </p>
                         <p> &nbsp;&nbsp;MISSING SOURCE CODE  </p>
                         <p> =======================  </p>";
            }
            else
            {
                //ShowWhitespaces = true;
                if (ShowWhitespaces)
                {
                    CodeCsharp.CodeStyles.Whitespace = Paints.Yellow;
                    Whitespace = "_";
                }

                var space = "";
                var spans = ConvertToSpans(code);
               
                if (spans[0].Text.Trim() == "")
                    spans.RemoveAt(0);

                code = "";
                foreach (var item in spans)
                {
                    item.Text = item.Text.Replace("&", "&amp;");
                    item.Text = item.Text.Replace("<", "&lt;");
                    item.Text = item.Text.Replace(">", "&gt;");
                    item.Text = item.Text.Replace("©", "&copy;");
                    item.Text = item.Text.Replace("®", "&reg;");

                    if (item.Text == LineBreak || item.Text == Environment.NewLine)
                    {
                        code += "<br>";
                    }
                    else if (item.Text.Trim() == "")
                    {
                        space = item.Text;
                        space = space.Replace(" ", Whitespace);
                        //space = space.Replace(" ", "");

                        code += space.ToHtmlSpan(CodeCsharp.CodeStyles.Whitespace);
                    }
                    else if (item.Text != "")
                    {
   
                        var ind = item.Text.IndexOf(item.Text.Trim());
                        space = item.Text.Substring(0, ind);
                        space = space.Replace(" ", Whitespace);

                        //code += space.ToHtmlSpan(CodeCsharp.CodeStyles.Whitespace);
                        code += space.ToHtmlSpan(Paints.Red);
                        
                        var text = item.Text.Substring(ind);
                        code += text.ToHtmlSpan(item.Foreground);

                        if (item.Text.EndsWith(" "))
                        {
                            code += Whitespace.ToHtmlSpan(Paints.Red); 
                        }
                    }
                    else
                    {
                        code += item.Text.ToHtmlSpan(Paints.Red);
                    }
                }
                code = "<p>" + code + "</p>";
            }
            var body = @"<body>" + code + "</body>";
            var html = @"<html>" + head + body + "</html>";
            return html;
        }

        //TODO move to CodeXaml class
        public static List<CodeSpan> FormatCode(string code)
        {            
            var codeHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

            //var fs = new FormattedString();
            var indent = 0;
            var codeSpans = new List<CodeSpan>();

            var lines = code.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var index = 0;
            foreach (var item in lines)
            {
                var line = item.Replace("\t", "   ");

                if (line.Contains(codeHeader))
                {
                    codeSpans.AddSpan(codeHeader, Paints.Gray);
                }
                else
                {
                    line = line.Trim();

                    if (line == string.Empty)
                        continue;

                    if (line.StartsWith("</")) //|| line.StartsWith("</")
                    {
                        indent--;
                    }

                    var spaces = "";
                    for (int i = 0; i < indent; i++)
                    {
                        spaces += "  ";
                    }
                    codeSpans.AddSpan(spaces, Paints.Red);
                  
                    codeSpans.AddRange(ParseLine(line));

                    if (line.StartsWith("<") && !line.StartsWith("</"))
                        indent++;

                    if (line.EndsWith("/>")) //|| line.StartsWith("</")
                    {
                        indent--;
                    }

                }
                index++;
                codeSpans.Add(new CodeSpan { Text = "\r\n" });
            }
            return codeSpans;
        }
    }      
}