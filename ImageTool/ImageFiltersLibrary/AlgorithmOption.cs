using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageFiltersLibrary.Parameters;

namespace ImageFiltersLibrary
{
    public class AlgorithmOption
    {
        /// <summary>
        /// Type of input whether input has predefined options
        /// </summary>
        public InputType InputType
        {
            get;
            set;
        }

        /// <summary>
        /// Options Available
        /// </summary>
        public Dictionary<AlgorithmParameter, string> Options
        {
            get;
            set;
        }

        public string ParameterName
        {
            get;
            set;
        }

        public AlgorithmOption(InputType inputType, Dictionary<AlgorithmParameter, string> options)
        {
            this.InputType = inputType;
            this.Options = options;
        }
    }
}
