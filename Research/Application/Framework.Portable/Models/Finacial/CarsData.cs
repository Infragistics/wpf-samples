using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Infragistics.Framework
{
    public class CarsBusinessLogic : ObservableModel
    {
        public ObservableCollection<Car> Data { get; set; }                   

        public CarsBusinessLogic()
        {
            Data = new ObservableCollection<Car>();

            Data.Add(new Car("Dodge", "Ram", 22050.00, 153));
            Data.Add(new Car("Ford", "Explorer", 27175.00, 96));
            Data.Add(new Car("Ford", "F-150", 27255.99, 95));
            Data.Add(new Car("Ford", "GT", 120000.00, 22));
            Data.Add(new Car("BMW", "Z4", 35600.00, 42));
            Data.Add(new Car("BMW", "Z3", 29500.00, 45));
            Data.Add(new Car("Toyota", "Camry", 20790.99, 131));
            Data.Add(new Car("Honda", "Civic", 19890.99, 221));
            Data.Add(new Car("Chevrolet", "Camaro", 26770.99, 168));
            Data.Add(new Car("Nissan", "Pathfinder", 24550, 196));
            Data.Add(new Car("Jeep", "Wrangler", 26888.99, 210));
        }        
    }


    public class Car : ObservableModel
    {
        string _make;
        string _model;
        double _baseprice;
        int _mileage;

        public Car()
        {
        }

        public Car(string make, string model, double baseprice, int mileage)
        {
            this.Make = make;
            this.Model = model;
            this.BasePrice = baseprice;
            this.Mileage = mileage;
        }

        public string Make
        {
            get { return _make; }
            set
            {
                if (_make != value)
                {
                    _make = value;
                    OnPropertyChanged("Make");
                }
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        public double BasePrice
        {
            get { return _baseprice; }
            set
            {
                if (_baseprice != value)
                {
                    _baseprice = value;
                    OnPropertyChanged("BasePrice");
                }
            }
        }

        public int Mileage
        {
            get { return _mileage; }
            set
            {
                if (_mileage != value)
                {
                    _mileage = value;
                    OnPropertyChanged("Mileage");
                }
            }
        }
    }

}
