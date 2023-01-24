using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ImageTool.Services
{
    public class NavigationService
    {
        public NavigationService(MainWindow window)
        {
            MainWindow = window;
        }

        public MainWindow MainWindow { get; set; }

        public void Navigate(UserControl view)
        {
            MainWindow.UpdateView(view);
        }
    }
}
