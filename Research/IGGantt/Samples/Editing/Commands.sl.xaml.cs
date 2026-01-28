using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGGantt.Resources;
using Infragistics;
using Infragistics.Controls.Menus;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGGantt.Samples.Editing
{
    public partial class Commands : SampleContainer
    {
        private XmlDataProvider _dataProvider = new XmlDataProvider();

        public Commands()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(GanttStrings.Error + " " + e.Error.Message);
                return;
            }

            XDocument document = e.Result;

            string str = document.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(str);

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                stream.Position = 0;
                LoadProjectFromStream(stream);
                stream.Close();
            }
        }

        private void LoadProjectFromStream(Stream stream)
        {
            // Create a xamGantt Project and load project data from a Stream
            Project project = new Project();
            project.LoadFromProjectXml(stream);
            this.xamgantt.VisibleDateRange = new DateRange(project.Start, project.Finish);
            this.xamgantt.Project = project;
        }

        private void btn_Open_Click(object sender, RibbonToolEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = GanttStrings.OpenFileDialogFilter;

            bool? isOpened = dialog.ShowDialog();
            if (isOpened == true)
            {
                try
                {
                    using (Stream stream = dialog.File.OpenRead())
                    {
                        LoadProjectFromStream(stream);
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GanttStrings.Error + " " + ex.Message);
                }
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton btnLink = sender as HyperlinkButton;
            string selectedBtnTag = btnLink.Tag.ToString();

            _dataProvider.GetXmlDataAsync("MSProjectFiles/" + selectedBtnTag);
        }

        private void CheckBoxTool_Checked(object sender, RibbonToolEventArgs e)
        {
            ProjectSettings settings = new ProjectSettings();
            settings.ShouldCalculateAfterEachEdit = false;

            this.xamgantt.Project.Settings = settings;
        }

        private void CheckBoxTool_Unchecked(object sender, RibbonToolEventArgs e)
        {
            ProjectSettings settings = new ProjectSettings();
            settings.ShouldCalculateAfterEachEdit = true;

            this.xamgantt.Project.Settings = settings;
        }
    }
}
