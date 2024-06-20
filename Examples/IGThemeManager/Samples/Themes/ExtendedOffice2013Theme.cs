using System.Reflection;
using Infragistics.Themes;

namespace IGThemeManager.Samples.Themes
{
    public class ExtendedOffice2013Theme : Office2013Theme
    {
        protected override void ConfigureControlMappings()
        {
            base.ConfigureControlMappings();

            string assemblyFullName = Assembly.GetExecutingAssembly().FullName;
            Mappings[ControlMappingKeys.XamTileManager] = ThemeBase.BuildLocationString(assemblyFullName, @"/Samples/Themes/ExtendedTheme.xamTileManager.xaml");
        }
    }

}
