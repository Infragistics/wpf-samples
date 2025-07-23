using IGGantt.Samples.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Shared.Models;

namespace IGGantt.Samples.ViewModel
{
    public class SimpleProjectViewModel : ObservableModel
    {
        public SimpleProjectViewModel()
        {
            this._project = ProjectDataHelper.GenerateProjectDataWithoutConstraints();
        }

        public SimpleProjectViewModel(bool LoadCriticalData)
        {
            if (LoadCriticalData)
            {
                this._project = ProjectDataHelper.GenerateCriticalData();
            }
            else
            {
                this._project = ProjectDataHelper.GenerateProjectDataWithoutConstraints();
            }
        }

        private Project _project;
        public Project Project
        {
            get
            {
                return this._project;
            }
            set
            {
                if (this._project != value)
                {
                    this._project = value;
                    this.OnPropertyChanged("Project");
                }
            }
        }
    }
}
