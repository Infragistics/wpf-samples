using System;
using System.Collections.Generic;

namespace Infragistics.Framework.Converters
{
    /// <summary>
    /// Provides formatting for C# language 
    /// </summary>
    public class CodeCsharp : CodeCLikeFormat
    {
        public CodeCsharp()
        {
            CodeStyles.Default = Paints.White;
            CodeStyles.Preprocessor = Paints.Gray;
            CodeStyles.Keyword = Paint.FromString("#FF4B98D7");
            CodeStyles.Type = Paint.FromString("#FF5FC8A4");
            CodeStyles.Symbol = Paints.White;
            CodeStyles.Property = Paints.White;
            CodeStyles.String = Paint.FromString("#FFBB7F58");
            CodeStyles.Comment = Paint.FromString("#FF34D06A");
        }
        #region Properties
        /// <summary>
        /// The list of C# keywords.
        /// </summary>
        protected override string Keywords
        {
            get
            {
                return "add abstract alias as ascending async await base bool break byte case catch char "
                     + "checked class const continue decimal default delegate descending do double dynamic else "
                     + "enum event explicit extern false finally fixed float for foreach goto group get "
                     + "if implicit in int interface internal is join let lock long namespace new null "
                     + "object operator orderby out override partial params private protected public readonly "
                     + "ref return sbyte sealed select set short sizeof stackalloc static string struct "
                     + "switch this throw true try typeof uint ulong unchecked unsafe ushort "
                     + "using var virtual void volatile where while yield";
            }
        }

        /// <summary>
        /// The list of C# preprocessors.
        /// </summary>
        protected override string Preprocessors
        {
            get
            {
                return "#if #else #elif #endif #define #undef #warning "
                     + "#error #line #region #endregion #pragma";
            }
        }
        /// <summary>
        /// The list of regular expressions for types of objects
        /// </summary>
        protected override List<string> Types
        {
            get
            {
                var patterns = new List<string>();
                patterns.Add(@"?<=class )(.*?)(?= :");
               
                //patterns.Add(@"?<=enum )(.*?)(?:\r\n");
                //patterns.Add(@"?<=struct )(.*?)(?:\r\n");

                //patterns.Add(@"?<=class )(.*?)(?=\{");
                //patterns.Add("?<=class )(.*?)(?:\r\n");

                patterns.Add(@"?<= is )(.*?)(?=\)");         // is TYPE
                patterns.Add(@"?<= is )(.*?)(?= ");
                patterns.Add(@"?<=as )(.*?)(?=\)");          // as TYPE
                patterns.Add(@"?<=as )(.*?)(?=\;");
                patterns.Add(@"?<=new )(.*?)(?=\(");         // new TYPE
                patterns.Add(@"?<=new )(.*?)(?=\{");
                patterns.Add(@"?<=new )(.*?)(?= ");

                patterns.Add(@"?<=\<)(.*)(?=\>");           // <TYPE>

                patterns.Add(@"?<=internal )(.*)(?=\<");        
                patterns.Add(@"?<=static )(.*)(?=\<");       
                patterns.Add(@"?<=public )(.*)(?=\<");      // public TYPE<TYPE> property
                patterns.Add(@"?<=protected )(.*)(?=\<");
                patterns.Add(@"?<=private )(.*)(?=\<");

                patterns.Add(@"?<=internal )(.*?)(?= ");       
                patterns.Add(@"?<=static )(.*?)(?= ");        
                patterns.Add(@"?<=public )(.*?)(?= ");       // public TYPE property
                patterns.Add(@"?<=protected )(.*?)(?= ");
                patterns.Add(@"?<=private )(.*?)(?= ");

                patterns.Add(@"?<== \()(.*)(?=\)Activator");
                patterns.Add(@"?<=ClearValue\()(.*)(?=\.");

                return patterns;
            }
        } 
        #endregion
    }

    /// <summary>
    /// Provides a base class for formatting languages similar to C.
    /// </summary>
    public abstract class CodeCLikeFormat : CodeFormat
    {
        /// <summary>
        /// Regular expression string to match single line and multi-line 
        /// comments (// and /* */). 
        /// </summary>
        protected override string CommentRegEx
        {
            get { return @"/\*.*?\*/|//.*?(?=\r|\n)"; }
        }

        protected override string CommentSlashRegEx
        {
            get { return @"/\*.*?\*/|///.*?(?=\r|\n)"; }
        }

        /// <summary>
        /// Regular expression string to match string and character literals. 
        /// </summary>
        protected override string StringRegEx
        {
            get { return @"@?""""|@?"".*?(?!\\).""|''|'.*?(?!\\).'"; }
        }
    }
}