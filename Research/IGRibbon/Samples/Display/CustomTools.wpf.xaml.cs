using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Ribbon;
using Infragistics.Windows.Ribbon.Internal;

namespace IGRibbon.Samples.Display
{
    public partial class CustomTools : SampleContainer
    {
        public CustomTools()
        {
            InitializeComponent();
        }
    }

    public partial class ButtonToolProxy : RibbonToolProxy<CustomButtonTool>
    {
        public ButtonToolProxy()
            : base()
        {
        }
    }

    public partial class CustomButtonTool : Button, IRibbonTool
    {
        static CustomButtonTool()
        {
            RibbonGroup.MaximumSizeProperty.OverrideMetadata(typeof(CustomButtonTool),
             new FrameworkPropertyMetadata(RibbonToolSizingMode.ImageAndTextNormal));
        }

        #region Common Tool Properties

        #region Id

        /// <summary>
        /// Identifies the Id dependency property.
        /// </summary>
        /// <seealso cref="Id"/>
        public static readonly DependencyProperty IdProperty = RibbonToolHelper.IdProperty.AddOwner(typeof(CustomButtonTool));

        /// <summary>
        /// Returns/sets the Id associated with the tool.
        /// </summary>
        /// <seealso cref="IdProperty"/>
        [System.ComponentModel.Bindable(true)]
        public string Id
        {
            get
            {
                return (string)this.GetValue(CustomButtonTool.IdProperty);
            }
            set
            {
                this.SetValue(CustomButtonTool.IdProperty, value);
            }
        }

        #endregion //Id

        #endregion //Common Tool Properties

        #region IRibbonTool Members

        public Infragistics.Windows.Ribbon.Internal.RibbonToolProxy ToolProxy
        {
            get
            {
                //throw new Exception("The method or operation is not implemented.");
                return new ButtonToolProxy();
            }
        }

        #endregion
    }
}