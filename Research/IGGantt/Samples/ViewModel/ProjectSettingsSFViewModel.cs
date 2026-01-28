using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using IGGantt.Resources;
using IGGantt.Samples.DataSource;
using System.Threading;

namespace IGGantt.Samples.ViewModel
{
    public class ProjectSettingsSFViewModel : INotifyPropertyChanged
    {
        #region Private variables

        private List<DisplayFormatNameAndCode> numberFormats;
        private List<DisplayFormatNameAndCode> dateFormats;
        private DisplayFormatNameAndCode selectedNumberFormat;
        private DisplayFormatNameAndCode selectedDateFormat;

        #endregion // Private variables

        #region Constructor

        public ProjectSettingsSFViewModel()
        {
            SelectedDateFormat = DateDisplayFormat[0];
            SelectedNumberFormat = NumberDisplayFormats[1];

            Tasks = CustomTaskProvider.GenerateCustomTasks();
        }

        #endregion // Constructor

        #region Public properties

        public object Tasks { get; set; }

        public DisplayFormatNameAndCode SelectedNumberFormat
        {
            get { return selectedNumberFormat; }
            set
            {
                if (value != null)
                {
                    selectedNumberFormat = value;
                }
                OnPropertyChanged("SelectedNumberFormat");
            }
        }

        public DisplayFormatNameAndCode SelectedDateFormat
        {
            get { return selectedDateFormat; }
            set
            {
                if (value != null)
                {
                    selectedDateFormat = value;
                }
                OnPropertyChanged("SelectedDateFormat");
            }
        }

        public List<DisplayFormatNameAndCode> NumberDisplayFormats
        {
            get
            {
                if (numberFormats == null)
                {
                    numberFormats = GetNumberDisplayFormats();
                }
                return numberFormats;
            }
        }

        public List<DisplayFormatNameAndCode> DateDisplayFormat
        {
            get
            {
                if (dateFormats == null)
                {
                    dateFormats = GetDateDisplayFormats();
                }
                return dateFormats;
            }
        }

        #endregion // Public properties

        #region Private methods for filling display formats

        private List<DisplayFormatNameAndCode> GetNumberDisplayFormats()
        {
            List<DisplayFormatNameAndCode> numberDisplayFormats = new List<DisplayFormatNameAndCode>();

            // Excerpt of standard numeric formats
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrExponentialE, Code = "E" }); // "Exponential (scientific), E"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrFixedPointF, Code = "F" }); // "Fixed-point, F"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrGeneralG, Code = "G" }); // "General, G"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrNumberN, Code = "N" }); // "Number, N"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrPercentP, Code = "P" }); // "Percent, P"

            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrRoundtripR, Code = "R" }); // "Round-trip, R"

            // Exceprt of custom numeric formats
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.numstrZeroPlaceholder, Code = "00000" }); // "Zero placeholder, 0, 1234 00000 -> 01234"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.numstrDigitPlaceholder, Code = "#####" }); // "Digit placeholder, #, 1234 ##### -> 1234"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.numstrDecimalPoint, Code = "0.00" }); // "Decimal point, ., 0.5678 0.00 -> .057"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.numstrGroupSeparator, Code = "##,#" }); // "Group separator and number scaling, , , 1234 ##,# (en-US) -> 123,4"
            numberDisplayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.numstrPercentagePlaceholder, Code = "%#0.00" }); // "Percentage placeholder, % , 0.5678 %#0.00 -> %56.78"

            numberDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.numstrPerMillePlacehoder,Code = "#0.00\u2030" }); 
            //"Per mille placeholder, \u2030 , 0.05678 #0.00\u2030 -> 56.78\u2030", Code = "#0.00\u2030"

            return numberDisplayFormats;
        }

        private List<DisplayFormatNameAndCode> GetDateDisplayFormats()
        {
            List<DisplayFormatNameAndCode> dateDisplayFormats = new List<DisplayFormatNameAndCode>();

            if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                // JP date formats
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat01, Code = "yy/M/dd H:mm" }); // "09/1/25 12:33"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat02, Code = "yy/M/d" }); // "09/1/25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat03, Code = "yyyy-MM-dd" }); // "2009-1-25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat04, Code = "yyyy/M/d tt h:mm" }); // "2009/1/25 午後 12:33"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat05, Code = "y/M/d" }); // "9/1/25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat06, Code = "M 月 d 日 H:mm" }); // "1 月 25 日 12:33"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat07, Code = "yyyy 年 M 月 d 日" }); // "2011 年 3 月 17 日"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat08, Code = "M 月 d 日" }); // "1 月 25 日"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat09, Code = "MM/dd" }); // "01/25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat10, Code = "yy/MM/dd dddd tt h:mm" }); // "09/01/25 金曜日 午後 12:33"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat11, Code = "yy/M/d (ddd)" }); // "09/1/5 金"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat12, Code = "gg y 年 M 月 d 日" }); // "平成 23 年 3 月 17 日"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat13, Code = "dddd H:mm" }); // "金曜日 23:33"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat14, Code = "M 月 d 日 dddd" }); // "1 月 5 日 金曜日"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat15, Code = "M/d (ddd)" }); // "1/25 金"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat16, Code = "d 日 (ddd)" }); // "25 日 金"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat17, Code = "M/d" }); // "1/25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat18, Code = "d 日" }); // "25 日"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.jpDateFormat19, Code = "tt h:mm" }); // "午後 12:33"
            }
            else
            {
                // EN date formats
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1slash25slash091233PM, Code = "M/dd/yy hh:mm tt" }); // "1/25/09 12:33 PM"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1slash25slash09, Code = "M/dd/yy" }); //"1/25/09"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1slash25slash2009, Code = "M/dd/yy" }); // "1/25/2009"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJanuary25c20091233PM, Code = "MMMM dd, yyyy hh:mm tt" }); // "January 25, 2009 12:33 PM"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJanuary25c2009, Code = "MMMM dd, yyyy" }); // "January 25, 2009"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJan251233PM, Code = "MMMM dd hh:mm tt" }); // "Jan 25 12:33 PM"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJan25cmap09, Code = "MMM dd, \\'yy" }); // "Jan 25, '09"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJanuary25, Code = "MMMM dd" }); // "January 25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJan25, Code = "MMM dd" }); // "Jan 25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1slash25slash091233PM, Code = "ddd M/dd/yy hh:mm tt" }); // "Sun 1/25/09 12:33 PM"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1slash25slash09, Code = "ddd M/dd/yy" }); // "Sun 1/25/09"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJan25cmap12, Code = "ddd MMM dd, \\'yy" }); // "Sun Jan 25, '12"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1233PM, Code = "ddd hh:mm tt" }); // "Sun 12:33 PM"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJan25, Code = "ddd MMM dd" }); // "Sun Jan 25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1slash25, Code = "ddd M/d" }); // "Sun 1/25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun25, Code = "ddd dd" }); // "Sun 25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1slash25, Code = "M/d" }); // "1/25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds25, Code = "dd" }); // "25"
                dateDisplayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1233PM, Code = "hh:mm tt" }); // "12:33 PM"
            }

            return dateDisplayFormats;
        }

        #endregion // Private methods for filling display formats

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged

        #region DisplayFormatNameAndCode helper class definition

        public class DisplayFormatNameAndCode
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        #endregion // DisplayFormatNameAndCode helper class definition
    }
}
