using System.Windows;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Framework;
using Infragistics.Controls.Interactions;

namespace IGExtensions.Common.Controls
{
    public partial class ColorWashEditor : XamDialogWindow
    {
        public ColorWashEditor()
        {
            InitializeComponent();
            this.IsModal = false;
            this.Header = ColorWashEditorStrings.WashEditorTitle;
        }
        public event ColorWashSettingsChangedEventHandler WashSettingsChanged;
       
        private void ColorWashEditorPanel_WashSettingsChanged(object sender, ColorWashSettingsChangedEventArgs e)
        {
            DebugManager.Log("ColorWashEditor.WashSettings changed: " + e.ColorWashSettings.WashColor + " " + e.ColorWashSettings.WashMode);

            if (this.WashSettingsChanged != null)
            {
                this.WashSettingsChanged(sender, e);
            }
        }
        public void InitializeWashSettings(ColorWashSettings washSettings)
        {
            this.ColorWashEditorPanel.InitializeWashSettings(washSettings);
        }
        
        public new void Show()
        {
            if (this.Visibility == Visibility.Collapsed)
                this.Visibility = Visibility.Visible;
            base.Show();
        }
    }

    
}
