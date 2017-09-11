using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Interaction logic for AuthFileCreate.xaml
    /// </summary>
    public partial class AuthFileCreate : Window
    {
        public AuthFileCreate()
        {
            InitializeComponent();
            ButtonCreate.Click += ButtonCreate_Click;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            FileStream stream = File.Create("auth.cfg");
            byte[] data = Encoding.UTF8.GetBytes(((TextBlock)LanguageCombobox.SelectedItem).Text + "\n" + LoginText.Text + "\n" + PasswordText.Password);
            stream.Write(data, 0, data.Length);
            stream.Close();
            this.Close();
        }
    }
}
