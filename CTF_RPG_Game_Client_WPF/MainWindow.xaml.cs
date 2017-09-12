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
        private Map CurrentMap;
        private List<Image> ImagesMap = new List<Image>();

        public MainWindow()
        {
            InitializeComponent();
            Manager = ConnectionManager.GetManager();
            LastAnswer = JsonData.Parse(Manager.Get("map"));

            ButtonW.Click += ButtonW_Click;
            ButtonA.Click += ButtonA_Click;
            ButtonS.Click += ButtonS_Click;
            ButtonD.Click += ButtonD_Click;

            DrowMap();
        }

        private void ButtonD_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("d"));
            DrowMap();
        }

        private void ButtonS_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("s"));
            DrowMap();
        }

        private void ButtonA_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("a"));
            DrowMap();
        }

        private void ButtonW_Click(object sender, RoutedEventArgs e)
        {
            LastAnswer = JsonData.Parse(Manager.Get("w"));
            DrowMap();
        }

        private void DrowMap()
        {
            CurrentMap = new Map(LastAnswer.BigWindow, 25, 51);
            for (int i = 0; i < Map.HEIGHT; i++)
            {
                for (int j = 0; j < Map.WIDTH; j++)
                {

                }
            }
        }
    }

    public class SimpleCommand : ICommand
    {
        // Ссылка на метод который будет выполняться при активации команды (в интерфейс не входит)
        private Action _action;

        // Конструктор, т.к. класс делался для того, чтобы показать вызов разных методов, 
        // то в качестве параметра и передается метод, который необходимо вызывать
        public SimpleCommand(Action p_action)
        {
            _action = p_action;
        }

        // Метод, возвращающий true если команда может быть выполнена, в противном случае false 
        // (входит в интерфейс)
        // У меня, для упрощения всегда возвращается true
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Событие, которое необходимо вызывать, если поменялась доступность команды, 
        // т.к. у меня всегда команда доступна, см. выше, то я это событие и не вызываю (входит в интерфейс)
        public event EventHandler CanExecuteChanged;

        // Метод, который вызывается, при срабатывании команды (у меня вызывается метод переданный 
        // в конструктор)
        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
            }
        }
    }
}

