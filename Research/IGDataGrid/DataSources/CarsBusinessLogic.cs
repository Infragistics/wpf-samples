using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace IGDataGrid.DataSources
{
    /// <summary>
    /// Class containing the business logic used to interact with Car data.  In this sample
    /// the class only contains one method which returns a collection of cars, but in 
    /// the real-world you would probably also have methods for finding an individual car,
    /// saving car data, or accessing a collection of cars based on some filter criteria.
    /// </summary>
    public class CarsBusinessLogic
    {
        ObservableCollection<Car> _cars = null;

        public ObservableCollection<Car> Cars
        {
            get
            {
                if (this._cars == null)
                {
                    this._cars = new ObservableCollection<Car>();
                    this._cars.Add(new Car("Dodge", "Ram", Colors.Blue, 22050.00, 153));
                    this._cars.Add(new Car("Ford", "Explorer", Colors.Green, 27175.00, 96));
                    this._cars.Add(new Car("BMW", "Z4", Colors.Silver, 35600.00, 42));
                    this._cars.Add(new Car("Toyota", "Camry", Colors.Black, 20790.99, 131));
                }

                return _cars;
            }
        }
    }

    /// <summary>
    /// The sample Car class contains information about a specific car
    /// </summary>
    public class Car : INotifyPropertyChanged
    {
        string m_make;
        string m_model;
        Color m_color;
        double m_baseprice;
        int m_milage;

        public Car(string make, string model, Color color, double baseprice, int milage)
        {
            this.Make = make;
            this.Model = model;
            this.Color = color;
            this.BasePrice = baseprice;
            this.Milage = milage;
        }

        public string Make
        {
            get { return m_make; }
            set
            {
                if (m_make != value)
                {
                    m_make = value;
                    NotifyPropertyChanged("Make");
                }
            }
        }

        public string Model
        {
            get { return m_model; }
            set
            {
                if (m_model != value)
                {
                    m_model = value;
                    NotifyPropertyChanged("Model");
                }
            }
        }

        public Color Color
        {
            get { return m_color; }
            set
            {
                if (m_color != value)
                {
                    m_color = value;
                    NotifyPropertyChanged("Color");
                }
            }
        }

        public double BasePrice
        {
            get { return m_baseprice; }
            set
            {
                if (m_baseprice != value)
                {
                    m_baseprice = value;
                    NotifyPropertyChanged("BasePrice");
                }
            }
        }

        public int Milage
        {
            get { return m_milage; }
            set
            {
                if (m_milage != value)
                {
                    m_milage = value;
                    NotifyPropertyChanged("Milage");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
