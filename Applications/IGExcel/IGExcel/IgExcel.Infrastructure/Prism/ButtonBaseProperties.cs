using System.Windows;
using System.Windows.Controls.Primitives;

namespace IgExcel.Infrastructure.Prism
{
    public class ButtonBaseProperties
    {
        #region InvokeCommandOnLoadProperty

        public static readonly DependencyProperty InvokeCommandOnFirstLoadProperty = DependencyProperty.RegisterAttached("InvokeCommandOnFirstLoad ", typeof(bool), typeof(ButtonBaseProperties), new PropertyMetadata(false, OnInvokeCommandOnFirstLoadChanged));

        public static void SetInvokeCommandOnFirstLoad(ButtonBase btn, bool value)
        {
            btn.SetValue(InvokeCommandOnFirstLoadProperty, value);
        }

        public static bool GetInvokeCommandOnFirstLoad(ButtonBase btn)
        {
            return (bool)btn.GetValue(InvokeCommandOnFirstLoadProperty);
        }

        private static void OnInvokeCommandOnFirstLoadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase btn = d as ButtonBase;

            if (btn != null)
            {
                if ((bool)e.NewValue == true)
                {
                    btn.Loaded += btn_Loaded;
                }
                else
                {
                    btn.Loaded -= btn_Loaded;
                }

            }
        }

        private static void btn_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonBase b = sender as ButtonBase;
            b.Loaded -= btn_Loaded;

            if (b.Command != null)
                b.Command.Execute(b.CommandParameter);
        }

        #endregion //InvokeCommandOnLoadProperty
    }
}
