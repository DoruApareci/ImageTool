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
    public class DoubleSliderViewModel : BaseViewModel
    {

        double sliderOne;
        double sliderTwo;
        AlgorithmOption sliderOneParam;
        AlgorithmOption sliderTwoParam;
        AlgorithmParameter lastSelectedColour = null;
        ICommand selectColour;

        public double SliderOneValue
        {
            get
            {
                return sliderOne;
            }
            set
            {
                sliderOne = (int)value;
                ApplyEffect();
            }
        }

        public double SliderTwoValue
        {
            get
            {
                return sliderTwo;
            }
            set
            {
                sliderTwo = (int)value;
                ApplyEffect();
            }
        }

        public string SliderNameOne
        {
            get
            {
                return (sliderOneParam.Options.Keys.First() as AlgorithmParameter).ParameterName;
            }
        }

        public string SliderNameTwo
        {
            get
            {
                return (sliderTwoParam.Options.Keys.First() as AlgorithmParameter).ParameterName;
            }
        }

        public int MaxSliderOne
        {
            get
            {
                return (sliderOneParam.Options.Keys.First() as RangeAlgorithmParameter).Maximum;
            }
        }

        public int MaxSliderTwo
        {
            get
            {
                return (sliderTwoParam.Options.Keys.First() as RangeAlgorithmParameter).Maximum;
            }
        }

        public int MinSliderOne
        {
            get
            {
                return (sliderOneParam.Options.Keys.First() as RangeAlgorithmParameter).Minimum;
            }
        }

        public int MinSliderTwo
        {
            get
            {
                return (sliderTwoParam.Options.Keys.First() as RangeAlgorithmParameter).Minimum;
            }
        }

        public IDictionary<AlgorithmParameter, string> ColourSelectionOption
        {
            get
            {
                return AlgorithmOptions.FirstOrDefault(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice && x.ParameterName == "BorderColour").Options;
            }
        }

        public DoubleSliderViewModel(Effects effect)
        {
            AlgorithmOptions = ImageProcessingAlgorithm.GetOptions(effect);
            ImageProcessingAlgorithm.SetEffects(effect);

            sliderOneParam = AlgorithmOptions.First(x => (x.InputType == ImageFiltersLibrary.InputType.SingleInput) &&
                    (x.ParameterName == "BlockWidth"));
            sliderTwoParam = AlgorithmOptions.First(x => (x.InputType == ImageFiltersLibrary.InputType.SingleInput) &&
                    (x.ParameterName == "BlockHeight"));

            SliderOneValue = ((MaxSliderOne - MinSliderOne) / 2) + MinSliderOne;
            SliderTwoValue = ((MaxSliderTwo - MinSliderTwo) / 2) + MinSliderTwo;
            var colours = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice && x.ParameterName == "BorderColour");
            lastSelectedColour = colours.Options.First(x => x.Value == "Gray").Key;
            selectColour = new BaseCommand(new Action<object>(SelectColour));
        }

        public ICommand SelectColourCommand
        {
            get
            {
                return selectColour;
            }
        }

        public override void LoadImage(string fileName)
        {
            InputImage = ImageProcessingAlgorithm.LoadInputImage(fileName, out Message);
            ApplyEffect();
        }

        public override System.Windows.Media.ImageSource SaveImage()
        {
            ImageSource result = null;
            var parameter = new AlgorithmParameter()
            {
                Value = (int)SliderOneValue,
                ParameterName = SliderNameOne
            };
            var parameter1 = new AlgorithmParameter()
            {
                Value = (int)SliderTwoValue,
                ParameterName = SliderNameTwo
            };
            List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
            algorithmParameter.Add(parameter);
            algorithmParameter.Add(parameter1);
            algorithmParameter.Add(lastSelectedColour);

            result = ImageProcessingAlgorithm.ApplyEffectOnOriginalDimensions(algorithmParameter, out Message);
            if (!String.IsNullOrEmpty(Message))
            {
                MessageBox.Show("Some error occured \n" + Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        void SelectColour(object colour)
        {
            if (InputImage != null)
            {
                var param = (KeyValuePair<ImageFiltersLibrary.Parameters.AlgorithmParameter, string>)colour;
                lastSelectedColour = param.Key;
                ApplyEffect();
            }
        }

        void ApplyEffect()
        {
            if (InputImage != null)
            {
                var parameter = new AlgorithmParameter()
                {
                    Value = (int)SliderOneValue,
                    ParameterName = SliderNameOne
                };
                var parameter1 = new AlgorithmParameter()
                {
                    Value = (int)SliderTwoValue,
                    ParameterName = SliderNameTwo
                };
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(parameter);
                algorithmParameter.Add(parameter1);
                algorithmParameter.Add(lastSelectedColour);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
            }

        }
    }
}
