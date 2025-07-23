using System;
using System.Collections.Generic;
using System.Windows;
using IGBarcode.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGBarcode.Samples.Data
{
    public partial class DataBinding : Infragistics.Samples.Framework.SampleContainer
    {
        public DataBinding()
        {
            InitializeComponent();
        }
        private void BarcodeDataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateOutput(BarcodeStrings.XWB_PropChanged + e.PropertyName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        #region AuxiliaryMethods

        private void UpdateOutput(string text)
        {
            if (TextBlockOutput != null)
            {
                TextBlockOutput.Text += text + Environment.NewLine;
                ScrollViewerOutput.UpdateLayout();
                ScrollViewerOutput.ScrollToVerticalOffset(Double.MaxValue);
            }
        }

        private void ClearOutput()
        {
            TextBlockOutput.Text = string.Empty;
        }

        #endregion
    }

    #region BarcodeDataModel

    public class BarcodeDataModel : ObservableModel
    {
        public BarcodeDataModel()
        {
            this.Products = new List<string>()
                       {
                           "BA8327",
                           "AR5381",
                           "BE2349",
                           "BE2908",
                           "BL2036"
                       };

            this.SelectedProduct = this.Products[0];
        }

        public List<string> Products { get; private set; }

        private string _selectedProduct;
        public string SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
    }

    #endregion
}