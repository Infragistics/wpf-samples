using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Calculations;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGFormulaEditor.Samples.Display
{
    /// <summary>
    /// Interaction logic for FormulaEditorSample.xaml
    /// </summary>
    public partial class FormulaEditorSample : SampleContainer
    {
        // A string to store the previosly selected item of the NamedReferences ComboBox
        string _selectedNamedReference = "Addition";
        private XamCalculationManager _calculationManager;

        public FormulaEditorSample()
        {
            InitializeComponent();
            this.Loaded += xamFormulaEditor_Loaded;
            this.formulaEditor.FormulaEditorDialogDisplaying += OnFormulaEditorDialogDisplaying;
        }

        void OnFormulaEditorDialogDisplaying(object sender, FormulaEditorDialogDisplayingEventArgs e)
        { 
            // Apply a theme to the Dialog
            ThemeLoader.SetTheme(e.Dialog, ThemeType.RoyalLight);
        }

        

        // In the Loaded event the Formulas of the NamedReferences of the _calculationManager
        // are assigned as Formulas for the corresponding TextBoxes.
        void xamFormulaEditor_Loaded(object sender, RoutedEventArgs e)
        {
            if (_calculationManager == null)
            {
                _calculationManager = new XamCalculationManager();
            }

            _calculationManager = (XamCalculationManager)this.Resources["CalculationManager"];
            ControlCalculationSettings additionCalculationSettings = new ControlCalculationSettings
            {
                Formula = _calculationManager.NamedReferences["myAddition"].Formula
            };
            XamCalculationManager.SetControlSettings(this.Addition, additionCalculationSettings);
            ControlCalculationSettings multiplicationCalculationSettings = new ControlCalculationSettings
            {
                Formula = _calculationManager.NamedReferences["myMultiplication"].Formula
            };
            XamCalculationManager.SetControlSettings(this.Multiplication, multiplicationCalculationSettings);
            ControlCalculationSettings subtractionCalculationSettings = new ControlCalculationSettings
            {
                Formula = _calculationManager.NamedReferences["mySubtraction"].Formula
            };
            XamCalculationManager.SetControlSettings(this.Subtraction, subtractionCalculationSettings);
        }

        private void namedReferencesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.namedReferencesCombo != null && formulaEditor != null)
            {
                string selectedReference = ((ComboBoxItem)this.namedReferencesCombo.SelectedItem).Tag.ToString();
                this._calculationManager.NamedReferences["my" + _selectedNamedReference].Formula = this.formulaEditor.Formula;
                this._selectedNamedReference = selectedReference;
                this.formulaEditor.Target = FindName(selectedReference);
                this.formulaEditor.Formula = _calculationManager.NamedReferences["my" + selectedReference].Formula;
            }
        }
    }
}
