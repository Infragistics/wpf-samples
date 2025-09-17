using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Infragistics.Samples.Core;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Converters;
using Infragistics.SamplesBrowser.Converters;
using Infragistics.SamplesBrowser.Converters.CSharpColorizer;
using Infragistics.SamplesBrowser.ViewModel;
using System.Reflection;

namespace Infragistics.SamplesBrowser
{
    using System.Windows.Shapes;
    using Path = System.IO.Path;

    /// <summary>
    /// Interaction logic for SamplesBrowserWindow.xaml
    /// </summary>
    public partial class SamplesBrowserWindow : Window
    {
        public const int HtCaption = 0x2;
        public const int WmNclbuttondown = 0xA1;
        private static double _minWidth, _minHeight;

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }


        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #region Fields

        private string _apppath = String.Empty;
        Storyboard sbFadeOutMenuItems, sbFadeInMenuItems;
        CSharpFormat cSharpFormat = new CSharpFormat();
        private const double MENU_ANIMATE_MAXTIME = 20;
        private const double MENU_EXPANDED_SIZE = 256;
        private const double MENU_COLLAPSED_SIZE = 31;
        private const string SAMPLES_SUBDIRECTORY_LOCATION = "..\\";
        private const string SAMPLES_SUBDIRECTORY_LOCATION1 = "Samples";
        private double menuTimer = 0;
        private double menuDelta = 0;
        private double menuPreviousWidth = 0;
        private Thread snippetsThread;
        private delegate void ArgsDelegate0();

        #endregion Fields

        #region Properties

        public static readonly DependencyProperty IsMaximizeButtonVisibleProperty = DependencyProperty.Register(
        "IsMaximizeButtonVisible", typeof(bool), typeof(SamplesBrowserWindow), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsRestoreButtonVisibleProperty = DependencyProperty.Register(
        "IsRestoreButtonVisible", typeof(bool), typeof(SamplesBrowserWindow), new PropertyMetadata(default(bool)));

        public bool IsMaximizeButtonVisible
        {
            get { return (bool)GetValue(IsMaximizeButtonVisibleProperty); }
            set { SetValue(IsMaximizeButtonVisibleProperty, value); }
        }

        public bool IsRestoreButtonVisible
        {
            get { return (bool)GetValue(IsRestoreButtonVisibleProperty); }
            set { SetValue(IsRestoreButtonVisibleProperty, value); }
        }

        #endregion // Properties

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                // Sent to a window when the size or position of the window is about to change. 
                // An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.
                // Handles the case of the window overlapping the taskbar.
                // https://blogs.msdn.microsoft.com/llobo/2006/08/01/maximizing-window-with-windowstylenone-considering-taskbar/
                case 0x0024: // WM_GETMINMAXINFO
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
                case 0x0046: // WM_WINDOWPOSCHANGING
                    // Handles the problem with overriding the minWidth/minHeight.
                    // https://stackoverflow.com/questions/14993670/windows-minwidth-and-minheight-are-both-ignored-when-re-sizing-with-the-resizeg
                    WINDOWPOS pos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
                    const uint NOMOVE = 0x0002;

                    if ((pos.flags & (int)NOMOVE) != 0)
                    {
                        return IntPtr.Zero;
                    }

                    Window wnd = (Window)HwndSource.FromHwnd(hwnd).RootVisual;
                    if (wnd == null)
                    {
                        return IntPtr.Zero;
                    }

                    bool changedPos = false;
                    if (pos.cx < _minWidth) { pos.cx = (int)_minWidth; changedPos = true; }
                    if (pos.cy < _minHeight) { pos.cy = (int)_minHeight; changedPos = true; }
                    if (!changedPos)
                    {
                        return IntPtr.Zero;
                    }

                    Marshal.StructureToPtr(pos, lParam, true);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = System.Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = System.Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = System.Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = System.Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>x coordinate of point.</summary>
            public int x;
            /// <summary>y coordinate of point.</summary>
            public int y;
            /// <summary>Construct a point of coordinates (x,y).</summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public static readonly RECT Empty = new RECT();
            public int Width { get { return System.Math.Abs(right - left); } }
            public int Height { get { return bottom - top; } }
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }
            public bool IsEmpty { get { return left >= right || top >= bottom; } }
            public override string ToString()
            {
                if (this == Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }
            /// <summary>Return the HashCode for this struct (not guaranteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }
            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2) { return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom); }
            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2) { return !(rect1 == rect2); }
        }

        #region Constructor

        public SamplesBrowserWindow()
        {
            AssemblyInfo info = AssemblyProvider.GetAssemblyInfo(Assembly.GetExecutingAssembly());

            _apppath = Assembly.GetExecutingAssembly().CodeBase;  //get the executing assembly path
            _apppath = _apppath.Substring(8);  //remove the file:///
            _apppath = System.IO.Path.GetDirectoryName(_apppath); //Strip the file name
            DirectoryInfo dir = new DirectoryInfo(_apppath);  //Figure out the proper parent directory
            _apppath = dir.FullName; // dir.Parent.Parent.FullName;

            InitializeComponent();

            SourceInitialized += (s, e) =>
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
            };

            MinimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            MaximizeButton.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            CloseButton.Click += (s, e) => Close();

            this.Title = Infragistics.SamplesBrowser.Properties.Resources.WelcomePage_SampleBrowserLabel;

            this.AddHandler(
                Hyperlink.RequestNavigateEvent,
                (RequestNavigateEventHandler)((sender, e) => Process.Start(e.Uri.OriginalString))
                );

            this.PreviewKeyDown += new KeyEventHandler(SamplesBrowserFrame_PreviewKeyDown);

            //this.TraceFocusedElementChanges();

            // Get home fade in/out storyboards
            sbFadeOutMenuItems = this.Resources["FadeOutMenuItems"] as Storyboard;
            sbFadeInMenuItems = this.Resources["FadeInMenuItems"] as Storyboard;

            sbFadeOutMenuItems.Completed += delegate { this.SearchArea.Visibility = Visibility.Hidden; };
            sbFadeInMenuItems.Completed += delegate { this.SearchArea.Visibility = Visibility.Visible; };

            // Initialize Menu State
            IsMenuExpanded = true;

            // Enforce MinWidth of MenuColumn
            MenuColumn.MinWidth = MENU_COLLAPSED_SIZE;

            // Add Event Handlers
            MenuBodySplitter.DragCompleted += new System.Windows.Controls.Primitives.DragCompletedEventHandler(MenuBodySplitter_DragCompleted);

            // Set Table of Contents			
            SetTOC(Properties.Resources.TableOfContentsUri);

            // this will load a sample when starting the samples browser
            // if specified in the command line parameters
            OpenAutoRunSample();

            // this event handler will be used to preload assemblies / classes
            this.CategoryTree.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(TreeViewItem_Expanded));

            // used to taking screenshots of samples
            // SetScreenshotMode();

            var toc = (TableOfContentsViewModel)this.DataContext;
            this.CrrVersionTextBlock.Text = toc.CurrentVersion + " Version"; 
        }

        private TocItemViewModel FindTocItem(List<TocItemViewModel> tocItems, string[] path, int index)
        {
            foreach (TocItemViewModel tocItem in tocItems)
            {
                if (tocItem.Name == path[index])
                {
                    if (path.Length - 1 == index)
                    {
                        return tocItem;
                    }
                    else
                    {
                        return FindTocItem(tocItem.Children, path, index + 1);
                    }
                }
            }
            return null;
        }

        private void OpenAutoRunSample()
        {
            try
            {
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    string[] sample = Environment.GetCommandLineArgs()[1].Split(new char[] { '\\', '/' });
                    if (sample.Length == 3)
                    {
                        TableOfContentsViewModel toc = (TableOfContentsViewModel)this.DataContext;
                        TocItemViewModel tocItem = FindTocItem(toc.Children, sample, 0);
                        tocItem.IsSelected = true;
                        this.frSample.Source = new Uri(tocItem.XamlFilePath);
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }

        void SamplesBrowserFrame_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
                e.Handled = true;
        }

        [Conditional("DEBUG")]
        void TraceFocusedElementChanges()
        {
            FocusManager.FocusedElementProperty.AddOwner(
                typeof(UIElement),
                new FrameworkPropertyMetadata((sender, e) => Debug.WriteLine("Focused Element: " + e.NewValue)));
        }

        #endregion Constructor

        #region Event Handlers

        #region Frame
        void frSample_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // Toggle Visibility of Tab Items
            Cursor = Cursors.Arrow;
            this.tabViews.SelectedIndex = 0;
            try
            {
                if (frSample.Source.OriginalString.Contains("Welcome"))
                {
                    this.tabSample.Visibility = Visibility.Hidden;
                    this.tabXAML.Visibility = Visibility.Hidden;
                    this.tabSnippet.Visibility = Visibility.Hidden;
                    this.tabCode.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.tabSample.Visibility = Visibility.Visible;
                    this.tabXAML.Visibility = Visibility.Visible;
                    this.tabSnippet.Visibility = Visibility.Visible;
                    this.tabCode.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception:  " + ex.Message);
            }
        }

        private void frSample_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // Prohibit back navigation via the Backspace key, because the application
            // was not designed to support that.  Also prevent a refresh.
            if (e.NavigationMode == NavigationMode.Back || e.NavigationMode == NavigationMode.Refresh)
            {
                e.Cancel = true;
            }
        }
        #endregion Frame

        #region Buttons

        private void btnToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (IsMenuExpanded)
            {
                CollapseMenu();
            }
            else
            {
                ExpandMenu();
            }

            IsMenuExpanded = !IsMenuExpanded;
        }

        void OnOpenWebPageButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;

            string url = btn.CommandParameter as string;
            if (String.IsNullOrEmpty(url))
                return;

            try
            {
                Process.Start(url);
            }
            catch
            {
            }
        }

        #endregion Buttons

        #region MenuBodySplitter
        void MenuBodySplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (MenuColumn.ActualWidth <= MENU_COLLAPSED_SIZE)
            {
                sbFadeOutMenuItems.Begin(LayoutRoot);
                IsMenuExpanded = false;
            }
            else
            {
                sbFadeInMenuItems.Begin(LayoutRoot);
                IsMenuExpanded = true;
            }
        }
        #endregion MenuBodySplitter

        #region tabViews_SelectionChanged
        private void tabViews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource == e.Source && e.Source is TabControl)
            {
                string _framesource = String.Empty;
                string _ctrlName = String.Empty;
                string[] splitedSource;
                try
                {
                    switch (tabViews.SelectedIndex)
                    {
                        case 0:
                            // Force a new instance of the sample to be instantiated, when the "Sample" tab is selected:
                            //BindingOperations.GetBindingExpressionBase(frSample, Frame.SourceProperty).UpdateTarget();
                            break;

                        case 1:
                            ScrollViewer scroll = XamlCode.Parent as ScrollViewer;
                            scroll.ScrollToTop();
                            scroll.ScrollToLeftEnd();
                            // Load xaml file

                            //Parse the name of the sample currently loaded
                            _framesource = frSample.Source.OriginalString;
                            splitedSource = _framesource.Split(';');
                            _ctrlName = splitedSource[0]; // Get name of the control folder e.g. IGGrid
                            _framesource = splitedSource[1].Replace("component/Samples/", "");
                            CurrentXamlText = System.IO.File.ReadAllText(System.IO.Path.Combine(_apppath, SAMPLES_SUBDIRECTORY_LOCATION, _ctrlName, SAMPLES_SUBDIRECTORY_LOCATION1, _framesource));
                            XamlCode.Width = 2000;
                            break;

                        case 2:
                            ScrollViewer snippetScroll = XamlSnippet.Parent as ScrollViewer;
                            snippetScroll.ScrollToTop();
                            snippetScroll.ScrollToLeftEnd();
                            // Load xaml file

                            //Parse the name of the sample currently loaded
                            _framesource = frSample.Source.OriginalString;
                            splitedSource = _framesource.Split(';');
                            _ctrlName = splitedSource[0]; // Get name of the control folder e.g. IGGrid
                            _framesource = splitedSource[1].Replace("component/Samples/", "");
                            //ToDo: Localize the following string, or provide some better visual indication that snippets are being loaded!
                            CurrentXamlText = "Please wait!";
                            XamlSnippet.Width = 2000;

                            GetSnippetsAsync(_ctrlName + "/" + SAMPLES_SUBDIRECTORY_LOCATION1 + "/" + _framesource);
                            break;

                        case 3:
                            ScrollViewer scrollSharp = CSharpCode.Parent as ScrollViewer;
                            scrollSharp.ScrollToTop();
                            scrollSharp.ScrollToLeftEnd();
                            // Load CSharp

                            //Parse the name of the sample currently loaded
                            _framesource = frSample.Source.OriginalString;
                            splitedSource = _framesource.Split(';');
                            _ctrlName = splitedSource[0]; // Get name of the control folder e.g. IGGrid
                            _framesource = splitedSource[1].Replace("component/Samples/", "");
                            CurrentCSharpCode = System.IO.File.ReadAllText(System.IO.Path.Combine(_apppath, SAMPLES_SUBDIRECTORY_LOCATION, _ctrlName, SAMPLES_SUBDIRECTORY_LOCATION1, _framesource + ".cs"));
                            CSharpCode.Width = 2000;
                            break;
                        case 4:
                            codeFilesCombo.SelectedIndex = 0;
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception:  " + ex.Message);
                }
            }
        }
        #endregion tabViews_SelectionChanged

        #region CodeFilesCombo_SelectionChanged
        private void CodeFilesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if selected tab is not the related tab do not load any content
            if (tabViews.SelectedIndex != 4 || codeFilesCombo.SelectedItem == null)
                return;

            if (!(CategoryTree.SelectedItem is SampleViewModel) || ((SampleViewModel)CategoryTree.SelectedItem).CodeFiles == null)
            {
                FileCode.Content = null;
                return;
            }

            ScrollViewer scroll = FileCode.Parent as ScrollViewer;
            scroll.ScrollToTop();
            scroll.ScrollToLeftEnd();

            // Load text file
            string _framesource = String.Empty;

            //Parse the name of the sample currently loaded
            _framesource = frSample.Source.OriginalString;
            _framesource = _framesource.Split(';')[0]; // Get name of the control folder e.g. IGGrid

            if (codeFilesCombo.SelectedItem.ToString().EndsWith("cs"))
                FileCode.Content = (Resources["cSharpColorizerConverter"] as CSharpColorizerConverter).Convert(
                    System.IO.File.ReadAllText(System.IO.Path.Combine(_apppath, SAMPLES_SUBDIRECTORY_LOCATION, _framesource, SAMPLES_SUBDIRECTORY_LOCATION1, codeFilesCombo.SelectedItem.ToString())),
                    null, null, null);
            else
                FileCode.Content = (Resources["xamlColorizerConverter"] as XAMLColorizerConverter).Convert(
                    System.IO.File.ReadAllText(System.IO.Path.Combine(_apppath, SAMPLES_SUBDIRECTORY_LOCATION, _framesource, SAMPLES_SUBDIRECTORY_LOCATION1, codeFilesCombo.SelectedItem.ToString())),
                    null, null, null);

            FileCode.Width = 2000;
        }
        #endregion // CodeFilesCombo_SelectionChanged

        #region Category Tree
        private void CategoryTree_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.frSample.Source != null) return;

            Uri u = new Uri("Views\\WelcomePage.xaml", UriKind.Relative);
            this.frSample.Source = u;
        }

        private void CategoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tv = (TreeView)sender;
            TocItemViewModel tocItem = (TocItemViewModel)tv.SelectedItem;
            if (tocItem != null)
            {
                if (this.frSample.Content is SampleContainer)
                {
                    ((SampleContainer)this.frSample.Content).OnSampleDisposed(EventArgs.Empty);
                }
                this.frSample.Source = new Uri(tocItem.XamlFilePath, UriKind.RelativeOrAbsolute);
            }
        }
        #endregion //Category Tree

        #region Sample description changed
        private void txtCurrentItemDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).ScrollToHome();
        }

        #endregion //Sample description changed

        private void tabCustom_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool tabCustomVisible = tabCustom.Visibility == System.Windows.Visibility.Visible;
            Style lastTabItemStyle = (Style)this.TryFindResource("TabItemLast_FeatureBrowser");
            Style tabItemStyle = (Style)this.TryFindResource("TabItem_FeatureBrowser");
            tabCode.Style = tabCustomVisible ? tabItemStyle : lastTabItemStyle;
        }

        private void MainWindowOnStateChanged(object sender, EventArgs e)
        {
            UpdateMaximizeAndRestoreButtonsState();
        }

        private void UpdateMaximizeAndRestoreButtonsState()
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    IsMaximizeButtonVisible = true;
                    IsRestoreButtonVisible = false;
                    break;
                case WindowState.Maximized:
                    IsMaximizeButtonVisible = false;
                    IsRestoreButtonVisible = true;
                    break;
            }
        }
        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            UpdateMaximizeAndRestoreButtonsState();
            _minWidth = this.MinWidth;
            _minHeight = this.MinHeight;
        }

        //Resize Handlers
        #region ResizeWindows
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                if (senderRect != null)
                {
                    Window mainWindow = senderRect.Tag as Window;
                    if (senderRect != null)
                    {
                        double width = e.GetPosition(mainWindow).X;
                        double height = e.GetPosition(mainWindow).Y;

                        senderRect.CaptureMouse();
                        if (senderRect.Name.ToLower().Contains("right"))
                        {
                            width += 5;
                            mainWindow.Width = System.Math.Max(width, mainWindow.MinWidth);
                        }
                        if (senderRect.Name.ToLower().Contains("left"))
                        {
                            width -= 5;
                            var delta = mainWindow.Width - width;

                            if (delta >= mainWindow.MinWidth)
                                mainWindow.Left += width;

                            mainWindow.Width = System.Math.Max(delta, mainWindow.MinWidth);
                        }
                        if (senderRect.Name.ToLower().Contains("bottom"))
                        {
                            height += 5;
                            mainWindow.Height = System.Math.Max(height, mainWindow.MinHeight);
                        }
                        if (senderRect.Name.ToLower().Contains("top"))
                        {
                            height -= 5;
                            var delta = mainWindow.Height - height;
                            if (delta >= mainWindow.MinHeight)
                                mainWindow.Top += height;

                            mainWindow.Height = System.Math.Max(delta, mainWindow.MinHeight);
                        }
                    }
                }
            }
        }
        #endregion

        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }

            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, WmNclbuttondown, HtCaption, 0);
        }

        #endregion Event Handlers

        #region Dependency Properties

        #region IsMenuExpanded
        /// <summary>
        /// Identifies the 'IsMenuExpanded' dependency property
        /// </summary>
        public static readonly DependencyProperty IsMenuExpandedProperty = DependencyProperty.Register("IsMenuExpanded",
        typeof(bool), typeof(SamplesBrowserWindow), new FrameworkPropertyMetadata());

        /// <summary>
        /// Identifies whether the main menu is expanded or collapsed
        /// </summary>
        [Category("Behavior")]
        public bool IsMenuExpanded
        {
            get
            {
                return (bool)this.GetValue(SamplesBrowserWindow.IsMenuExpandedProperty);
            }
            set
            {
                this.SetValue(SamplesBrowserWindow.IsMenuExpandedProperty, value);
            }
        }

        #endregion IsMenuExpanded

        #region CurrentXamlText

        public static DependencyProperty CurrentXamlTextProperty =
            DependencyProperty.Register("CurrentXamlText",
            typeof(string), typeof(SamplesBrowserWindow));

        public string CurrentXamlText
        {
            get { return ((string)base.GetValue(CurrentXamlTextProperty)); }
            set { base.SetValue(CurrentXamlTextProperty, value); }
        }

        #endregion CurrentXamlText

        #region CurrentCSharpCode

        public static DependencyProperty CurrentCSharpCodeProperty =
            DependencyProperty.Register("CurrentCSharpCode",
            typeof(string), typeof(SamplesBrowserWindow));

        public string CurrentCSharpCode
        {
            get { return ((string)base.GetValue(CurrentCSharpCodeProperty)); }
            set { base.SetValue(CurrentCSharpCodeProperty, value); }
        }

        #endregion CurrentCSharpCode

        #endregion Dependency Properties

        #region Methods

        #region Menu Management
        private void ExpandMenu()
        {
            // Reset Timer
            menuTimer = 0;

            // Set previous width
            menuPreviousWidth = MenuColumn.ActualWidth;

            // Calculate New Delta
            menuDelta = MENU_EXPANDED_SIZE - MenuColumn.ActualWidth;

            // Start Animation
            CompositionTarget.Rendering += new EventHandler(AnimateMenu);

            // Fade In Elements
            sbFadeInMenuItems.Begin(LayoutRoot);
        }

        private void CollapseMenu()
        {
            // Reset Timer
            menuTimer = 0;

            // Set previous width
            menuPreviousWidth = MenuColumn.ActualWidth;

            // Calculate New Delta
            menuDelta = MENU_COLLAPSED_SIZE - MenuColumn.ActualWidth;

            // Start Animation
            CompositionTarget.Rendering += new EventHandler(AnimateMenu);

            // Fade Out Elements
            sbFadeOutMenuItems.Begin(LayoutRoot);
        }

        void AnimateMenu(object sender, EventArgs e)
        {
            if (menuTimer == MENU_ANIMATE_MAXTIME)
            {
                CompositionTarget.Rendering -= this.AnimateMenu;
                if (IsMenuExpanded)
                {
                    MenuColumn.Width = new GridLength(MENU_EXPANDED_SIZE, GridUnitType.Pixel);
                }
                else
                {
                    MenuColumn.Width = new GridLength(MENU_COLLAPSED_SIZE, GridUnitType.Pixel);
                }
            }
            else
            {
                MenuColumn.Width = new GridLength(SamplesBrowserWindow.easeInQuad(menuTimer, menuPreviousWidth, menuDelta, MENU_ANIMATE_MAXTIME), GridUnitType.Pixel);
                menuTimer++;
            }
        }
        #endregion Menu Management

        #region SetTOC
        public void SetTOC(string tocName)
        {
            var toc = TableOfContentsViewModel.Create(tocName);
            base.DataContext = toc;
        }
        #endregion

        #region Snippets

        private void GetSnippetsAsync(string _framesource)
        {
            if (snippetsThread != null && snippetsThread.IsAlive)
                snippetsThread.Abort("User cancellation");

            snippetsThread = new Thread(new ParameterizedThreadStart(GetSnippets));
            snippetsThread.Start(_framesource);
        }

        private void GetSnippets(object xamlText)
        {
            string xaml = System.IO.File.ReadAllText(System.IO.Path.Combine(_apppath, SAMPLES_SUBDIRECTORY_LOCATION, (string)xamlText));

            Regex regSnippet = new Regex(@"(([^\n\r]*)<!-- *#BEGIN SNIPPET# *-->([\s\S]*?)<!-- *#END SNIPPET# *-->)", RegexOptions.Multiline & RegexOptions.IgnorePatternWhitespace & RegexOptions.CultureInvariant);

            MatchCollection snippetCollection = regSnippet.Matches(xaml);

            if (snippetCollection.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (Match snippet in snippetCollection)
                {
                    string currentSnippet = snippet.Groups[1].Value;
                    string whiteSpace = snippet.Groups[2].Value;
                    currentSnippet = Regex.Replace(currentSnippet, @"(^\s{" + whiteSpace.Length.ToString() + "})", String.Empty, RegexOptions.Multiline);
                    sb.Append(currentSnippet);
                    sb.Append("\n\n");
                }
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    CurrentXamlText = sb.ToString();
                    XamlSnippet.Width = 2000;
                }));
            }
            else
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    CurrentXamlText = "<!-- No snippet available -->";
                    XamlSnippet.Width = 2000;
                }));
        }
        #endregion // Snippets

        #endregion Methods

        #region SetTOC
        public static double easeInQuad(double time, double startVal, double newVal, double duration)
        {
            return newVal * (time /= duration) * time + startVal;
        }
        #endregion

        #region Preloading Assemblies / Classes

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            // obtaining the TreeViewItem, which is expanded
            TreeViewItem tvi = e.OriginalSource as TreeViewItem;

            // checking if this is a control TreeViewItem (and not a category one)
            if (tvi.Header is ControlViewModel)
            {
                // looking for the first category inside the control tree node
                ControlViewModel cvm = (ControlViewModel)tvi.Header;
                if (cvm.Children.Count > 0 && cvm.Children[0] is SubCategoryViewModel)
                {
                    // looking for the first sample inside the category tree node
                    SubCategoryViewModel scvm = (SubCategoryViewModel)cvm.Children[0];
                    if (scvm.Children.Count > 0 && scvm.Children[0] is SampleViewModel)
                    {
                        // obtaining the first samples tree node and its XamlFilePath property
                        SampleViewModel svm = (SampleViewModel)scvm.Children[0];
                        string path = svm.XamlFilePath;
                        int i = svm.XamlFilePath.IndexOf(';');
                        if (i != -1)
                        {
                            // the XamlFilePath property uses the following format:
                            // pack://application:,,,/Schedule.Samples;component/Samples/Data/DayViewCodeBehind.xaml
                            // we substract the magic number 23, because this is the length of the "pack://application:,,,/" part
                            string assemblyName = svm.XamlFilePath.Substring(23, i - 23);
                            try
                            {
                                var assembly = LoadAssembly(assemblyName);
                                if (Contains(assembly, assemblyName + ".Preloader"))
                                {
                                    Activator.CreateInstance(assemblyName, assemblyName + ".Preloader");
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }

        private Assembly LoadAssembly(string assemblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            foreach (var item in assemblies.OrderBy(a => a.FullName))
            {
                if (item.FullName.StartsWith(assemblyName))
                {
                    return item;
                }
            }
            return Assembly.Load(assemblyName);
        }
        private bool Contains(Assembly assembly, string typeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.FullName.Equals(typeName))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Preloading Assemblies / Classes



        private void SetScreenshotMode()
        {
            this.frSample.Width = 700;
            this.frSample.Height = 500;

            var grid = (Grid)this.tabSample.Content;
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            var button = new Button() { Content = "Get Screenshot" };
            grid.Children.Add(button);
            Grid.SetRow(button, 2);
            button.Click += GetScreenshot;
        }

        private void GetScreenshot(object sender, RoutedEventArgs e)
        {

            var currentSample = this.CategoryTree.SelectedItem as SampleViewModel;
            if (currentSample == null)
            {
                return;
            }
            List<string> words = new List<string>(currentSample.Name.Split(' ', '-'));
            var upperCaseWords = new List<string>();
            words = words.Where(w => w.Length > 1).ToList();
            words.ForEach(l => upperCaseWords.Add(l[0].ToString().ToUpperInvariant() + l.Substring(1)));
            var camelCaseName = string.Concat<string>(upperCaseWords);

            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                currentSample.Parent.Parent.Name);
            var imageName = currentSample.Parent.Parent.Name + "_" + camelCaseName + ".png";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            int width = 700;
            int height = 500;

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(frSample.Content as Visual);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (Stream stream = File.OpenWrite(Path.Combine(folderPath, imageName)))
            {
                encoder.Save(stream);
            }
        }
    }
}
