using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CTF_RPG_Game_Client_WPF
{
    /// <summary>
    /// Interaction logic for TaskInfoWindow.xaml
    /// </summary>
    public partial class TaskInfoWindow : Window
    {
        public TaskInfoWindow(string message, string info)
        {
            InitializeComponent();
            ButtonExit.MouseEnter += ButtonExit_MouseEnter;
            ButtonExit.MouseLeave += ButtonExit_MouseLeave;
            ButtonExit.MouseLeftButtonDown += ButtonExit_MouseLeftButtonDown;

            MessageTextBlock.Text = message;
            InfoTextBlock.Text = info;

            this.Loaded += TaskInfoWindow_Loaded;
        }

        private void TaskInfoWindow_Loaded(object sender, EventArgs e)
        {
            this.Height = ButtonExit.ActualHeight + InfoTextBlock.ActualHeight + MessageTextBlock.ActualHeight + TitleTextBlock.ActualHeight + 10;
        }

        private void ButtonExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ButtonExit_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonExit.Background = Brushes.Transparent;
        }

        private void ButtonExit_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonExit.Background = Brushes.Blue;
        }
    }
}
