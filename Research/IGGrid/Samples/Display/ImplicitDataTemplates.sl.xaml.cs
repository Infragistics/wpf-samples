using System.Windows;
using IGGrid.ViewModels;
using Infragistics.Samples.Framework;

namespace IGGrid.Samples.Display
{
    public partial class ImplicitDataTemplates : SampleContainer
    {
        public ImplicitDataTemplates()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            ContactDetailsViewModel _vm = new ContactDetailsViewModel();
            this.DataContext = _vm;
        }
    }
}