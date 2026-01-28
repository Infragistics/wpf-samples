using System.Windows;
using System.Windows.Controls;
using Infragistics.Calculations;
using Infragistics.Samples.Framework;

namespace IGFormulaEditor.Samples.Display
{
    /// <summary>
    /// Interaction logic for xamFormulaEditorSample.xaml
    /// </summary>
    public partial class FormulaEditorSample : SampleContainer
    {
        // A string to store the previosly selected item of the NamedReferences ComboBox
        string SelectedNamedReference = "Addition";

        public FormulaEditorSample()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(xamFormulaEditor_Loaded);
        }

        // In the Loaded event the Formulas of the NamedReferences of the CalculationManager
        // are assigned as Formulas for the corresponding TextBoxes.
        void xamFormulaEditor_Loaded(object sender, RoutedEventArgs e)
        {
            ControlCalculationSettings additionCalculationSettings = new ControlCalculationSettings();
            additionCalculationSettings.Formula = this.CalculationManager.NamedReferences["myAddition"].Formula;
            XamCalculationManager.SetControlSettings(this.Addition, additionCalculationSettings);
            ControlCalculationSettings multiplicationCalculationSettings = new ControlCalculationSettings();
            multiplicationCalculationSettings.Formula = this.CalculationManager.NamedReferences["myMultiplication"].Formula;
            XamCalculationManager.SetControlSettings(this.Multiplication, multiplicationCalculationSettings);
            ControlCalculationSettings SubtractionCalculationSettings = new ControlCalculationSettings();
            SubtractionCalculationSettings.Formula = this.CalculationManager.NamedReferences["mySubtraction"].Formula;
            XamCalculationManager.SetControlSettings(this.Subtraction, SubtractionCalculationSettings);
        }

        private void namedReferencesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.namedReferencesCombo != null)
            {
                string selectedReference = ((ComboBoxItem)this.namedReferencesCombo.SelectedItem).Tag.ToString();
                this.CalculationManager.NamedReferences["my" + SelectedNamedReference].Formula = this.formulaEditor.Formula;
                this.SelectedNamedReference = selectedReference;
                this.formulaEditor.Target = FindName(selectedReference);
                this.formulaEditor.Formula = this.CalculationManager.NamedReferences["my" + selectedReference].Formula;
            }
        }
    }
}
