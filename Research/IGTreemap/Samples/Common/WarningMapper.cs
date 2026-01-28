using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;

namespace IGTreemap.Samples.ValueMappers
{
    public class WarningMapper : ValueMapper
    {
        /// <summary>
        /// The MapValue method, which applies a value to the nodes.
        /// </summary>
        /// <param name="node">The current node.</param>
        public override void MapValue(TreemapNode node)
        {
            if (node.DataContext == null)
            {
                return;
            }

            //Check if the current node is of the ValueTypeName and has children in it's template
            if (this.ValueTypeName == node.DataContext.GetType().Name && VisualTreeHelper.GetChildrenCount(node) > 0)
            {
                InventoryEntry inventoryEntry = (InventoryEntry)node.DataContext;

                //The custom logic
                if (inventoryEntry.Quantity < 300)
                {
                    //Get an instance of the WarningImage from the node's template
                    FrameworkElement rootElement = VisualTreeHelper.GetChild(node, 0) as FrameworkElement;
                    Image warningImage = rootElement.FindName("WarningImage") as Image;

                    //Set the visibility of the image
                    warningImage.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// The ResetValue method, which returns the nodes to their previous state.
        /// </summary>
        /// <param name="node">The current node.</param>
        public override void ResetValue(TreemapNode node)
        {
            //Not used.
        }
    }
}
