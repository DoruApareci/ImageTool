using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFiltersLibrary.Parameters
{
    public class RangeAlgorithmParameter : AlgorithmParameter
    {
        public int Maximum
        {
            get;
            set;
        }
        public int Minimum
        {
            get;
            set;
        }

        private int stepSize = 1;
        public int StepSize
        {
            get
            {
                return stepSize;
            }
            set
            {
                stepSize = value;
            }
        }
    }
}
