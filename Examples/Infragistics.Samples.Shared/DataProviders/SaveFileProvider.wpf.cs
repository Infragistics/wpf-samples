using Microsoft.Win32;

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