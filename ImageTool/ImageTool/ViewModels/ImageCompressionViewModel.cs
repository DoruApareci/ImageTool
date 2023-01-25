using ImageTool.Commands;
using ImageTool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageTool.ViewModels
{
    internal class ImageCompressionViewModel : INotifyPropertyChanged
    {

        #region Privates

        ICommand openFile;
        ICommand saveImageCommand;
        ICommand loadEffectCommand;
        string inputFileName;

        #endregion

        public ImageCompressionViewModel()
        {
            openFile = new BaseCommand(new Action<object>(SelectFile));
            loadEffectCommand = new BaseCommand(new Action<object>(LoadEffectControl));
            saveImageCommand = new BaseCommand(new Action<object>(SaveImage));
            enumValue = ImageFiltersLibrary.Effects.Quantize;
            LoadEffectControl(enumValue);
            InputFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "image.jpg");
        }

        public string InputFileName
        {
            get
            {
                return inputFileName;
            }
            set
            {
                if (inputFileName != value)
                {
                    inputFileName = value;
                    if (!String.IsNullOrEmpty(inputFileName))
                    {
                        CurrentViewModel.LoadImage(inputFileName);
                    }
                }
            }
        }

        public ICommand OpenFile
        {
            get
            {
                return openFile;
            }
        }

        public ICommand LoadEffectCommand
        {
            get
            {
                return loadEffectCommand;
            }
        }

        public BaseViewModel CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel = value;
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                return saveImageCommand;
            }
        }
        #region Private Methods

        void SelectFile(object input)
        {
            var filename = FileIOService.SelectFile();
            if (!String.IsNullOrEmpty(filename))
            {
                InputFileName = filename;
            }
        }

        bool ValidateFile(string fileName)
        {
            return FileIOService.ValidateFile(fileName);
        }

        void LoadEffectControl(object effect)
        {
            GetViewModel(effect);
            if (InputFileName != null)
            {
                CurrentViewModel.LoadImage(InputFileName);
            }
        }
        void GetViewModel(object effect)
        {
            CurrentViewModel = null;
            var currentEffect = effect.ToString();
            CurrentViewModel = new MultipleChoiceViewModel(enumValue);
        }

        BaseViewModel currentViewModel;

        void SaveImage(object typeOfSave)
        {
            ImageSource imageToBeSaved = null;
            string proposedFileName = string.Empty;
            if (typeOfSave.ToString() == "Original")
            {
                proposedFileName = System.IO.Path.GetFileNameWithoutExtension(InputFileName) + "_" + enumValue + System.IO.Path.GetExtension(InputFileName);
            }
            else
            {
                imageToBeSaved = CurrentViewModel.OutputImage;
                proposedFileName = System.IO.Path.GetFileNameWithoutExtension(InputFileName) + "_" + enumValue + "_Scaled" + System.IO.Path.GetExtension(InputFileName);
            }
            var fileName = FileIOService.ShowFileDialogue(imageToBeSaved, proposedFileName);
            if (!String.IsNullOrEmpty(fileName))
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (imageToBeSaved == null)
                {
                    imageToBeSaved = CurrentViewModel.SaveImage();
                }
                FileIOService.SaveImageFile(imageToBeSaved, fileName);
                imageToBeSaved = null;
                Mouse.OverrideCursor = null;
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        ImageFiltersLibrary.Effects enumValue;
    }
}
