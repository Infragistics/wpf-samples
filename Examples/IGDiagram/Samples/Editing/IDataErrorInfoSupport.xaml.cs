using IGDiagram.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace IGDiagram.Samples.Editing
{
    public partial class IDataErrorInfoSupport : SampleContainer
    {
        private Type diagramItemDisplayTemplateKey = typeof(TextBlock);
        private Style diagramItemDisplayTemplateStyle;
        private bool isAlreadyLoaded = false;

        public IDataErrorInfoSupport()
        {
            InitializeComponent();
            this.SampleLoaded += IDataErrorInfoSupport_SampleLoaded;
        }

        private void IDataErrorInfoSupport_SampleLoaded(object sender, EventArgs e)
        {
            if(!isAlreadyLoaded)
            { 
                // Move the TextBlock's display template to the application resources
                diagramItemDisplayTemplateStyle = this.Resources[diagramItemDisplayTemplateKey] as Style;
                if (diagramItemDisplayTemplateStyle != null)
                {
                    this.Resources.Remove(diagramItemDisplayTemplateKey);
                    
                    // Remove the TextBlock's display template when unloading the sample
                    if (Application.Current.Resources.Contains(diagramItemDisplayTemplateKey))
                    {
                        Application.Current.Resources.Remove(diagramItemDisplayTemplateKey);
                    }

                    Application.Current.Resources.Add(diagramItemDisplayTemplateKey, diagramItemDisplayTemplateStyle);
                }

                isAlreadyLoaded = true;
            }
        }
    }

    public class PropertyChangedModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Member : PropertyChangedModel, IDataErrorInfo
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private ObservableCollection<Member> _subordinates;
        public ObservableCollection<Member> Subordinates
        {
            get { return _subordinates; }
            set
            {
                _subordinates = value;
                OnPropertyChanged("Subordinates");
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        result = DiagramStrings.Err_NameCannotBeEmpty;
                    }
                }
                return result;
            }
        }
    }

    public class MembersViewModel : PropertyChangedModel
    {
        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                OnPropertyChanged("Members");
            }
        }

        public MembersViewModel()
        {
            XmlDataProvider dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Employees.xml");
        }

        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                XDocument document = e.Result;
                Members = new ObservableCollection<Member>(GetMembers(document.Root));
            }
            else
            {
                GenerateDesignData();
            }
        }

        private IEnumerable<Member> GetMembers(XElement parent)
        {
            return (from employee in parent.Elements("Employees")
                    select new Member
                    {
                        Name = employee.Element("EmpName").Value,
                        Subordinates = new ObservableCollection<Member>(GetMembers(employee))
                    });
        }

        private void GenerateDesignData()
        {
            Member member = new Member()
            {
                Name = "David Green",
            };

            Members = new ObservableCollection<Member>() { member };
        }
    }
}
