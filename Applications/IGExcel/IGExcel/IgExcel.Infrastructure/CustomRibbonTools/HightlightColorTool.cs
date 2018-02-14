using Infragistics.Windows.Ribbon;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace IgExcel.Infrastructure
{
    [TemplatePart(Name = "PART_ListView", Type = typeof(ListView))]
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    public class HightlightColorTool : Button, IRibbonTool
    {

        private ListView listView;
        private Popup popup;

        #region Constructors

        static HightlightColorTool()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HightlightColorTool), new FrameworkPropertyMetadata(typeof(HightlightColorTool)));

            RibbonGroup.MaximumSizeProperty.OverrideMetadata(typeof(HightlightColorTool), new FrameworkPropertyMetadata(RibbonToolSizingMode.ImageAndTextNormal));
        }

        public HightlightColorTool()
        {

        }

        #endregion //Constructors

        #region OnApplyTemplate

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            listView = (ListView)this.GetTemplateChild("PART_ListView");
            popup = (Popup)this.GetTemplateChild("PART_Popup");

            //Close the popup on selection
            listView.SelectionChanged += (s, a) =>
            {
                popup.IsOpen = false;

                if (Command != null)
                    Command.Execute(CommandParameter);
            };
        }


        #endregion //OnApplyTemplate

        #region Image
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
                  typeof(string), typeof(HightlightColorTool), new PropertyMetadata(null));

        public string Image
        {
            get
            {
                return (string)this.GetValue(HightlightColorTool.ImageProperty);
            }
            set
            {
                this.SetValue(HightlightColorTool.ImageProperty, value);
            }
        }

        #endregion //ImagePath

        #region ItemsSource
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(HightlightColorTool), new PropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)this.GetValue(HightlightColorTool.ItemsSourceProperty);
            }
            set
            {
                this.SetValue(HightlightColorTool.ItemsSourceProperty, value);
            }
        }

        #endregion //ItemsSource

        #region SelectedValue

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(object), typeof(HightlightColorTool), new PropertyMetadata(null));

        public object SelectedValue
        {
            get
            {
                return (object)this.GetValue(HightlightColorTool.SelectedValueProperty);
            }
            set
            {
                this.SetValue(HightlightColorTool.SelectedValueProperty, value);
            }
        }

        #endregion //ItemsSource

        #region Common Tool Properties

        #region Id

        /// <summary>
        /// Identifies the Id dependency property.
        /// </summary>
        /// <seealso cref="Id"/>
        public static readonly DependencyProperty IdProperty = RibbonToolHelper.IdProperty.AddOwner(typeof(HightlightColorTool));

        /// <summary>
        /// Returns/sets the Id associated with the tool.
        /// </summary>
        /// <seealso cref="IdProperty"/>
        [System.ComponentModel.Bindable(true)]
        public string Id
        {
            get
            {
                return (string)this.GetValue(ColorPickerTool.IdProperty);
            }
            set
            {
                this.SetValue(ColorPickerTool.IdProperty, value);
            }
        }

        #endregion //Id

        #endregion //Common Tool Properties

        #region IRibbonTool Members

        public Infragistics.Windows.Ribbon.Internal.RibbonToolProxy ToolProxy
        {
            get
            {
                return new HightlightColorToolProxy();
            }
        }

        #endregion
    }
}
