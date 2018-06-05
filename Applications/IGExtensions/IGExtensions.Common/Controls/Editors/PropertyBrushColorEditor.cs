using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace IGExtensions.Common.Controls
{
	[TemplatePart(Name = "BrushColorPopup", Type = typeof(Popup))]
	[TemplatePart(Name = "BrushColorPreviewBorder", Type = typeof(Border))]
	[TemplatePart(Name = "BrushColorPicker", Type = typeof(ColorPicker))]
	[TemplatePart(Name = "BrushColorPickerContainer", Type = typeof(Border))]
	public class PropertyBrushColorEditor : PropertyBaseEditor  
	{
		protected ColorPicker BrushColorPicker;
		protected Border BrushColorPickerContainer;
		protected Popup BrushColorPopup;
		protected Border BrushColorPreviewBorder;
		protected bool IsTemplateApplied;
		protected bool IsUpdating;
	 
		public PropertyBrushColorEditor()
		{
			DefaultStyleKey = typeof(PropertyBrushColorEditor);
			this.Loaded += OnBrushCollectionEditorLoaded;
		}

		private void OnBrushCollectionEditorLoaded(object sender, RoutedEventArgs e)
		{
			IsLoaded = true;
			//UpdateBrushPreview();
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			BrushColorPreviewBorder = base.GetTemplateChild("BrushColorPreviewBorder") as Border;
			if (BrushColorPreviewBorder != null)
			{
				BrushColorPreviewBorder.MouseLeftButtonUp += OnBrushColorPreviewBorderMouseClick;
			   // BrushColorPicker.MouseLeave += new System.Windows.Input.MouseEventHandler(BrushColorPicker_MouseLeave);
			}
			BrushColorPickerContainer = base.GetTemplateChild("BrushColorPickerContainer") as Border;
			if (BrushColorPickerContainer != null)
			{
				BrushColorPickerContainer.MouseLeave += OnBrushColorPickerContainerMouseLeave;
			} 
			BrushColorPicker = base.GetTemplateChild("BrushColorPicker") as ColorPicker;
			if (BrushColorPicker != null)
			{
				//BrushColorPicker.SelectedBrush = this.BrushColor;
				//BrushColorPicker.MouseLeave += new System.Windows.Input.MouseEventHandler(BrushColorPicker_MouseLeave);
			}
			BrushColorPopup = base.GetTemplateChild("BrushColorPopup") as Popup;
			if (BrushColorPopup != null)
			{

			}
			if (this.BrushColor == null)
			{
				this.BrushColor = new SolidColorBrush(this.PropertyColor);
			}
		}

		private void OnBrushColorPreviewBorderMouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (BrushColorPopup != null)
			{
				this.BrushColorPicker.InitializeSelection(this.BrushColor);
		
				//this.BrushColorPicker.Width = this.ActualWidth - 5;
				//this.BrushColorPicker.Height = this.ActualWidth - 5;
				this.BrushColorPopup.IsOpen = !BrushColorPopup.IsOpen;
			}
		}

		private void OnBrushColorPickerContainerMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (BrushColorPopup != null)
				BrushColorPopup.IsOpen = false;
		}

		#region Properties
		public const string OpacitySliderVisibilityPropertyName = "OpacitySliderVisibility";
		public static readonly DependencyProperty OpacitySliderVisibilityProperty = DependencyProperty.Register(
			OpacitySliderVisibilityPropertyName, typeof(Visibility), typeof(PropertyBrushColorEditor),
			new PropertyMetadata(Visibility.Visible, (sender, e) =>
		{
			(sender as PropertyBrushColorEditor).OnPropertyChanged(new PropertyChangedEventArgs(OpacitySliderVisibilityPropertyName));
		})); 
		/// <summary>
		/// Gets or sets the OpacitySliderVisibility property 
		/// </summary>
		public Visibility OpacitySliderVisibility
		{
			get { return (Visibility)GetValue(OpacitySliderVisibilityProperty); }
			set { SetValue(OpacitySliderVisibilityProperty, value); }
		}
		public const string PropertyColorPropertyName = "PropertyColor";
		public static readonly DependencyProperty PropertyColorProperty = DependencyProperty.Register(
			PropertyColorPropertyName, typeof(Color), typeof(PropertyBrushColorEditor),
			new PropertyMetadata(Colors.Blue, (sender, e) =>
			{
				(sender as PropertyBrushColorEditor).OnPropertyChanged(new PropertyChangedEventArgs(PropertyColorPropertyName));
			}));
		/// <summary>
		/// Gets or sets the PropertyColor property 
		/// </summary>
		public Color PropertyColor
		{
			get { return (Color)GetValue(PropertyColorProperty); }
			set { SetValue(PropertyColorProperty, value); }
		}

		//TODO changed to PropertyBrush
		public const string BrushColorPropertyName = "BrushColor";
		public static readonly DependencyProperty BrushColorProperty = DependencyProperty.Register(
			BrushColorPropertyName, typeof(Brush), typeof(PropertyBrushColorEditor), 
			new PropertyMetadata(null, (sender, e) =>
			{
				(sender as PropertyBrushColorEditor).OnPropertyChanged(new PropertyChangedEventArgs(BrushColorPropertyName));
			}));
		/// <summary>
		/// Gets or sets the BrushColor property 
		/// </summary>
		public SolidColorBrush BrushColor
		{
			get { return (SolidColorBrush)GetValue(BrushColorProperty); }
			set { SetValue(BrushColorProperty, value); }
		}
		#endregion

		public new void OnPropertyChanged(PropertyChangedEventArgs ea)
		{
			base.OnPropertyChanged(ea);

			switch (ea.PropertyName)
			{
				case "BrushColor":
					{
						this.PropertyColor = BrushColor.Color;
						break;
					}
			}
		}
	}
}