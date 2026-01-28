using System;
using System.Collections.Generic;
using System.ComponentModel;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for IDataErrorInfoSupport.xaml
    /// </summary>
    public partial class IDataErrorInfoSupport : SampleContainer
    {
        public IDataErrorInfoSupport()
        {
            InitializeComponent();
            Type[] enumTypes = new Type[]
            {
                typeof( SupportDataErrorInfo ),
                typeof( DataErrorDisplayMode ),
            };

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));

            BindingList<OrderEntry> orders = new BindingList<OrderEntry>();

            Random r = new Random();

            orders.Add(new OrderEntry("Anti-Wrinkle Cream", 5, 2));
            orders.Add(new OrderEntry("Body Moisturizing Spray", 0, 1));
            orders.Add(new OrderEntry("Bronze Self-Tanning Kit", 9.99m, 1));
            orders.Add(new OrderEntry("Blusher Brush", 4, -1));
            orders.Add(new OrderEntry("Compact Refill", 5, 1));
            orders.Add(new OrderEntry("Day Cream with Vitamin E", 6, 3));
            orders.Add(new OrderEntry("Dermocleansing Lotion", 0, 0));
            orders.Add(new OrderEntry("Duo Eyeshadow Applicator", 3, -2));
            orders.Add(new OrderEntry("Eyelash Adhesive", 2, 1));
            orders.Add(new OrderEntry("Eyeshadow, waterproof", 7, 1));

            this.XamDataGrid1.DataSource = orders;
        }
    }

    #region OrderEntry Class

    public class OrderEntry : INotifyPropertyChanged, IDataErrorInfo, IEditableObject
    {
        #region Member Vars

        private string _productName;
        private decimal _price;
        private int _quantity;

        private bool _isInEdit;
        private bool _isAddItem;

        // Used by CancelEdit implementation to restore to original values.
        private Dictionary<string, object> _origValues;

        private string _dataError;
        private Dictionary<string, string> _dataErrors = new Dictionary<string, string>();

        #endregion // Member Vars

        #region Constructors

        public OrderEntry()
        {
            _isAddItem = true;
            this.BeginEdit();
        }

        public OrderEntry(string productName, decimal price, int quantity)
        {
            _productName = productName;
            _price = price;
            _quantity = quantity;

            this.ValidatePrice(false);
            this.ValidateProductName(false);
            this.ValidateQuantity(false);
        }

        #endregion // Constructors

        #region Public Properties

        #region Price

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;

                    this.ValidatePrice(false);

                    this.RaisePropertyChanged("Price");
                }
            }
        }

        private void ValidatePrice(bool raiseNotification)
        {
            string error = null;
            if (_price <= 0)
                error = DataGridStrings.ErrorMessage_PriceValue;

            this.SetFieldDataError("Price", error, raiseNotification);
        }

        #endregion // Price

        #region ProductName

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                if (_productName != value)
                {
                    _productName = value;

                    this.ValidateProductName(false);

                    this.RaisePropertyChanged("ProductName");
                }
            }
        }

        private void ValidateProductName(bool raiseNotification)
        {
            string error = null;
            if (string.IsNullOrEmpty(_productName))
                error = DataGridStrings.ErrorMessage_ProductName;

            this.SetFieldDataError("ProductName", error, raiseNotification);
        }

        #endregion // ProductName

        #region Quantity

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;

                    this.ValidateQuantity(false);

                    this.RaisePropertyChanged("Quantity");
                }
            }
        }

        private void ValidateQuantity(bool raiseNotification)
        {
            string error = null;
            if (_quantity <= 0)
                error = DataGridStrings.ErrorMessage_QuantityValue;

            this.SetFieldDataError("Quantity", error, raiseNotification);
        }

        #endregion // Quantity

        #endregion // Public Properties

        #region Public Methods

        #region GetFieldDataError

        public string GetFieldDataError(string fieldName)
        {
            string error;
            _dataErrors.TryGetValue(fieldName, out error);

            return error;
        }

        #endregion // GetFieldDataError

        #region GetRecordDataError

        public string GetRecordDataError()
        {
            return _dataError;
        }

        #endregion // GetRecordDataError

        #region HasDataError

        public bool HasDataError()
        {
            return !string.IsNullOrEmpty(_dataError)
                || null != _dataErrors && _dataErrors.Count > 0;
        }

        #endregion // HasDataError

        #region SetFieldDataError

        public void SetFieldDataError(string fieldName, string error, bool raiseNotification)
        {
            if (string.IsNullOrEmpty(error))
            {
                _dataErrors.Remove(fieldName);

                // If there are no field errors anymore, reset the record error as well.
                if (0 == _dataErrors.Count)
                    this.SetRecordDataError(null, true);
            }
            else
            {
                _dataErrors[fieldName] = error;

                this.SetRecordDataError(DataGridStrings.ErrorMessage_InvalidValues, true);
            }

            if (raiseNotification)
                this.RaisePropertyChanged(fieldName);
        }

        #endregion // SetFieldDataError

        #region SetRecordDataError

        public void SetRecordDataError(string error, bool raiseNotification)
        {
            if (_dataError != error)
            {
                _dataError = error;

                if (raiseNotification)
                    this.RaisePropertyChanged("");
            }
        }

        #endregion // SetRecordDataError

        #endregion // Public Methods

        #region Private Methods

        #region RaisePropertyChanged

        private void RaisePropertyChanged(string propName)
        {
            if (null != _propertyChangedDelegate)
                _propertyChangedDelegate(this, new PropertyChangedEventArgs(propName));
        }

        #endregion // RaisePropertyChanged

        #endregion // Private Methods

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get
            {
                return _dataError;
            }
        }

        string IDataErrorInfo.this[string fieldName]
        {
            get
            {
                return this.GetFieldDataError(fieldName);
            }
        }

        #endregion

        #region IEditableObject Members

        #region BeginEdit

        public void BeginEdit()
        {
            // If order entry is already being edited, then return.
            if (_isInEdit)
                return;

            // Store the original values so CancelEdit implementation can revert to them.
            _origValues = new Dictionary<string, object>();
            _origValues.Add("ProductName", _productName);
            _origValues.Add("Price", _price);
            _origValues.Add("Quantity", _quantity);

            // Set the flag.
            _isInEdit = true;
        }

        #endregion // BeginEdit

        #region CancelEdit

        public void CancelEdit()
        {
            if (_isInEdit)
            {
                // Restore original values.
                _productName = (string)_origValues["ProductName"];
                _price = (decimal)_origValues["Price"];
                _quantity = (int)_origValues["Quantity"];
                _origValues = null;

                // Reset the flag.
                _isInEdit = false;
            }
        }

        #endregion // CancelEdit

        #region EndEdit

        public void EndEdit()
        {
            if (_isInEdit)
            {
                // We are delaying validating field values for errros in an add-record until the 
                // user attempts to commit the add-record. In the data presenter, when the user 
                // begins to add a new record, initial field values would be all empty and thus 
                // invalid. We don't want to display data errors in the add-record at this point. 
                // We want to delay displaying data errors until the user attempts to commit the
                // record with invalid values.
                if (_isAddItem)
                {
                    this.ValidatePrice(true);
                    this.ValidateProductName(true);
                    this.ValidateQuantity(true);

                    // If there are any invalid field values, throw an exception in EndEdit which
                    // will cause the data presenter to prompt the user with the message of the
                    // exception, and not commit the record.
                    if (this.HasDataError())
                    {
                        throw new InvalidOperationException(DataGridStrings.ErrorMessage_OrderEntry + this.GetRecordDataError());
                    }

                    _isAddItem = false;
                }

                _origValues = null;
                _isInEdit = false;
            }
        }

        #endregion // EndEdit

        #endregion

        #region INotifyPropertyChanged Members

        private PropertyChangedEventHandler _propertyChangedDelegate;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChangedDelegate = (PropertyChangedEventHandler)Delegate.Combine(_propertyChangedDelegate, value);
            }
            remove
            {
                _propertyChangedDelegate = (PropertyChangedEventHandler)Delegate.Remove(_propertyChangedDelegate, value);
            }
        }
        #endregion
    }
    #endregion // OrderEntry Class
}
