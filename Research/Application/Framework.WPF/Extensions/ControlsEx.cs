 
namespace System.Windows.Controls
{
     
    public static class FrameworkEx
    {
        public static double GetValue(this Slider slider)
        {
            return slider == null ? double.NaN : slider.Value;
        }
        public static bool IsActive(this CheckBox checkBox)
        {
            if (checkBox == null) return false;
            if (checkBox.IsChecked == true) return true;
            //if (!checkBox.IsChecked.Value) return false;
            return false;

        }
    }
}