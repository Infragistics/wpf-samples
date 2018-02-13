using Infragistics.Windows.Ribbon;
using Prism.Interactivity;

namespace IgExcel.Infrastructure.Behaviours
{
    public class GalleryItemSelectedCommandBehavior : CommandBehaviorBase<GalleryItemGroup>
    {
        public GalleryItemSelectedCommandBehavior(GalleryItemGroup galleryTool)
            : base(galleryTool)
        {
            galleryTool.GalleryTool.ItemSelected += GalleryTool_ItemSelected;
        }

        private void GalleryTool_ItemSelected(object sender, Infragistics.Windows.Ribbon.Events.GalleryItemEventArgs e)
        {
            GalleryTool galleryTool = sender as GalleryTool;
            
            if (e.Item == null || galleryTool.IsVisible == false)
                return;

            this.ExecuteCommand(e.Item.Key);
        }
    }
}
