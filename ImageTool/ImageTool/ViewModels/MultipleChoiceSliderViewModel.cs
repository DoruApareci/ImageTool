using ImageFiltersLibrary.Parameters;
using ImageFiltersLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using ImageTool.Commands;

namespace ImageTool.ViewModels
{
    public class MultipleChoiceSliderViewModel : BaseViewModel
    {
        string parameterName;
        ICommand selectMethodCommand;
        AlgorithmParameter lastSelected;
        AlgorithmOption slider;
        public MultipleChoiceSliderViewModel(Effects effect)
        {
            ImageProcessingAlgorithm.SetEffects(effect);
            AlgorithmOptions = ImageProcessingAlgorithm.GetOptions(effect);
            slider = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.SingleInput);
            parameterName = (slider.Options.Keys.First() as AlgorithmParameter).ParameterName;
            SliderValue = Max / 2;
            var method = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice);
            lastSelected = method.Options.First().Key;
            selectMethodCommand = new BaseCommand(new Action<object>(SelectMethod));
        }

        private double sliderValue;
        
        public double SliderValue
        {
            get
            {
                return sliderValue;
            }
            set
            {
                sliderValue = value;
                NotifyPropertyChanged("SliderValue");
                ApplyEffect();
            }
        }
        
        public int Minimum
        {
            get
            {
                return (slider.Options.Keys.First() as RangeAlgorithmParameter).Minimum;
            }
        }

        public int Max
        {
            get
            {
                return (slider.Options.Keys.First() as RangeAlgorithmParameter).Maximum;
            }
        }

        public string SliderName
        {
            get
            {
                return (slider.Options.Keys.First() as RangeAlgorithmParameter).ParameterName;
            }
        }

        public ICommand SelectMethodCommand
        {
            get
            {
                return selectMethodCommand;
            }
        }

        void ApplyEffect()
        {
            if (InputImage != null && lastSelected != null)
            {
                var parameter = new AlgorithmParameter()
                {
                    Value = (int)sliderValue,
                    ParameterName = parameterName
                };
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(parameter);
                algorithmParameter.Add(lastSelected);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
            }
        }

        public override void LoadImage(string fileName)
        {
            InputImage = ImageProcessingAlgorithm.LoadInputImage(fileName, out Message);
            ApplyEffect();
            List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
            algorithmParameter.Add(lastSelected);
            //PreviewImages = ImageProcessingAlgorithm.GetPreview(algorithmParameter, out Message);
        }

        public IDictionary<AlgorithmParameter, string> Options
        {
            get
            {
                return AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice).Options;
            }
        }

        public override ImageSource SaveImage()
        {
            ImageSource result = null;
            if (lastSelected != null && InputImage != null)
            {
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                var parameter = new AlgorithmParameter()
                {
                    Value = (int)sliderValue,
                    ParameterName = parameterName
                };
                algorithmParameter.Add(parameter);
                algorithmParameter.Add(lastSelected);
                result = ImageProcessingAlgorithm.ApplyEffectOnOriginalDimensions(algorithmParameter, out Message);
                if (!String.IsNullOrEmpty(Message))
                {
                    MessageBox.Show("Some error occured \n" + Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }

        void SelectMethod(object methodName)
        {
            lastSelected = methodName as AlgorithmParameter;
            if (InputImage != null)
            {
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                var parameter = new AlgorithmParameter()
                {
                    Value = (int)sliderValue,
                    ParameterName = parameterName
                };
                algorithmParameter.Add(parameter);
                algorithmParameter.Add(lastSelected);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
            }
        }
    }
}
