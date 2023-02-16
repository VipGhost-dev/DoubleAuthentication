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
using System.Windows.Threading;

namespace DoubleAuthentication
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        int ten = 10;
        DispatcherTimer disTimer = new DispatcherTimer();
        string code;
        public Auth()
        {
            InitializeComponent();
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(DisTimer_Tick);
            disTimer.Start();
            MainWindow.countNum = 0;
            this.code = code;
        }

        private void DisTimer_Tick(object sender, EventArgs e)
        {
            if (ten == 0)
            {
                this.Close();
            }
            else
            {
                TenSec.Text = "Оставшееся время: " + ten + " секунд";
            }
            ten--;
        }

        private void CodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CodeBox.Text == code)
            {
                MainWindow.correct = true;
                this.Close();
            }
            MainWindow.countNum = CodeBox.Text.Length;
        }
    }
}
