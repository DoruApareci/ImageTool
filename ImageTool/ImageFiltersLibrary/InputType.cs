using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFiltersLibrary
{
    /// <summary>
    /// Type of input accepted by algorithm
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// Can be a radio button or a combobox
        /// </summary>
        MultipleChoice,

        //Can be a text box or a slider
        SingleInput,

        /// <summary>
        /// Takes no input 
        /// </summary> 
        NoInput
    }
}
