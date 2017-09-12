using System;
using System.Net;
using System.IO;
using System.Text;
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
    /// Interaction logic for ConnectionFileCreate.xaml
    /// </summary>
    public partial class ConnectionFileCreate : Window
    {
        public ConnectionFileCreate()
        {
            InitializeComponent();
            ButtonCreate.Click += ButtonCreate_Click;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            IPAddress IP;
            UInt16 Port;
            if (IPAddress.TryParse(ServerIpText.Text, out IP) && UInt16.TryParse(ServerPortText.Text, out Port))
            {
                FileStream stream = File.Create("connection.cfg");
                byte[] data = Encoding.UTF8.GetBytes(ServerIpText.Text + "\n" + ServerPortText.Text);
                stream.Write(data, 0, data.Length);
                stream.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect IP address or port", "ERROR", MessageBoxButton.OK);
            }
        }
    }
}
