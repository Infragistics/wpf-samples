using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Samples.Shared.Tools;

namespace Infragistics.Samples.Shared.Controls
{
    /// <summary>
    /// Interaction logic for EnumValuesProvider.xaml
    /// </summary>
    public partial class EnumValuesProvider : UserControl
    {
        private static uint objectCounter = 0;

        private uint objectId;
        private DisplayTemplate displayTemplate;
        private bool isEnumTypeFlagged;
        private object cachedValue;

        #region EnumValueHolder

        public class EnumValueHolder : INotifyPropertyChanged
        {
            private EnumValuesProvider _editor;
            private object _value;
            private string _name;
            private bool _isChecked;

            public EnumValueHolder(EnumValuesProvider editor, object enumValue, string enumName, bool isChecked)
            {
                _editor = editor;
                _value = enumValue;
                _name = enumName;
                _isChecked = isChecked;
            }

            public object Value
            {
                get
                {
                    return _value;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public bool IsChecked
            {
                get
                {
                    return _isChecked;
                }
                set
                {
                    if (_isChecked != value)
                    {
                        this.InternalSetIsChecked(value);
                        _editor.OnHolderCheckedStateChanged(this);
                    }
                }
            }

            internal void InternalSetIsChecked(bool newValue)
            {
                if (_isChecked != newValue)
                {
                    _isChecked = newValue;
                    this.RaisePropertyChanged("IsChecked");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void RaisePropertyChanged(string propName)
            {
                if (null != this.PropertyChanged)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

            public override string ToString()
            {
                return this.Name;
            }
        }

        #endregion // EnumValueHolder

        #region Constructor

        static EnumValuesProvider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumValuesProvider), new FrameworkPropertyMetadata(typeof(EnumValuesProvider)));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EnumValuesProvider()
        {
            this.objectId = objectCounter++;

            InitializeComponent();
        }

        #endregion // Constructor

        #region Properties

        #region Public Properties

        #region EnumType

        /// <summary>
        /// Identifies the <see cref="EnumType"/> dependency property
        /// </summary>
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
                "EnumType",
                typeof(Type),
                typeof(EnumValuesProvider),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnEnumTypeChanged)),
                new ValidateValueCallback(ValidateEnumType)
            );

        /// <summary>
        /// Enum type being edited by this provider.
        /// </summary>
        [Bindable(true)]
        public Type EnumType
        {
            get
            {
                return (Type)this.GetValue(EnumTypeProperty);
            }
            set
            {
                this.SetValue(EnumTypeProperty, value);
            }
        }

        private static bool ValidateEnumType(object objVal)
        {
            Type type = (Type)objVal;

            if (null == type)
                return true;

            if (type.IsEnum)
            {
                // Commented these conditions so the control will accept non flagged enums too.
                // object[] flagsAttributes = type.GetCustomAttributes(typeof(FlagsAttribute), true);
                // if (null != flagsAttributes && flagsAttributes.Length > 0)
                return true;
            }

            return false;
        }

        private static bool ExistEnumValueHolderWithName(string name, Collection<EnumValueHolder> list)
        {
            foreach (EnumValueHolder evh in list)
            {
                if (evh.Name == name) return true;
            }
            return false;
        }

        private static void OnEnumTypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            EnumValuesProvider provider = (EnumValuesProvider)dependencyObject;
            Type enumType = (Type)e.NewValue;

            if (null != enumType)
            {
                Collection<EnumValueHolder> list = new Collection<EnumValueHolder>();
                foreach (object val in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, val);
                    // when there are obsolete enum values it is possible to have diplicates, this check should remove them
                    if (!ExistEnumValueHolderWithName(name, list))
                    {
                        list.Add(new EnumValueHolder(provider, val, name, false));
                    }
                }

                provider.SetValue(EnumValueHoldersProperty, list);
            }
            else
            {
                provider.SetValue(EnumValueHoldersProperty, null);
            }

            // Set the isEnumTypeFlagged field
            if (provider.EnumType != null)
            {
                object[] flagsAttributes = provider.EnumType.GetCustomAttributes(typeof(FlagsAttribute), true);
                provider.isEnumTypeFlagged = (null != flagsAttributes && flagsAttributes.Length > 0);
            }
        }

        /// <summary>
        /// Resets the EnumType property to its default state.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetEnumType()
        {
            this.ClearValue(EnumTypeProperty);
        }

        #endregion // EnumType

        #region EnumValueHolders

        public static readonly DependencyProperty EnumValueHoldersProperty = DependencyProperty.Register(
                "EnumValueHolders",
                typeof(Collection<EnumValueHolder>),
                typeof(EnumValuesProvider),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnEnumValueHoldersChanged))
            );

        /// <summary>
        /// Identifies the Read-Only <see cref="EnumValueHolders"/> dependency property
        /// </summary>
        // public static  DependencyProperty EnumValueHoldersProperty = EnumValueHoldersProperty.DependencyProperty;

        /// <summary>
        /// A collection of holder objects where each holder object represnets a member of enum.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [ReadOnly(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Collection<EnumValueHolder> EnumValueHolders
        {
            get
            {
                return (Collection<EnumValueHolder>)this.GetValue(EnumValueHoldersProperty);
            }
            set
            {
                this.SetValue(EnumValueHoldersProperty, value);
            }
        }

        private static void OnEnumValueHoldersChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            EnumValuesProvider editor = (EnumValuesProvider)dependencyObject;
            Collection<EnumValueHolder> newVal = (Collection<EnumValueHolder>)e.NewValue;
            editor.SyncHoldersWithValue();
        }

        #endregion // EnumValueHolders

        #region Value

        /// <summary>
        /// Identifies the <see cref="Value"/> dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(EnumValuesProvider),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnValueChanged),
                    new CoerceValueCallback(OnCoerceValue)));

        /// <summary>
        /// Current value of the provider. Reflects current checked states of all the flag members.
        /// </summary>

        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get
            {
                return (object)this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            EnumValuesProvider provider = (EnumValuesProvider)dependencyObject;
            object newVal = (object)e.NewValue;
            if (newVal != null && !newVal.Equals(provider.cachedValue))
            {
                // set the EnumType if not set
                if (provider.EnumType == null)
                {
                    provider.EnumType = newVal.GetType();
                }
                provider.cachedValue = newVal;
                provider.SyncHoldersWithValue(newVal);
                provider.SyncSelectedEnumIndexWithValue(newVal);
                provider.OnValueChanged(e);
            }
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        private static object OnCoerceValue(DependencyObject dependencyObject, object valueAsObject)
        {
            EnumValuesProvider editor = (EnumValuesProvider)dependencyObject;
            object val = (object)valueAsObject;

            // Make sure the value assigned is of type EnumType. If not then convert.

            Type enumType = editor.EnumType;
            if (null == enumType || null == val)
                return val;

            return Convert.ChangeType(val, enumType);
        }

        #endregion // Value

        #region DisplayTemplate

        public DisplayTemplate DisplayTemplate
        {
            get
            {
                return this.displayTemplate;
            }
            set
            {
                this.displayTemplate = value;

                if (value == DisplayTemplate.None)
                {
                    this.Style = null;
                }
                else
                {
                    this.Style = (Style)this.Resources["EnumValuesProvider" + displayTemplate.ToString() + "Style"];
                }
            }
        }
        #endregion // DisplayTemplate

        #region SelectedEnumIndex

        public int? SelectedEnumIndex
        {
            get { return (int?)GetValue(SelectedEnumIndexProperty); }
            set { SetValue(SelectedEnumIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEnumIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEnumIndexProperty =
            DependencyProperty.Register("SelectedEnumIndex", typeof(int?),
            typeof(EnumValuesProvider), new UIPropertyMetadata(null,
                new PropertyChangedCallback(OnSelectedEnumIndexChanged)));

        public static void OnSelectedEnumIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            EnumValuesProvider provider = dependencyObject as EnumValuesProvider;

            int? enumTypeCount = null;
            if (provider.EnumType != null)
                enumTypeCount = Enum.GetValues(provider.EnumType).Length;

            if (enumTypeCount != null && !provider.isEnumTypeFlagged && enumTypeCount >= (int)e.NewValue)
            {
                provider.Value = Enum.ToObject(provider.EnumType, e.NewValue);
            }
        }

        #endregion // SelectedEnumIndex

        #endregion // Public Properties

        #endregion // Properties

        #region Methods

        #region Private/Internal Methods

        #region GetValueFromHolders

        private object GetValueFromHolders()
        {
            Collection<EnumValueHolder> holders = this.EnumValueHolders;
            if (null == holders)
                return null;

            if (isEnumTypeFlagged)
            {
                long ret = 0;

                // Combine all the checked holders using 'Or'.
                foreach (EnumValueHolder holder in this.EnumValueHolders)
                {
                    if (holder.IsChecked)
                    {
                        long val = this.ToLong(holder.Value);
                        ret |= val;
                    }
                }
                return Enum.ToObject(this.EnumType, ret);
            }
            else // the enum type is not flagged
            {
                for (int i = 0; i < holders.Count; i++)
                {
                    if (holders[i].IsChecked)
                    {
                        return holders[i].Value;
                    }
                }

                return cachedValue;
            }
        }

        #endregion // GetValueFromHolders

        #region OnHolderCheckedStateChanged

        /// <summary>
        /// Called whenever the checked state of the holder changes. It updates the Value property
        /// accordingly.
        /// </summary>
        /// <param name="holder">Holder whose checked state changed.</param>
        private void OnHolderCheckedStateChanged(EnumValueHolder holder)
        {
            if (isEnumTypeFlagged && null != this.EnumValueHolders)
            {
                // If member with 0 value (None) is checked then that becomes the value
                // and all other holders are unchecked.
                //
                object newVal = null;
                long holderValue = this.ToLong(holder.Value);
                foreach (EnumValueHolder ii in this.EnumValueHolders)
                {
                    long iiVal = this.ToLong(ii.Value);

                    if (holder == ii)
                    {
                        if (0 == holderValue && holder.IsChecked)
                        {
                            newVal = holder.Value;
                            break;
                        }
                    }

                    // If a bit is unchecked, then uncheck All (or any value that intersects with
                    // the unchecked bit).
                    // 
                    if (0 != (holderValue & iiVal) && !holder.IsChecked)
                    {
                        ii.InternalSetIsChecked(false);
                    }
                }

                // Setting Value to 0 will uncheck all the non-zero holders.
                // 
                if (null != newVal)
                    this.Value = newVal;
            }

            if (!isEnumTypeFlagged) // && !holder.IsChecked) // The EnumType is not flagged
            {
                foreach (EnumValueHolder ii in this.EnumValueHolders)
                {
                    if (holder != ii && holder.IsChecked)
                    {
                        ii.IsChecked = false;
                    }
                }
            }

            this.SyncValueWithHolders();
        }

        #endregion // OnHolderCheckedStateChanged

        #region SyncHoldersWithValue

        /// <summary>
        /// Sets the checked state of holders based on the value.
        /// </summary>
        private void SyncHoldersWithValue()
        {
            this.SyncHoldersWithValue(this.Value);
        }

        /// <summary>
        /// Sets the checked state of holders based on the value.
        /// </summary>
        /// <param name="valueObj">Value to sync holders with.</param>
        private void SyncHoldersWithValue(object valueObj)
        {
            Collection<EnumValueHolder> holders = this.EnumValueHolders;
            if (holders == null) return;

            if (isEnumTypeFlagged && null != holders)
            {
                bool isValueNull = null == valueObj || valueObj is DBNull;
                long valueAsLong = !isValueNull ? this.ToLong(valueObj) : 0;

                foreach (EnumValueHolder holder in holders)
                {
                    long val = this.ToLong(holder.Value);

                    bool isChecked;

                    if (isValueNull)
                        // If value is null then all holders should be unchecked.
                        isChecked = false;
                    else if (0 == val)
                        // If value is 0 then only holders with 0 value (like None) should be checked.
                        isChecked = 0 == valueAsLong;
                    else
                        // Otherwise check the holder whose value is part of the specified value.
                        isChecked = val == (val & valueAsLong);

                    holder.InternalSetIsChecked(isChecked);
                }
            }
            else // EnumType is not flagged
            {
                foreach (EnumValueHolder holder in holders)
                {
                    if (holder.Value.Equals(Value))
                    {
                        holder.IsChecked = true;
                    }
                    else
                    {
                        holder.IsChecked = false;
                    }
                }
            }
        }

        #endregion // SyncHoldersWithValue

        #region SyncSelectedEnumIndexWithValue
        /// <summary>
        /// Sets the SelectedEnumIndex based on the value. 
        /// Only applies if the EnumType is not flagged.
        /// </summary>
        /// <param name="newValue">The value of the new Value property that will be synced with the SelectedEnumIndex</param>
        private void SyncSelectedEnumIndexWithValue(object newValue)
        {
            if (!isEnumTypeFlagged && newValue != null)
            {
                this.SetValue(EnumValuesProvider.SelectedEnumIndexProperty, Convert.ToInt32(newValue));
            }
        }

        #endregion // SyncSelectedEnumIndexWithValue


        #region SyncValueWithHolders

        /// <summary>
        /// Sets the value based on the checked states of the holders.
        /// </summary>
        private void SyncValueWithHolders()
        {
            this.SetValue(ValueProperty, this.GetValueFromHolders());
        }

        #endregion // SyncValueWithHolders

        #region ToLong

        private long ToLong(object enumVal)
        {
            return Convert.ToInt64(enumVal);
        }

        #endregion // ToLong

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnumValueHolders == null) return;
            foreach (EnumValueHolder enumHolder in EnumValueHolders)
            {
                enumHolder.IsChecked = false;
            }
            if (e.AddedItems.Count > 0)
            {
                EnumValueHolder addedValue = (EnumValueHolder)(e.AddedItems[0]);
                addedValue.IsChecked = true;
            }
        }

        #endregion // Private/Internal Methods

        private void control_Loaded(object sender, RoutedEventArgs e)
        {
            switch (displayTemplate)
            {
                case DisplayTemplate.RadioButtonList:
                    {
                        // If more than one providers with radioButtonList template
                        // are used in a page, each group should have different GroupName.
                        string groupName = "Group" + this.objectId;

                        foreach (var radioButton in VisualTreeHelperExtended.FindElementsByType<RadioButton>(this))
                            radioButton.GroupName = groupName;

                        break;
                    }
            }

            if (this.SelectedEnumIndex != null)
                ((System.Windows.Controls.Primitives.Selector)sender).SelectedIndex = (int)SelectedEnumIndex;
        }

        #endregion // Methods

        #region Events

        public event DependencyPropertyChangedEventHandler ValueChanged;

        #endregion // Events

        #region Static Methods

        /// <summary>
        /// Returns the values of an enum type.
        /// </summary>
        public static IEnumerable<T> GetEnumValues<T>() where T : new()
        {
            T item = new T();
            return from f in item.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)
                   select (T)f.GetValue(item);
        }
        #endregion // Static Methods
    }

    public enum DisplayTemplate
    {
        CheckBoxList,
        RadioButtonList,
        ComboBox,
        None
    }

    public class FontStretchesList : List<FontStretch>
    {
        public FontStretchesList()
        { 
            Add(FontStretches.Normal);
            Add(FontStretches.Condensed);
            Add(FontStretches.Expanded);
            Add(FontStretches.ExtraCondensed);
            Add(FontStretches.Medium);
            Add(FontStretches.SemiCondensed);
            Add(FontStretches.SemiExpanded);
            Add(FontStretches.UltraCondensed);
            Add(FontStretches.UltraExpanded);
        }
    }

    public class FontStylesList : List<FontStyle>
    {
        public FontStylesList()
        {
            Add(FontStyles.Normal);
            Add(FontStyles.Italic);
            Add(FontStyles.Oblique);
        }
    }

    public class FontWeightsList : List<FontWeight>
    {
        public FontWeightsList()
        {
            Add(FontWeights.Normal);
            Add(FontWeights.Bold);
            Add(FontWeights.Black);
            Add(FontWeights.DemiBold);
            Add(FontWeights.ExtraBlack);
            Add(FontWeights.ExtraBold);
            Add(FontWeights.ExtraLight);
            Add(FontWeights.Heavy);
            Add(FontWeights.Light);
            Add(FontWeights.Medium);
            Add(FontWeights.Regular);
            Add(FontWeights.SemiBold);
            Add(FontWeights.Thin);
            Add(FontWeights.UltraBlack);
            Add(FontWeights.UltraBold);
            Add(FontWeights.UltraLight);
        }
    }

    public class FontNamesList : List<string>
    {
        public FontNamesList()
        {
            Add("Verdana");
            Add("Arial");
            Add("Consolas");
            Add("Calibri");
            Add("Courier New");
            Add("Tahoma");
            Add("Times New Roman");
        }
    }

    public class ColorsList : List<Color>
    {
        public ColorsList()
        {
            Add(Colors.Black);
            Add(Colors.Blue);
            Add(Colors.Brown);
            Add(Colors.Green);
            Add(Colors.Magenta);
            Add(Colors.Orange);
            Add(Colors.Purple);
            Add(Colors.Red);
            Add(Colors.Yellow);
            Add(Colors.White);
            Add(Colors.LightGray);
            Add(Colors.DarkGray);
        }
    }

    public class BrushesList : List<SolidColorBrush>
    {
        public BrushesList()
        {
            foreach (var color in new ColorsList())
            {
                Add(new SolidColorBrush(color));
            } 
        }
    }

    public class VisibilityList : List<Visibility>
    {
        public VisibilityList()
        {
            Add(Visibility.Visible);
            Add(Visibility.Collapsed);
        }
    }

    
}
