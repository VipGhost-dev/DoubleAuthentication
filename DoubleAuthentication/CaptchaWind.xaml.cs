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
using static System.Net.Mime.MediaTypeNames;

namespace DoubleAuthentication
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWind.xaml
    /// </summary>
    public partial class CaptchaWind : Window
    {
        public static string otvetCap;
        public CaptchaWind()
        {
            InitializeComponent();
            Random rnd = new Random();
            int lines = rnd.Next(30, 70);

            for (int i = 0; i < lines; i++)
            {
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256))); 
                Line line = new Line()
                {
                    X1 = rnd.Next((int)cap.Width),
                    Y1 = rnd.Next((int)cap.Height),
                    X2 = rnd.Next((int)cap.Width),
                    Y2 = rnd.Next((int)cap.Height),
                    Stroke = brush,
                };
                cap.Children.Add(line);
            }
            int otvetRaz = rnd.Next(7, 11); 
            otvetCap = "";
            for (int i = 0; i < otvetRaz; i++)
            {
                int j = rnd.Next(2);
                if (j == 0)
                {
                    otvetCap = otvetCap + rnd.Next(9).ToString();
                }
                else
                {
                    int l = rnd.Next(2);
                    if (l == 0)
                    {
                        otvetCap = otvetCap + (char)rnd.Next('A', 'Z' + 1);
                    }
                    else
                    {
                        otvetCap = otvetCap + (char)rnd.Next('a', 'z' + 1);
                    }
                }
            }
            int Begin = 0;
            int End = 0; 
            int h = (int)cap.Width / otvetCap.Length; 
            for (int i = 0; i < otvetCap.Length; i++)
            {
                if (i == 0) 
                {
                    End += h;
                }
                else
                {
                    Begin = End;
                    End += h;
                }
                int height = rnd.Next((int)cap.Height);
                int width = rnd.Next(Begin, End);
                int styleT = rnd.Next(3); 
                if (styleT == 0)
                {
                    int fontSize = rnd.Next(16, 33);
                    TextBlock txt = new TextBlock()
                    {
                        Text = otvetCap[i].ToString(),
                        FontWeight = FontWeights.Bold,
                        Padding = new Thickness(width, height, 0, 0),
                        FontSize = fontSize,
                    };
                    cap.Children.Add(txt);
                }
                else if (styleT == 1)
                {
                    int fontSize = rnd.Next(16, 33);
                    TextBlock txt = new TextBlock()
                    {
                        Text = otvetCap[i].ToString(),
                        FontStyle = FontStyles.Italic,
                        Padding = new Thickness(width, height, 0, 0),
                        FontSize = fontSize,
                    };
                    cap.Children.Add(txt);
                }
                else if (styleT == 2)
                {
                    int fontSize = rnd.Next(16, 33);
                    TextBlock txt = new TextBlock()
                    {
                        Text = otvetCap[i].ToString(),
                        FontWeight = FontWeights.Bold,
                        FontStyle = FontStyles.Italic,
                        Padding = new Thickness(width, height, 0, 0),
                        FontSize = fontSize,
                    };
                    cap.Children.Add(txt);
                }
            }
        }

        private void capBtn_Click(object sender, RoutedEventArgs e)
        {
            if (capBox.Text == otvetCap)
            {
                MainWindow.correct = true;
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
    }
}
