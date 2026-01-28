using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Display
{
    public partial class KeyTips : SampleContainer
    {
        public KeyTips()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DefaultKeyCombo.ItemsSource = loadItems();
        }

        private void DefaultKeyCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.MyRibbon.KeyTipControlKey = (Key)((ComboBox)sender).SelectedItem;
        }

        private List<Key> loadItems()
        {
            List<Key> keyList = new List<Key>();
            keyList.Add(Key.A);
            keyList.Add(Key.B);
            keyList.Add(Key.C);
            keyList.Add(Key.D);
            return keyList;
        }
    }
}
