using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Display
{
    /// <summary>
    /// Interaction logic for LoadingAndSavingLayouts.xaml
    /// </summary>
    public partial class LoadingAndSavingLayouts : SampleContainer
    {
        public LoadingAndSavingLayouts()
        {
            InitializeComponent();
        }

        // ==========================================================
        // Save the current layout
        // ==========================================================
        private void btnSaveLayout_Click(object sender, RoutedEventArgs e)
        {
            // Create a memory stream
            System.IO.MemoryStream stream = new MemoryStream();

            try
            {
                // Tell the XamDockManager to save its current layout to the stream.
                this.dockManager.SaveLayout(stream);

                // Get a StreamReader and read the stream that contains the saved layout into a string variable.
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                string layout = sr.ReadToEnd();

                // Save the layout in our SavedLayouts collection which is bound to the combobox on our form.
                //		Note:	We are using a custom class to hold the layout along with a description.  The 
                //				custom class is what is actually being added to the collection.
                this.SavedLayouts.Add(new LayoutInfo("Layout #" + (this.SavedLayouts.Count + 1).ToString(), layout));
                this.SavedLayoutCombo.SelectedIndex = this.SavedLayoutCombo.Items.Count - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception during Save:" + Environment.NewLine + ex.Message, "SaveLayout Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                stream.Close();
            }
        }

        // ==========================================================
        // Load a previously saved layout
        // ==========================================================
        void cboSavedLayouts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the previously saved layout from the currently selected entry in the combobox.
            ComboBox cb = e.Source as ComboBox;
            if (cb != null && cb.SelectedIndex != -1)
            {
                // Write the saved layout to a MemoryStream
                string layout = this.SavedLayouts[cb.SelectedIndex].LayoutData;
                MemoryStream stream = new MemoryStream();

                try
                {
                    StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
                    sw.Write(layout);
                    sw.Flush();
                    stream.Position = 0;

                    // Tell the XamDockManager to load the layout from the stream.
                    this.dockManager.LoadLayout(stream);
                }
                catch (NotImplementedException)
                {
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        // ==========================================================
        // Member Variables
        // ==========================================================
        private ObservableCollection<LayoutInfo> _savedLayouts;

        // ==========================================================
        // Constructor
        // ==========================================================

        // ==========================================================
        // A collection of saved layouts.
        // ==========================================================
        public ObservableCollection<LayoutInfo> SavedLayouts
        {
            get
            {
                if (this._savedLayouts == null)
                    this._savedLayouts = new ObservableCollection<LayoutInfo>();

                return this._savedLayouts;
            }

        }

        // ==========================================================
        // A class we use to hold layout data and a layout description
        // ==========================================================
        public class LayoutInfo
        {
            private string _layoutDescription;
            private string _layoutData;

            internal LayoutInfo(string layoutDescription, string layoutData)
            {
                this._layoutDescription = layoutDescription;
                this._layoutData = layoutData;
            }

            public override string ToString()
            {
                return this.LayoutDescription;
            }

            internal string LayoutDescription
            {
                get { return this._layoutDescription; }
            }

            internal string LayoutData
            {
                get { return this._layoutData; }
            }
        }
    }
}
