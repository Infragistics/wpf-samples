
using System.Windows;

namespace IgExcel.Views
{
    public partial class InfoView
    {
        public InfoView()
        {
            InitializeComponent();
        }

        #region FileName

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(FileNamePropertyName,
                                                                                                 typeof(string),
                                                                                                 typeof(InfoView),
                                                                                                 new PropertyMetadata(null));


        internal const string FileNamePropertyName = "FileName";

        public string FileName
        {
            get
            {
                return (string)this.GetValue(InfoView.FileNameProperty);
            }
            set
            {
                this.SetValue(InfoView.FileNameProperty, value);
            }
        }

        #endregion //DocumentProperties

        #region DocumentProperties

        public static readonly DependencyProperty DocumentPropertiesProperty = DependencyProperty.Register(DocumentPropertiesPropertyName,
                                                                                                           typeof(object),
                                                                                                           typeof(InfoView),
                                                                                                           new PropertyMetadata(null));

        internal const string DocumentPropertiesPropertyName = "DocumentProperties";

        public object DocumentProperties
        {
            get
            {
                return (object)this.GetValue(InfoView.DocumentPropertiesProperty);
            }
            set
            {
                this.SetValue(InfoView.DocumentPropertiesProperty, value);
            }
        }

        #endregion //DocumentProperties
    }
}
