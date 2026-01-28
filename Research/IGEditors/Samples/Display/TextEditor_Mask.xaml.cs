using Infragistics.Samples.Framework;
using System.Windows.Controls;

namespace IGEditors.Samples.Display
{
    /// <summary>
    /// Interaction logic for TextEditor_Mask.xaml
    /// </summary>

    public partial class TextEditor_Mask : SampleContainer
    {
        public TextEditor_Mask()
        {
            InitializeComponent();
        }

        private void cboInvalidValueBehaviors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // we need to reset the text value to update the text editor value display
            // normally, this wouldn't be necessary as the data mode is not typically changed on the fly
            string val = this.txtEditor.Text;
            this.txtEditor.Value = null;
            this.txtEditor.Text = val;
        }
    }
}