using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes; 
using System.Windows;
using System.Windows.Controls;

namespace IGDiagram.Samples.Styling
{
    public partial class Theming : SampleContainer
    { 
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent(); 
        } 
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme to the xamDiagram controls
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);

            // clone the node and connection definitions' styles so that the theme is applied to those styles as well
            // this is only necessary if dynamic theme changes are used with data bound 
            // diagrams where the NodeDefinition.NodeStyle and ConnectionDefinitionBase.ConnectionStyle properties are set
            xamDiagram.NodeDefinitions[0].NodeStyle = CloneStyle(xamDiagram.NodeDefinitions[0].NodeStyle, this.Resources);
            xamDiagram.ConnectionDefinitions[0].ConnectionStyle = CloneStyle(xamDiagram.ConnectionDefinitions[0].ConnectionStyle, this.Resources);
        }

        /// <summary>
        /// Clones the supplied style copying all its setters and sets the BasedOn
        /// property if an implicit style with the same TargetType is found 
        /// in the <paramref name="basedOnLocation"/> resource dictionary.
        /// </summary>
        /// <param name="basedOnLocation">The resource dictionary to be searched for an implicit style with the same TargetType.</param>
        public static Style CloneStyle(Style original, ResourceDictionary basedOnLocation)
        {
            var style = new Style(original.TargetType);

            foreach (var setter in (original.Setters))
            {
                style.Setters.Add(setter);
            }

            if (basedOnLocation != null && basedOnLocation.Contains(original.TargetType))
            {
                var basedOn = basedOnLocation[original.TargetType] as Style;
                if (basedOn != null && basedOn.TargetType == original.TargetType)
                {
                    style.BasedOn = basedOn;
                }
            }
            return style;
        }
    }
}