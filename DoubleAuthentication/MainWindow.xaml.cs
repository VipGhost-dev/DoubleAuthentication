using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace DoubleAuthentication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer disTimer = new DispatcherTimer();
        int time;
        public static bool correct;
        int countError;
        public static int countNum;
        public MainWindow()
        {
            InitializeComponent();
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(DisTimer_Tick);
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if(LoginB.Text == "admin")
            {
                if(PasswordB.Password == "admin")
                {
                    EveAuth();
                }
                else
                {
                    MessageBox.Show("Введен неверный пароль!");
                }
            }
            else
            {
                MessageBox.Show("Пользователю с таким логином не существует");
            }
        }

        private void DisTimer_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                GetCodeBtn.IsEnabled = true;
                GetCodeBtn.Visibility = Visibility.Visible;
                disTimer.Stop();
                TimerCode.Visibility = Visibility.Collapsed;
            }
            else
            {
                TimerCode.Text = "Получить новый код можно будет через " + time + " секунд";
            }
            time--;
        }

        private void GetCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            EveAuth();
        }

        private void EveAuth()
        {
            correct = false;
            Random rnd = new Random();
            int code = rnd.Next(0, 100000);
            MessageBox.Show("Код для входа: " + code.ToString("D5"));
            Auth auth = new Auth(code.ToString("D5"));
            auth.ShowDialog();
            if (correct == true) 
            {
                MessageBox.Show("Вы успешно авторизованы!");
            }
            else 
            {
                if (countError >= 1) 
                {
                    CaptchaWind captcha = new CaptchaWind();
                    captcha.ShowDialog();
                    if (correct == true)
                    {
                        MessageBox.Show("Вы успешно авторизованы!");
                    }
                    else 
                    {
                        MessageBox.Show("Текст введён не верно!");
                        CaptchaWind captchaR = new CaptchaWind();
                        captchaR.ShowDialog();
                        if (correct == true)
                        {
                            MessageBox.Show("Вы успешно авторизованы!");
                        }
                        else
                        {
                            MessageBox.Show("Вы неверно ввели каптчу. Вход не удачен");
                            LoginB.Text = "";
                            PasswordB.Password = "";
                            LoginB.IsEnabled = true;
                            PasswordB.IsEnabled = true;
                            EnterBtn.Visibility = Visibility.Visible;
                            GetCodeBtn.Visibility = Visibility.Collapsed;
                            countError = 0;
                        }
                    }
                }
                else
                {
                    if (countNum == 5) 
                    {
                        MessageBox.Show("Введенный код не является верным! ");
                    }
                    EnterBtn.Visibility = Visibility.Collapsed;
                    GetCodeBtn.Visibility = Visibility.Collapsed;
                    LoginB.IsEnabled = false;
                    PasswordB.IsEnabled = false;
                    time = 60;
                    TimerCode.Text = "Получить новый код можно будет через " + time + " секунд";
                    TimerCode.Visibility = Visibility.Visible;
                    disTimer.Start();
                    countError++;
                }
            }
        }
    }
}
