using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IGPropertyGrid.Samples.DataModel
{
    public class EmployeePropertyDescriptorEN : PropertyDescriptor
    {
        private EmployeesCollectionEN collection;
        private EmployeeEN employee;

        public EmployeePropertyDescriptorEN(EmployeesCollectionEN col, int idx)
            : base("#" + idx.ToString(), null)
		{
			this.collection = col;
            this.employee = col[idx];
		} 

        public override string DisplayName
		{
			get 
			{
                return this.employee.FirstName + " " + this.employee.LastName;
			}
		}

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return this.collection.GetType(); }
        }

        public override object GetValue(object component)
        {
            return this.employee;
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return this.employee.GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }

    public class EmployeePropertyDescriptorJA : PropertyDescriptor
    {
        private EmployeesCollectionJA collection;
        private EmployeeJA employee;

        public EmployeePropertyDescriptorJA(EmployeesCollectionJA col, int idx)
            : base("#" + idx.ToString(), null)
        {
            this.collection = col;
            this.employee = col[idx];
        }

        public override string DisplayName
        {
            get
            {
                return this.employee.FirstName + " " + this.employee.LastName;
            }
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return this.collection.GetType(); }
        }

        public override object GetValue(object component)
        {
            return this.employee;
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return this.employee.GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
