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
    public partial class LoadingProjectXMLFile : SampleContainer
    {
        private XmlDataProvider _dataProvider = new XmlDataProvider();
        private bool _pageInitialized = false;

        public LoadingProjectXMLFile()
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

        private void Btn_LoadXMLFile_Click(object sender, RoutedEventArgs e)
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

                        Lbl_FileNameLoaded.Content = GanttStrings.LoadedFile + " " + dialog.SafeFileName;
                        
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

                Lbl_FileNameLoaded.Content = GanttStrings.LoadedFile + " " + selectedValue;
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
        }

        private void LoadProjectFromStream(Stream stream)
        {
            // Create a xamGantt Project and load project data from a Stream
            var project = new Project();
            project.LoadFromProjectXml(stream);
            this.gantt.VisibleDateRange = new DateRange(project.Start, project.Finish);

            this.gantt.Project = project;
        }
    }
}
