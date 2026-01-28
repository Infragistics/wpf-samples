using Infragistics.Controls.Editors;
using System.Windows;
using System.Windows.Controls;

namespace IGPropertyGrid.Samples.DataModel
{
    /// <summary>
    /// Interaction logic for CustomEditorPersonPhoneNumbersControl.xaml
    /// </summary>
    public partial class CustomEditorPersonPhoneNumbersControl : UserControl
    {
        public PersonPhoneNumbers _phones;

        public CustomEditorPersonPhoneNumbersControl()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "DataContext")
            {
                if (this.DataContext == null)
                {
                    this.dataGrid1.DataSource = null;
                }
                else
                {
                    if (this.DataContext is PropertyGridPropertyItem)
                    {
                        PropertyGridPropertyItem pgpi = (PropertyGridPropertyItem)this.DataContext;
                        this._phones = pgpi.Value as PersonPhoneNumbers;
                        this.dataGrid1.DataSource = this._phones;
                    }
                }
            }
        }
    }
}
