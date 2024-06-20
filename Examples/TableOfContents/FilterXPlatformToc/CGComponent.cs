using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterXPlatformToc
{
    public class CGComponent
    {
        public CGComponent(int ComponentID, string RouteName, int ProductFamilyID, int CmsContentID,
            int GettingStartedID, int FaqID)
        {
            this.CmsContentID = CmsContentID;
            this.ComponentID = ComponentID;
            this.FaqID = FaqID;
            this.GettingStartedID = GettingStartedID;
            this.ProductFamilyID = ProductFamilyID;
            this.RouteName = RouteName;
        }
        public int ComponentID { get; set; }
        public string RouteName { get; set; }
        public int ProductFamilyID { get; set; }
        public int CmsContentID { get; set; }
        public int GettingStartedID { get; set; }
        public int FaqID { get; set; }
    }
}
