using IGPropertyGrid.Resources;
using Infragistics.Samples.Framework;
using System.Collections;
using System.Windows.Controls;

namespace IGPropertyGrid.Samples.Data
{
    public partial class SingleObjectEditing : SampleContainer
    {
        public SingleObjectEditing()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            ArrayList typesList = new ArrayList();
            typesList.Add(new Button() { Content = PropertyGridStrings.buttonDefaultContent });
            typesList.Add(new TextBox() { Text = PropertyGridStrings.textBoxDefaultContent });

            this.ListOfTypes.ItemsSource = typesList;
            this.ListOfTypes.SelectedIndex = 0;
        }
    }
}
