using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Windows.Ribbon;
using Infragistics.Windows.Themes;
using IGRibbon.Resources;

namespace IGRibbon.Samples.Styling
{
    public partial class LiveResourceWashing : SampleContainer
    {
        #region Fields

        private readonly WashGroupCollection _washGroups = new WashGroupCollection();
        private readonly WashGroup _wgTabArea = new WashGroup();
        private readonly WashGroup _wgText = new WashGroup();
        private readonly WashGroup _wgCaption = new WashGroup();
        private readonly WashGroup _wgHover = new WashGroup();
        private readonly WashGroup _wgPressed = new WashGroup();
        private readonly WashGroup _wgIsChecked = new WashGroup();
        private readonly WashGroup _wgBaseColor = new WashGroup();
        private readonly WashGroup _wgHeaderPanel = new WashGroup();

        private ResourceWasher _currentWasher = new ResourceWasher();
        private ResourceDictionary _currentTheme;

        ResourceDictionary[] initialDictionaries;

        #endregion Fields

        #region Constructor
        public LiveResourceWashing()
        {
            InitializeComponent();
            // Setting loading and unloading event handlers
            Loaded += new RoutedEventHandler(LiveResourceWashing_Loaded);
            Unloaded += new RoutedEventHandler(LiveResourceWashing_Unloaded);


            // Configure WashGroups
            Color washColor = Color.FromArgb(0xFF, 0x33, 0x99, 0xCC);
            _wgTabArea.Name = "TabArea";
            _wgTabArea.WashColor = washColor;
            _wgText.Name = "Text";
            _wgText.WashColor = washColor;
            _wgCaption.Name = "Caption";
            _wgCaption.WashColor = washColor;
            _wgHover.Name = "Hover";
            _wgHover.WashColor = washColor;
            _wgPressed.Name = "Pressed";
            _wgPressed.WashColor = washColor;
            _wgIsChecked.Name = "IsChecked";
            _wgIsChecked.WashColor = washColor;
            _wgBaseColor.Name = "BaseColor";
            _wgBaseColor.WashColor = washColor;
            _wgHeaderPanel.Name = "HeaderPanel";
            _wgHeaderPanel.WashColor = washColor;

            // Add WashGroups to WashGroup collection
            _washGroups.Add(_wgTabArea);
            _washGroups.Add(_wgText);
            _washGroups.Add(_wgCaption);
            _washGroups.Add(_wgHover);
            _washGroups.Add(_wgPressed);
            _washGroups.Add(_wgIsChecked);
            _washGroups.Add(_wgBaseColor);
            _washGroups.Add(_wgHeaderPanel);

            // Register the WashBaseLight and WashBaseDark themes
            ThemeManager.Register("WashBaseLight", "Ribbon", RibbonWashBaseLight.Instance);
            ThemeManager.Register("WashBaseDark", "Ribbon", RibbonWashBaseDark.Instance);

            // Start in simple mode
            SimpleWashColorMode = true;

            cbxWashBase.SelectedIndex = 0;
            cbxWashMode.SelectedIndex = 0;

            WashResources();

        }

        void LiveResourceWashing_Unloaded(object sender, RoutedEventArgs e)
        {
            // Clear all resources
            Application.Current.Resources.MergedDictionaries.Clear();

            // Return initial ones
            for (int i = 0; i < initialDictionaries.Length; i++)
            {
                Application.Current.Resources.MergedDictionaries.Add(initialDictionaries[i]);
            }
        }

        void LiveResourceWashing_Loaded(object sender, RoutedEventArgs e)
        {
            if (initialDictionaries == null)
                initialDictionaries = new ResourceDictionary[Application.Current.Resources.MergedDictionaries.Count];
            Application.Current.Resources.MergedDictionaries.CopyTo(initialDictionaries, 0);
        }
        #endregion Constructor

        #region Methods
        private void SetWashGroupColor(object sender, RoutedEventArgs e)
        {
            ButtonTool setBtn = sender as ButtonTool;
            TextBox targetTextBox;
            WashGroup targetWashGroup;
            switch (setBtn.Name)
            {
                case "btnSetTabArea":
                    targetWashGroup = _wgTabArea;
                    targetTextBox = tbTabArea;
                    break;

                case "btnSetText":
                    targetWashGroup = _wgText;
                    targetTextBox = tbText;
                    break;

                case "btnSetCaption":
                    targetWashGroup = _wgCaption;
                    targetTextBox = tbCaption;
                    break;

                case "btnSetIsChecked":
                    targetWashGroup = _wgIsChecked;
                    targetTextBox = tbIsChecked;
                    break;

                case "btnSetHover":
                    targetWashGroup = _wgHover;
                    targetTextBox = tbHover;
                    break;

                case "btnSetPressed":
                    targetWashGroup = _wgPressed;
                    targetTextBox = tbPressed;
                    break;

                case "btnSetBaseColor":
                    targetWashGroup = _wgBaseColor;
                    targetTextBox = tbBaseColor;
                    break;

                case "btnSetHeaderPanel":
                    targetWashGroup = _wgHeaderPanel;
                    targetTextBox = tbHeaderPanel;
                    break;

                case "btnSetMasterWashColor":
                    targetWashGroup = _wgBaseColor;
                    targetTextBox = tbSingleColor;
                    break;

                default:
                    Debug.Fail("Unknown Button: " + setBtn.Name);
                    return;
            }

            ColorPickerDialog colorPickerDialog = new ColorPickerDialog();
            try
            {
                colorPickerDialog.StartingColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(targetTextBox.Text);
            }
            catch
            {
                colorPickerDialog.StartingColor = Colors.Black;
            }

            colorPickerDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            colorPickerDialog.Owner = Application.Current.MainWindow;

            bool? dialogResult = colorPickerDialog.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                if (targetWashGroup != null)
                    targetWashGroup.WashColor = colorPickerDialog.DialogSelectedColor;

                if (targetTextBox != null)
                {
                    targetTextBox.Text = colorPickerDialog.DialogSelectedColor.ToString();
                }
            }

            WashResources();
        }

        private void WashResources()
        {
            // since focus/mouse/etc may be within the ribbon and the elements within 
            // may still be in the middle of some processing it is better to wash 
            // the resources asynchronously since that will change the styles and 
            // templates
            this.Dispatcher.BeginInvoke(new Action(this.WashResourcesImpl), null);
        }

        private void WashResourcesImpl()
        {
            // if an editor is in edit mode, etc. then make sure to 
            // shift focus out of the ribbon first
            if (this.xamRibbon.IsKeyboardFocusWithin)
                System.Windows.Input.Keyboard.Focus(null);

            // We could get in here before the mode has been selected in which case we should wait until its set later.
            if (cbxWashMode.SelectedIndex < 0)
                return;

            // Remove the old washer from application resources
            if (_currentWasher != null)
                Application.Current.Resources.MergedDictionaries.Remove(_currentWasher);

            if (this._currentTheme != null)
                Application.Current.Resources.MergedDictionaries.Remove(_currentTheme);

            _currentWasher = new ResourceWasher();
            _currentTheme = new ResourceDictionary();

            try
            {
                if (SimpleWashColorMode)
                {
                    foreach (WashGroup collection in _washGroups)
                    {
                        //Debug.WriteLine(collection.Name);
                        collection.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbSingleColor.Text.ToString());
                    }
                }
                else
                {
                    foreach (WashGroup washGroup in _washGroups)
                    {
                        switch (washGroup.Name)
                        {
                            case "TabArea":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbTabArea.Text.ToString());
                                break;
                            case "Text":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbText.Text.ToString());
                                break;
                            case "Caption":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbCaption.Text.ToString());
                                break;
                            case "Hover":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbHover.Text.ToString());
                                break;
                            case "Pressed":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbPressed.Text.ToString());
                                break;
                            case "IsChecked":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbIsChecked.Text.ToString());
                                break;
                            case "BaseColor":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbBaseColor.Text.ToString());
                                break;
                            case "HeaderPanel":
                                washGroup.WashColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(tbHeaderPanel.Text.ToString());
                                break;
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(RibbonStrings.Msg_InvalidColorValue, RibbonStrings.Msg_InputError);
                return;
            }

            // Set WashMode
            _currentWasher.BeginInit();
            if (-1 < cbxWashMode.SelectedIndex)
                _currentWasher.WashMode = (WashMode)cbxWashMode.SelectedItem;

            string selectedWashBase = cbxWashBase.SelectedItem as string;
            if (String.IsNullOrEmpty(selectedWashBase))
                return;

            if (_currentTheme != null)
                Application.Current.Resources.MergedDictionaries.Remove(_currentTheme);

            if (_currentWasher != null)
                Application.Current.Resources.MergedDictionaries.Remove(_currentWasher);

            switch (selectedWashBase)
            {
                case "RibbonWashBaseLight":
                    _currentWasher.SourceDictionary = new RibbonWashBaseLightBrushes();
                    _currentWasher.WashGroups = _washGroups;
                    _currentWasher.EndInit();

                    // inject the washer into the application merged dictionaries
                    _currentTheme = ThemeManager.GetResourceSet("WashBaseLight", "Ribbon");
                    break;

                case "RibbonWashBaseDark":
                    _currentWasher.SourceDictionary = new RibbonWashBaseDarkBrushes();
                    _currentWasher.WashGroups = _washGroups;
                    _currentWasher.EndInit();

                    // inject the washer into the application merged dictionaries
                    _currentTheme = ThemeManager.GetResourceSet("WashBaseDark", "Ribbon");
                    break;

                case "RibbonOffice2k7Black":
                    _currentWasher.SourceDictionary = new RibbonOffice2k7BlackBrushes();
                    _currentWasher.WashGroups = _washGroups;
                    _currentWasher.EndInit();

                    // inject the washer into the application merged dictionaries
                    _currentTheme = ThemeManager.GetResourceSet("Office2k7Black", "Ribbon");
                    break;
                case "RibbonOffice2k7Silver":
                    _currentWasher.SourceDictionary = new RibbonOffice2k7SilverBrushes();
                    _currentWasher.WashGroups = _washGroups;
                    _currentWasher.EndInit();

                    _currentTheme = ThemeManager.GetResourceSet("Office2k7Silver", "Ribbon");
                    break;

                case "RibbonOffice2k7Blue":
                    _currentWasher.SourceDictionary = new RibbonOffice2k7BlueBrushes();
                    _currentWasher.WashGroups = _washGroups;
                    _currentWasher.EndInit();

                    _currentTheme = ThemeManager.GetResourceSet("Office2k7Blue", "Ribbon");
                    break;
            }

            if (_currentTheme != null)
                Application.Current.Resources.MergedDictionaries.Add(_currentTheme);

            if (_currentWasher != null)
                Application.Current.Resources.MergedDictionaries.Add(_currentWasher);

            generateXAMLcode();
        }

        public void generateXAMLcode()
        {
            string brushesFile = "<igThemes:RibbonOffice2k7BlackBrushes />";
            string xamlText = "<ResourceDictionary>\n";
            xamlText += "\t<ResourceDictionary.MergedDictionaries>\n";

            string selectedWashBase = cbxWashBase.SelectedItem as string;
            switch (selectedWashBase)
            {
                case "RibbonWashBaseLight":
                    xamlText += "\t\t<igThemes:RibbonWashBaseLight />\n";
                    brushesFile = "<igThemes:RibbonWashBaseLightBrushes />";
                    break;
                case "RibbonWashBaseDark":
                    xamlText += "\t\t<igThemes:RibbonWashBaseDark />\n";
                    brushesFile = "<igThemes:RibbonWashBaseDarkBrushes />";
                    break;
                case "RibbonOffice2k7Black":
                    xamlText += "\t\t<igThemes:RibbonOffice2k7Black />\n";
                    brushesFile = "<igThemes:RibbonOffice2k7BlackBrushes />";
                    break;
                case "RibbonOffice2k7Silver":
                    xamlText += "\t\t<igThemes:RibbonOffice2k7Silver />\n";
                    brushesFile = "<igThemes:RibbonOffice2k7Brushes />";
                    break;
                case "RibbonOffice2k7Blue":
                    xamlText += "\t\t<igThemes:RibbonOffice2k7Blue />\n";
                    brushesFile = "<igThemes:RibbonOffice2k7BlueBrushes />";
                    break;
            }

            // Show Simple Code Output 
            WashMode selectedWashMode = (WashMode)cbxWashMode.SelectedItem;
            if (SimpleWashColorMode)
            {

                if (selectedWashMode == WashMode.HueSaturationReplacement)
                {
                    xamlText += "\t\t\t<igThemes:ResourceWasher AutoWash=\"True\" WashColor=\"" + tbSingleColor.Text.ToString() + "\" WashMode=\"HueSaturationReplacement\" >\n";
                }
                else
                {
                    xamlText += "\t\t\t<igThemes:ResourceWasher AutoWash=\"True\" WashColor=\"" + tbSingleColor.Text.ToString() + "\" WashMode=\"SoftLightBlend\" >\n";
                }

                xamlText += "\t\t\t\t<igThemes:ResourceWasher.SourceDictionary>\n";
                xamlText += "\t\t\t\t\t" + brushesFile + "\n";
                xamlText += "\t\t\t\t</igThemes:ResourceWasher.SourceDictionary>\n" +
                        "\t\t\t</igThemes:ResourceWasher>\n" +
                    "\t</ResourceDictionary.MergedDictionaries>\n" +
                "</ResourceDictionary>\n";
            }
            // Show Advanced WashGroup Code Output
            else
            {
                if (selectedWashMode == WashMode.HueSaturationReplacement)
                {
                    xamlText += "\t\t\t<igThemes:ResourceWasher AutoWash=\"True\" WashMode=\"HueSaturationReplacement\" >\n";
                }
                else
                {
                    xamlText += "\t\t\t<igThemes:ResourceWasher AutoWash=\"True\" WashMode=\"SoftLightBlend\" >\n";
                }

                xamlText += "\t\t\t\t<igThemes:ResourceWasher.SourceDictionary>\n";
                xamlText += "\t\t\t\t\t" + brushesFile + "\n";
                xamlText += "\t\t\t\t</igThemes:ResourceWasher.SourceDictionary>\n" +
                              "\t\t\t\t<igThemes:ResourceWasher.WashGroups>\n" +
                              "\t\t\t\t\t<igThemes:WashGroupCollection>\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"BaseColor\"  WashColor=\"" + tbBaseColor.Text.ToString() + "\" />\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"TabArea\" WashColor=\"" + tbTabArea.Text.ToString() + "\" />\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"HeaderPanel\"  WashColor=\"" + tbHeaderPanel.Text.ToString() + "\"/>\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"Caption\"  WashColor=\"" + tbCaption.Text.ToString() + "\" />\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"Hover\" WashColor=\"" + tbHover.Text.ToString() + "\" />\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"Pressed\" WashColor=\"" + tbPressed.Text.ToString() + "\" />\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"IsChecked\" WashColor=\"" + tbIsChecked.Text.ToString() + "\" />\n" +
                                    "\t\t\t\t\t\t<igThemes:WashGroup Name=\"Text\" WashColor=\"" + tbText.Text.ToString() + "\" />\n" +
                                "\t\t\t\t\t</igThemes:WashGroupCollection>\n" +
                            "\t\t\t\t</igThemes:ResourceWasher.WashGroups>\n" +
                        "\t\t\t</igThemes:ResourceWasher>\n" +
                    "\t</ResourceDictionary.MergedDictionaries>\n" +
                "</ResourceDictionary>\n";
            }

            WashXamlText = xamlText;
        }


        #endregion Methods

        #region Dependency Properties

        #region WashXamlText

        /// <summary>
        /// Identifies the 'WashXamlText' dependency property
        /// </summary>
        public static readonly DependencyProperty WashXamlTextProperty =
            DependencyProperty.Register(
            "WashXamlText",
            typeof(string),
            typeof(LiveResourceWashing));

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

        public static DependencyProperty SimpleWashColorModeProperty =
            DependencyProperty.Register("SimpleWashColorMode",
            typeof(bool), typeof(LiveResourceWashing));

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

            WashResources();
        }

        private void rbHueSaturationReplacement_Click(object sender, RoutedEventArgs e)
        {
            WashResources();
        }

        private void rbSoftLightBlend_Click(object sender, RoutedEventArgs e)
        {
            WashResources();
        }


        private void cbxWashBase_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            WashResources();
        }

        private void cbxWashMode_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            WashResources();
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