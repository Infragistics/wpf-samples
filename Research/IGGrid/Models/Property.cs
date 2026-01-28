using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Models
{
    public class Property : ObservableModel
    {
        public Property()
        {
        }

        private string mlsNumber;
        public string MLS
        {
            get
            {
                return this.mlsNumber;
            }
            set
            {
                if (this.mlsNumber != value)
                {
                    this.mlsNumber = value;
                    this.OnPropertyChanged("MLS");
                }
            }
        }

        private DateTime listingDate;
        public DateTime ListingDate
        {
            get
            {
                return this.listingDate;
            }
            set
            {
                if (this.listingDate != value)
                {
                    this.listingDate = value;
                    this.OnPropertyChanged("ListingDate");
                }
            }
        }

        private bool isSold;
        public bool IsSold
        {
            get
            {
                return this.isSold;
            }
            set
            {
                if (this.isSold != value)
                {
                    this.isSold = value;
                    this.OnPropertyChanged("IsSold");
                }
            }
        }

        private int numberOfBedRooms;
        public int NumberOfBedRooms
        {
            get
            {
                return this.numberOfBedRooms;
            }
            set
            {
                if (this.numberOfBedRooms != value)
                {
                    this.numberOfBedRooms = value;
                    this.OnPropertyChanged("NumberOfBedRooms");
                }
            }
        }

        private int numberOfBathRooms;
        public int NumberOfBathRooms
        {
            get
            {
                return this.numberOfBathRooms;
            }
            set
            {
                if (this.numberOfBathRooms != value)
                {
                    this.numberOfBathRooms = value;
                    this.OnPropertyChanged("NumberOfBathRooms");
                }
            }
        }

        private decimal sqFeet;
        public decimal SqFeet
        {
            get
            {
                return this.sqFeet;
            }
            set
            {
                if (this.sqFeet != value)
                {
                    this.sqFeet = value;
                    this.OnPropertyChanged("SqFeet");
                }
            }
        }

        private double price;
        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (this.price != value)
                {
                    this.price = value;
                    this.OnPropertyChanged("Price");
                }
            }
        }

        private string city;
        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                if (this.city != value)
                {
                    this.city = value;
                    this.OnPropertyChanged("City");
                }
            }
        }

        private string state;
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                if (this.state != value)
                {
                    this.state = value;
                    this.OnPropertyChanged("State");
                }
            }
        }

        private string postalCode;
        public string PostalCode
        {
            get
            {
                return this.postalCode;
            }
            set
            {
                if (this.postalCode != value)
                {
                    this.postalCode = value;
                    this.OnPropertyChanged("PostalCode");
                }
            }
        }
    }
}
