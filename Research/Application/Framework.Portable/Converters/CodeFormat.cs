using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable DoNotCallOverridableMethodsInConstructor
namespace Infragistics.Framework.Converters
{
    /// <summary>
    /// Provides a base class for formatting most programming languages.
    /// </summary>
    public abstract class CodeFormat : SourceFormat
    {
        /// <summary>
        /// Must be overridden to provide a list of keywords defined in 
        /// each language.
        /// </summary>
        /// <remarks>
        /// Keywords must be separated with spaces.
        /// </remarks>
        protected abstract string Keywords
        {
            get;
        }

        /// <summary>
        /// Can be overridden to provide a list of preprocessors defined in each language.
        /// </summary>
        /// <remarks>
        /// Preprocessors must be separated with spaces.
        /// </remarks>
        protected virtual string Preprocessors
        {
            get { return ""; }
        }

       
        /// <summary>
        /// Must be overridden to provide a regular expression string
        /// to match strings literals. 
        /// </summary>
        protected abstract string StringRegEx
        {
            get;
        }

        /// <summary>
        /// Must be overridden to provide a regular expression comment in code 
        /// to match comments. 
        /// </summary>
        protected abstract string CommentRegEx
        {
            get;
        }

        /// <summary>
        /// Can be overridden to provide a regular expression /// comment
        /// </summary>
        protected virtual string CommentSlashRegEx
        {
            get { return ""; }
        }

        /// <summary>
        /// Determines if the language is case sensitive.
        /// </summary>
        /// <value><b>true</b> if the language is case sensitive, <b>false</b> 
        /// otherwise. The default is true.</value>
        /// <remarks>
        /// A case-insensitive language formatter must override this 
        /// property to return false.
        /// </remarks>
        public virtual bool CaseSensitive
        {
            get { return true; }
        }
        /// <summary>
        /// Can be overridden to provide a regular expression for types of objects
        /// </summary>
        protected virtual List<string> Types
        {
            get { return new List<string>(); } 
        }
        /// <summary/>
        protected CodeFormat()
        {
            //if (Keywords == null) throw new Exception("Keywords");

            //generate the keyword and preprocessor Regex from the keyword lists
            var reg = new Regex(@"\w+|-\w+|#\w+|@@\w+|#(?:\\(?:s|w)(?:\*|\+)?\w+)+|@\\w\*+");
            var regKeyword = reg.Replace(Keywords,      @"(?<=^|\W)$0(?=\W)");
            var regPreproc = reg.Replace(Preprocessors, @"(?<=^|\s)$0(?=\s|$)");
            reg = new Regex(@" +");
            regKeyword = reg.Replace(regKeyword, @"|");
            regPreproc = reg.Replace(regPreproc, @"|");
           
            if (regPreproc.Length == 0)
                regPreproc = "(?!.*)_{37}(?<!.*)"; //use something quite impossible...
            
            //build a master Regex with capturing groups
            var regAll = new StringBuilder();
            regAll.Append("(");
            //regAll.Append(CommentSlashRegEx);
            //regAll.Append(")|(");
            regAll.Append(CommentRegEx);
            regAll.Append(")|(");
            regAll.Append(StringRegEx);
            if (regPreproc.Length > 0)
            {
                regAll.Append(")|(");
                regAll.Append(regPreproc);
            }
             
            regAll.Append(")|(");
            regAll.Append(regKeyword);

            if (Types != null && Types.Count > 0)
            {
                foreach (var pattern in Types)
                {
                    regAll.Append(")|(");
                    regAll.Append(pattern);
                }
            }
            regAll.Append(")");

            var caseInsensitive = CaseSensitive ? 0 : RegexOptions.IgnoreCase;
            CodeRegex = new Regex(regAll.ToString(), RegexOptions.Multiline | caseInsensitive);

            CodeSpans = new List<CodeSpan>();
        }

        /// <summary>
        /// Called to evaluate the HTML fragment corresponding to each 
        /// matching token in the code.
        /// </summary>
        /// <param name="match">The <see cref="Match"/> resulting from a 
        /// single regular expression match.</param>
        /// <returns>A string containing the HTML code fragment.</returns>
        protected override string MatchEval(Match match) //protected override
        {
            if (match.Groups[1].Success) // code comment
            {
                var reader = new StringReader(match.ToString());
                string line; 
                while ((line = reader.ReadLine()) != null)
                {
                    var span = new CodeSpan(line);
                    span.Foreground = CodeStyles.Comment; 
                    CodeSpans.Add(span);
                }
                return "::::::";
            }
            //else if (match.Groups[2].Success) // triple slash comment
            //{
            //    var reader = new StringReader(match.ToString());
            //    string line; 
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        var span = new CodeSpan(line);
            //        span.Foreground = CodeStyles.Comment; // new Paint(0, 128, 0);
            //        CodeSpans.Add(span);
            //    }
            //    return "::::::";
            //}
            else if (match.Groups[2].Success) //string literal
            {
                var span = new CodeSpan(match.ToString());
                span.Foreground = CodeStyles.String; 
                CodeSpans.Add(span);
                return "::::::";
            }
            else if (match.Groups[3].Success) //preprocessor keyword
            {
                var span = new CodeSpan(match.ToString());
                span.Foreground = CodeStyles.Preprocessor; 

                CodeSpans.Add(span);
                return "::::::";
            }
            
            else if ((match.Groups[4]).Success) //keyword
            {
                var span = new CodeSpan(match.ToString());
                span.Foreground = CodeStyles.Keyword;  
                CodeSpans.Add(span);
                return "::::::";
            }
            else //if (match.Groups[4].Success) //types
            {
                foreach (Group g in match.Groups)
                {
                    if (g.Success)
                    {
                        var span = new CodeSpan(match.ToString());
                        span.Foreground = CodeStyles.Type; 
                        CodeSpans.Add(span);
                        return "::::::";
                    }
                }
               // var os = match.Groups.ToList().Select(global => )
                
            }
             
            return "";
        }
    }
}

// ReSharper restore DoNotCallOverridableMethodsInConstructor