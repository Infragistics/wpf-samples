using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IGExtensions.Framework
{
    /// <summary>
    /// Represents manager that provides functionality to install/uninstall Click-Once applications
    /// </summary>
    // https://github.com/IvanLeonenko/CustomizedClickOnce
    public class AppClickOnceManager
    {
        private const string UninstallString = "UninstallString";
        private const string DisplayNameKey = "DisplayName";
        private const string UninstallStringFile = "UninstallString.bat";
        private const string ApprefExtension = ".appref-ms";
        protected RegistryKey UninstallRegistryKey;

        private static string AppLocation
        {
            get { return Assembly.GetExecutingAssembly().Location; }
        }
        private static string AppPath
        {
            get { return Path.GetDirectoryName(AppLocation); }
        }

        public string ProductCompany { get; private set; }
        public string ProductName { get; private set; }
        public string ProductHelpLink { get; set; }
        public string ProductInfoLink { get; set; }
        public string UninstallFile { get; private set; }

        public AppClickOnceManager(Assembly applicationAssembly)
        {
            var ai = AssemblyProvider.GetAssemblyInfo(applicationAssembly);
            var productCompany = ai.AssemblyCompany;
            var productName = ai.AssemblyProduct + " " + ai.AssemblyVersion;
            RegisterApplication(productCompany, productName);
        }
        public AppClickOnceManager(string productCompany, string productName)
        {
            RegisterApplication(productCompany, productName);
        }
        private void RegisterApplication(string productCompany, string productName)
        {
            ProductHelpLink = "http://www.infragistics.com/help/";
            ProductInfoLink = "http://www.infragistics.com/products/wpf/application-samples";

            ProductCompany = productCompany;
            ProductName = productName;

            var appDataPath = Environment.SpecialFolder.LocalApplicationData;
            var publisherFolder = Path.Combine(Environment.GetFolderPath(appDataPath), ProductCompany);
            if (!Directory.Exists(publisherFolder))
                 Directory.CreateDirectory(publisherFolder);
            UninstallFile = Path.Combine(publisherFolder, UninstallStringFile);
            UninstallRegistryKey = GetUninstallRegistryKeyByProductName(ProductName);
        }


        #region Shortcut related
        private string GetShortcutPath()
        {
            var programsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            var shortcutPath = Path.Combine(programsPath, ProductCompany);
            return Path.Combine(shortcutPath, ProductName) + ApprefExtension;
        }

        private string GetStartupShortcutPath()
        {
            var startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            return Path.Combine(startupPath, ProductName) + ApprefExtension;
        }

        public void AddShortcutToStartup()
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
                return;
            var startupPath = GetStartupShortcutPath();
            if (File.Exists(startupPath))
                return;
            File.Copy(GetShortcutPath(), startupPath);
        }

        private void RemoveShortcutFromStartup()
        {
            var startupPath = GetStartupShortcutPath();
            if (File.Exists(startupPath))
                File.Delete(startupPath);
        }
        #endregion

        #region Update registry
        public void UpdateUninstallParameters()
        {
            if (UninstallRegistryKey == null)
                return;
            UpdateUninstallString();
            UpdateDisplayIcon();
            SetNoModify();
            SetNoRepair();
            SetHelpLink();
            SetInfoLink();
        }

        private static RegistryKey GetUninstallRegistryKeyByProductName(string productName)
        {
            var subKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
            if (subKey == null)
                return null;
            foreach (var name in subKey.GetSubKeyNames())
            {
                var permission = RegistryKeyPermissionCheck.ReadWriteSubTree;
                var rights = RegistryRights.QueryValues | RegistryRights.ReadKey | RegistryRights.SetValue;
                var application = subKey.OpenSubKey(name, permission, rights);
                if (application == null)
                    continue;
                foreach (var appKey in application.GetValueNames().Where(appKey => appKey.Equals(DisplayNameKey)))
                {
                    if (application.GetValue(appKey).Equals(productName))
                        return application;
                    break;
                }
            }
            return null;
        }

        private void UpdateUninstallString()
        {
            var uninstallString = (string)UninstallRegistryKey.GetValue(UninstallString);
            if (!String.IsNullOrEmpty(UninstallFile) && uninstallString.StartsWith("rundll32.exe"))
                 File.WriteAllText(UninstallFile, uninstallString);
            var str = String.Format("\"{0}\" uninstall", Path.Combine(AppPath, "uninstall.exe"));
            UninstallRegistryKey.SetValue(UninstallString, str);
        }

        private void UpdateDisplayIcon()
        {
            var str = String.Format("{0},0", Path.Combine(AppPath, "uninstall.exe"));
            UninstallRegistryKey.SetValue("DisplayIcon", str);
        }

        private void SetNoModify()
        {
            UninstallRegistryKey.SetValue("NoModify", 1, RegistryValueKind.DWord);
        }

        private void SetNoRepair()
        {
            UninstallRegistryKey.SetValue("NoRepair", 1, RegistryValueKind.DWord);
        }

        private void SetHelpLink()
        {
            UninstallRegistryKey.SetValue("HelpLink", this.ProductHelpLink, RegistryValueKind.String);
        }

        private void SetInfoLink()
        {
            UninstallRegistryKey.SetValue("URLInfoAbout", this.ProductInfoLink, RegistryValueKind.String);
        }
        #endregion

        #region uninstall
        public void Uninstall()
        {
            try
            {
                //kill process
                foreach (var process in Process.GetProcessesByName(ProductName))
                {
                    process.Kill();
                    break;
                }

                if (!File.Exists(UninstallFile))
                    return;
                RemoveShortcutFromStartup();

                var uninstallString = File.ReadAllText(UninstallFile);
                var fileName = uninstallString.Substring(0, uninstallString.IndexOf(" "));
                var args = uninstallString.Substring(uninstallString.IndexOf(" ") + 1);

                var proc = new Process
                {
                    StartInfo =
                    {
                        Arguments = args,
                        FileName = fileName,
                        UseShellExecute = false
                    }
                };

                proc.Start();
                RespondToClickOnceRemovalDialog();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, IntPtr wParam, IntPtr lParam);
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;

        private void RespondToClickOnceRemovalDialog()
        {
            var myWindowHandle = IntPtr.Zero;
            for (var i = 0; i < 250 && myWindowHandle == IntPtr.Zero; i++)
            {
                Thread.Sleep(150);
                foreach (var proc in Process.GetProcessesByName("dfsvc"))
                    if (!String.IsNullOrEmpty(proc.MainWindowTitle) && 
                        proc.MainWindowTitle.StartsWith(ProductName))
                    {
                        myWindowHandle = proc.MainWindowHandle;
                        break;
                    }
            }
            if (myWindowHandle == IntPtr.Zero)
                return;

            SetForegroundWindow(myWindowHandle);
            Thread.Sleep(100);
            const uint wparam = 0 << 29 | 0;

            PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)(UserKeys.Shift | UserKeys.Tab), (IntPtr)wparam);
            //PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)(UserKeys.Shift | UserKeys.Tab), (IntPtr)wparam);
            PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)(UserKeys.Shift | UserKeys.Tab), (IntPtr)wparam);
            //PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)(UserKeys.Shift | UserKeys.Tab), (IntPtr)wparam);

            PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)UserKeys.Down, (IntPtr)wparam);
            //PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)UserKeys.Down, (IntPtr)wparam);

            PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)UserKeys.Tab, (IntPtr)wparam);
            //PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)UserKeys.Tab, (IntPtr)wparam);

            PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)UserKeys.Enter, (IntPtr)wparam);
            //PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)UserKeys.Enter, (IntPtr)wparam);
        }
        #endregion
    }
    [Flags]
    public enum UserKeys
    {
        Shift = 65536,
        LButton = 1,
        Back = 8,
        Tab = Back | LButton,
        Space = 32,
        Down = Space | Back,
        MButton = 4,
        Clear = Back | MButton,
        Enter = Clear | Tab
    }
}