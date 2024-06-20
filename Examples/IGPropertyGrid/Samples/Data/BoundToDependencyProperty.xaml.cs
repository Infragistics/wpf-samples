using IGPropertyGrid.Samples.DataModel;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using System.Windows;
using System.Windows.Data;

namespace IGPropertyGrid.Samples.Data
{
    public partial class BoundToDependencyProperty : SampleContainer
    {
        public BoundToDependencyProperty()
        {
            InitializeComponent();
        }

        private void XamPropertyGrid1_PropertyItemValueChanged(object sender, PropertyItemEventArgs e)
        {
            MyDependencyObject mdo = (MyDependencyObject)this.xamPropertyGrid1.SelectedObject;
            BindingExpression be = BindingOperations.GetBindingExpression(mdo, MyDependencyObject.MyDependencyProperty1Property);
            if (be == null)
            {
                this.ButtonAdd.IsEnabled = true;
                this.ButtonRemove.IsEnabled = false;
            }
            else
            {
                this.ButtonAdd.IsEnabled = false;
                this.ButtonRemove.IsEnabled = true;
            }
        }

        private void AddBinding_Click(object sender, RoutedEventArgs e)
        {
            MyDependencyObject mdo = (MyDependencyObject)this.xamPropertyGrid1.SelectedObject;

            Binding b = new Binding();
            b.Source = this.myTextBox;
            b.Path = new PropertyPath("Text");
            b.Mode = BindingMode.TwoWay;
            
            BindingOperations.SetBinding(mdo, MyDependencyObject.MyDependencyProperty1Property, b);
        }

        private void RemoveBinding_Click(object sender, RoutedEventArgs e)
        {
            MyDependencyObject mdo = (MyDependencyObject)this.xamPropertyGrid1.SelectedObject;
            BindingOperations.ClearBinding(mdo, MyDependencyObject.MyDependencyProperty1Property);
        }
    }
}
