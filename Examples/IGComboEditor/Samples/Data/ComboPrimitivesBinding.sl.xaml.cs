using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace IGComboEditor.Samples.Data
{
    public partial class ComboPrimitivesBinding : SampleContainer
    {
        public ComboPrimitivesBinding()
        {
            InitializeComponent();
            Loaded += ComboPrimitivesBinding_Loaded;
        }

        private void ComboPrimitivesBinding_Loaded(object sender, RoutedEventArgs e)
        {
            ComboEditorEnum.ItemsSource = Enum.GetValues(typeof (WeekDaysNames));
        }
    }
    
    public class Data : ObservableModel
    {
        public List<int> IntList { get; set; }

        public List<bool> BoolList { get; set; }

        public List<double> DoubleList { get; set; }

        public List<DateTime> DatesList { get; set; }

        public List<char> CharsList { get; set; }

        public Data()
        {
            InitIntList();
            InitBoolList();
            InitDoubleList();
            InitDatesList();
            InitCharsList();
        }

        private void InitIntList()
        {
            IntList = new List<int>();

            for (int i = 0; i <= 5; i++)
                IntList.Add(i);
        }

        private void InitBoolList()
        {
            BoolList = new List<bool> { false, true };
        }

        private void InitDoubleList()
        {
            DoubleList = new List<double> { Double.Epsilon, Math.PI };
        }

        private void InitDatesList()
        {
            DatesList = new List<DateTime> { new DateTime(2015, 6, 30), DateTime.Today };
        }

        private void InitCharsList()
        {
            CharsList = new List<char> { '%', '&', '$', '#', '@' };
        }
    }

    public enum WeekDaysNames
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    };
}
