using System;
using System.ComponentModel;
using Infragistics.Controls.Maps;   // ShapefileConverter

namespace IGSurfaceChart.Samples 
{
    /// <summary>
    /// Represents a base class for data loaded from a shapefile
    /// </summary>
    public abstract class ShapefileBase : ShapefileConverter
    {
        protected ShapefileBase()
        {
            this.ImportCompleted += OnShapefileImported;
        }
        void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        {
            UpdateRange();
            UpdateData();
        }
        public abstract void UpdateData();

        public virtual void UpdateRange()
        {
            if (!IsReady()) return;

            XMin = Math.Min(this.WorldRect.Left, this.WorldRect.Right);
            YMin = Math.Min(this.WorldRect.Top, this.WorldRect.Bottom);

            XMax = Math.Max(this.WorldRect.Left, this.WorldRect.Right);
            YMax = Math.Max(this.WorldRect.Top, this.WorldRect.Bottom);
        }

        public virtual bool IsReady()
        {
            if (this.Count == 0) return false;
            if (string.IsNullOrEmpty(ZMemberPath)) return false; 

            return true;
        }
        private string _zMemberPath;
        /// <summary> Gets or sets ZMemberPath </summary>
        public string ZMemberPath
        {
            get { return _zMemberPath; }
            set
            {
                if (_zMemberPath == value) return;
                _zMemberPath = value;
                OnPropertyChanged("ZMemberPath"); UpdateData();
            }
        }
        protected double XMin { get; set; }
        protected double XMax { get; set; }
        protected double YMin { get; set; }
        protected double YMax { get; set; }
    }

}