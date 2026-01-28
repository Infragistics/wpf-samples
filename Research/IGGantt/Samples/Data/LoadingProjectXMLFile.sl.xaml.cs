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

namespace IGGantt.Samples.Data
{
    public partial class LoadingProjectXMLFile : SampleContainer
    {
        private XmlDataProvider _dataProvider = new XmlDataProvider();

        public LoadingProjectXMLFile()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            Cmb_XMLFiles.Items.Add(GanttStrings.ChooseFile);
            Cmb_XMLFiles.Items.Add(GanttStrings.ProjectFile1);
            Cmb_XMLFiles.Items.Add(GanttStrings.ProjectFile2);

            Cmb_XMLFiles.SelectedIndex = 0;

            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
        }

        private void LoadProjectFromStream(Stream stream)
        {
            // Create a xamGantt Project and load project data from a Stream
            Project project = new Project();
            project.LoadFromProjectXml(stream);
            this.gantt.VisibleDateRange = new DateRange(project.Start, project.Finish);

            this.gantt.Project = project;
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

        private void Btn_LoadXMLFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = GanttStrings.OpenFileDialogFilter;

            bool? isOpened = dialog.ShowDialog();
            if (isOpened == true)
            {
                try
                {
                    using (Stream stream = dialog.File.OpenRead())
                    {
                        LoadProjectFromStream(stream);

                        Txt_FileNameLoaded.Text = GanttStrings.LoadedFile + " " + dialog.File.Name;

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

        private void Cmb_XMLFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_XMLFiles.SelectedIndex != 0)
            {
                string selectedValue = Cmb_XMLFiles.SelectedValue.ToString();
                _dataProvider.GetXmlDataAsync("MSProjectFiles/" + selectedValue);

                Txt_FileNameLoaded.Text = GanttStrings.LoadedFile + " " + selectedValue;
            }
        }
    }
}
