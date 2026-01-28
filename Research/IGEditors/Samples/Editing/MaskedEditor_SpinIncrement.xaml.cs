using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;
using System;

namespace IGEditors.Samples.Editing
{
    /// <summary>
    /// Interaction logic for MaskedEditor_SpinIncrement.xaml
    /// </summary>

    public partial class MaskedEditor_SpinIncrement : SampleContainer
    {
		public MaskedEditor_SpinIncrement( )
        {
            InitializeComponent();

			Type[] enumTypes = new Type[]
			{
				typeof( SpinButtonDisplayMode )
			};

			foreach (Type t in enumTypes)
				this.Resources.Add( "enum_" + t.Name, Enum.GetValues(t));

			object[] maskedEditorSpinIncrements = new object[]
			{
				0.5,
				1,
				5,
				10,
				"log"
			};

			object[] dateEditorSpinIncrements = new object[]
			{
				"1d",
				"1m",
				"1y",
			};

			this.Resources.Add("maskedEditorSpinIncrementValues", maskedEditorSpinIncrements);
			this.Resources.Add("dateEditorSpinIncrementValues", dateEditorSpinIncrements);
			this.maskedEditorSpinIncrement.SelectedIndex = 0;
			this.dateEditorSpinIncrement.SelectedIndex = 0;
        }
    }
}