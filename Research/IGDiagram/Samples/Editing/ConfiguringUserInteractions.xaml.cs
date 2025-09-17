using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using System.ComponentModel;

namespace IGDiagram.Samples.Editing
{
    public partial class ConfiguringUserInteractions : SampleContainer, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DiagramItem _DiagramItem = null;

        public ConfiguringUserInteractions()
        {
            InitializeComponent();
        }

        private void Diagram_SelectionChanged(object sender, DiagramSelectionChangedEventArgs e)
        {
            if (e.AddedItems.Length > 0)
            {
                if (e.AddedItems[0] != null)
                {
                    SelectedDiagramItem = e.AddedItems[0];
                }
            }
            else
            {
                SelectedDiagramItem = null;
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        public DiagramItem SelectedDiagramItem
        {
            get
            {
                return _DiagramItem;
            }

            set
            {
                _DiagramItem = value;
                OnPropertyChanged("SelectedDiagramItem");
            }
        }
    }
}
