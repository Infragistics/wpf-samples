using System.ComponentModel;

namespace Infragistics.Samples.Shared.Models
{
    public class NetworkConnection : ObservableModel
    {
        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }

        private NodeModel _target;
        public NodeModel Target
        {
            get { return _target; }
            set
            {
                if (value != _target)
                {
                    _target = value;
                    OnPropertyChanged("Target");
                }
            }
        }
         
    }
}
