using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGOrgChart.Samples.Display
{
    public partial class HierarchicalNodeLayouts : SampleContainer
    {
        public HierarchicalNodeLayouts()
        {
            InitializeComponent();
        }
    }

    public class SampleClassModel : ObservableModel
    {
        public ObservableCollection<Class> Classes { get; set; }

        public SampleClassModel()
        {
            BindingFlags bindingAttr =
               BindingFlags.Static |
               BindingFlags.Instance |
               BindingFlags.Public |
               BindingFlags.DeclaredOnly;

            Type type = typeof(Control);

            this.Classes = new ObservableCollection<Class>
            {
                new Class
                {
                    ClassName = type.Name,

                    Methods = type.GetMethods(bindingAttr)
                        .Where(method => !method.IsSpecialName)
                        .Distinct()
                        .Select(method => new Method(method)),

                    Interfaces = type.GetInterfaces()
                        .Select(iface => new Interface(iface)),
                        
                    Properties = type.GetProperties(bindingAttr)
                        .Select(property => new Property(property))
                 }
            };

            OnPropertyChanged("Classes");
        }
    }
}
