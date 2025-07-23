using System.Collections.Generic;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGGrid.Samples.Display
{
    public partial class RightToLeftSupport : SampleContainer
    {
        public RightToLeftSupport()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = GridData();
        }

        public List<object> GridData()
        {
            List<object> _gridData = new List<object>();

            ArabicDataObject ado1 = new ArabicDataObject();
            ado1.Firstname = "حبيب";
            ado1.Lastname = "حميد";
            ado1.Age = "١٢";

            ArabicDataObject ado2 = new ArabicDataObject();
            ado2.Firstname = "ماجد";
            ado2.Lastname = "مشاري";
            ado2.Age = "٢٣";

            ArabicDataObject ado3 = new ArabicDataObject();
            ado3.Firstname = "فرح";
            ado3.Lastname = "كريم";
            ado3.Age = "٣٤";

            _gridData.Add(ado1);
            _gridData.Add(ado2);
            _gridData.Add(ado3);

            return _gridData;
        }

        public class ArabicDataObject
        {
            private string _firstname;

            public string Firstname
            {
                get { return _firstname; }
                set { _firstname = value; }
            }
            private string _lastname;

            public string Lastname
            {
                get { return _lastname; }
                set { _lastname = value; }
            }
            private string _age;

            public string Age
            {
                get { return _age; }
                set { _age = value; }
            }
        }
    }
}



