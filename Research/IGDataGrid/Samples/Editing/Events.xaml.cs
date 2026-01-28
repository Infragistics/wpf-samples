using System;
using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for Events.xaml
    /// </summary>
    public partial class Events : SampleContainer
    {
        public Events()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Events_Loaded);
        }

        void Events_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }

        void XamDataGrid1_SelectedItemsChanging(object sender, Infragistics.Windows.DataPresenter.Events.SelectedItemsChangingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("SelectedItemsChanging - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_SelectedItemsChanged(object sender, Infragistics.Windows.DataPresenter.Events.SelectedItemsChangedEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("SelectedItemsChanged - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_RecordDeactivating(object sender, Infragistics.Windows.DataPresenter.Events.RecordDeactivatingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("RecordDeactivating - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_RecordActivated(object sender, Infragistics.Windows.DataPresenter.Events.RecordActivatedEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("RecordActivated - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_RecordActivating(object sender, Infragistics.Windows.DataPresenter.Events.RecordActivatingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("RecordActivating - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_InitializeRecord(object sender, Infragistics.Windows.DataPresenter.Events.InitializeRecordEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("InitializeRecord - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_EditModeEnded(object sender, Infragistics.Windows.DataPresenter.Events.EditModeEndedEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("EditModeEnded - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_EditModeEnding(object sender, Infragistics.Windows.DataPresenter.Events.EditModeEndingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("EditModeEnding - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_EditModeStarted(object sender, Infragistics.Windows.DataPresenter.Events.EditModeStartedEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("EditModeStarted - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_EditModeStarting(object sender, Infragistics.Windows.DataPresenter.Events.EditModeStartingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("EditModeStarting - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_CellDeactivating(object sender, Infragistics.Windows.DataPresenter.Events.CellDeactivatingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("CellDeactivating - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_CellActivating(object sender, Infragistics.Windows.DataPresenter.Events.CellActivatingEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("CellActivating - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void XamDataGrid1_CellActivated(object sender, Infragistics.Windows.DataPresenter.Events.CellActivatedEventArgs e)
        {
            string origSource = string.Empty;
            string source = string.Empty;

            if (e.OriginalSource is FrameworkElement)
                origSource = ((FrameworkElement)e.OriginalSource).Name;

            if (e.Source is FrameworkElement)
                source = ((FrameworkElement)e.Source).Name;

            this.AddEventToEventList("CellActivated - OrigSource: '" + origSource + "', Source: '" + source + "'");
        }

        void AddEventToEventList(string eventDescription)
        {
            string name = string.Empty;

            IInputElement e = System.Windows.Input.Keyboard.FocusedElement;

            if (e == null)
                return;

            FrameworkElement fe = e as FrameworkElement;
            if (fe != null)
            {
                if (fe.Name.Length > 0)
                    name = fe.Name;
                else
                    name = fe.ToString();
            }
            else
            {
                name = e.ToString();
            }

            this.lbEvents.Items.Insert(0, "[" + DateTime.Now.ToString("HH:mm:ss:fffffff") + "]  " + eventDescription + ", FocusedElement: " + name);
        }
    }
}
