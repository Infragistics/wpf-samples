using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Controls.Editors;
using IGMultiColumnComboEditor.Resources;

namespace IGMultiColumnComboEditor.Samples.Display
{
    public partial class ColumnTextWrapping : SampleContainer
    {
        public ColumnTextWrapping()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            List<TestData> testData = new List<TestData>()
            {
                new TestData(){ ID = "1", Description = MultiColumnComboEditorStrings.Description_Content },
                new TestData(){ ID = "2", Description = MultiColumnComboEditorStrings.Description_Content },
                new TestData(){ ID = "3", Description = MultiColumnComboEditorStrings.Description_Content },
                new TestData(){ ID = "4", Description = MultiColumnComboEditorStrings.Description_Content },
                new TestData(){ ID = "5", Description = MultiColumnComboEditorStrings.Description_Content }
            };
            this.xamMultiColumnComboEditor.ItemsSource = testData;
        }

        private void chkEnableTextWrapping_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cbox = sender as CheckBox;
            if (cbox == null) return;

            XamMultiColumnComboEditor editor = this.xamMultiColumnComboEditor as XamMultiColumnComboEditor;
            if (editor == null) return;
            TextComboColumn combo = editor.Columns["Description"] as TextComboColumn;
            if (combo == null) return;


            if (cbox.IsChecked == true)
            {
                combo.TextWrapping = TextWrapping.Wrap;
                combo.Width = new ComboColumnWidth(150, false);
                this.xamMultiColumnComboEditor.UpdateLayout();
            }
            else
            {
                combo.TextWrapping = TextWrapping.NoWrap;
                combo.Width = ComboColumnWidth.Auto;
                this.xamMultiColumnComboEditor.UpdateLayout();
            }
        }
    }

    public class TestData
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }
}