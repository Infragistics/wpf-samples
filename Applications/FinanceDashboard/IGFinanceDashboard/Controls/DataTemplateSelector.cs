// code from WPF

namespace System.Windows.Controls
{
    // Summary: 
    // Provides a way to choose a System.Windows.DataTemplate based on the data 
    // object and the data-bound element. 
    public class DataTemplateSelector
    {
        // Summary: 
        // Initializes a new instance of the System.Windows.Controls.DataTemplateSelector 
        // class. 
        public DataTemplateSelector()
        {
        }

        // Summary: 
        // When overridden in a derived class, returns a System.Windows.DataTemplate 
        // based on custom logic. 
        // 
        // Parameters: 
        // item: 
        // The data object for which to select the template. 
        // 
        // container: 
        // The data-bound object. 
        // 
        // Returns: 
        // Returns a System.Windows.DataTemplate or null. The default value is null. 
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }
    }
}