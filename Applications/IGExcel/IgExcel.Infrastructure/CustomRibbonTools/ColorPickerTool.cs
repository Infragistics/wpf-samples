using Infragistics.Controls.Editors;
using Infragistics.Undo;
using Infragistics.Windows.Ribbon;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IgExcel.Infrastructure
{
    [TemplatePart(Name = "PART_ColorPicker", Type = typeof(XamColorPicker))]
    public class ColorPickerTool : Button, IRibbonTool
    {
        private XamColorPickerExtended colorPicker;
        bool selectionWasMade = false;

        #region Constructors

        static ColorPickerTool()
        {
            RibbonGroup.MaximumSizeProperty.OverrideMetadata(typeof(ColorPickerTool), new FrameworkPropertyMetadata(RibbonToolSizingMode.ImageAndTextNormal));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerTool), new FrameworkPropertyMetadata(typeof(ColorPickerTool)));
        }


        public ColorPickerTool()
        {

        }

        #endregion //Constructors

        #region OnApplyTemplate

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            colorPicker = (XamColorPickerExtended)this.GetTemplateChild("PART_ColorPicker");

            colorPicker.SelectedColorChanged += (s, a) =>
            {
                selectionWasMade = true;
            };

            colorPicker.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "SelectedColorPreview")
                {
                    if (Command != null && colorPicker.IsDropDownOpen)
                        Command.Execute(colorPicker.SelectedColorPreview);
                }
            };

            colorPicker.DropDownOpening += (s, a) =>
            {
                selectionWasMade = false;

                if (this.UndoManager != null)
                {
                    this.UndoManager.StartTransaction("SelectedColorPreviewTra", "SelectedColorPreviewTra");
                }
            };

            colorPicker.DropDownClosing += (s, a) =>
            {
                System.Console.WriteLine("DropDownClosing");

                if (this.UndoManager != null && this.UndoManager.CurrentTransaction != null)
                {
                    this.UndoManager.CurrentTransaction.Rollback();
                }

                if (Command != null && selectionWasMade)
                    Command.Execute(colorPicker.SelectedColor);
            };

        }

        #endregion //OnApplyTemplate

        #region Image
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
                  typeof(string), typeof(ColorPickerTool), new PropertyMetadata(null));

        public string Image
        {
            get
            {
                return (string)this.GetValue(ColorPickerTool.ImageProperty);
            }
            set
            {
                this.SetValue(ColorPickerTool.ImageProperty, value);
            }
        }

        #endregion //ImagePath

        #region SelectedColor
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor",
                  typeof(Color?), typeof(ColorPickerTool), new PropertyMetadata(null));

        public Color? SelectedColor
        {
            get
            {
                return (Color?)this.GetValue(ColorPickerTool.SelectedColorProperty);
            }
            set
            {
                this.SetValue(ColorPickerTool.SelectedColorProperty, value);
            }
        }

        #endregion //ImagePath

        #region UndoManagerProperty
        public static readonly DependencyProperty UndoManagerProperty = DependencyProperty.Register("UndoManager",
                  typeof(UndoManager), typeof(ColorPickerTool), new PropertyMetadata(null));

        public UndoManager UndoManager
        {
            get
            {
                return (UndoManager)this.GetValue(ColorPickerTool.UndoManagerProperty);
            }
            set
            {
                this.SetValue(ColorPickerTool.UndoManagerProperty, value);
            }
        }

        #endregion //UndoManagerProperty

        #region TransparentColorItemText

        public static readonly DependencyProperty TransparentColorItemTextProperty = DependencyProperty.Register("TransparentColorItemText",
                  typeof(string), typeof(ColorPickerTool), new PropertyMetadata(null));

        public string TransparentColorItemText
        {
            get
            {
                return (string)this.GetValue(ColorPickerTool.TransparentColorItemTextProperty);
            }
            set
            {
                this.SetValue(ColorPickerTool.TransparentColorItemTextProperty, value);
            }
        }

        #endregion //TransparentColorItemText

        #region Common Tool Properties

        #region Id

        /// <summary>
        /// Identifies the Id dependency property.
        /// </summary>
        /// <seealso cref="Id"/>
        public static readonly DependencyProperty IdProperty = RibbonToolHelper.IdProperty.AddOwner(typeof(ColorPickerTool));

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
                return new ColorPickerToolProxy();
            }
        }

        #endregion
    }
}
