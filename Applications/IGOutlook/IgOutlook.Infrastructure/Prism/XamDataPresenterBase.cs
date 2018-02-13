using Infragistics.Windows.Controls;
using Infragistics.Windows.DataPresenter;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataPresenterBase
    {
        #region SortDirectionProperty

        public static readonly DependencyProperty SortDirectionProperty = DependencyProperty.RegisterAttached("SortDirection", typeof(ListSortDirection), typeof(XamDataPresenterBase), new PropertyMetadata(ListSortDirection.Ascending, OnSortDirectionChanged));

        public static void SetSortDirection(DataPresenterBase presenter, object value)
        {
            presenter.SetValue(GroupByFieldNameProperty, value);
        }

        public static string GetSortDirectione(DataPresenterBase presenter)
        {
            return presenter.GetValue(GroupByFieldNameProperty) as string;
        }

        private static void OnSortDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPresenterBase presenter = (DataPresenterBase)d;
            ListSortDirection sortDirection = (ListSortDirection)e.NewValue;
            string fieldName = (string)presenter.GetValue(XamDataPresenterBase.GroupByFieldNameProperty);

            //Sort direction was set before the layout was initialized
            if (presenter.FieldLayouts.Count < 1)
            {
                presenter.FieldLayoutInitialized += ApplySortDirection_On_FieldLayoutInitialized;
                return;
            }

            if (presenter.FieldLayouts[0].SortedFields.Count < 1) return;

            presenter.FieldLayouts[0].SortedFields.Clear();
            presenter.FieldLayouts[0].SortedFields.Add(new FieldSortDescription(fieldName, sortDirection, true));
        }

        static void ApplySortDirection_On_FieldLayoutInitialized(object sender, Infragistics.Windows.DataPresenter.Events.FieldLayoutInitializedEventArgs e)
        {
            DataPresenterBase presenter = (DataPresenterBase)sender;
            presenter.FieldLayoutInitialized -= ApplySortDirection_On_FieldLayoutInitialized;

            if (presenter.FieldLayouts.Count < 1) return;

            if (presenter.FieldLayouts[0].SortedFields.Count < 1) return;

            presenter.FieldLayouts[0].SortedFields.Clear();

            string fieldName = (string)presenter.GetValue(XamDataPresenterBase.GroupByFieldNameProperty);
            ListSortDirection sortDirection = (ListSortDirection)presenter.GetValue(XamDataPresenterBase.SortDirectionProperty);

            presenter.FieldLayouts[0].SortedFields.Add(new FieldSortDescription(fieldName, sortDirection, true));
        }

        #endregion //SortDirectionProperty

        #region GroupByFieldNameProperty

        public static readonly DependencyProperty GroupByFieldNameProperty = DependencyProperty.RegisterAttached("GroupByFieldName", typeof(string), typeof(XamDataPresenterBase), new PropertyMetadata(null, OnGroupByFieldNameChanged));

        public static void SetGroupByFieldName(DataPresenterBase presenter, object value)
        {
            presenter.SetValue(GroupByFieldNameProperty, value);
        }

        public static string GetGroupByFieldName(DataPresenterBase presenter)
        {
            return presenter.GetValue(GroupByFieldNameProperty) as string;
        }

        private static void OnGroupByFieldNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPresenterBase presenter = (DataPresenterBase)d;
            string fieldName = (string)e.NewValue;
            ListSortDirection sortDirection = (ListSortDirection)presenter.GetValue(XamDataPresenterBase.SortDirectionProperty);


            if (presenter.FieldLayouts.Count < 1) return;

            presenter.FieldLayouts[0].SortedFields.Clear();

            presenter.FieldLayouts[0].SortedFields.Add(new FieldSortDescription(fieldName, sortDirection, true));
        }

        #endregion //GroupByFieldNameProperty

        #region FilterValueProperty

        public static readonly DependencyProperty FilterValueProperty = DependencyProperty.RegisterAttached("FilterValue", typeof(string), typeof(XamDataPresenterBase), new PropertyMetadata(null, OnFilterValueChanged));

        public static void SetFilterValue(DataPresenterBase presenter, object value)
        {
            presenter.SetValue(FilterValueProperty, value);
        }

        public static string GetFilterValue(DataPresenterBase presenter)
        {
            return presenter.GetValue(FilterValueProperty) as string;
        }

        private static void OnFilterValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPresenterBase presenter = (DataPresenterBase)d;
            string filterValue = (string)e.NewValue;
            string[] filterByFields = ReadCsvs((string)presenter.GetValue(XamDataPresenterBase.FilterByFieldsProperty));

            //If the field layout is not initialized or the FilterByFields property is not set return.
            if (presenter.FieldLayouts.Count <= 0 || filterByFields.Length == 0) return;

            //Clear any exisitng filters 
            presenter.FieldLayouts[0].RecordFilters.Clear();

            //If the filter is empty return
            if (string.IsNullOrEmpty(filterValue)) return;

            //Make sure the filter action is Hide
            presenter.FieldLayoutSettings.FilterAction = RecordFilterAction.Default;
            presenter.FieldLayoutSettings.RecordFiltersLogicalOperator = LogicalOperator.Or;

            //Create the filters
            foreach (var fieldName in filterByFields)
            {
                ComparisonCondition condition = new ComparisonCondition();
                condition.Operator = ComparisonOperator.Contains;
                condition.Value = filterValue;

                RecordFilter rFilter = new RecordFilter();
                rFilter.FieldName = fieldName;

                rFilter.Conditions.Add(condition);

                presenter.FieldLayouts[0].RecordFilters.Add(rFilter);
            }

        }

        #endregion //FilterValueProperty

        #region FilterByFieldsProperty

        public static readonly DependencyProperty FilterByFieldsProperty = DependencyProperty.RegisterAttached("FilterByFields", typeof(string), typeof(XamDataPresenterBase));

        public static void SetFilterByFields(DataPresenterBase presenter, object value)
        {
            presenter.SetValue(FilterByFieldsProperty, value);
        }

        public static string GetFilterByFields(DataPresenterBase presenter)
        {
            return presenter.GetValue(FilterByFieldsProperty) as string;
        }

        #endregion //FilterByFieldsProperty

        #region DoubleClickCommandProperty

        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(XamDataPresenterBase), new PropertyMetadata(null, OnDoubleClickCommandChanged));
        public static void SetDoubleClickCommand(DataPresenterBase grid, ICommand command)
        {
            grid.SetValue(DoubleClickCommandProperty, command);
        }
        public static ICommand GetDoubleClickCommand(DataPresenterBase grid)
        {
            return grid.GetValue(DoubleClickCommandProperty) as ICommand;
        }

        private static void OnDoubleClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPresenterBase presenter = (DataPresenterBase)d;

            presenter.MouseDoubleClick += (o, ea) =>
            {
                ICommand command = (ICommand)presenter.GetValue(DoubleClickCommandProperty);
                object commandParameter = presenter.GetValue(DoubleClickCommandParameterProperty);

                if (command.CanExecute(commandParameter))
                    command.Execute(commandParameter);
            };
        }

        #endregion //DoubleClickCommandProperty

        #region DoubleClickCommandParameterProperty

        public static readonly DependencyProperty DoubleClickCommandParameterProperty = DependencyProperty.RegisterAttached("DoubleClickCommandParameter", typeof(object), typeof(XamDataPresenterBase));

        public static void SetDoubleClickCommandParameter(DataPresenterBase grid, object parameter)
        {
            grid.SetValue(DoubleClickCommandParameterProperty, parameter);
        }

        public static object GetDoubleClickCommandParameter(DataPresenterBase grid)
        {
            return grid.GetValue(DoubleClickCommandParameterProperty);
        }

        #endregion //DoubleClickCommandParameterProperty

        #region Private Methods

        private static string[] ReadCsvs(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new string[0];

            return input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
        }

        #endregion //Private Methods
    }
}
