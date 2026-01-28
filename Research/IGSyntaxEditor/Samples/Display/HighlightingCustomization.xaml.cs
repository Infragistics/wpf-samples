using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using IGSyntaxEditor.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Documents;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class HighlightingCustomization : SampleContainer
    {
        private ClassificationAppearanceMap classMap;

        public HighlightingCustomization()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // Initialize a text document with C# language content in the xamSyntaxEditor
            var td = new TextDocument();
            td.Language = CSharpLanguage.Instance;
            td.InitializeText(SyntaxEditorStrings.SourceCS);
            this.xamSyntaxEditor1.Document = td;

            // create new ClassificationAppearanceMap and assign it to the xamSyntaxEditor
            classMap = new ClassificationAppearanceMap();
            this.xamSyntaxEditor1.ClassificationAppearanceMap = classMap;

            // fill the Classification Types in a combo box
            PropertyInfo[] pia = typeof(ClassificationType).GetProperties();
            foreach (PropertyInfo pi in pia)
            {
                if (pi.PropertyType == typeof(ClassificationType))
                {
                    this.cbHighligtClassType.Items.Add(pi.Name);
                }
            }
            this.cbHighligtClassType.SelectedIndex = 0;

            // fill colors in a combo box
            var colorList = new List<Color>();
            colorList.Add(Colors.Black);
            colorList.Add(Colors.DarkOrange);
            colorList.Add(Colors.OrangeRed);
            colorList.Add(Colors.Crimson);
            colorList.Add(Colors.DeepPink);
            colorList.Add(Colors.Purple);
            colorList.Add(Colors.DodgerBlue);
            colorList.Add(Colors.DarkGreen);
            colorList.Add(Colors.YellowGreen);
            colorList.Add(Colors.White);

            this.cbFColor.ItemsSource = colorList;
            this.cbFColor.SelectedIndex = 0;

            this.cbBColor.ItemsSource = colorList;
            this.cbBColor.SelectedIndex = 9;
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            // get the selected classification type as string
            string propertyName = this.cbHighligtClassType.SelectedValue as string;
            if (propertyName != null)
            {
                // obtain the right classification type object using reflection
                var pi = typeof(ClassificationType).GetProperty(propertyName);
                var ct = pi.GetValue(typeof(ClassificationType), null) as ClassificationType;

                // add the classification map entry in the 
                classMap.AddMapEntry(
                    ct,
                    new TextDocumentAppearance()
                    {
                        Foreground = new SolidColorBrush((Color)this.cbFColor.SelectedValue),
                        Background = new SolidColorBrush((Color)this.cbBColor.SelectedValue),
                        FontBold = (bool)this.cbBold.IsChecked,
                        FontItalic = (bool)this.cbItalic.IsChecked
                        // you can also change other properties as font family and size
                    },
                    true // replace if already exists
                );
            }
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            // remove the selected classification type from the xamSyntaxEditor's classification map
            string propertyName = this.cbHighligtClassType.SelectedValue as string;
            if (propertyName != null)
            {
                var pi = typeof(ClassificationType).GetProperty(propertyName);
                var ct = pi.GetValue(typeof(ClassificationType), null) as ClassificationType;
                classMap.RemoveMapEntry(ct);
            }
        }

    }
}
