using ImageFiltersLibrary.Parameters;
using ImageFiltersLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;

namespace ImageTool.ViewModels
{
    public class SliderSelectionViewModel:BaseViewModel
    {
        string parameterName;
        AlgorithmOption slider;
        
        public SliderSelectionViewModel(Effects effect)
        {
            AlgorithmOptions = ImageProcessingAlgorithm.GetOptions(effect);
            ImageProcessingAlgorithm.SetEffects(effect);
            slider = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.SingleInput);
            parameterName = (slider.Options.Keys.First() as AlgorithmParameter).ParameterName;
            SliderValue = (Minimum + (Max - Minimum) / 2);
        }

        private double sliderValue;

        public int StepSize
        {
            get
            {
                return (slider.Options.Keys.First() as RangeAlgorithmParameter).StepSize;
            }
        }

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

        void ApplyEffect()
        {
            if (InputImage != null)
            {
                var parameter = new AlgorithmParameter()
                {
                    Value = (int)sliderValue,
                    ParameterName = parameterName
                };
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(parameter);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
            }
        }

        public override void LoadImage(string fileName)
        {
            InputImage = ImageProcessingAlgorithm.LoadInputImage(fileName, out Message);
            ApplyEffect();
        }

        public override ImageSource SaveImage()
        {
            ImageSource result = null;
            if (InputImage != null)
            {
                var parameter = new AlgorithmParameter()
                {
                    Value = (int)sliderValue,
                    ParameterName = parameterName
                };
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(parameter);
                result = ImageProcessingAlgorithm.ApplyEffectOnOriginalDimensions(algorithmParameter, out Message);
                if (!String.IsNullOrEmpty(Message))
                {
                    MessageBox.Show("Some error occured \n" + Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }
    }
}
