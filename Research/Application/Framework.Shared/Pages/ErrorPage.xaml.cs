using System; 
using System.Diagnostics; 

using Xamarin.Forms;

namespace Infragistics.Framework
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            Debug.WriteLine("creating error page...");
            InitializeComponent();
        }
    }
}
