using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents a view model for selecting a sample from a list of Sample objects
    /// </summary>
    public class GallerySampleViewModel : ObservableModel
    {
        public GallerySampleViewModel()
        {
            _samples = new List<FrameworkElement>();
            _selecetedIndex = 0;
        }

        #region Properties
        /// <summary>
        /// Gets names of samples in the list of Samples
        /// </summary>
        public List<string> SamplesNames
        {
            get
            {
                return this.Samples.Select(sample => sample.Tag.ToString()).ToList();
            }
        }
        /// <summary>
        /// Gets a list of Samples
        /// </summary>
        public List<FrameworkElement> Samples
        {
            get { return _samples; }
            protected set
            {
                _samples = value;
                this.OnPropertyChanged("Samples");
            }
        }
        private List<FrameworkElement> _samples;
        private int _selecetedIndex;
        /// <summary>
        /// Gets or sets the currently selected index of sample in the Samples list
        /// </summary>
        public int SelectedSampleIndex
        {
            get { return _selecetedIndex; }
            set
            {
                if (value < 0 || value > this.Samples.Count) return;
                //if (value == _selecetedIndex) return;
                _selecetedIndex = value;
                this.ShowSample(value);
                this.OnPropertyChanged("SampleSelectedIndex");
            }
        }
        /// <summary>
        /// Gets the the currently selected sample control
        /// </summary>
        public FrameworkElement SelectedSample
        {
            get
            {
                if (this.Samples.Count == 0) return null;
                return this.Samples[SelectedSampleIndex];
            }
        }
        ///// <summary>
        ///// Gets the the currently selected sample item
        ///// </summary>
        //public SampleGalleryModel SelectedSampleItem
        //{
        //    get
        //    {
        //        if (this.Samples.Count == 0) return null;
        //        return this.Samples[SelectedSampleIndex];
        //    }
        //}
        #endregion

        #region Methods

        //public void AddSample(FrameworkElement sample)
        //{
        //    _samples.Add(sample);
        //}
        public void AddSample(FrameworkElement sample, string name)
        {
            sample.Tag = name;
            _samples.Add(sample);
            //_samples.Add(new SampleGalleryModel { Control = sample, Name = name });
        }
        /// <summary>
        /// Returns an index of the previous sample in Samples list
        /// </summary>
        /// <returns></returns>
        public int GetPreviousSampleIndex()
        {
            _selecetedIndex = _selecetedIndex == 0 ? this.Samples.Count - 1 : _selecetedIndex - 1;
            return _selecetedIndex;
        }
        /// <summary>
        /// Returns an index of the next sample in Samples list
        /// </summary>
        /// <returns></returns>
        public int GetNextSampleIndex()
        {
            _selecetedIndex = (_selecetedIndex + 1) % this.Samples.Count;
            return _selecetedIndex;
        }
        public FrameworkElement GetSelectedSample()
        {
            if (_selecetedIndex < 0 || _selecetedIndex > this.Samples.Count) return null;

            return this.Samples[_selecetedIndex];
        }
        /// <summary>
        /// Shows a sample with the passed index
        /// </summary>
        /// <param name="sampleIndex"></param>
        public void ShowSample(int sampleIndex)
        {
            if (sampleIndex < 0 || sampleIndex > this.Samples.Count) return;

            this.HideAllSamples();
            _selecetedIndex = sampleIndex;
            this.Samples[sampleIndex].Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Shows the next sample in the Samples list
        /// </summary>
        public void ShowNextSample()
        {
            this.HideAllSamples();
            this.SelectedSampleIndex = GetNextSampleIndex();
        }
        /// <summary>
        /// Shows the previous sample in the Samples list
        /// </summary>
        public void ShowPreviousSample()
        {
            this.HideAllSamples();
            this.SelectedSampleIndex = GetPreviousSampleIndex();
        }
        /// <summary>
        /// Hides all samples in the Samples list
        /// </summary>
        public void HideAllSamples()
        {
            foreach (var sample in this.Samples)
            {
                sample.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

    }
    public class SampleGalleryModel : ObservableModel
    {
    
        /// <summary>
        /// Gets or sets sample identifier
        /// </summary>
        public string Identifier
        {
            get { return _identifier; }
            set
            {
                _identifier = value;
                this.OnPropertyChanged("Identifier");
            }
        }
        private string _identifier;

        /// <summary>
        /// Gets or sets sample name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.OnPropertyChanged("Name");
            }
        }
        private string _name;

        /// <summary>
        /// Gets or sets sample control
        /// </summary>
        public FrameworkElement Control
        {
            get { return _control; }
            set
            {
                _control = value;
                this.OnPropertyChanged("Control");
            }
        }
        private FrameworkElement _control;
    }
}