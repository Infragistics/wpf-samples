using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Infragistics.Controls.Maps;

namespace IGMap.Samples.Editing
{
    public partial class MultipleLayers : Infragistics.Samples.Framework.SampleContainer
    {
        public MultipleLayers()
        {
            InitializeComponent();

            this.OptionsPanel.Visibility = Visibility.Collapsed;
            this.loaderMessage.Visibility = Visibility.Visible;
        }

        private int totalLayers;
        private int loadedLayers;
        private double totalProgress;
  
        private void OnMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            totalLayers = this.theMap.Layers.Count;
            if (e.Action == MapLayerImportAction.End)
            {
                loadedLayers += 1;
                string name = ((MapLayer) sender).LayerName;
                totalProgress = (double) loadedLayers/totalLayers*100;
                UpdatePorgressBar();
            }
        }

        private void UpdatePorgressBar()
        {
            loaderProgress.Value = totalProgress;
            txtProgress.Text = String.Format("... {0:0.00} %", totalProgress);
            if (loadedLayers == totalLayers)
            {
                ((Storyboard) this.Resources["sbFadeOutLoaderMessage"]).Begin();
				this.OptionsPanel.Visibility = Visibility.Visible;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk == null || chk.Content == null || theMap == null)
            {
                return;
            }
            var layerName = chk.Tag as string;

            MapLayer layer = theMap.Layers[layerName];
            ;
            if (layer == null)
            {
                return;
            }

            layer.IsVisible = chk.IsChecked ?? true;
        }
    }
}