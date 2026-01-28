using System.Threading;
using IGComboEditor.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IGComboEditor.Samples.Editing
{
    public partial class UsingSelectionChangedEvent : SampleContainer
    {
        public ObservableCollection<EventData> _eventFiringHistory = new ObservableCollection<EventData>();

        public UsingSelectionChangedEvent()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.eventsListBox.ItemsSource = this._eventFiringHistory;
            this.ComboEditor.SelectedIndex = 0;
            this.JPCustomValueEnteredActionCheck();
        }

        private void XWComboEditor_SelectionChanged(object sender, Infragistics.Controls.Editors.SelectionChangedEventArgs e)
        {
            this._eventFiringHistory.Add(new EventData(this._eventFiringHistory.Count, e));
            if (ComboEditor != null && ComboEditor.SelectedItem != null)
            {

                // SelectedItem returns business data object 
                var selectedCustomer = (Customer)ComboEditor.SelectedItem;

                // Fills text fields
                txtBlockName.Text = selectedCustomer.ContactName;
                txtBlockTitle.Text = selectedCustomer.ContactTitle;
                txtBlockPhone.Text = selectedCustomer.Phone;
                txtBlockCompany.Text = selectedCustomer.Company;
                txtBlockCompany.Text = selectedCustomer.City;

                // Place an image
                image.Visibility = Visibility.Visible;
                string imageResourcePath = ((Customer)ComboEditor.SelectedItem).ImageResourcePath;

                var bmi = new BitmapImage(new Uri(imageResourcePath));

                image.Source = bmi;
                image.Stretch = Stretch.Uniform;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this._eventFiringHistory.Clear();
        }

        private void JPCustomValueEnteredActionCheck()
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                ComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }

    }

    public class EventData
    {
        public EventData(int id, SelectionChangedEventArgs eventArgs)
        {
            this.Id = id;
            this.EventArgs = eventArgs;
        }

        private string ExtractContactNames(IEnumerable list)
        {
            if (!list.Cast<object>().Any())
            {
                return ComboEditorStrings.CE_SelectionChanged_None;
            }
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(((Customer) item).ContactName + ", ");
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public int Id { get; set; }
        public SelectionChangedEventArgs EventArgs { get; set; }

        public override string ToString()
        {
            return string.Format(ComboEditorStrings.CE_SelectionChanged_EventFired,
                              this.Id, this.ExtractContactNames(this.EventArgs.AddedItems),
                              this.ExtractContactNames(this.EventArgs.RemovedItems)); 
        }
    }
}