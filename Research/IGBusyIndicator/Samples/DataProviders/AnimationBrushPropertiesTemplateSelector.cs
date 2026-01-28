using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IGBusyIndicator.Samples.DataProviders
{
    internal class AnimationBrushPropertiesTemplateSelector : DataTemplateSelector
    {
        private const string TemplateName = "AnimationBrushPropertiesTemplate_{0}";

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var element = container as FrameworkElement;
            var brushProperties = item as AnimationBrushProperties;
            if (brushProperties != null && element != null)
            {
                var templateName = string.Format(TemplateName, brushProperties.Name);
                return element.FindResource(templateName) as DataTemplate;
            }

            return null;
        }
    }
}
