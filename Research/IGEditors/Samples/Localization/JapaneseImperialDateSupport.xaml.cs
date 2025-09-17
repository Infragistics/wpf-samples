using IGEditors.Resources;
using Infragistics.Samples.Framework;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Markup;
using SG = System.Globalization;

namespace IGEditors.Samples.Localization
{
    public partial class JapaneseImperialDateSupport : SampleContainer
    {
        public JapaneseImperialDateSupport()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            CustomComboBoxItem ccbi = cb.SelectedItem as CustomComboBoxItem;
            if (ccbi != null)
            {
                this.xdte.FormatProvider = ccbi.CultureInfo;
                this.xdte.Language = XmlLanguage.GetLanguage(ccbi.CultureInfo.IetfLanguageTag);
            }
        }
    }

    public class JapaneseImperialDateSupportVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CustomComboBoxItem> customComboBoxItem = new ObservableCollection<CustomComboBoxItem>();

        public JapaneseImperialDateSupportVM()
        {
            SG.CultureInfo ci;
            CustomComboBoxItem ccbo;

            ci = new SG.CultureInfo("ja-JP");
            ci.DateTimeFormat.Calendar = new SG.GregorianCalendar();
            ccbo = new CustomComboBoxItem()
            {
                DisplayName = EditorsStringRes.CBI_Gregorian_Calendar,
                CultureInfo = ci,
            };
            customComboBoxItem.Add(ccbo);

            ci = new SG.CultureInfo("ja-JP");
            ci.DateTimeFormat.Calendar = new SG.JapaneseCalendar();
            ccbo = new CustomComboBoxItem()
            {
                DisplayName = EditorsStringRes.CBI_Japanese_Calendar,
                CultureInfo = ci,
            };
            customComboBoxItem.Add(ccbo);
        }

        public ObservableCollection<CustomComboBoxItem> Cultures
        {
            get
            {
                return customComboBoxItem;
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class CustomComboBoxItem
    {
        public string DisplayName { get; set; }
        public SG.CultureInfo CultureInfo { get; set; }
    }
}
