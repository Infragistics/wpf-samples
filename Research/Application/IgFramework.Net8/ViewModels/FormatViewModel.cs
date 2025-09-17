using Infragistics.Controls;
using System;
using System.Collections.Generic;
using System.Globalization; 
using System.Runtime.CompilerServices; 
//using Infragistics.Controls; 

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a view model for Format Specifiers 
    /// </summary>
    public class FormatViewModel : ObservableModel
    { 
        public FormatViewModel()
        {
            FormatSuffixes = new List<string> { NULL, "K", "]", "M", "B", "%" };
            FormatPrefixes = new List<string> { NULL, "$", "[", "+", "-", "@" };

            Notations = new List<string> { NULL, EMPTY, "N", "J" };
            SignDisplays = new List<string> { "auto", "always", "exceptZero", "never" };

            Styles = new List<string> { NULL, "decimal", "currency", "percent", "never" };
            
            // start from some often used locals
            Locals = new List<string> { NULL,
                "en-US (English (United States))",
                "en-GB (English (United Kingdom))",
                "nl-NL (Dutch (Netherlands))",
                "ja-JP (Japanese (Japan))",
                "zh-Hans (Chinese (Simplified))",
                "pl-PL (Polish (Poland))" };

            // get rest of locals
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var locals = new List<string>();
            foreach (var c in cultures)
            {
                var info = c.Name + " (" + c.EnglishName + ")";
                // skip generic locals
                if (c.Name.Contains("-") && !Locals.Contains(info))
                {
                    locals.Add(info);
                }
            }
            Locals.AddRange(locals); 
        }

        public List<string> FormatSuffixes { get; set; }
        public List<string> FormatPrefixes { get; set; }
        public List<string> Notations { get; set; }
        public List<string> SignDisplays { get; set; }
        public List<string> Locals { get; set; }
        public List<string> Styles { get; set; }

        private const string NULL = "<NULL>";
        private const string EMPTY = "<EMPTY>";

        private string _FormatSuffix = null;
        public string FormatSuffix
        {
            get { return _FormatSuffix; }
            set
            {
                if (value == NULL) value = null;
                if (value == EMPTY) value = "";

                if (_FormatSuffix == value) return;

                _FormatSuffix = value;
                OnPropertyChanged();
                OnPropertyChanged("FormatString");
            }
        }

        private string _FormatPrefix = null;
        public string FormatPrefix
        {
            get { return _FormatPrefix; }
            set
            {
                if (value == NULL) value = null;
                if (value == EMPTY) value = "";

                if (_FormatPrefix == value) return;
                _FormatPrefix = value;
                OnPropertyChanged();
                OnPropertyChanged("FormatString");
            }
        }

        private bool _FormatWithSpaces = true;
        public bool FormatWithSpaces
        {
            get { return _FormatWithSpaces; }
            set
            {
                if (_FormatWithSpaces == value) return;
                _FormatWithSpaces = value;
                OnPropertyChanged();
                OnPropertyChanged("FormatString");
            }
        }


        public string FormatString // e.g. '$ {0} K'
        {
            get
            {
                var sep = _FormatWithSpaces ? " " : "";
                return _FormatPrefix + sep + "{0}" + sep + _FormatSuffix;
            }
        }

        public string FormatDisplay // e.g. '"$ {0} K"'
        {
            get
            {
                var sep = _FormatWithSpaces ? " " : "";
                return "\"" + FormatString + "\"";
            }
        }

        private int _MinimumFractionDigits = 0;
        public int MinimumFractionDigits
        {
            get { return _MinimumFractionDigits; }
            set
            {
                if (_MinimumFractionDigits == value) return;
                _MinimumFractionDigits = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private int _MaximumFractionDigits = 4;
        public int MaximumFractionDigits
        {
            get { return _MaximumFractionDigits; }
            set
            {
                if (_MaximumFractionDigits == value) return;
                _MaximumFractionDigits = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private int _MinimumIntegerDigits = 1;
        public int MinimumIntegerDigits
        {
            get { return _MinimumIntegerDigits; }
            set
            {
                if (_MinimumIntegerDigits == value) return;
                _MinimumIntegerDigits = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private bool _UseGrouping = true;
        public bool UseGrouping
        {
            get { return _UseGrouping; }
            set
            {
                if (_UseGrouping == value) return;
                _UseGrouping = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private string _Notation = "";
        public string Notation
        {
            get { return _Notation; }
            set
            {
                if (_Notation == value) return;
                _Notation = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private string _SignDisplay = "exceptZero"; //"auto";
        public string SignDisplay
        {
            get { return _SignDisplay; }
            set
            {
                if (value == NULL) value = null;
                if (value == EMPTY) value = "";

                if (_SignDisplay == value) return;
                _SignDisplay = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private string _Style = "decimal";
        public string Style
        {
            get { return _Style; }
            set
            {
                if (value == NULL) value = null;
                if (value == EMPTY) value = "";

                if (_Style == value) return;

                _Style = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        private string _Local = null;
        public string Local
        {
            get { return _Local; }
            set
            {
                if (value == NULL) value = null;
                if (value == EMPTY) value = "";

                var parts = value.Split(' ');
                if (parts.Length > 1)
                {
                    value = parts[0];
                }

                if (_Local == value) return;
                _Local = value;
                OnPropertyChanged(); OnPropertyChanged("Specifiers");
            }
        }

        public FormatSpecifier Specifier
        {
            get
            {
                return new NumberFormatSpecifier
                {
                    MinimumFractionDigits = MinimumFractionDigits,
                    MaximumFractionDigits = MaximumFractionDigits,
                    MinimumIntegerDigits = MinimumIntegerDigits,
                    SignDisplay = SignDisplay,
                    UseGrouping = UseGrouping,
                    Notation = Notation,
                    Locale = Local,
                    Style = Style,
                };
            }
        }
         
        public object[] Specifiers
        {
            get
            { 
                return new object[] { Specifier };
            }
        }
    }

}
