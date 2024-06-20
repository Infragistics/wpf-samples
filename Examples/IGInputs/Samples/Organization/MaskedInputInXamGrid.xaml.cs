using IGInputs.Resources;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace IGInputs.Samples.Organization
{
    public partial class MaskedInputInXamGrid : SampleContainer
    {
        public MaskedInputInXamGrid()
        {
            InitializeComponent();

            this.DataContext = new ProductDeliveryTask[]
            {
                new ProductDeliveryTask
                {
                    Name=InputStrings.Embedded_MineralWater,
                    Package = PackageType.Bottle,
                    PriorityLevel = 0,
                    Price = 10,
                    DateOrdered = DateTime.Now.AddDays(-4),
                    IsActive=true
                },
                new ProductDeliveryTask
                {
                    Name=InputStrings.Embedded_BlueCheese,
                    Package = PackageType.Other,
                    PriorityLevel = 2,
                    Price = 22,
                    DateOrdered = DateTime.Now.AddDays(-2),
                    IsActive=true
                },
                new ProductDeliveryTask
                {
                    Name=InputStrings.Embedded_CherryTomatoes,
                    Package = PackageType.Box,
                    PriorityLevel = 2,
                    Price = 34,
                    IsActive=true
                },
                new ProductDeliveryTask
                {
                    Name=InputStrings.Embedded_LightBeer,
                    Package = PackageType.Can,
                    PriorityLevel = 1,
                    Price = 7,
                    DateOrdered = DateTime.Now,
                    IsActive=false
                }
            };
        }
    }

    public class ProductDeliveryTask
    {
        public string Name { get; set; }
        public int PriorityLevel { get; set; }
        public PackageType Package { get; set; }
        public decimal Price { get; set; }
        public DateTime? DateOrdered { get; set; }
        public bool IsActive { get; set; }
    }

    public class Packaging
    {
        public string PackageName { get; set; }    
    }

    public enum PackageType
    {
        Box,
        Bottle,
        Can,
        Other
    }

    public class PackageNameProvider : List<Packaging>
    {
        public PackageNameProvider()
        {
            this.AddRange(PackageNameProvider.SetupEnum());
        }

        internal static ObservableCollection<Packaging> Items;

        private static IEnumerable<Packaging> SetupEnum()
        {
            if (PackageNameProvider.Items == null)
            {
                PackageNameProvider.Items = new ObservableCollection<Packaging>();
                Items.Add(new Packaging { PackageName = "Box" });
                Items.Add(new Packaging { PackageName = "Bottle" });
                Items.Add(new Packaging { PackageName = "Can" });
                Items.Add(new Packaging { PackageName = "Other" });
            }

            return PackageNameProvider.Items;
        }
    }

    public class PackageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Packaging selectedItem = null;
            var values = parameter as PackageNameProvider;
            if (values != null)
            {
                var selectedValue = (PackageType)value;
                foreach (Packaging item in values)
                {
                    if (item.PackageName == selectedValue.ToString())
                    {
                        selectedItem = item;
                        break;
                    }
                }
            }
            return selectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;

            var selectedType = value as Packaging;
            if (selectedType != null)
            {
                result = selectedType.PackageName;
            }

            return result;
        }
    }
}
