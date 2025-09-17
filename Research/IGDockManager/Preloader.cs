using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Infragistics.Windows.DockManager;

namespace IGDockManager
{
    public class Preloader
    {
        static Preloader()
        {
            new WindowsFormsHost();
            new DataGridView();
            new DataGridViewColumn();
            new XamDockManager();
        }
    }
}
