using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Media;

namespace IGDataGrid.Datasources
{
    public class NamedColors : ObservableCollection<string>
    {
        public NamedColors()
        {
            Type colorsType = typeof(Colors);
            foreach (PropertyInfo property in colorsType.GetProperties())
            {
                Add((string)property.Name);
            }
        }
    }
}
