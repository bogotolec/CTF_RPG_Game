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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CTF_RPG_Game_Client_WPF
{
    public partial class MainWindow : Window
    {
        public static ConnectionManager Manager;
        private JsonData LastAnswer;

        public MainWindow()
        {
            InitializeComponent();
            Manager = ConnectionManager.GetManager();
            LastAnswer = JsonData.Parse(Manager.Get("map"));
        }
    }
}
