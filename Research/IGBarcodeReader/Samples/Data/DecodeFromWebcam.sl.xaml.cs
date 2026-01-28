using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IGBarcodeReader.Resources;
using Infragistics.Controls.Barcodes;
using Infragistics.Samples.Shared.Models;


namespace IGBarcodeReader.Samples.Data
{
    public partial class DecodeFromWebcam : Infragistics.Samples.Framework.SampleContainer
    {
        private WebcamCaptureViewModel _webcamViewModel;

        public DecodeFromWebcam()
        {
            InitializeComponent();

            TextBlockOutput.TextChanged += TextBlockOutput_TextChanged;
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            //Create a new instance of the WebcamCaptureViewModel and assign it
            //as a data context to the root element.
            _webcamViewModel = new WebcamCaptureViewModel();
            LayoutRoot.DataContext = _webcamViewModel;

        }

        // Executes when the user navigates to this page.
        /*protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Create a new instance of the WebcamCaptureViewModel and assign it
            //as a data context to the root element.
            _webcamViewModel = new WebcamCaptureViewModel();
            LayoutRoot.DataContext = _webcamViewModel;
        }
         */

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
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Stop the webcam.
            _webcamViewModel.StopWebcamCapture();

            base.OnNavigatingFrom(e);
        }

        void TextBlockOutput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ScrollViewerData.UpdateLayout();
            ScrollViewerData.ScrollToVerticalOffset(double.MaxValue);
        }
    }

    public class WebcamCaptureViewModel : ObservableModel
    {
        public WebcamCaptureViewModel()
        {
            _barcodeReader = new Infragistics.Controls.Barcodes.BarcodeReader();
            _barcodeReader.MaxNumberOfSymbolsToRead = 1;
            _barcodeReader.MinSymbolSize = 50;
            _barcodeReader.DecodeComplete += DecodeComplete;

            _webcamSurface = new Rectangle();

            _hasWebcam = false;
            _isDecoding = false;
        }

        #region Private Members
        private Infragistics.Controls.Barcodes.BarcodeReader _barcodeReader;
        private Rectangle _webcamSurface;
        private BitmapSource _filteredImage;
        private CaptureSource _captureSource;
        private string _outputMessage;
        private bool _hasWebcam;
        private bool _isDecoding;
        #endregion

        #region Public Properties
        /// <summary>
        /// The surface that displays video from the Webcam. 
        /// </summary>
        public Rectangle WebcamSurface { get { return _webcamSurface; } }

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
            VideoCaptureDevice webcam = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();

            if (webcam == null)
            {
                MessageBox.Show(BarcodeReaderStrings.BarcodeReader_NoWebcamFound);
                return;
            }

            if (CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess())
            {
                _captureSource = new CaptureSource();
                _captureSource.VideoCaptureDevice = webcam;

                VideoBrush videoBrush = new VideoBrush();
                videoBrush.SetSource(_captureSource);
                videoBrush.Stretch = Stretch.UniformToFill;

                _captureSource.Start();

                _webcamSurface.Fill = videoBrush;

                _hasWebcam = true;

                OnPropertyChanged("WebcamSurface");
                OnPropertyChanged("CanStartWebcam");
                OnPropertyChanged("CanDecode");
                OnPropertyChanged("CanStop");
            }
            else
            {
                MessageBox.Show(BarcodeReaderStrings.BarcodeReader_NoPermissions);
            }
        }

        /// <summary>
        /// Starts decoding the video from the Webcam.
        /// </summary>
        public void StartDecoding()
        {
            _isDecoding = true;
            _outputMessage = string.Empty;

            Deployment.Current.Dispatcher.BeginInvoke(() => { DecodeInternal(); });

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
            if (_hasWebcam) _captureSource.Stop();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets a frame from the Webcam and starts decoding it.
        /// </summary>
        private void DecodeInternal()
        {
            if (_isDecoding)
            {
                BitmapSource inputImage = null;

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        //Get the input image from the webcam surface.
                        inputImage = new WriteableBitmap(_webcamSurface, null);
                        //Decode the input image.
                        _barcodeReader.DecodeAsync(inputImage);
                    });
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
















