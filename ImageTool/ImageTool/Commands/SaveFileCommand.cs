using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTool.Commands
{
    public class SaveFileCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter)
        {
            //check if file is loaded
            return true;
        }

        public override void Execute(object? parameter)
        {
            //save file dialog with image filter
            //and export/save image
        }
    }
}
