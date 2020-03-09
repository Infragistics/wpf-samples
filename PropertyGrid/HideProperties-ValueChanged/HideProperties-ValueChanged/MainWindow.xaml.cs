using HideProperties_ValueChanged.Business;
using Infragistics.Controls.Editors;
using System.Linq;
using System.Windows;

namespace HideProperties_ValueChanged
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var p = new Person();
            p.FirstName = "Brian";
            p.LastName = "Lagunas";
            p.Age = 21;
            p.Spouse = "Diana Lagunas";

            _pg.SelectedObject = p;
            _pg.PropertyItemValueChanged += PropertyItemValueChanged;
        }

        private void PropertyItemValueChanged(object sender, PropertyItemEventArgs e)
        {
            if (e.PropertyItem.PropertyName == "Age")
            {
                PropertyGridItem item = GetPropertyGridItem("Spouse");

                if ((int)e.PropertyItem.Value < 18)
                    item.HideFromDisplay = true;
                else
                    item.HideFromDisplay = false;
            }
        }

        PropertyGridItem GetPropertyGridItem(string name)
        {
            PropertyGridItem item = null;
            if (_pg.IsCategorized)
                item = _pg.Items.SelectMany(x => x.Children.Where(c => c.PropertyName == name)).FirstOrDefault() as PropertyGridItem;
            else
                item = _pg.Items.Where(x => x.DisplayName == name).FirstOrDefault() as PropertyGridPropertyItem;

            return item;
        }
    }
}
