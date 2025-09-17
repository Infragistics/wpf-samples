using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGGantt.Resources;
using IGGantt.Samples.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class TimescaleDisplayFormats : SampleContainer
    {
        List<DisplayFormatNameAndCode> displayFormats;

        public TimescaleDisplayFormats()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs rea)
        {
            GetProjectData();
            FillDisplayFormats();
        }

        private void GetProjectData()
        {
            DataContext = ProjectDataHelper.GenerateProjectData();
        }

        private void cbDisplayFormatOnChanged(object sender, SelectionChangedEventArgs scea)
        {
            ComboBox cb = sender as ComboBox;
            Timescale ts = (xamGantt.TimescaleResolved as Timescale);
            if (ts.Bands.Count > 0)
            {
                if (cb.Name == "cbDisplayFormatTop")
                {
                    ts.Bands[0].DisplayFormat = displayFormats[cb.SelectedIndex].Code;
                }
                else if (cb.Name == "cbDisplayFormatMiddle")
                {
                    ts.Bands[1].DisplayFormat = displayFormats[cb.SelectedIndex].Code;
                }
            }

            xamGantt.ViewSettings = new ProjectViewSettings();
            xamGantt.ViewSettings.Timescale = ts;
        }

        #region Fill combos methods

        private void FillDisplayFormats()
        {
            CultureInfo cijp = new CultureInfo("ja-JP");
            if (System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Equals(cijp.ToString()) ||
                System.Threading.Thread.CurrentThread.CurrentUICulture.ToString().Equals(cijp.ToString()))
            {
                FillCombosForJPCulture();
            }
            else
            {
                FillCombosForUSCulture();
            }
        }

        private void FillCombosForUSCulture()
        {
            displayFormats = new List<DisplayFormatNameAndCode>();

            // Simulating MS Project 2010 Display format menu.
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJan25cmap09, Code = "MMM dd, \\'yy" }); // "Jan 25, '09"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJanuary25, Code = "MMMM dd" }); // "January 25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJan25cmFeb1ellipsis, Code = "MMM dd" }); // "Jan 25, Feb 1,..."
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsJ25cmF1ellpsis, Code = "{m:1} dd" }); // "J 25, Feb 1,..."
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1slash25slash12, Code = "M/d/yy" }); // "1/25/12"

            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1slash25cm2slash1cmellipsis, Code = "M/dd" }); // "1/25, 2/1,..."
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun25, Code = "ddd dd" }); // "Sun 25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1slash25slash12, Code = "ddd M/d/yy" }); // "Sun 1/25/12"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJanuary25cmap12, Code = "ddd MMMM dd, \\'yy" }); // "Sun January 25, '12"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJan25cmap12, Code = "ddd MMM dd, \\'yy" }); // "Sun Jan 25, '12"

            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJanuary25, Code = "ddd MMMM dd" }); // "Sun January 25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJan25, Code = "ddd MMM dd" }); // "Sun Jan 25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSJan25, Code = "{day:1} MMM dd" }); // "S Jan 25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSunJ25, Code = "ddd {m:1} dd" }); // "Sun J 25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSJ25, Code = "{day:1} {m:1} dd" }); // "S J 25"

            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1slash25, Code = "ddd M/d" }); // "Sun 1/25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsS1slash25, Code = "{day:1} M/d" }); // "S 1/25"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds1cm2cmellipsis52cm1, Code = "{w}" }); // "1,2,..., 52, 1, 2"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSun1cmellpsiscmSun52, Code = "ddd {w}" }); // "Sun 1,..., Sun 52"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds11cmellipsis71cm12cmellipsis752, Code = "d {w}" }); // "1 1,...,7 1, 1 2, ..., 7 52"

            displayFormats.Add(new DisplayFormatNameAndCode {
                Name = GanttStrings.tdsWeek1fromstart, Code = String.Format("{0} {1} ", GanttStrings.Week, "{start:w}") });  // "Week 1, Week 2, (From start)"
            displayFormats.Add(new DisplayFormatNameAndCode {
                Name = GanttStrings.tdsW1fromstart, Code = String.Format("{0} {1}", GanttStrings.W, "{start:w1}") }); // "W1, W2, W3, W4 (From start)"  
            displayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.tdsWeek4fromend, Code = String.Format("{0} {1}", GanttStrings.Week, "{finish:w}")}); // "Week 4, Week 3, (From end)" 
            displayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.tdsW4fromend, Code = String.Format("{0} {1}", GanttStrings.W, "{finish:w1}") }); // "W4, W3, W2, W1(From end)" 
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tds4fromedn, Code = "{finish:w1}" }); // "4, 3, 2, 1 (From end)"

            // Replacement tags
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsQuartersFull, Code = "{Q}" }); // "1st, 2nd, (quarters)"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsQuartersShort, Code = "{q}" }); // "1, 2, (quarters)"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsHalfYearsFull, Code = "{HY}" }); // "1st, 2nd (half year)"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsHalfYearsShort, Code = "{hy}" }); // "1, 2 (half year)"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsTOMFull, Code = "{TOM}" }); // "Beginning, Middle, End (thirds of month)"

            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsTOMShort, Code = "{tom}" }); // "B, M, E (thirds of month)"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsScmMcmT, Code = "{day:1}" }); // "S, M, T"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsSucmMocmTu, Code = "{day:2}" }); // "Su, Mo, Tu"
            displayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.tdsDay1fromstart, Code = String.Format("'{0}' {1}", GanttStrings.Day, "{start:d}") }); // "Day 1, Day 2, (From start)"
            displayFormats.Add(new DisplayFormatNameAndCode { 
                Name = GanttStrings.tdsD1fromstart, Code = String.Format("{0} {1}", GanttStrings.D, "{start:d}") }); // "D1, D2, D3, D4 (From start)"

            var names = from df in displayFormats
                        select df.Name;
            cbDisplayFormatTop.ItemsSource = names;
            cbDisplayFormatMiddle.ItemsSource = names.ToArray();
            cbDisplayFormatTop.SelectedIndex = 0;
            cbDisplayFormatMiddle.SelectedIndex = 31;
        }

        private void FillCombosForJPCulture()
        {
            displayFormats = new List<DisplayFormatNameAndCode>();

            // Simulating MS Project 2010 Display format menu.
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "2009年1月25日", Code = "yyyy年MMMMdd日" }); // "2009年1月25日"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1月25日", Code = "MMMMdd日" }); // "1月25日"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "12/1/25", Code = "yy/M/dd" }); // "1/25/12"

            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1/25, 2/1,...", Code = "M/dd" }); // "1/25, 2/1,..."
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "12/1/25 日", Code = "yy/M/dd 日" }); // 12/1/25 日
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "2012年01月25日 日曜日", Code = "yyyy年MMMMdd日 日曜日" }); // 2012年01月25日 日曜日

            displayFormats.Add(new DisplayFormatNameAndCode { Name = "01月25日 日曜日", Code = "MMMMdd日 日曜日" }); // 01月25日 日曜日

            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1/25 日", Code = "MM/dd 日" }); // 1/25 日
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1/25 (日)", Code = "M/d (日)" }); // 1/25 (日)
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1,2,..., 52, 1, 2", Code = "{w}" }); // "1,2,..., 52, 1, 2"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "第1日曜日,..., 第52日曜日", Code = "第{w}日曜日" }); // 第1日曜日,..., 第52日曜日
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1 1,...,7 1, 1 2, ..., 7 52", Code = "d {w}" }); // "1 1,...,7 1, 1 2, ..., 7 52"

            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1週, 2週, (開始日から)", Code = String.Format("{1}{0}", "週", "{start:w}") });  
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "W1, W2, W3, W4 (開始日から)", Code = String.Format("{0}{1}", "W", "{start:w1}")});
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "4週, 3週, (終了日から) ",Code = String.Format("{1}{0}", "週", "{finish:w}") 
            }); // 4週, 3週, (終了日から) 
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "W4, W3, W2, W1(終了日から)", Code = String.Format("{0}{1}", "W", "{finish:w1}")
            }); // W4, W3, W2, W1(終了日から) 
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "4, 3, 2, 1 (終了日から)", Code = "{finish:w1}" }); // 4, 3, 2, 1 (終了日から)

            // Replacement tags
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsQuartersFull, Code = "{Q}" }); // 第1, 第2, (四半期)
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsQuartersShort, Code = "{q}" }); // 1, 2, (四半期)
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsHalfYearsFull, Code = "{HY}" }); // 1st, 2nd (半年)
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsHalfYearsShort, Code = "{hy}" }); // 1, 2 (半年)
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsTOMFull, Code = "{TOM}" }); // 初旬, 中旬, 下旬 (月の3分の1)

            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsTOMShort, Code = "{tom}" }); // B, M, E (月の3分の1)
            //displayFormats.Add(new DisplayFormatNameAndCode { Name = "S, M, T", Code = "{day:1}" }); // "S, M, T"
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "日, 月, 火", Code = "{day:1}" }); // 日, 月, 火
            displayFormats.Add(new DisplayFormatNameAndCode { Name = "1日, 2日, (開始日から)", Code = String.Format("{1}{0}", "日", "{start:d}")
            }); // 1日, 2日, (開始日から)
            displayFormats.Add(new DisplayFormatNameAndCode { Name = GanttStrings.tdsD1fromstart,Code = String.Format("{0}{1}", "D", "{start:d}")
            }); // D1, D2, D3, D4 (開始日から)

            var names = from df in displayFormats
                        select df.Name;
            cbDisplayFormatTop.ItemsSource = names;
            cbDisplayFormatMiddle.ItemsSource = names.ToArray();
            cbDisplayFormatTop.SelectedIndex = 1;
            cbDisplayFormatMiddle.SelectedIndex = 23;
        }

        #endregion // Fill combos methods

        // Helper class
        private class DisplayFormatNameAndCode
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}
