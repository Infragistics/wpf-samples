using Prism.Mvvm;

namespace IgExcel.Infrastructure
{
    public class ViewModelBase : BindableBase, IViewModel
    {

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }


        private string iconSource;
        public string IconSource
        {
            get { return iconSource; }
            set { SetProperty<string>(ref iconSource, value); }
        }
    }
}
