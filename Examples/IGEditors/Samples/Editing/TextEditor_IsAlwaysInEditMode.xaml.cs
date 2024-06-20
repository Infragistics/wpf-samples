using Infragistics.Samples.Framework;
using System.Windows;

namespace IGEditors.Samples.Editing
{
    /// <summary>
    /// Interaction logic for TextEditor_IsAlwaysInEditMode.xaml
    /// </summary>
    public partial class TextEditor_IsAlwaysInEditMode : SampleContainer
    {
        public TextEditor_IsAlwaysInEditMode()
        {
            InitializeComponent();
        }

		private void chkIsAlwaysInEditMode_Checked(object sender, RoutedEventArgs e)
		{
			if (this.chkIsAlwaysInEditMode.IsChecked ?? false)
			{
				this.txtEditor.StartEditMode();
				this.txtEditor1.StartEditMode();
			}
		}
    }
}