using System.Windows.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Samples.Framework;

namespace IGCalendar.Samples.Display
{
    public partial class CalendarSettings : SampleContainer
    {
        public CalendarSettings()
        {
            InitializeComponent();
            myCalendar.ResourceProvider = new CalendarResourceProvider() { ResourceSet = CalendarResourceSet.IGTheme };
            this.txtDimensionsColumns.TextChanged += Dimensions_TextChanged;
            this.txtDimensionsRows.TextChanged += Dimensions_TextChanged;
        }

        private void Dimensions_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBlock = (TextBox)sender;
            int result = 1;
            if (textBlock.Text != null)
                int.TryParse(textBlock.Text.ToString(), out result);

            CalendarDimensions dimensions = this.myCalendar.Dimensions;
            if (textBlock.Tag.ToString() == "Rows")
                dimensions.Rows = result < 6 ? result : 6;
            else
                dimensions.Columns = result < 6 ? result : 6;

            this.myCalendar.Dimensions = dimensions;
        }
    }
}
