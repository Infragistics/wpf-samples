using System; 
using System.Diagnostics; 

using Xamarin.Forms;

namespace Infragistics.Framework
{
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            Debug.WriteLine("creating test page...");
            InitializeComponent();
        }
    }
}
