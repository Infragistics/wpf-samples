using Infragistics.Controls.Menus;
using Infragistics.Controls.Schedules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDateNavigatorProperties
    {
        #region SelectedDataItem

        public static readonly DependencyProperty SelectedDatesProperty = DependencyProperty.RegisterAttached("SelectedDates", typeof(ObservableCollection<DateTime>), typeof(XamDateNavigatorProperties), new PropertyMetadata(null, OnSelectedDatesChanged));

        public static void SetSelectedDates(XamDateNavigator dateNavigator, object value)
        {
            dateNavigator.SetValue(SelectedDatesProperty, value);
        }

        public static string GetSelectedDates(XamDateNavigator dateNavigator)
        {
            return dateNavigator.GetValue(SelectedDatesProperty) as string;
        }

        private static void OnSelectedDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;

            XamDateNavigator dateNavigator = (XamDateNavigator)d;

            try
            {
                dateNavigator.SelectedDates.Clear();

                var dates = (ObservableCollection<DateTime>)e.NewValue;

                foreach (var date in dates)
                {
                    dateNavigator.SelectedDates.Add(date);
                }

            }
            catch { }

        }

        #endregion //SelectedDataItem

    }
}
