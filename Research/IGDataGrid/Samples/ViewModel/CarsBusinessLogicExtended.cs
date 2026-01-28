using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using IGDataGrid.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGDataGrid.Samples.ViewModel
{
    public class CarsBusinessLogicExtended : ObservableModel
    {
        private ObservableCollection<Car> _cars;

        public CarsBusinessLogicExtended()
        {
            GetData();
        }

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                if (_cars != value)
                {
                    _cars = value;
                    OnPropertyChanged("Cars");
                }
            }
        }

        private void GetData()
        {
            if (_cars == null)
            {
                _cars = new ObservableCollection<Car>
                    {
                        new Car
                            {
                                Make = "Dodge",
                                Model = "Ram",
                                Color = Colors.Purple,
                                BasePrice = 22050,
                                Milage = 1353,
                                CarType = DataGridStrings.CarType_NearlyNew,
                                Rating = 2,
                                Year = new DateTime(2011, 6, 1), 
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Ford",
                                Model = "Explorer",
                                Color = Colors.Lime,
                                BasePrice = 27175,
                                Milage = 96,
                                CarType = DataGridStrings.CarType_NearlyNew,
                                Rating = 4,
                                Year = new DateTime(2012, 1, 1),
                                IsAvailable = false
                            },
                        new Car
                            {
                                Make = "BMW",
                                Model = "Z4",
                                Color = Colors.DarkOrange,
                                BasePrice = 35600,
                                Milage = 42,
                                CarType = DataGridStrings.CarType_New,
                                Rating = 4,
                                Year = new DateTime(2012, 9, 1),
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Toyota",
                                Model = "Camry",
                                Color = Colors.MediumPurple,
                                BasePrice = 20790,
                                Milage = 131,
                                CarType = DataGridStrings.CarType_Used,
                                Rating = 3,
                                Year = new DateTime(2009, 4, 1),
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Mercedes-Benz",
                                Model = "E Class",
                                Color = Colors.OrangeRed,
                                BasePrice = 45000,
                                Milage = 100,
                                CarType = DataGridStrings.CarType_New,
                                Rating = 5,
                                Year = new DateTime(2013, 2, 1),
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Renault",
                                Model = "Clio",
                                Color = Colors.SlateGray,
                                BasePrice = 3200,
                                Milage = 47847,
                                CarType = DataGridStrings.CarType_Used,
                                Rating = 1,
                                Year = new DateTime(2010, 10, 1),
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Ford",
                                Model = "Focus",
                                Color = Colors.Yellow,
                                BasePrice = 10000,
                                Milage = 15314,
                                CarType = DataGridStrings.CarType_Used,
                                Rating = 2,
                                Year = new DateTime(2009, 1, 1),
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Volvo",
                                Model = "C30",
                                Color = Colors.OliveDrab,
                                BasePrice = 15000,
                                Milage = 20106,
                                CarType = DataGridStrings.CarType_Used,
                                Rating = 2,
                                Year = new DateTime(2011, 8, 1),
                                IsAvailable = true
                            },
                        new Car
                            {
                                Make = "Mazda",
                                Model = "CX-7",
                                Color = Colors.HotPink,
                                BasePrice = 14000,
                                Milage = 54400,
                                CarType = DataGridStrings.CarType_Used,
                                Rating = 4,
                                Year = new DateTime(2009, 2, 1),
                                IsAvailable = false
                            }
                    };
            }
        }
    }

    public class Car : ObservableModel
    {
        private int _baseprice;
        private string _carType;
        private Color _color;
        private string _make;
        private int _milage;
        private string _model;
        private double _rating;
        private DateTime _year;
        private bool _isAvailable;

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

        public Color Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged("Color");
                }
            }
        }


        public int BasePrice
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

        public int Milage
        {
            get { return _milage; }
            set
            {
                if (_milage != value)
                {
                    _milage = value;
                    OnPropertyChanged("Milage");
                }
            }
        }

        public string CarType
        {
            get { return _carType; }
            set
            {
                if (_carType != value)
                {
                    _carType = value;
                    OnPropertyChanged("CarType");
                }
            }
        }

        public double Rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged("Rating");
                }
            }
        }

        public DateTime Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    OnPropertyChanged("IsAvailable");
                }
            }
        }
    }

    public class CarTypes : ObservableCollection<string>
    {
        public CarTypes()
        {
            Add(DataGridStrings.CarType_Used);
            Add(DataGridStrings.CarType_NearlyNew);
            Add(DataGridStrings.CarType_New);
        }
    }

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (value is Color)
            {
                brush = new SolidColorBrush((Color) value);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = new Color();

            if (value is SolidColorBrush)
            {
                color = (value as SolidColorBrush).Color;
            }

            return color;
        }
    }

    public class IntToDoubleConverter : IValueConverter
    {
        public static readonly IntToDoubleConverter Instance = new IntToDoubleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return (double)(int)value;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
                return System.Convert.ToInt32((double)value);
            
            return value;
        }
    }
}