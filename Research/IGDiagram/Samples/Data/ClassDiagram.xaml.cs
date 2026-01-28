using Infragistics.Samples.Framework;

namespace IGDiagram.Samples.Data
{
    public partial class ClassDiagram : SampleContainer
    {
        public ClassDiagram()
        {
            InitializeComponent();
            this.Loaded += (s, e) => Diagram.ScaleToFit();
            
        }
    }
}
