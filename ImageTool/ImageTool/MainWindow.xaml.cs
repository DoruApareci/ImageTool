using ImageTool.Commands;
using ImageTool.Services;
using ImageTool.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ImageTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public NavigationService _navService { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            _navService = new NavigationService(this);
            AboutCmd = new BaseCommand(new Action<object>(AboutDialog));
        }
        ICommand AboutCmd { get; set; }
        
        public void AboutDialog(object obj)
        {
            MessageBox.Show("2023 Apareci Dorin");
        }


    // Start: MenuLeft PopupButton //
        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnHome;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Home";
            }
        }

        private void btnHome_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnFilters_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnFilters;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Image filters";
            }
        }

        private void btnFilters_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnCompression_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnCompression;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Image compression";
            }
        }

        private void btnCompression_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnSeamCarving_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSeamCarving;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Seam Carving";
            }
        }

        private void btnSeamCarving_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        
        private void btnAbout_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnAbout;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "About";
            }
        }

        private void btnAbout_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize

        private void RowDefinition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        
        public void UpdateView(UserControl userControl)
        {
            fContainer.Content = null;
            fContainer.Content = userControl;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            _navService.Navigate(new UTMLogoView());
        }

        private void btnFilters_Click(object sender, RoutedEventArgs e)
        {
            _navService.Navigate(new MainView());
        }

        private void btnCompression_Click(object sender, RoutedEventArgs e)
        {
            _navService.Navigate(new ImageCompressionView());
        }

        private void btnSeamCarving_Click(object sender, RoutedEventArgs e)
        {
            _navService.Navigate(new SeamCarvingView());
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog ab = new AboutDialog();
            ab.ShowDialog();
        }
    }
}
