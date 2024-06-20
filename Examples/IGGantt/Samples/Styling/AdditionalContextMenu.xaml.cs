using System.Windows;
using IGGantt.Samples.DataSource;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Styling
{
    public partial class AdditionalContextMenu : SampleContainer
    {
        public AdditionalContextMenu()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            this.Unloaded += OnSampleUnloaded;
        }

        void OnSampleUnloaded(object sender, RoutedEventArgs rea)
        {
            xamGantt.ContextMenuProvider = null;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs rea)
        {
            DataContext = ProjectDataHelper.GenerateProjectData();
        }
    }
}
