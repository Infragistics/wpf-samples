using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Windows.Themes;

namespace IGDockManager.Samples.Style
{
    /// <summary>
    /// Interaction logic for CustomThemes_Dynamic.xaml
    /// </summary>
    public partial class LiveResourceWashing : SampleContainer
    {
        #region Fields
        private WashGroupCollection washGroups = new WashGroupCollection();
        private WashGroup wgThemeColor = new WashGroup();
        private WashGroup wgAlternateColor = new WashGroup();

        private ResourceWasher _currentWasher = new ResourceWasher();
        private ResourceDictionary CurrentTheme;
        private const bool USE_THEME = false;

        ResourceDictionary[] initialDictionaries;

        #endregion Fields

        public LiveResourceWashing()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(CustomThemes_Dynamic_Loaded);
            Unloaded += new RoutedEventHandler(CustomThemes_Dynamic_Unloaded);

            // Hookup Wash Groups
            wgThemeColor.Name = "ThemeColor";
            wgThemeColor.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2F99C6");
            wgAlternateColor.Name = "AlternateColor";
            wgAlternateColor.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF1C5E7A");


            // Add WashGroups to WashGroup Collection //
            washGroups.Add(wgThemeColor);
            washGroups.Add(wgAlternateColor);


            // Register the WashBaseLight and WashBaseDark themes
            ThemeManager.Register("WashBaseLight", "DockManager", DockManagerWashBaseLight.Instance);
            ThemeManager.Register("WashBaseDark", "DockManager", DockManagerWashBaseDark.Instance);

            // Start in Simple Mode First //
            SimpleWashColorMode = true;

            // Set SelectedIndex of Theme ComboBox
            cbxWashBase.SelectedIndex = 0;
        }

        private void CustomThemes_Dynamic_Unloaded(object sender, RoutedEventArgs e)
        {
            // Clear all resources
            Application.Current.Resources.MergedDictionaries.Clear();

            // Return initial ones
            for (int i = 0; i < initialDictionaries.Length; i++)
            {
                Application.Current.Resources.MergedDictionaries.Add(initialDictionaries[i]);
            }
        }

        private void CustomThemes_Dynamic_Loaded(object sender, RoutedEventArgs e)
        {
            if (initialDictionaries == null)
                initialDictionaries = new ResourceDictionary[Application.Current.Resources.MergedDictionaries.Count];
            Application.Current.Resources.MergedDictionaries.CopyTo(initialDictionaries, 0);
        }


        #region Methods
        private void SetWashGroupColor(object sender, RoutedEventArgs e)
        {
            Button setBtn = sender as Button;
            TextBox targetText = new TextBox();
            WashGroup wgActive = new WashGroup();
            switch (setBtn.Name)
            {
                case "btnSetTabArea":
                    wgActive = wgThemeColor;
                    targetText = tbTabArea;
                    break;

                case "btnSetText":
                    wgActive = wgAlternateColor;
                    targetText = tbText;
                    break;

                case "btnSetMasterWashColor":
                    wgActive = wgThemeColor;
                    targetText = tbSingleColor;
                    break;
            }

            ColorPickerDialog colorPickerDialog = new ColorPickerDialog();
            // Validate the value coming in to set the Starting color correctly //
            Regex regexExp = new Regex("#+([0-9A-Fa-f]{2}){4}");
            if (regexExp.IsMatch(targetText.Text))
            {
                MatchCollection matchObj = regexExp.Matches(targetText.Text);
                if (matchObj.Count > 0)
                {
                    colorPickerDialog.StartingColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(targetText.Text);
                }
            }
            else
            {
                colorPickerDialog.StartingColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000");
            }


            bool? dialogResult = colorPickerDialog.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                if (wgActive != null)
                    wgActive.WashColor = colorPickerDialog.DialogSelectedColor;

                if (targetText != null)
                {
                    // targetText.Background = new SolidColorBrush(colorPickerDialog.SelectedColor);
                    targetText.Text = colorPickerDialog.DialogSelectedColor.ToString(); // targetText.Background.ToString();
                }
            }

            ReWash();
        }

        private void ReWash()
        {
            //remove the old washer from application resources
            if (_currentWasher != null)
                Application.Current.Resources.MergedDictionaries.Remove(_currentWasher);

            if (this.CurrentTheme != null)
                Application.Current.Resources.MergedDictionaries.Remove(CurrentTheme);

            _currentWasher = new ResourceWasher();
            CurrentTheme = new ResourceDictionary();


            if (SimpleWashColorMode)
            {
                foreach (WashGroup collection in washGroups)
                {
                    Debug.WriteLine(collection.Name);
                    switch (collection.Name)
                    {
                        case "ThemeColor":
                            collection.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbSingleColor.Text.ToString());
                            break;
                        case "AlternateColor":
                            collection.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbSingleColor.Text.ToString());
                            break;
                    }
                }
            }
            else
            {
                foreach (WashGroup collection in washGroups)
                {
                    switch (collection.Name)
                    {
                        case "ThemeColor":
                            collection.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbTabArea.Text.ToString());
                            break;
                        case "AlternateColor":
                            collection.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbText.Text.ToString());
                            break;
                    }
                }
            }


            // Set WashMode
            _currentWasher.BeginInit();
            if ((bool)rbHueSaturationReplacement.IsChecked)
            {
                _currentWasher.WashMode = WashMode.HueSaturationReplacement;
            }
            else
            {
                _currentWasher.WashMode = WashMode.SoftLightBlend;
            }


            ComboBoxItem selectedWashBase = (ComboBoxItem)cbxWashBase.SelectedItem;
            foreach (ResourceDictionary current in Application.Current.Resources.MergedDictionaries)
            {
                Debug.WriteLine(current.ToString());
            }

            switch (selectedWashBase.Content.ToString())
            {
                case "DockManagerWashBaseLight":
                    _currentWasher.SourceDictionary = new DockManagerWashBaseLightBrushes();
                    _currentWasher.WashGroups = washGroups;
                    _currentWasher.EndInit();

                    // inject the washer into the application merged dictionaries
                    CurrentTheme = ThemeManager.GetResourceSet("WashBaseLight", "DockManager");
                    Application.Current.Resources.MergedDictionaries.Remove(CurrentTheme);
                    Application.Current.Resources.MergedDictionaries.Remove(_currentWasher);
                    Application.Current.Resources.MergedDictionaries.Add(CurrentTheme);
                    Application.Current.Resources.MergedDictionaries.Add(_currentWasher);
                    break;
                case "DockManagerWashBaseDark":
                    _currentWasher.SourceDictionary = new DockManagerWashBaseDarkBrushes();
                    _currentWasher.WashGroups = washGroups;
                    _currentWasher.EndInit();

                    // inject the washer into the application merged dictionaries
                    CurrentTheme = ThemeManager.GetResourceSet("WashBaseDark", "DockManager");
                    Application.Current.Resources.MergedDictionaries.Remove(CurrentTheme);
                    Application.Current.Resources.MergedDictionaries.Remove(_currentWasher);
                    Application.Current.Resources.MergedDictionaries.Add(CurrentTheme);
                    Application.Current.Resources.MergedDictionaries.Add(_currentWasher);
                    break;
            }

            generateXAMLcode();
        }

        public void generateXAMLcode()
        {
            string brushesFile = "<igThemes:DockManagerWashBaseLightBrushes />";
            string xamlText = "<ResourceDictionary>\n";
            xamlText += " <ResourceDictionary.MergedDictionaries>\n";
            ComboBoxItem selectedWashBase = (ComboBoxItem)cbxWashBase.SelectedItem;

            switch (selectedWashBase.Content.ToString())
            {
                case "DockManagerWashBaseLight":
                    xamlText += "    <igThemes:DockManagerWashBaseLight />\n";
                    brushesFile = "<igThemes:DockManagerWashBaseLightBrushes />";
                    break;
                case "DockManagerWashBaseDark":
                    xamlText += "    <igThemes:DockManagerWashBaseDark />\n";
                    brushesFile = "<igThemes:DockManagerWashBaseDarkBrushes />";
                    break;
            }

            // Show Simple Code Output 
            if (SimpleWashColorMode)
            {

                if ((bool)rbHueSaturationReplacement.IsChecked)
                {
                    xamlText += "      <igThemes:ResourceWasher\n";
                    xamlText += "        AutoWash=\"True\" WashColor=\"" + tbSingleColor.Text.ToString() + "\"\n";
                    xamlText += "        WashMode=\"HueSaturationReplacement\">\n";
                }
                else
                {
                    xamlText += "      <igThemes:ResourceWasher\n";
                    xamlText += "        AutoWash=\"True\" WashColor=\"" + tbSingleColor.Text.ToString() + "\"\n";
                    xamlText += "        WashMode=\"SoftLightBlend\">\n";
                }

                xamlText += "        <igThemes:ResourceWasher.SourceDictionary>\n";
                xamlText += "          " + brushesFile + "\n";
                xamlText += "        </igThemes:ResourceWasher.SourceDictionary>\n" +
                        "      </igThemes:ResourceWasher>\n" +
                    "  </ResourceDictionary.MergedDictionaries>\n" +
                "</ResourceDictionary>\n";
            }
            // Show Advanced WashGroup Code Output
            else
            {
                if ((bool)rbHueSaturationReplacement.IsChecked)
                {
                    xamlText += "      <igThemes:ResourceWasher\n";
                    xamlText += "        AutoWash=\"True\" WashMode=\"HueSaturationReplacement\">\n";
                }
                else
                {
                    xamlText += "      <igThemes:ResourceWasher\n";
                    xamlText += "        AutoWash=\"True\" WashMode=\"SoftLightBlend\">\n";
                }

                xamlText += "        <igThemes:ResourceWasher.SourceDictionary>\n";
                xamlText += "          " + brushesFile + "\n";
                xamlText += "        </igThemes:ResourceWasher.SourceDictionary>\n" +
                              "        <igThemes:ResourceWasher.WashGroups>\n" +
                              "          <igThemes:WashGroupCollection>\n" +
                                    "            <igThemes:WashGroup Name=\"ThemeColor\"\n" +
                                    "              WashColor=\"" + tbTabArea.Text.ToString() + "\" />\n" +
                                    "            <igThemes:WashGroup Name=\"AlternateColor\"\n" +
                                    "              WashColor=\"" + tbText.Text.ToString() + "\" />\n" +
                                "          </igThemes:WashGroupCollection>\n" +
                            "        </igThemes:ResourceWasher.WashGroups>\n" +
                        "      </igThemes:ResourceWasher>\n" +
                    "  </ResourceDictionary.MergedDictionaries>\n" +
                "</ResourceDictionary>\n";
            }

            WashXamlText = xamlText;
            this.SetValue(WashXamlTextProperty, xamlText);
        }


        #endregion Methods

        #region Dependency Properties

        #region WashXamlText
        /// <summary>
        /// Identifies the 'WashXamlText' dependency property
        /// </summary>
        public static readonly DependencyProperty WashXamlTextProperty = DependencyProperty.Register("WashXamlText",
        typeof(string), typeof(LiveResourceWashing), new FrameworkPropertyMetadata());

        /// <summary>
        /// The image that was selected
        /// </summary>
        public string WashXamlText
        {
            get
            {
                return (string)this.GetValue(LiveResourceWashing.WashXamlTextProperty);
            }
            set
            {
                this.SetValue(LiveResourceWashing.WashXamlTextProperty, value);
            }
        }

        #endregion WashXamlText

        #region SimpleWashColorMode
        /// <summary>
        /// Identifies the 'SimpleWashColorMode' dependency property
        /// </summary>
        public static DependencyProperty SimpleWashColorModeProperty =
            DependencyProperty.Register("SimpleWashColorMode",
            typeof(bool), typeof(LiveResourceWashing));
        /// <summary>
        /// The Value of the Wash Mode, True or False
        /// </summary>
        public bool SimpleWashColorMode
        {
            get { return ((bool)base.GetValue(SimpleWashColorModeProperty)); }
            set { base.SetValue(SimpleWashColorModeProperty, value); }
        }

        #endregion SimpleWashColorMode

        #endregion Dependency Properties

        #region Event Handlers

        private void toggleVisibility_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Name.ToString())
            {
                case "rbSimple":
                    SimpleWashColorMode = true;
                    SimpleStyleGroup.Visibility = Visibility.Visible;
                    AdvancedStyleGroup.Visibility = Visibility.Collapsed;
                    break;
                case "rbAdvanced":
                    SimpleWashColorMode = false;
                    AdvancedStyleGroup.Visibility = Visibility.Visible;
                    SimpleStyleGroup.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void rbHueSaturationReplacement_Click(object sender, RoutedEventArgs e)
        {
            ReWash();
        }

        private void rbSoftLightBlend_Click(object sender, RoutedEventArgs e)
        {
            ReWash();
        }

        private void OnWashBaseChanged(object sender, SelectionChangedEventArgs e)
        {
            ReWash();
        }

        #endregion Event Handlers
    }

    [ValueConversion(typeof(double), typeof(double)), Localizability(LocalizationCategory.NeverLocalize)]
    public class StringToBrushConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                try
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    Color c = (Color)System.Windows.Media.ColorConverter.ConvertFromString(value.ToString());
                    brush.Color = c;
                    return brush;
                }
                catch (Exception)
                {
                    return new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
        #endregion
    }
}
