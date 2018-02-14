using Infragistics.Windows.Ribbon;
using IgOutlook.Infrastructure;
using Infragistics.Windows.OutlookBar;

namespace IgOutlook
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : XamRibbonWindow
    {
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; 
        }

        private void XamOutlookBar_SelectedGroupChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var group = ((XamOutlookBar)sender).SelectedGroup as IOutlookBarGroup;
            if (group != null)
            {
                Commands.NavigateCommand.Execute(group.DefaultNavigationPath);
            }
        }
    }
}
