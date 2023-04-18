using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Yakout.Models;
using Yakout.Stores;
using Yakout.ViewModels;

namespace Yakout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            lbl.Content = DateTime.Now.ToString();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Start();

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            lbl.Content = DateTime.Now.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnPOS_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetUp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }
    }
}
