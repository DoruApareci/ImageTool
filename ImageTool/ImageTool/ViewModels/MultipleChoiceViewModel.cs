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
    public class MultipleChoiceViewModel:BaseViewModel
    {
        ICommand selectMethodCommand;
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

        public MultipleChoiceViewModel(Effects effect)
        {
            selectMethodCommand = new BaseCommand(new Action<object>(SelectMethod));
            ImageProcessingAlgorithm.SetEffects(effect);
            AlgorithmOptions = ImageProcessingAlgorithm.GetOptions(effect);
            var method = AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice);
            lastSelected = method.Options.First().Key;
        }
        
        public ICommand SelectMethodCommand
        {
            get
            {
                return selectMethodCommand;
            }
        }
        
        public IDictionary<AlgorithmParameter, string> Options
        {
            get
            {
                return AlgorithmOptions.First(x => x.InputType == ImageFiltersLibrary.InputType.MultipleChoice).Options;
            }
        }
        
        protected void SelectMethod(object methodName)
        {
            lastSelected = methodName as AlgorithmParameter;
            if (InputImage != null)
            {

                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(lastSelected);
                OutputImage = ImageProcessingAlgorithm.ApplyEffect(algorithmParameter, out Message);
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
            if (InputImage != null)
            {
                List<AlgorithmParameter> algorithmParameter = new List<AlgorithmParameter>();
                algorithmParameter.Add(lastSelected);
                var result = ImageProcessingAlgorithm.ApplyEffectOnOriginalDimensions(algorithmParameter, out Message);

                if (!String.IsNullOrEmpty(Message))
                {
                    MessageBox.Show("Some error occured \n" + Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return result;
            }
            return null;
        }
    }
}
