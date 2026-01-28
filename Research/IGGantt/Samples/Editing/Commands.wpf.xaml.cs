using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using IGGantt.Resources;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Windows.Ribbon;
using Microsoft.Win32;

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

        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = GanttStrings.OpenFileDialogFilter;
            dialog.Title = GanttStrings.OpenFileDialogTitle;

            bool? isOpened = dialog.ShowDialog();
            if (isOpened == true)
            {
                try
                {
                    using (Stream stream = dialog.OpenFile())
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

        private void LoadProjectFromStream(Stream stream)
        {
            // Create a xamGantt Project and load project data from a Stream
            Project project = new Project();
            project.LoadFromProjectXml(stream);
            this.xamgantt.VisibleDateRange = new DateRange(project.Start, project.Finish);
            this.xamgantt.Project = project;
        }

        private void ButtonTool_Click(object sender, RoutedEventArgs e)
        {
            ButtonTool btnTool = sender as ButtonTool;
            string selectedBtnTag = btnTool.Tag.ToString();

            _dataProvider.GetXmlDataAsync("MSProjectFiles/" + selectedBtnTag);
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

        private void CheckBoxTool_Checked(object sender, RoutedEventArgs e)
        {
            ProjectSettings settings = new ProjectSettings();
            settings.ShouldCalculateAfterEachEdit = false;

            this.xamgantt.Project.Settings = settings;
        }

        private void CheckBoxTool_Unchecked(object sender, RoutedEventArgs e)
        {
            ProjectSettings settings = new ProjectSettings();
            settings.ShouldCalculateAfterEachEdit = true;

            this.xamgantt.Project.Settings = settings;
        }
    }   
}
