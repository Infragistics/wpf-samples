using Infragistics.Controls.Charts;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGDataChart.Controls
{
    /// <summary>
    /// Interaction logic for AnnotationDialog.xaml
    /// </summary>
    public partial class AnnotationDialog : Window
    {
        public bool ShouldCommit { get; set; }
        public UserAnnotationInformation AnnotationInformation { get; internal set; }


        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

            string htmlContent = _monacoHTML;
            webView.CoreWebView2.NavigateToString(htmlContent);
        }


        private string _monacoHTML = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ margin: 0; display: flex; height: 100vh; }}
        #editor {{ width: 50%; height: 100%; }}
        #preview {{ width: 50%; padding: 10px; overflow-y: auto; }}
    </style>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.6/require.min.js""></script>
    <script src=""""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/monaco-editor/0.44.0/min/vs/loader.min.js""></script>
</head>
<body>
    <div id=""editor""></div>
    <div id=""preview""></div>
    <script>
        require.config({{ 
            paths: {{ 
                'vs': 'https://cdnjs.cloudflare.com/ajax/libs/monaco-editor/0.44.0/min/vs',
                'marked': 'https://cdn.jsdelivr.net/npm/marked/marked.min' 
            }} 
        }});
        require(['vs/editor/editor.main', 'marked'], function (_, marked) {{
            const editor = monaco.editor.create(document.getElementById('editor'), {{
                value: '# Hello Markdown\n\nStart editing...',
                language: 'markdown',
                theme: 'vs-dark'
            }});

            const preview = document.getElementById('preview');
            const updatePreview = () => {{
                var markdown = editor.getValue();
                preview.innerHTML = marked.parse(markdown);
                window.chrome.webview.postMessage(markdown);
            }};

            editor.onDidChangeModelContent(updatePreview);
            updatePreview();
            window.addEventListener('resize', () => {{
                requestAnimationFrame(() => {{
                    editor.layout();
                }});
            }});

        }});
    </script>
</body>
</html>
";
        private string _resultText;


        public AnnotationDialog()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string markdownContent = e.TryGetWebMessageAsString();
            Dispatcher.Invoke(() => _resultText = markdownContent);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            ShouldCommit = true;
            AnnotationInformation.Label = LabelTextBox.Text;
            AnnotationInformation.AnnotationData = _resultText;
            AnnotationInformation.MainColor = mainColor.Value;
            AnnotationInformation.BadgeColor = badgeColor.Value;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
