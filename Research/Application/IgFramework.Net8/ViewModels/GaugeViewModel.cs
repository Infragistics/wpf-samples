using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a view model for LabelFormat, TitleFormat, and SubtitleFormat Specifiers 
    /// </summary>
    public class GaugeViewModel : FormatViewModel
    {
        public override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName == "FormatString")
            {
                OnPropertyChanged("LabelFormatString");
                OnPropertyChanged("TitleFormatString");
                OnPropertyChanged("SubtitleFormatString");
                OnPropertyChanged("FormatDisplay");
            }

            base.OnPropertyChanged(propertyName);
        }
        #region FormatEvent Props
        private bool _UseLabelFormatEvent = false;
        public bool UseLabelFormatEvent
        {
            get { return _UseLabelFormatEvent; }
            set
            {
                if (_UseLabelFormatEvent == value) return;
                _UseLabelFormatEvent = value;
                OnPropertyChanged();
            }
        }

        private bool _UseSubtitleFormatEvent = false;
        public bool UseSubtitleFormatEvent
        {
            get { return _UseSubtitleFormatEvent; }
            set
            {
                if (_UseSubtitleFormatEvent == value) return;
                _UseSubtitleFormatEvent = value;
                OnPropertyChanged();
            }
        }

        private bool _UseTitleFormatEvent = false;
        public bool UseTitleFormatEvent
        {
            get { return _UseTitleFormatEvent; }
            set
            {
                if (_UseTitleFormatEvent == value) return;
                _UseTitleFormatEvent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region FormatSpecifier Props

        private bool _UseLabelFormatSpecifier = true;
        public bool UseLabelFormatSpecifier
        {
            get { return _UseLabelFormatSpecifier; }
            set
            {
                if (_UseLabelFormatSpecifier == value) return;
                _UseLabelFormatSpecifier = value;
                OnPropertyChanged(); OnPropertyChanged("LabelFormatString");
            }
        }

        private bool _UseSubtitleFormatSpecifier = true;
        public bool UseSubtitleFormatSpecifier
        {
            get { return _UseSubtitleFormatSpecifier; }
            set
            {
                if (_UseSubtitleFormatSpecifier == value) return;
                _UseSubtitleFormatSpecifier = value;
                OnPropertyChanged(); OnPropertyChanged("SubtitleFormatString");
            }
        }

        private bool _UseTitleFormatSpecifier = true;
        public bool UseTitleFormatSpecifier
        {
            get { return _UseTitleFormatSpecifier; }
            set
            {
                if (_UseTitleFormatSpecifier == value) return;
                _UseTitleFormatSpecifier = value;
                OnPropertyChanged(); OnPropertyChanged("TitleFormatString");
            }
        }

        public string LabelFormatString
        {
            get { return _UseLabelFormatSpecifier ? FormatString : null; }
        }

        public string SubtitleFormatString
        {
            get { return _UseSubtitleFormatSpecifier ? FormatString : null; }
        }

        public string TitleFormatString
        {
            get { return _UseTitleFormatSpecifier ? FormatString : null; }
        }
        #endregion

    }

}
