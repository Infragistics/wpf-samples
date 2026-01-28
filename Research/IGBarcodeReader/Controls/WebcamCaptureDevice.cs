using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Clipboard = System.Windows.Forms.Clipboard;
using IDataObject = System.Windows.Forms.IDataObject;

namespace Infragistics.Samples.WPF.xamFeatureBrowser.Samples.BarcodeReader.CaptureDevice
{
    public class WebcamCaptureDevice
    {
        #region Private Constants
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;
        private const short SWP_NOZORDER = 0x4;
        private const short HWND_BOTTOM = 1;
        private const short SWP_NOMOVE = 0x2;
        #endregion

        #region Dll Imports
        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA
            (short wDriverIndex, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        #endregion

        #region Private Members
        private PictureBox _container;
        private short _deviceIndex;
        private int _deviceId;
        private int _hHwnd;
        #endregion

        #region Conrstructor
        public WebcamCaptureDevice()
        {
            _container = new PictureBox();

            this.WebcamHost = new WindowsFormsHost();
            this.WebcamHost.Child = _container;

            _deviceId = 0;
            _deviceIndex = 0;
            _hHwnd = 0;

            string deviceName = String.Empty.PadRight(100);
            string deviceVersion = String.Empty.PadRight(100);

            capGetDriverDescriptionA(_deviceIndex, ref deviceName, 100, ref deviceVersion, 100);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the Webcam Host.
        /// </summary>
        public WindowsFormsHost WebcamHost { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Connects to the Webcam.
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            IntPtr handle = _container.Handle;

            string deviceIndex = _deviceIndex.ToString();

            _hHwnd = capCreateCaptureWindowA(ref deviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, handle.ToInt32(), 0);

            bool isConnected = (SendMessage(_hHwnd, WM_CAP_DRIVER_CONNECT, _deviceId, 0) != 0);
            if (!isConnected)
            {
                DestroyWindow(_hHwnd);

                return false;
            }

            SendMessage(_hHwnd, WM_CAP_SET_SCALE, -1, 0);
            SendMessage(_hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);
            SendMessage(_hHwnd, WM_CAP_SET_PREVIEW, -1, 0);
            SetWindowPos(_hHwnd, HWND_BOTTOM, 0, 0, _container.Height, _container.Width, SWP_NOMOVE | SWP_NOZORDER);

            return true;
        }

        /// <summary>
        /// Takes a snapshot of the current frame of the Webcam.
        /// </summary>
        /// <returns>The snapshot.</returns>
        public BitmapSource TakeSnapshot()
        {
            SendMessage(_hHwnd, WM_CAP_EDIT_COPY, 0, 0);

            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(typeof(Bitmap)))
            {
                Bitmap bitmap = (Bitmap)dataObject.GetData(typeof(Bitmap));
                IntPtr hBitmap = bitmap.GetHbitmap();
                BitmapSource snapshot = Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(hBitmap);

                return snapshot;
            }

            return null;
        }

        /// <summary>
        /// Disconnects the Webcam.
        /// </summary>
        public void Disconnect()
        {
            SendMessage(_hHwnd, WM_CAP_DRIVER_DISCONNECT, _deviceId, 0);
            DestroyWindow(_hHwnd);
        }
        #endregion
    }
}
