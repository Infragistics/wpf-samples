using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGGantt.Resources;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Microsoft.Win32;

namespace IGGantt.Samples.Data
{
    public partial class SavingProjectXMLFile : SampleContainer
    {
        private XmlDataProvider _dataProvider = new XmlDataProvider();
        private bool _pageInitialized = false;

        public SavingProjectXMLFile()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (!_pageInitialized)
            {
                Cmb_XMLFiles.Items.Add(GanttStrings.ChooseFile);
                Cmb_XMLFiles.Items.Add(GanttStrings.ProjectFile1);
                Cmb_XMLFiles.Items.Add(GanttStrings.ProjectFile2);
                Cmb_XMLFiles.SelectedIndex = 0;
                _pageInitialized = true;
                _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            }
        }

        private void Cmb_XMLFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_XMLFiles.SelectedIndex != 0)
            {
                string selectedValue = Cmb_XMLFiles.SelectedValue.ToString();
                _dataProvider.GetXmlDataAsync("MSProjectFiles/" + selectedValue);
            }
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

            using (var stream = new MemoryStream(byteArray))
            {
                stream.Position = 0;
                LoadProjectFromStream(stream);
                stream.Close();
            }
            this.Btn_SaveXMLFile.IsEnabled = true;
        }

        private void LoadProjectFromStream(Stream stream)
        {
            // Create a xamGantt Project and load project data from a Stream
            var project = new Project();
            project.LoadFromProjectXml(stream);
            this.gantt.VisibleDateRange = new DateRange(project.Start, project.Finish);
            this.gantt.Project = project;
        }

        private void Btn_SaveXMLFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = GanttStrings.OpenFileDialogFilter;
            dialog.Title = GanttStrings.OpenFileDialogTitle;
            bool? isSaved = dialog.ShowDialog();
            if (isSaved == true)
            {
                try
                {
                    using (Stream stream = dialog.OpenFile())
                    {
                        SaveProjectToStream(stream);
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GanttStrings.Error + " " + ex.Message);
                }
            }
            Cmb_XMLFiles.SelectedIndex = 0;
        }

        private void SaveProjectToStream(Stream stream)
        {
            // obtain the xamGantt Project
            Project project = this.gantt.Project;
            project.SaveAsProjectXml(stream);
        }

    }
}
