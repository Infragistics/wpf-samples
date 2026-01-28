using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using IGBarcodeReader.Resources;
using Infragistics.Controls.Barcodes;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Samples.WPF.xamFeatureBrowser.Samples.BarcodeReader.CaptureDevice;

namespace IGBarcodeReader.Samples.Data
{
    public partial class DecodeFromWebcam : Infragistics.Samples.Framework.SampleContainer
    {
        private WebcamCaptureViewModel _webcamViewModel;

        public DecodeFromWebcam()
        {
            InitializeComponent();
            this.Loaded += Sample_Loaded;

            TextBlockOutput.TextChanged += TextBlockOutput_TextChanged;
        }

        private void Sample_Loaded(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigating += NavigationService_Navigating;

            //Create a new instance of the WebcamCaptureViewModel and assign it
            //as a data context to the root element.
            _webcamViewModel = new WebcamCaptureViewModel();
            LayoutRoot.DataContext = _webcamViewModel;
        }

        private void ButtonStartWebcam_Click(object sender, RoutedEventArgs e)
        {
            //Start the webcam.
            _webcamViewModel.StartWebcamCapture();
        }

        private void ButtonDecode_Click(object sender, RoutedEventArgs e)
        {
            //Start decoding from the webcam.
            _webcamViewModel.StartDecoding();
        }

        private void ButtonStopDecoding_Click(object sender, RoutedEventArgs e)
        {
            //Stop decoding.
            _webcamViewModel.StopDecoding();
        }

        // Executes when the user navigates away from this page.
        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            this.NavigationService.Navigating -= NavigationService_Navigating;

            _webcamViewModel.StopWebcamCapture();
        }

        void TextBlockOutput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ScrollViewerData.UpdateLayout();
            ScrollViewerData.ScrollToVerticalOffset(double.MaxValue);
        }
    }

    public class WebcamCaptureViewModel : AsyncObservableModel
    {
        public WebcamCaptureViewModel()
        {
            _barcodeReader = new BarcodeReader();
            _barcodeReader.MaxNumberOfSymbolsToRead = 1;
            _barcodeReader.MinSymbolSize = 50;
            _barcodeReader.DecodeComplete += DecodeComplete;

            _webcamDevice = new WebcamCaptureDevice();

            _hasWebcam = false;
            _isDecoding = false;
        }

        #region Private Members
        private Infragistics.Controls.Barcodes.BarcodeReader _barcodeReader;
        private WebcamCaptureDevice _webcamDevice;
        private BitmapSource _filteredImage;
        private string _outputMessage;
        private bool _hasWebcam;
        private bool _isDecoding;
        #endregion

        #region Public Properties
        /// <summary>
        /// The surface that displays video from the Webcam. 
        /// </summary>
        public WindowsFormsHost WebcamSurface { get { return _webcamDevice.WebcamHost; } }

        /// <summary>
        /// The processed Input Image.
        /// </summary>
        public BitmapSource FilteredImage { get { return _filteredImage; } }

        /// <summary>
        /// The output of the decoding.
        /// </summary>
        public string OutputMessage { get { return _outputMessage; } }

        /// <summary>
        /// Enables and disables the 'Start Webcam' button.
        /// </summary>
        public bool CanStartWebcam { get { return !_hasWebcam; } }

        /// <summary>
        /// Enables and disables the 'Decode' button.
        /// </summary>
        public bool CanDecode { get { return _hasWebcam ? !_isDecoding : _hasWebcam; } }

        /// <summary>
        /// Enables and disables the 'Stop Decoding' button.
        /// </summary>
        public bool CanStop { get { return _hasWebcam ? _isDecoding : _hasWebcam; } }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the Webcam.
        /// </summary>
        public void StartWebcamCapture()
        {
            if (_webcamDevice.Connect())
            {
                _hasWebcam = true;

                OnPropertyChanged("WebcamSurface");
                OnPropertyChanged("CanStartWebcam");
                OnPropertyChanged("CanDecode");
                OnPropertyChanged("CanStop");
            }
            else
            {
                MessageBox.Show(BarcodeReaderStrings.BarcodeReader_NoWebcamFound);
            }
        }

        /// <summary>
        /// Starts decoding the video from the Webcam.
        /// </summary>
        public void StartDecoding()
        {
            _isDecoding = true;
            _outputMessage = string.Empty;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                        new ThreadStart(() =>
                                        {
                                            DecodeInternal();
                                        }));

            OnPropertyChanged("CanDecode");
            OnPropertyChanged("CanStop");
            OnPropertyChanged("OutputMessage");
        }

        /// <summary>
        /// Stops the decoding process.
        /// </summary>
        public void StopDecoding()
        {
            _isDecoding = false;
        }

        /// <summary>
        /// Stops the webcam.
        /// </summary>
        public void StopWebcamCapture()
        {
            if (_isDecoding) StopDecoding();
            if (_hasWebcam) _webcamDevice.Disconnect();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Starts decoding asynchronously.
        /// </summary>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DecodeInternal();
        }

        /// <summary>
        /// Gets a frame from the Webcam and starts decoding it.
        /// </summary>
        private void DecodeInternal()
        {
            if (_isDecoding)
            {
                BitmapSource inputImage = null;

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                                   new ThreadStart(() =>
                                                                   {
                                                                       //Get the input image from the webcam surface.
                                                                       inputImage = _webcamDevice.TakeSnapshot();
                                                                       //Decode the input image.
                                                                       _barcodeReader.DecodeAsync(inputImage);
                                                                   }));
            }
            else
            {
                OnPropertyChanged("CanDecode");
                OnPropertyChanged("CanStop");
            }
        }

        /// <summary>
        /// Processes the ReaderDecodeArgs data.
        /// </summary>
        private void DecodeComplete(object sender, ReaderDecodeArgs e)
        {
            if (e.SymbolFound)
            {
                _outputMessage += string.Format
                    (
                        BarcodeReaderStrings.BarcodeReader_DecodedString,
                        e.Symbology, //The Symbology of the decoded barcode.
                        e.Value,     //The Value of the decoded barcode.
                        Environment.NewLine
                    );

                OnAsyncPropertyChanged("OutputMessage");
            }

            _filteredImage = e.FilteredImage;

            OnAsyncPropertyChanged("FilteredImage");

            DecodeInternal();
        }
        #endregion
    }
}
