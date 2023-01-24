using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ImageFiltersLibrary;

namespace ImageTool.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        ImageSource outputImage;
        ImageSource inputImage;
        #endregion

        #region Protected Fields
        protected IImageProcessingAlgorithm ImageProcessingAlgorithm;
        protected string Message;
        protected IList<AlgorithmOption> AlgorithmOptions;
        List<BitmapSource> previewImages = new List<BitmapSource>();
        #endregion

        protected BaseViewModel()
        {
            ImageProcessingAlgorithm = new ImageProcessingAlgorithm();
        }
        #region Public Fields
        
        public ImageSource OutputImage
        {
            get
            {
                return outputImage;
            }
            set
            {
                outputImage = value;
            }
        }

        public ImageSource InputImage
        {
            get
            {
                return inputImage;
            }
            set
            {
                inputImage = value;
            }
        }
        public abstract void LoadImage(string fileName);
        public abstract ImageSource SaveImage();
        #endregion

        #region INotifyPropertyChanged Members
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
