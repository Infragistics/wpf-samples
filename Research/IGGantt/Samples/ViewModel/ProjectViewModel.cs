using IGGantt.Samples.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Shared.Models;

namespace IGGantt.Samples.ViewModel
{
    public class ProjectViewModel : ObservableModel
    {
        public ProjectViewModel()
        {
            this._project = ProjectDataHelper.GenerateProjectData();
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
