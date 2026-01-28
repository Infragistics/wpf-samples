using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Xml.Linq;
using IGUndoRedoFramework.Model;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Undo;
using igProviders = Infragistics.Samples.Shared.DataProviders;
using sharedModels = Infragistics.Samples.Shared.Models;

namespace IGUndoRedoFramework.ViewModel
{
    public class CustomersViewModel : sharedModels.ObservableModel
    {
        #region Private Members
        private UndoManager _undoManager;
        private CustomerCollection _customers;
        private CollectionViewSource _viewSource;
        private igProviders.XmlDataProvider xmlDataProvider;
        #endregion

        public CustomersViewModel()
        {
            _undoManager = new UndoManager();
            _undoManager.RegisterReference(this);
            _customers = new CustomerCollection(_undoManager);

            // Temporary prevent recording of operations
            UndoManager.Suspend();
            
            // Load sample data
            LoadXMLData();
            
            _viewSource = new CollectionViewSource();
            _viewSource.Source = _customers;

            this.Customers.MoveCurrentToFirst();
        }

        private void LoadXMLData()
        {
            // create xml data provider that will load data from xml file
            xmlDataProvider = new igProviders.XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            xmlDataProvider.GetXmlDataAsync("Customers.xml");
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                CustomerID = d.Element("CustomerID").GetString(),
                                Company = d.Element("CompanyName").GetString(),
                                ContactName = d.Element("ContactName").GetString(),
                                ContactTitle = d.Element("ContactTitle").GetString(),
                                Country = d.Element("Country").GetString()
                              }).ToList<Customer>();

            foreach (var c in dataSource)
            {
                _customers.Add(c);
            }

            // Resume recording of history of undo/redo operations
            UndoManager.Resume();
        }
        public UndoManager UndoManager
        {
            get { return _undoManager; }
        }
        public ICollectionView Customers
        {
            get { return _viewSource.View; }
        }
    }
    
    public class CustomerCollection : ObservableCollectionExtendedWithUndo<Customer>
    {
        public CustomerCollection(UndoManager undoManager) : base(undoManager)
        {
            undoManager.RegisterReference(this);
        }

        protected override void InsertItem(int index, Customer item)
        {
            item.Owner = this;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Customer item = this[index];
            item.Owner = null;

            base.RemoveItem(index);
        }
    }
}
