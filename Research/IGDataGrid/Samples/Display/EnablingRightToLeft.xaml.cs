using System.Collections;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for EnablingRightToLeft.xaml
    /// </summary>
    public partial class EnablingRightToLeft : SampleContainer
    {
        public EnablingRightToLeft()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.DataSource = GridData();
        }

        ArrayList GridData()
        {
            ArrayList _gridData = new ArrayList();

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
