using Infragistics.Samples.Shared.Tools;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;
using System.Windows;
using System.Windows.Controls;

namespace IGDataGrid.Samples.Display
{
    public partial class ApplyTemplatesConditionally
    {
        public ApplyTemplatesConditionally()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CustomDisplayTemplateSelector selector1 = (CustomDisplayTemplateSelector)this.Resources["customDisplaySelector"];
            selector1.SetBudget((int)((Slider)sender).Value);

            CustomEditingTemplateSelector selector2 = (CustomEditingTemplateSelector)this.Resources["customEditingSelector"];
            selector2.SetBudget((int)((Slider)sender).Value);
            
            if (dataGrid != null)
            {
                foreach (Record rec in this.dataGrid.Records)
                {
                    if (rec is DataRecord)
                    {
                        ((DataRecord)rec).RefreshCellValues();
                    }
                }
            }
        }
    }

    public class CustomDisplayTemplateSelector : DataTemplateSelector
    {
        private int Budget;

        public void SetBudget(int budget)
        {
            Budget = budget;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            var editor = TemplateEditor.GetEditor(container);

            // clearing the Tag property which in this case is used
            // to track the template returned in edit mode.
            editor.Tag = null;

            if (item != null)
            {
                if ((int)item > Budget)
                {
                    dataTemplate = editor.FindResource("displayTemplateRed") as DataTemplate;
                }
                else
                {
                    dataTemplate = editor.FindResource("displayTemplateGreen") as DataTemplate;
                }
            }

            if (dataTemplate != null)
            {
                return dataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }

    public class CustomEditingTemplateSelector : DataTemplateSelector
    {
        private int Budget;

        public void SetBudget(int budget)
        {
            Budget = budget;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            var editor = TemplateEditor.GetEditor(container);

            // If the Tag property is set this means that an edit template was already
            // returned and we need to return the same template to prevent the editor
            // to exit edit mode.
            if (editor.Tag is DataTemplate)
            {
                return editor.Tag as DataTemplate;
            }

            if (item != null)
            {
                double d = double.Parse(item.ToString());
                if (d > Budget)
                {
                    dataTemplate = editor.FindResource("editingTemplateRed") as DataTemplate;
                }
                else
                {
                    dataTemplate = editor.FindResource("editingTemplateGreen") as DataTemplate;
                }
            }

            if (dataTemplate != null)
            {
                editor.Tag = dataTemplate;
                return dataTemplate;
            }
                
            return base.SelectTemplate(item, container);
        }
    }
}
