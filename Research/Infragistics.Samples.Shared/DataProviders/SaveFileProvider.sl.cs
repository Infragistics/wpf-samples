using System.Windows.Controls;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class SaveFileProvider
    {
        public SaveFileProvider()
        {
            Dialog = new SaveFileDialog();
        }
        public SaveFileDialog Dialog { get; set; }
    }
}