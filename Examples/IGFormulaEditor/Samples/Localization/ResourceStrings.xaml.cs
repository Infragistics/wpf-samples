using System;
using System.Windows;
using System.Windows.Navigation;
using IGFormulaEditor.Resources;
using Infragistics.Calculations;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGFormulaEditor.Samples.Localization
{
    /// <summary>
    /// Interaction logic for ResourceStrings.xaml
    /// </summary>
    public partial class ResourceStrings : SampleContainer
    {
        public ResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamCalculationManager.RegisterResources(
                "IGFormulaEditor.Resources.SampleCalculationManagerStrings",
                typeof(SampleCalculationManagerStrings).Assembly);

            XamFormulaEditor.RegisterResources(
                "IGFormulaEditor.Resources.SampleFormulaEditorStrings",
                typeof(SampleFormulaEditorStrings).Assembly);

            InitializeComponent();
            Loaded += new RoutedEventHandler(Sample_Loaded);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamCalculationManager.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGFormulaEditor.Resources.SampleCalculationManagerStrings");

            XamFormulaEditor.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGFormulaEditor.Resources.SampleFormulaEditorStrings");

            base.OnNavigatingFrom(e);
        }

        void Sample_Loaded(object sender, RoutedEventArgs e)
        { 
            this.Loaded -= Sample_Loaded;
            this.formulaEditor.FormulaEditorDialogDisplaying += OnFormulaEditorDialogDisplaying;
        }

        void OnFormulaEditorDialogDisplaying(object sender, FormulaEditorDialogDisplayingEventArgs e)
        {
            // Apply IG theme to the dialog 
            ThemeLoader.SetTheme(e.Dialog, ThemeType.RoyalLight);
        }
    }
}