using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using Infragistics.Windows.Ribbon;

namespace IGRibbon.Samples.Data
{
    public partial class StateSerialization : Infragistics.Samples.Framework.SampleContainer
    {
        public StateSerialization()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StateSerialization_Loaded);
            this.Unloaded += new RoutedEventHandler(StateSerialization_Unloaded);
        }

        void StateSerialization_Loaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile isoLastState = IsolatedStorageFile.GetStore(
            IsolatedStorageScope.User |
            IsolatedStorageScope.Domain |
            IsolatedStorageScope.Assembly,
            null,
            null
            );

            StreamReader stateReader = new StreamReader(new IsolatedStorageFileStream("MyLastState.txt", FileMode.OpenOrCreate, isoLastState));
            // Open the isolated storage from the previous instance
            // and read each line of product data
            if (stateReader != null)
            {
                while (!stateReader.EndOfStream)
                {
                    string productLine = stateReader.ReadLine();
                    if (!String.IsNullOrEmpty(productLine))
                    {
                        string[] tokens = productLine.Split('|');
                        if (tokens != null)
                        {
                            if (tokens.Length != 2)
                            {
                                throw new ApplicationException("Malformed product string.");
                            }
                            else
                            {
                                xamRibbon.IsMinimized = System.Convert.ToBoolean(tokens[0]);
                                xamRibbon.QuickAccessToolbarLocation = (Infragistics.Windows.Ribbon.QuickAccessToolbarLocation)Enum.Parse(
                                    typeof(Infragistics.Windows.Ribbon.QuickAccessToolbarLocation), tokens[1]);
                            }
                        }
                    }
                    string qatItems = stateReader.ReadLine();
                    if (!String.IsNullOrEmpty(qatItems))
                    {
                        string[] tokens = qatItems.Split('|');
                        if (tokens != null)
                        {
                            getItemForQAT(tokens);
                        }
                    }

                }
            }
            stateReader.Close();
        }

        void StateSerialization_Unloaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile isoLastState = IsolatedStorageFile.GetStore(
           IsolatedStorageScope.User |
           IsolatedStorageScope.Domain |
           IsolatedStorageScope.Assembly,
           null,
           null
           );
            StreamWriter stateWriter = new StreamWriter(
            new IsolatedStorageFileStream(
            "MyLastState.txt",
            FileMode.Create,
            isoLastState
            )
            );

            // Write out a line for QAT position. System.Convert.ToInt32(xamRibbon.IsMinimized)
            stateWriter.WriteLine(
            String.Format("{0}|{1}", xamRibbon.IsMinimized, (Infragistics.Windows.Ribbon.QuickAccessToolbarLocation)xamRibbon.QuickAccessToolbarLocation));
            // Write out a line for QAT items.
            stateWriter.WriteLine(String.Format("{0}", getItemFromQAT()));
            stateWriter.Close();
        }

        #region QAT
        private string getItemFromQAT()
        {
            string result = String.Empty;
            foreach (QatPlaceholderTool QPhTool in xamRibbon.QuickAccessToolbar.Items)
            {
                if (QPhTool.TargetType == QatPlaceholderToolType.Tool)
                {
                    result += QPhTool.TargetId + ":1" + "|";
                }
                else if (QPhTool.TargetType == QatPlaceholderToolType.RibbonGroup)
                {
                    result += QPhTool.TargetId + ":0" + "|";
                }
            }
            if (result.Length > 0) result = result.Substring(0, result.Length - 1);
            return result;
        }


        private void getItemForQAT(string[] tokens)
        {
            string result = String.Empty;
            foreach (string token in tokens)
            {
                string[] tokens_ = token.Split(':');
                if (tokens_[1].Equals("0") == true)
                {
                    restoreQATGroups(tokens_[0]);
                }
                else if (tokens_[1].Equals("1") == true)
                {
                    restoreQATTools(tokens_[0]);
                }
            }
        }

        private void restoreQATGroups(string token)
        {
            foreach (RibbonTabItem ribTabItem in xamRibbon.Tabs)
            {
                foreach (RibbonGroup ribGroup in ribTabItem.RibbonGroups)
                {

                    if (ribGroup.Id.Equals(token))
                    {
                        if (isExistInQAT(token) == false)
                        {
                            QatPlaceholderTool mQATPhTool = new QatPlaceholderTool(ribGroup.Id, QatPlaceholderToolType.RibbonGroup);
                            xamRibbon.QuickAccessToolbar.Items.Add(mQATPhTool);
                        }
                        break;
                    }
                }
            }
        }

        private void restoreQATTools(string token)
        {
            string pId = null;
            foreach (RibbonTabItem ribTabItem in xamRibbon.Tabs)
            {
                foreach (RibbonGroup ribGroup in ribTabItem.RibbonGroups)
                {
                    for (int i = ribGroup.Items.Count - 1; i >= 0; i--)
                    {
                        object ribTool = ribGroup.Items[i] as object;
                        pId = getToolId(ribTool);
                        string[] items = pId.Split('|');
                        foreach (string item in items)
                        {
                            if (item.Equals(token))
                            {
                                if (isExistInQAT(item) == false)
                                {
                                    QatPlaceholderTool mQATPhTool = new QatPlaceholderTool(item, QatPlaceholderToolType.Tool);
                                    xamRibbon.QuickAccessToolbar.Items.Add(mQATPhTool);
                                }
                                return;
                            }
                        }

                    }
                }

            }
        }

        private string getToolId(object pTool)
        {
            string result = String.Empty;
            if (pTool is IRibbonTool)
            {

                if (pTool is ButtonTool)
                {
                    ButtonTool tmpTool = pTool as ButtonTool;
                    result += tmpTool.Id + "|";
                }
                if (pTool is ToggleButtonTool)
                {
                    ToggleButtonTool tmpTool = pTool as ToggleButtonTool;
                    result += tmpTool.Id + "|";
                }
                if (pTool is RadioButtonTool)
                {
                    RadioButtonTool tmpTool = pTool as RadioButtonTool;
                    result += tmpTool.Id + "|";
                }
                if (pTool is CheckBoxTool)
                {
                    CheckBoxTool tmpTool = pTool as CheckBoxTool;
                    result += tmpTool.Id + "|";
                }

                if (pTool is TextEditorTool)
                {
                    TextEditorTool tmpTool = pTool as TextEditorTool;
                    result += tmpTool.Id + "|";
                }

                if (pTool is MaskedEditorTool)
                {
                    MaskedEditorTool tmpTool = pTool as MaskedEditorTool;
                    result += tmpTool.Id + "|";
                }

                if (pTool is ComboEditorTool)
                {
                    ComboEditorTool tmpTool = pTool as ComboEditorTool;
                    result += tmpTool.Id + "|";
                }

                if (pTool is MenuTool)
                {
                    MenuTool tmpTool = pTool as MenuTool;
                    result += tmpTool.Id + "|";
                }
            }
            else if (pTool is ToolHorizontalWrapPanel)
            {
                ToolHorizontalWrapPanel hwPanel = pTool as ToolHorizontalWrapPanel;
                foreach (UIElement child in hwPanel.Children)
                {
                    string result_ = getToolId(child);
                    if (result_.Equals("") == false)
                    {
                        result += result_ + "|";
                    }
                }
            }
            else if (pTool is ToolVerticalWrapPanel)
            {
                ToolVerticalWrapPanel vwPanel = pTool as ToolVerticalWrapPanel;
                foreach (UIElement child in vwPanel.Children)
                {
                    string result_ = getToolId(child);
                    if (result_.Equals("") == false)
                    {
                        result += result_ + "|";
                    }
                }
            }
            else if (pTool is ButtonGroup)
            {
                ButtonGroup btnGroup = pTool as ButtonGroup;
                foreach (UIElement child in btnGroup.Children)
                {
                    string result_ = getToolId(child);
                    if (result_.Equals("") == false)
                    {
                        result += result_ + "|";
                    }
                }
            }
            if (result.Length > 0) result = result.Substring(0, result.Length - 1);
            return result;
        }

        private bool isExistInQAT(string item)
        {
            foreach (QatPlaceholderTool QPhTool in xamRibbon.QuickAccessToolbar.Items)
            {
                if (QPhTool.TargetId.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
