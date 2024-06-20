using System.Collections.ObjectModel;
using System.Windows;
using IGTimeline.Resources;
using Infragistics.Controls.Timelines;
using Infragistics.DragDrop;

namespace IGTimeline.Samples
{
    public partial class DraggingEventEntries : Infragistics.Samples.Framework.SampleContainer
    {
        private ObservableCollection<DataItem> _entries;

        public DraggingEventEntries()
        {
            InitializeComponent();
            _entries = new ObservableCollection<DataItem>();
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle1, 1, 1));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle2, 2, 2));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle3, 3, 1));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle4, 4, 0));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle5, 5, 1));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle6, 6, 2));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle7, 7, 1));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle8, 8, 0));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle9, 9, 1));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle10, 10, 2));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle11, 11, 1));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle12, 12, 0));
            _entries.Add(new DataItem(TimelineStrings.XWT_DateTimeTimeline_EventTitle13, 13, 1));

            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {

            DataListBox.ItemsSource = _entries;
        }

        private void OnDragSourceDragStart(object sender, DragDropStartEventArgs e)
        {
            //Assing the DataItem of the selected listbox item as data context for the Dragged Item.
            //This is needed for the DragTemplate.
            e.Data = e.DragSource.GetValue(DataContextProperty);

            this.TimelineInfoBox.Visibility = Visibility.Visible;
        }

        private void OnDragSourceDrop(object sender, DropEventArgs e)
        {
            this.TimelineInfoBox.Visibility = Visibility.Collapsed;

            //Get the dragged DataItem.
            DataItem draggedItem = e.DragSource.GetValue(DataContextProperty) as DataItem;

            //Create a NumericTimeEntry object from the dragged DataItem.
            NumericTimeEntry entry = new NumericTimeEntry()
            {
                Title = draggedItem.Title,
                Time = draggedItem.Time,
                Duration = draggedItem.Duration
            };

            //Add the new NumericTimeEntry object to xamTimeline's Series.
            ((NumericTimeSeries)Timeline.Series[0]).Entries.Add(entry);
            _entries.Remove(draggedItem);
        }
    }
    /// <summary>
    /// Represents a data item that will be dragged-and-dropped onto the xamTimeline.
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// Initializes a new <see cref="DataItem"/> instance.
        /// </summary>
        /// <param name="title">The title of the data item.</param>
        /// <param name="time">The time of the data item.</param>
        /// <param name="duration">The duration of the data item.</param>
        public DataItem(string title, double time, double duration)
        {
            this.Title = title;
            this.Time = time;
            this.Duration = duration;
        }

        public string Title { get; set; }
        public double Time { get; set; }
        public double Duration { get; set; }

    }
}
