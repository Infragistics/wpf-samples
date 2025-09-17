using System;
using Infragistics.Samples.Shared.Models;
using System.ComponentModel.DataAnnotations;
using IGGrid.Resources;

namespace IGGrid.Models
{
    public class Products : ObservableModel
    {
        private string _name;
        // 'XG_Product_ShortName' = 'Short Name' in GridStrings resource
        [DisplayAttribute(ShortName = "XG_Product_ShortName", ResourceType = typeof(GridStrings))]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged("Name");
                }

            }
        }

        private string _category;
        // 'XG_GroupedName' = 'Group Name' in GridStrings resource
        [DisplayAttribute(GroupName = "XG_GroupedCategory", ResourceType = typeof(GridStrings))]
        public string Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    this.OnPropertyChanged("Category");
                }

            }
        }

        private double _unitPrice;
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    this.OnPropertyChanged("UnitPrice");
                }
            }
        }
    }
}
