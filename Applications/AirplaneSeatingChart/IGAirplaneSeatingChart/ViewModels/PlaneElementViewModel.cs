using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;

namespace IGShowcase.AirplaneSeatingChart.ViewModels
{
    public class PlaneElementViewModel:INotifyPropertyChanged
    {
            public PlaneElementViewModel()
            {
                this.Shape = new List<List<Point>>();
            }
            public PlaneElementViewModel(string id, List<List<Point>> shape)
            {
                this.Id = id;
                this.Shape = shape;
            }
            public List<List<Point>> Shape { get; set; }
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            /// <value>The id.</value>
            public string Id { get; set; }
            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion
    }
}
