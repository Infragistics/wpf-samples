using System.ComponentModel;

namespace IGPropertyGrid.Samples.DataModel
{
    public enum Genders
    {
        M, F
    }

    public enum Departments
    {
        Accounting, Engineering, Testing, Documentation, Infrastructure
    }

    public class Employee
    {
        protected int _level = 1;

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual Genders Gender { get; set; }

        public virtual string Company { get; set; }
        public virtual Departments Department { get; set; }
        public virtual int Level { get; set; }
    }

    // Summary:
    //     This class is defining its reset value functionality using attributes.
    //
    public class EmployeeEN : Employee
    {
        [Category("Names")]
        [DisplayName("First Name")]
        [Description("This is the person's first name.")]
        [DefaultValue("default-first-name-attribute")]
        public override string FirstName { get; set; }

        [Category("Names")]
        [DisplayName("Last Name")]
        [Description("This is the person's last name.")]
        [DefaultValue("default-last-name-attribute")]
        public override string LastName { get; set; }

        [Category("Bio")]
        [DisplayName("Person Age")]
        [Description("This is the person's age.")]
        public override int Age { get; set; }

        [Category("Bio")]
        [DisplayName("Gender")]
        [Description("This is the person's gender.")]
        [ReadOnly(true)]
        public override Genders Gender { get; set; }

        [Category("Business")]
        [DisplayName("Company")]
        [Description("This is the company where the person works.")]
        public override string Company { get; set; }

        [Category("Business")]
        [DisplayName("Department")]
        [Description("This is the department in which the person works.")]
        public override Departments Department { get; set; }

        [Category("Business")]
        [DisplayName("Level")]
        [Description("This is the person's experience level.")]
        public override int Level
        {
            get { return this._level; }
            set { this._level = value; }
        }
    }

    // Summary:
    //     This class is defining its reset value functionality using attributes.
    //
    public class EmployeeJA : Employee
    {
        [Category("名前")]
        [DisplayName("名前")]
        [Description("名前。")]
        [DefaultValue("default-first-name-attribute")]
        public override string FirstName { get; set; }

        [Category("名前")]
        [DisplayName("名字")]
        [Description("名字。")]
        [DefaultValue("default-last-name-attribute")]
        public override string LastName { get; set; }

        [Category("情報")]
        [DisplayName("年齢")]
        [Description("年齢。")]
        public override int Age { get; set; }

        [Category("情報")]
        [DisplayName("性別")]
        [Description("性別。")]
        [ReadOnly(true)]
        public override Genders Gender { get; set; }

        [Category("ビジネス")]
        [DisplayName("会社")]
        [Description("勤務先。")]
        public override string Company { get; set; }

        [Category("ビジネス")]
        [DisplayName("部門")]
        [Description("所属部門。")]
        public override Departments Department { get; set; }

        [Category("ビジネス")]
        [DisplayName("レベル")]
        [Description("経験レベル。")]
        public override int Level
        {
            get { return this._level; }
            set { this._level = value; }
        }
    }

    // Summary:
    //     This class is defining its reset value functionality using methods.
    // Remarks:
    //     The class shoult implement the INotifyPropertyChanged interface and also
    //     should have defined a special methods for each property which should support resetting.
    //
    //     The "Reset[property name]" method will be invoked by the xamPropertyGrid to reset the value.
    //     This method should also fire the "PropertyChange" event.
    //
    //     The "ShouldSerialize[property name]" method is used by the xamPropertyGrid to property draw
    //     the reset glyph. This method should return true when the property does not have its default
    //     value currently set.
    //
    public class EmployeeMethodsEN : Employee, INotifyPropertyChanged
    {
        [Category("Names")]
        [DisplayName("First Name")]
        [Description("This is the person's first name.")]
        public override string FirstName { get; set; }
        private string _firstNameDefaultMethod = "default-first-name-method";

        public void ResetFirstName()
        {
            this.FirstName = _firstNameDefaultMethod;
            this.PropertyChange("FirstName");
        }

        public bool ShouldSerializeFirstName()
        {
            return !this.FirstName.Equals(_firstNameDefaultMethod); ;
        }



        [Category("Names")]
        [DisplayName("Last Name")]
        [Description("This is the person's last name.")]
        public override string LastName { get; set; }
        private string _lastNameDefaultMethod = "default-last-name-method";

        public void ResetLastName()
        {
            this.LastName = _lastNameDefaultMethod;
            this.PropertyChange("LastName");
        }

        public bool ShouldSerializeLastName()
        {
            return !this.LastName.Equals(_lastNameDefaultMethod);
        }



        [Category("Bio")]
        [DisplayName("Person Age")]
        [Description("This is the person's age.")]
        public override int Age { get; set; }

        [Category("Bio")]
        [Description("This is the person's gender.")]
        [ReadOnly(true)]
        public override Genders Gender { get; set; }



        [Category("Business")]
        [DisplayName("Company")]
        [Description("This is the company where the person works.")]
        public override string Company { get; set; }

        [Category("Business")]
        [DisplayName("Department")]
        [Description("This is the department in which the person works.")]
        public override Departments Department { get; set; }

        [Category("Business")]
        [DisplayName("Level")]
        [Description("This is the person's experience level.")]
        public override int Level
        {
            get { return this._level; }
            set { this._level = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void PropertyChange(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    // Summary:
    //     This class is defining its reset value functionality using methods.
    // Remarks:
    //     The class shoult implement the INotifyPropertyChanged interface and also
    //     should have defined a special methods for each property which should support resetting.
    //
    //     The "Reset[property name]" method will be invoked by the xamPropertyGrid to reset the value.
    //     This method should also fire the "PropertyChange" event.
    //
    //     The "ShouldSerialize[property name]" method is used by the xamPropertyGrid to property draw
    //     the reset glyph. This method should return true when the property does not have its default
    //     value currently set.
    //
    public class EmployeeMethodsJA : Employee, INotifyPropertyChanged
    {
        [Category("名前")]
        [DisplayName("名前")]
        [Description("名前。")]
        public override string FirstName { get; set; }
        private string _firstNameDefaultMethod = "default-first-name-method";

        public void ResetFirstName()
        {
            this.FirstName = _firstNameDefaultMethod;
            this.PropertyChange("FirstName");
        }

        public bool ShouldSerializeFirstName()
        {
            return !this.FirstName.Equals(_firstNameDefaultMethod); ;
        }



        [Category("名前")]
        [DisplayName("名字")]
        [Description("名字。")]
        public override string LastName { get; set; }
        private string _lastNameDefaultMethod = "default-last-name-method";

        public void ResetLastName()
        {
            this.LastName = _lastNameDefaultMethod;
            this.PropertyChange("LastName");
        }

        public bool ShouldSerializeLastName()
        {
            return !this.LastName.Equals(_lastNameDefaultMethod);
        }



        [Category("情報")]
        [DisplayName("年齢")]
        [Description("年齢。")]
        public override int Age { get; set; }

        [Category("情報")]
        [DisplayName("性別")]
        [Description("性別。")]
        [ReadOnly(true)]
        public override Genders Gender { get; set; }



        [Category("ビジネス")]
        [DisplayName("会社")]
        [Description("勤務先。")]
        public override string Company { get; set; }

        [Category("ビジネス")]
        [DisplayName("部門")]
        [Description("所属部門。")]
        public override Departments Department { get; set; }

        [Category("ビジネス")]
        [DisplayName("レベル")]
        [Description("経験レベル。")]
        public override int Level
        {
            get { return this._level; }
            set { this._level = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void PropertyChange(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
