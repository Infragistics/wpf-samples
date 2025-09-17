using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Infragistics.Framework.Converters
{
    /// <summary>
    ///	Provides a base implementation for all code formatters.
    /// </summary>
    public abstract class SourceFormat
    {
        /// <summary/>
        protected SourceFormat()
        {
            CodeStyles = new CodeStyles();
            CodeSpans = new List<CodeSpan>();
            TabSpaces = 2;
            UseLineNumbers = false;
            UseEmptyLines = false;
            Alternate = false;
            EmbedStyleSheet = false;
        }

        /// <summary>
        /// Gets or sets the tabs width.
        /// </summary>
        /// <value>The number of space characters to substitute for tab 
        /// characters. The default is <b>4</b>, unless overridden is a 
        /// derived class.</value>
        public byte TabSpaces { get; set; }
         
        public CodeStyles CodeStyles { get; set; }
    
        /// <summary>
        /// Enables or disables line numbers in output.
        /// </summary>
        /// <value>When <b>true</b>, line numbers are generated (default: <b>false</b>)</value>
        public bool UseLineNumbers { get; set; }

        public bool UseEmptyLines { get; set; }
        
        /// <summary>
        /// Enables or disables alternating line background.
        /// </summary>
        /// <value>When <b>true</b>, lines background is alternated (default: <b>false</b>)</value>
        public bool Alternate { get; set; }
         
        /// <summary>
        /// Enables or disables the embedded CSS style sheet.
        /// </summary>
        /// <value>When <b>true</b>, the CSS &lt;style&gt; element is included 
        /// in the HTML output (default: <b>false</b>)</value>
        public bool EmbedStyleSheet { get; set; }

        /// <summary>
        /// Transforms a source code string to HTML 4.01.
        /// </summary>
        /// <returns>A string containing the HTML formatted code.</returns>
        public List<CodeSpan> FormatCode(string source)
        {
            return FormatCode(source, UseEmptyLines, UseLineNumbers, Alternate, EmbedStyleSheet, false);
        }
        //public Paragraph FormatCode(string source)
        //{
        //    return FormatCode(source, _lineNumbers, _alternate, _embedStyleSheet, false);
        //}

        /// <summary>
        /// The regular expression used to capture language tokens.
        /// </summary>
        protected Regex CodeRegex { get; set; }
    
        //private List<Run> codeParagraphGlobal;
        ///// <summary>
        ///// This is a List of Run's that can be added later to the string of code
        ///// </summary>
        //protected List<Run> CodeParagraphGlobal
        //{
        //    get { return codeParagraphGlobal; }
        //    set { codeParagraphGlobal = value; }
        //}

        /// <summary>
        /// Gets or sets CodeSpans
        /// </summary>
        public List<CodeSpan> CodeSpans { get; set; }
    
        /// <summary>
        /// Called to evaluate the HTML fragment corresponding to each 
        /// matching token in the code.
        /// </summary>
        /// <param name="match">The <see cref="Match"/> resulting from a 
        /// single regular expression match.</param>
        /// <returns>A string containing the HTML code fragment.</returns>
        protected abstract string MatchEval(Match match); //protected abstract
      
        internal static string LineBreak = "\r\n";
        
        private List<CodeSpan> FormatCode(string source, bool useBlankLines, bool lineNumbers,
            bool alternate, bool embedStyleSheet, bool subCode)
        {
            CodeSpans = new List<CodeSpan>();
            
            if (!useBlankLines)
            {
                source = source.RemoveBlankLines();
            }

            var spans = new List<CodeSpan>();
            var sb = new StringBuilder(source);
            //replace special characters
            source = CodeRegex.Replace(sb.ToString(), new MatchEvaluator(this.MatchEval));
         
            string[] regMatches = { "::::::" };
            var split = source.Split(regMatches, new StringSplitOptions());

            var currentChunk = 0;
            //var statement = false;
            var intent = 0;

            foreach (var code in split)
            {
                currentChunk++;
                //var span = new CodeSpan();
                //span.Text = code.Replace("\r\n", "");
                //span.Foreground = CodeStyles.Default;
                //spans.Add(span);
                if (code.Contains(LineBreak))
                {
                    var lines = code.SplitToLines();
                    for (var i = 0; i < lines.Count(); i++)
                    {
                        var line = lines[i];
                     
                        if (code.Trim().StartsWith("}")) intent--;
            
                        spans.Add(new CodeSpan(line, CodeStyles.Default));

                        if (!line.Trim().Equals("") && 
                            !line.Trim().EndsWith("=") && i < lines.Count() - 1)
                            spans.Add(new CodeSpan(LineBreak));
                            
                            if (line.Trim().StartsWith("{")) intent++;
                      
                    }
                }
                else
                {
                    if (code.Trim().StartsWith("}")) intent--;
                    
                    spans.Add(new CodeSpan(code, CodeStyles.Default));

                    if (code.Trim().StartsWith("{")) intent++;
                }
                

                if ((currentChunk - 1) < CodeSpans.Count)
                {
                    var item = CodeSpans[currentChunk - 1];
                    spans.Add(item);
                    if (item.Text.Trim().StartsWith("//"))
                    {
                        spans.Add(new CodeSpan(LineBreak));
                    }
                }
            }
            return spans;
        }

    }
}