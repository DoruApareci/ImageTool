using ImageTool.Commands;
using ImageTool.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageTool.ViewModels
{
    class FilterBaseViewModel: INotifyPropertyChanged
    {
        #region Privates
        
        ICommand openFile;
        ICommand saveImageCommand;
        ICommand loadEffectCommand;
        List<String> effects;
        string inputFileName;
        string currentEffect;
        
        #endregion

        public FilterBaseViewModel()
        {
            openFile = new BaseCommand(new Action<object>(SelectFile));
            loadEffectCommand = new BaseCommand(new Action<object>(LoadEffectControl));
            saveImageCommand = new BaseCommand(new Action<object>(SaveImage));

            var availableEffects = Enum.GetNames(typeof(ImageFiltersLibrary.Effects)).ToList();
            availableEffects.Remove("Quantize");
            effects = availableEffects;
            LoadEffectControl(availableEffects[0]);
            CurrentEffect = effects[0];
            InputFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "default.jpg");
        }

        public string CurrentEffect
        {
            get
            {
                return currentEffect;
            }
            set
            {
                currentEffect = value;
                LoadEffectControl(currentEffect);
            }
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

        public List<String> Effects
        {
            get
            {
                return effects;
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
            enumValue = (ImageFiltersLibrary.Effects)Enum.Parse(typeof(ImageFiltersLibrary.Effects), currentEffect);
            if (currentEffect == "Edge" ||
                currentEffect == "Parabola")
            {
                CurrentViewModel = new MultipleChoiceViewModel(enumValue);
            }
            else if (currentEffect == "Sepia" ||
                currentEffect == "Outline")
            {
                CurrentViewModel = new SliderSelectionViewModel(enumValue);
            }
            else if (
                currentEffect == "Emboss" ||
                currentEffect == "XRay")
            {
                CurrentViewModel = new MultipleChoiceColourSelectionViewModel(enumValue);
            }
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
