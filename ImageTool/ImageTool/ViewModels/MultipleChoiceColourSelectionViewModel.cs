using ImageFiltersLibrary.Parameters;
using ImageFiltersLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using ImageTool.Commands;

namespace ImageTool.ViewModels
{
    public class MultipleChoiceColourSelectionViewModel : BaseViewModel
    {
        ICommand selectMethodCommand, selectColour;
        AlgorithmParameter lastSelected = null;

        public AlgorithmParameter LastSelected
        {
            get
            {
                return lastSelected;
            }
            set
            {
                lastSelected = value;
            }
        }
        AlgorithmParameter lastSelectedColour = null;

        public MultipleChoiceColourSelectionViewModel(ImageFiltersLibrary.Effects effect)
        {
            selectMethodCommand = new BaseCommand(new Action<object>(SelectMethod));
            selectColour = new BaseCommand(new Action<object>(SelectColour));
            ImageProcessingAlgorithm.SetEffects(effect);
            AlgorithmOptions = ImageProcessingAlgorithm.GetOptions(effect);
            if (effect == Effects.XRay)
            {
                lastSelected = new AlgorithmParameter();
            }
            else
            {
                var method = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice && x.ParameterName == "Method");
                lastSelected = method.Options.First().Key;
            }
            var colours = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice && x.ParameterName == "Colour");
            lastSelectedColour = colours.Options.First(x => x.Value == "Gray").Key;
        }

        public IDictionary<AlgorithmParameter, string> Options
        {
            get
            {
                var result = AlgorithmOptions.Where(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice && x.ParameterName == "Method").ToList();
                if (result.Count > 0)
                {
                    return result.First().Options;
                }
                return null;
            }
        }

        public IDictionary<AlgorithmParameter, string> ColourSelectionOption
        {
            get
            {
                return AlgorithmOptions.FirstOrDefault(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice && x.ParameterName == "Colour").Options;
            }
        }

        public ICommand SelectMethodCommand
        {
            get
            {
                return selectMethodCommand;
            }
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
            if (lastSelected != null)
            {
                SelectMethod(lastSelected);
            }
        }

        public override ImageSource SaveImage()
        {
            ImageSource result = null;
            if (lastSelected != null && lastSelectedColour != null && InputImage != null)
            {
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(lastSelectedColour);
                algorithmParameter.Add(lastSelected);
                result = ImageProcessingAlgorithm.ApplyEffectOnOriginalDimensions(algorithmParameter, out Message);
            }
            return result;
        }

        void SelectMethod(object methodName)
        {
            lastSelected = methodName as AlgorithmParameter;
            if (InputImage != null && lastSelectedColour != null)
            {
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(lastSelectedColour);
                algorithmParameter.Add(lastSelected);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
            }
        }

        void SelectColour(object colour)
        {
            if (InputImage != null && lastSelected != null)
            {
                var param = (KeyValuePair<ImageFiltersLibrary.Parameters.AlgorithmParameter, string>)colour;
                lastSelectedColour = param.Key;
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(lastSelectedColour);
                algorithmParameter.Add(lastSelected);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
            }
        }
    }
}
