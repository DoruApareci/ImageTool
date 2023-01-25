using ImageTool.Commands;
using ImageTool.Services;
using PropertyChanged;
using SeamCarverLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTool.ViewModels
{
    class SeamCarvingViewModel : INotifyPropertyChanged
    {
        //seam carver input data
        [AlsoNotifyFor("FutureWidth")]
        public double WidthRatio { get; set; } = 1;
        [AlsoNotifyFor("FutureHeight")]
        public double HeightRatio { get; set; } = 1;
        
        public int FutureWidth { get { return (int)Math.Round(WidthRatio * InputImage.Width); } }
        public int FutureHeight { get { return (int)Math.Round(HeightRatio * InputImage.Height); } }

        const int BlockSize = 16;

        bool CanSeamCarve(object obj)
        {
            return InputImage != null;
        }
        void SeamCarve(object obj)
        {
            var stopwatch = new Stopwatch();
            Bitmap inpImage = new Bitmap(InputFileName);
            Bitmap outImg = new Bitmap(InputFileName);
            stopwatch.Start();
            SeamCarverStandart standrtCarver = new 
                                    (
                                    EnergyFuncType.V1, //Energy Function 
                                    false, // HD
                                    false, // forward energy
                                    false, // Paralel
                                    0,//NeighbourCountRatio,
                                    BlockSize
                                    );
            try
            {
                outImg = standrtCarver.Generate(inpImage, FutureWidth, FutureHeight);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            stopwatch.Stop();
            LoadOutImage(outImg);
            Time = stopwatch.Elapsed.ToString();
        }

        public ImageSource OutputImage { get; set; }
        public ImageSource InputImage { get; set; }
        public ICommand OpenFile { get; set; }
        public ICommand SeamCarveCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        public string InputFileName { get; set; }
        public string Time { get; set; }

        public SeamCarvingViewModel()
        {
            OpenFile = new BaseCommand(new Action<object>(SelectFile));
            SaveImageCommand = new BaseCommand(new Action<object>(SaveImage));
            SeamCarveCommand = new BaseCommand(new Action<object>(SeamCarve), new Predicate<object>(CanSeamCarve));
            InputFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "1.jpg");
            LoadImage();
        }

        void SaveImage(object typeOfSave)
        {
            ImageSource imageToBeSaved = null;
            string proposedFileName = string.Empty;
                imageToBeSaved = OutputImage;
                proposedFileName = System.IO.Path.GetFileNameWithoutExtension(InputFileName) + "_SeamCarved" + System.IO.Path.GetExtension(InputFileName);
            var fileName = FileIOService.ShowFileDialogue(imageToBeSaved, proposedFileName);
            if (!String.IsNullOrEmpty(fileName))
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FileIOService.SaveImageFile(imageToBeSaved, fileName);
                imageToBeSaved = null;
                Mouse.OverrideCursor = null;
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        void SelectFile(object input)
        {
            var filename = FileIOService.SelectFile();
            if (!String.IsNullOrEmpty(filename))
            {
                InputFileName = filename;
                LoadImage();
            }
        }
        
        void LoadImage()
        {
            WidthRatio = 1;
            HeightRatio = 1;
            InputImage = new BitmapImage(new Uri(InputFileName));
            SeamCarve(new object());
        }
        
        void LoadOutImage(Bitmap img)
        {
            var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            OutputImage = bi;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
