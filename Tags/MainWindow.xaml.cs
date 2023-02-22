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

namespace Tags
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Random random = new Random();
            int[] arr = new int[15];
            for (int i = 0; i < arr.Length; i++)
            {
                int val = random.Next(1, 16);
                if (Array.IndexOf(arr, val) == -1)
                    arr[i] = val;
                else
                    i--;
            }

            // Заполнение пятнашек случайными числами из массива
            int index = 0;
            foreach (UIElement item in Root.Children)
            {
                if (item is Button)
                    if (((Button)item).Name != "Restart")
                    {
                        ((Button)item).Content = arr[index].ToString();
                        index++;
                        if (index > 14) break;
                    }
            }
        }



        private void Button_Restart(object sender, RoutedEventArgs e)
        {
            // Создание массива случайных оригинальных чисел от 1 до 15
            Random random = new Random();
            int[] arr = new int[15];
            for (int i = 0; i < arr.Length; i++)
            {
                int val = random.Next(1, 16);
                if (Array.IndexOf(arr, val) == -1)
                    arr[i] = val;
                else
                    i--;
            }

            //!!! Делаем последнюю кнопку пустой и черной
            Last.Content = "";
            Last.Background = Brushes.Black;

            // Заполнение пятнашек случайными числами из массива
            int index = 0;
            foreach (UIElement item in Root.Children)
            {
                if (item is Button)
                    if (((Button)item).Name != "Restart")
                    {
                        ((Button)item).Content = arr[index].ToString();
                        ((Button)item).Background = Brushes.White;  //!!! Делаем все кнопки белыми, т.к. одна всегда остается черной после предыдущей игры
                        index++;
                        if (index > 14) break;
                    }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Координаты пустой кнопки
            int rowEmpty = 0, columnEpty = 0;
            foreach (UIElement item in Root.Children)
            {
                if (item is Button)
                    if (string.IsNullOrEmpty(((Button)item).Content as string))
                    {
                        rowEmpty = Grid.GetRow((Button)item);
                        columnEpty = Grid.GetColumn((Button)item);
                    }
            }

            // Координаты нажатой кнопки
            int rowbtn = Grid.GetRow((Button)sender);
            int columnbtn = Grid.GetColumn((Button)sender);

            // Проверка, есть ли рядом пустая кнопка
            if ((rowbtn - 1 == rowEmpty && columnbtn == columnEpty) || (rowbtn + 1 == rowEmpty && columnbtn == columnEpty) || (columnbtn - 1 == columnEpty && rowbtn == rowEmpty) || (columnbtn + 1 == columnEpty && rowbtn == rowEmpty))
            {
                Button btn = (Button)sender;

                // Пустую кнопку заполняем текущим значением
                foreach (UIElement item in Root.Children)
                {
                    if (item is Button)
                        if (string.IsNullOrEmpty(((Button)item).Content as string))
                        {
                            ((Button)item).Content = btn.Content;
                            ((Button)item).Background = btn.Background;
                        }
                }

                // Текущую кнопку делаем пустой
                ((Button)sender).Content = "";
                ((Button)sender).Background = Brushes.Black;
            }

            // Цикл проверки на выигрыш
            int count = 0;
            foreach (UIElement item in Root.Children)
            {
                if (item is Button)
                    if (((Button)item).Name != "Restart")
                        if (((Button)item).Content != null)
                        {
                            if (((Button)item).Content != "")
                            {
                                if (int.Parse(((Button)item).Content.ToString()) > count)
                                    count = int.Parse(((Button)item).Content.ToString());
                                else
                                {
                                    count = 0;  
                                    break;
                                }
                            }
                        }
            }

            if (count == 15 && Grid.GetRow(Last) == 4 && Grid.GetRow(Last) == 4)
                Info.Text = "Congrats! WIN";
        }
    }
}
