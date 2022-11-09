using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTool.ViewModels
{
    internal class MainViewModel:BaseViewModel
    {
        //BitmapImage _image
        public int Progress { get; set; } = 50;
        public string StatusText { get; set; } = "Ready";
        public Bitmap Image { get; set; } = new Bitmap(100, 100);
    }
}
