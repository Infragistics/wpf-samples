using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for XamComboEditorInDataRecordCells.xaml
    /// </summary>
    public partial class XamComboEditorInDataRecordCells : SampleContainer
    {
        public XamComboEditorInDataRecordCells()
        {
            InitializeComponent();
            ComboBoxItemsProvider statusProvider = this.xamDG.TryFindResource("StatusItemsProvider") as ComboBoxItemsProvider;
            if (statusProvider != null)
            {
                statusProvider.ItemsSource = new ComboBoxDataItem[]
                {
                    new ComboBoxDataItem(ProgressState.Ready, "Ready"),
                    new ComboBoxDataItem(ProgressState.InProgress, "In Progress"),
                    new ComboBoxDataItem(ProgressState.PendingCompletion, "Pending Completion"),
                    new ComboBoxDataItem(ProgressState.Complete, "Complete")
                };
            }

            base.DataContext = new Task[]
            {
                new Task
                {
                    Name="Deliver Payload",
                    State = ProgressState.Ready,
                    PriorityLevel = 0
                },
                new Task
                {
                    Name="Initialize Instruments",
                    State = ProgressState.Complete,
                    PriorityLevel = 2
                },
                new Task
                {
                    Name="Analyze Heat Sensor Log",
                    State = ProgressState.InProgress,
                    PriorityLevel = 2
                },
                new Task
                {
                    Name="Report Temperature Readings",
                    State = ProgressState.PendingCompletion,
                    PriorityLevel = 1
                }
            };
        }

        public class Task
        {
            public string Name { get; set; }
            public int PriorityLevel { get; set; }
            public ProgressState State { get; set; }
        }

        public enum ProgressState
        {
            Complete,
            InProgress,
            PendingCompletion,
            Ready
        }
    }
}
