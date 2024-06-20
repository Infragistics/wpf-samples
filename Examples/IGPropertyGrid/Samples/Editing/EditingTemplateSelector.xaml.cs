using IGPropertyGrid.Resources;
using IGPropertyGrid.Samples.DataModel;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace IGPropertyGrid.Samples.Editing
{
    public partial class EditingTemplateSelector : SampleContainer
    {
        public EditingTemplateSelector()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                this.xamPropertyGrid1.SelectedObject = new EmployeeJA()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Department = Departments.Engineering,
                    Company = PropertyGridStrings.companyName,
                    Age = 31
                };
            }
            else
            {
                this.xamPropertyGrid1.SelectedObject = new EmployeeEN()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Department = Departments.Engineering,
                    Company = PropertyGridStrings.companyName,
                    Age = 31
                };
            }
        }
    }

    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PropertyGridTemplateSelectorContext)
            {
                PropertyGridTemplateSelectorContext context = item as PropertyGridTemplateSelectorContext;
                if (context.PropertyItem.PropertyName.Equals("Level"))
                {
                    XamPropertyGrid propertyGrid = container as XamPropertyGrid;
                    var resource = propertyGrid.TryFindResource("SliderIntEditor");
                    return (DataTemplate)resource;
                }
                // you still has the ability to return the edit temlate or read-only template
                // if such are defined using the "EditTemplate" and the "ReadOnlyTemplate"
                // if you return null the control's build in editor will be used
                return context.EditTemplate;
            }
            return null;
        }
    }

}
