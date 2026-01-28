using System.Reflection;
using Infragistics.Themes;

namespace IGThemeManager.Samples.Themes
{
    public class CustomTheme : ThemeBase
    {
        protected override void ConfigureControlMappings()
        {
            string assemblyFullName = Assembly.GetExecutingAssembly().FullName;

            Mappings.Add(ControlMappingKeys.XamTileManager,
                BuildLocationString(assemblyFullName, @"\Samples\Themes\CustomTheme.xamTileManager.xaml"));
        }
    }
}
