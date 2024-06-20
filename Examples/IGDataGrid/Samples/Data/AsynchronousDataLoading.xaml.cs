using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Data
{
    /// <summary>
    /// Interaction logic for AsynchronousDataLoading.xaml
    /// </summary>
    public partial class AsynchronousDataLoading :SampleContainer
    {
        Funds funds;

        public AsynchronousDataLoading()
        {
            InitializeComponent();
        }

        void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            this.btnLoadData.IsEnabled = false;
            this.lblStatus.Content = DataGridStrings.AsynchronousDataLoading_ConfigArea_Status_LoadingData;

            //Create a new background work component to handle
            // the work of loading the data from the XML file
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        void btnBindData_Click(object sender, RoutedEventArgs e)
        {
            if (funds != null)
            {
                this.XamDataGrid1.DataSource = funds.funds;
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblStatus.Content = DataGridStrings.AsynchronousDataLoading_ConfigArea_Status_DataLoaded;
            this.btnBindData.IsEnabled = true;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Deserialize the XML file
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("IGDataGrid.DataSources.MutualFundsList.xml");
                StreamReader r = new StreamReader(stream);

                XmlSerializer s = new XmlSerializer(typeof(Funds));
                funds = (Funds)s.Deserialize(r);
                r.Close();
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
        }
    }
    /// <summary>
    /// A container class which holds an array of Funds
    /// </summary>
    [XmlRoot("MutualFunds")]
    public class Funds
    {
        public Funds() { }

        [XmlArray("Funds")]
        [XmlArrayItem("Fund", typeof(Fund))]
        public ArrayList funds = new ArrayList();
    }

    /// <summary>
    /// A class representing an individual mutual fund
    /// </summary>
    [Serializable()]
    public class Fund : INotifyPropertyChanged
    {
        string m_name;
        string m_symbol;
        string m_category;
        string m_netassetvalue;
        string m_ytdreturn;
        string m_tradetime;
        string m_change;
        string m_previousclose;
        string m_totalassets;
        string m_morningstaroverallrating;
        string m_percentrankincategory;
        string m_rankincategory;
        string m_totalexpenseratio;
        string m_fiveyearaveragereturn;
        string m_morningstarriskranking;
        string m_fundsummary;
        string m_beta;
        string m_family;
        string m_inceptiondate;
        string m_manager;
        string m_managersince;
        string m_managerbio;

        public Fund() { }  //default empty constructor.  needed for serialization

        public Fund(string name, string symbol, string category)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Category = category;
        }

        [XmlArray("Holdings")]
        [XmlArrayItem("Holding", typeof(Holding))]
        public HoldingsCollection m_holdings = new HoldingsCollection();

        [XmlIgnore()]
        public HoldingsCollection Holdings
        {
            get { return m_holdings; }
        }

        [XmlElement()]
        public string Family
        {
            get { return m_family; }
            set { m_family = value; }
        }

        [XmlElement()]
        public string InceptionDate
        {
            get { return m_inceptiondate; }
            set { m_inceptiondate = value; }
        }

        [XmlElement()]
        public string Manager
        {
            get { return m_manager; }
            set { m_manager = value; }
        }

        [XmlElement()]
        public string ManagerSince
        {
            get { return m_managersince; }
            set { m_managersince = value; }
        }

        [XmlElement()]
        public string ManagerBio
        {
            get { return m_managerbio; }
            set { m_managerbio = value; }
        }

        [XmlElement()]
        public string Beta
        {
            get { return m_beta; }
            set { m_beta = value; }
        }

        [XmlElement()]
        public string MorningstarRiskRanking
        {
            get { return m_morningstarriskranking; }
            set { m_morningstarriskranking = value; }
        }

        [XmlElement()]
        public string FundSummary
        {
            get { return m_fundsummary; }
            set { m_fundsummary = value; }
        }

        [XmlElement()]
        public string TotalExpenseRatio
        {
            get { return m_totalexpenseratio; }
            set { m_totalexpenseratio = value; }
        }

        [XmlElement()]
        public string FiveYearAverageReturn
        {
            get { return m_fiveyearaveragereturn; }
            set { m_fiveyearaveragereturn = value; }
        }

        [XmlElement()]
        public string RankInCategory
        {
            get { return m_rankincategory; }
            set { m_rankincategory = value; }
        }

        [XmlElement()]
        public string PercentRankInCategory
        {
            get { return m_percentrankincategory; }
            set { m_percentrankincategory = value; }
        }

        [XmlElement()]
        public string NetAssetValue
        {
            get { return m_netassetvalue; }
            set { m_netassetvalue = value; }
        }

        [XmlElement()]
        public string YTDReturn
        {
            get { return m_ytdreturn; }
            set { m_ytdreturn = value; }
        }

        [XmlElement()]
        public string TradeTime
        {
            get { return m_tradetime; }
            set { m_tradetime = value; }
        }

        [XmlElement()]
        public string Change
        {
            get { return m_change; }
            set { m_change = value; }
        }

        [XmlElement()]
        public string PreviousClose
        {
            get { return m_previousclose; }
            set { m_previousclose = value; }
        }

        [XmlElement()]
        public string TotalAssets
        {
            get { return m_totalassets; }
            set { m_totalassets = value; }
        }

        [XmlElement()]
        public string MorningstarOverallRating
        {
            get { return m_morningstaroverallrating; }
            set { m_morningstaroverallrating = value; }
        }

        /// <summary>
        /// The name of the fund
        /// </summary>
        [XmlElement()]
        public string Name
        {
            get { return m_name; }
            set
            {
                if (value != m_name)
                {
                    m_name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// The fund ticker symbol
        /// </summary>
        [XmlElement()]
        public string Symbol
        {
            get { return m_symbol; }
            set
            {
                if (value != m_symbol)
                {
                    m_symbol = value;
                    NotifyPropertyChanged("Symbol");
                }
            }
        }

        /// <summary>
        /// The fund category
        /// </summary>
        [XmlElement()]
        public string Category
        {
            get { return m_category; }
            set
            {
                if (value != m_category)
                {
                    m_category = value;
                    NotifyPropertyChanged("Category");
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

    /// <summary>
    /// A Mutual Fund holding
    /// </summary>
    [Serializable()]
    public class Holding
    {
        string m_company;
        string m_symbol;
        string m_percentassets;
        string m_ytdreturn;

        public Holding() { }  //default empty constructor.  needed for serialization

        public Holding(string company, string symbol, string percentassets, string ytdreturn)
        {
            this.Company = company;
            this.PercentAssets = percentassets;
            this.Symbol = symbol;
            this.YTDReturn = ytdreturn;
        }

        [XmlElement()]
        public string Company
        {
            get { return m_company; }
            set { m_company = value; }
        }

        [XmlElement()]
        public string Symbol
        {
            get { return m_symbol; }
            set { m_symbol = value; }
        }

        [XmlElement()]
        public string YTDReturn
        {
            get { return m_ytdreturn; }
            set { m_ytdreturn = value; }
        }

        [XmlElement()]
        public string PercentAssets
        {
            get { return m_percentassets; }
            set { m_percentassets = value; }
        }
    }

    /// <summary>
    /// A collection of mutual fund holdings
    /// </summary>
    [Serializable()]
    public class HoldingsCollection : CollectionBase
    {
        #region public HoldingsCollection()
        /// <summary>
        ///  Default empty constructor.  Needed for serialization
        /// </summary>		
        public HoldingsCollection() { }
        #endregion

        #region public System.Boolean Contains(Holding holding)
        /// <summary>
        ///   Attempts to locate the Holding within the collection.
        /// </summary>
        /// <param name="holding">
        ///   Holding to locate.
        /// </param>
        /// <returns>
        ///   True if the Holding exists in the collection.
        /// </returns>
        public System.Boolean Contains(Holding holding)
        {
            return List.Contains(holding);
        }
        #endregion

        #region public Holding this[System.Int32 index]
        /// <summary>
        ///   Gets or Sets items in this collection.
        /// </summary>
        public Holding this[System.Int32 index]
        {
            get
            {
                return (Holding)List[index];
            }
            set
            {
                List[index] = value;
            }
        }
        #endregion

        #region public void CopyTo(System.Array array, System.Int32 index)
        /// <summary>
        ///   Copies the items from this collection into the array at the specified index.
        /// </summary>
        /// <param name="array">
        ///   Array to copy the items to.
        /// </param>
        /// <param name="index">
        ///   Index of position within the array to being copying at.
        /// </param>
        public void CopyTo(System.Array array, System.Int32 index)
        {
            if ((array.Length - index) >= this.Count)
            {
                for (System.Int32 i = 0; i < this.Count; i++)
                {
                    array.SetValue(List[i], index + i);
                }
            }
            else
                throw new System.ArgumentException("The Array to Copy To must have enough elements to copy all the items from this collection.", "Array");
        }
        #endregion

        #region internal IList Items
        internal IList Items
        {
            get
            {
                return List;
            }
        }
        #endregion

        #region public System.Boolean IsSynchronized
        /// <summary>
        ///   Gets a value indicating whether access to the Collection is synchronized (thread safe).
        /// </summary>
        public System.Boolean IsSynchronized
        {
            get
            {
                return List.IsSynchronized;
            }
        }
        #endregion

        #region public System.Object SyncRoot
        /// <summary>
        ///   Gets an object that can be used to synchronize access to the Collection.
        /// </summary>
        public System.Object SyncRoot
        {
            get
            {
                return List.SyncRoot;

            }
        }
        #endregion

        #region public System.Int32 Add(Holding holding)
        /// <summary>
        ///   Adds a holding to the collection.
        /// </summary>
        /// <param name="holding">
        ///   Holding to add to the collection.
        /// </param>
        /// <returns>
        ///   Index at which the Holding was added.
        /// </returns>
        public System.Int32 Add(Holding holding)
        {
            return List.Add(holding);

        }
        #endregion

        #region public System.Int32 IndexOf(Holding holding)
        /// <summary>
        ///   Returns the index for the Holding within the collection.
        /// </summary>
        /// <param name="holding">
        ///   Holding to locate within the collection.
        /// </param>
        /// <returns>
        ///   Index of the Holding.
        /// </returns>
        public System.Int32 IndexOf(Holding holding)
        {
            return List.IndexOf(holding);
        }
        #endregion

        #region public void Insert(System.Int32 index, Holding holding)
        /// <summary>
        ///   Inserts the Holding into the collection at the specified position.
        /// </summary>
        /// <param name="index">
        ///   Position at which to insert the holding.
        /// </param>
        /// <param name="holding">
        ///   Holding to insert.
        /// </param>
        public void Insert(System.Int32 index, Holding holding)
        {
            List.Insert(index, holding);
        }
        #endregion

        #region public System.Boolean IsFixedSize
        /// <summary>
        ///   Gets a value indicating whether the collection is a fixed size.
        /// </summary>
        public System.Boolean IsFixedSize
        {
            get
            {
                return List.IsFixedSize;
            }
        }
        #endregion

        #region public System.Boolean IsReadOnly
        /// <summary>
        ///   Gets a value indicating whether the Collection is read-only.
        /// </summary>
        public System.Boolean IsReadOnly
        {
            get
            {
                return List.IsReadOnly;
            }
        }
        #endregion

        #region public void Remove(Holding holding)
        /// <summary>
        ///   Removes the first occurrence of a specific Holding from the Collection.
        /// </summary>
        /// <param name="holding">
        ///   The Holding to remove.
        /// </param>
        public void Remove(Holding holding)
        {
            List.Remove(holding);
        }
        #endregion

    }

}
