using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Infragistics.Framework
{
    public class TestData : ObservableCollection<TestDataItem>
    {
        public TestData()
        {
            Add(new TestDataItem { Label = "Label1", Value = 3 });
            Add(new TestDataItem { Label = "Label2", Value = 1 });
            Add(new TestDataItem { Label = "Label3", Value = 4 });
            Add(new TestDataItem { Label = "Label4", Value = 2 });
            Add(new TestDataItem { Label = "Label5", Value = 7 });
            Add(new TestDataItem { Label = "Label6", Value = -3 });
            Add(new TestDataItem { Label = "Label7", Value = 4 });
            Add(new TestDataItem { Label = "Label8", Value = 1 });
            Add(new TestDataItem { Label = "Label9", Value = 3 });
        }

    }
    public class TestDataItem
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }
        private double? _value;
        public double? Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
