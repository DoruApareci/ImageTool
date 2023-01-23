using Microsoft.Win32;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTool.Commands
{
    public class OpenFileCommand : BaseCommand
    {
        public string FilePath { get; private set; } = string.Empty;
        
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"; 
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                if (!File.Exists(FilePath))
                {
                    FilePath = string.Empty;
                    throw new FileNotFoundException("File not found");
                }
            }
        }
    }
}
