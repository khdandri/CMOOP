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

namespace lab.work._4._4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;
            CheckWinCondition();
        }
        public void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Visibility.Visible;
            Button2.Visibility = Visibility.Visible;
            Button4.Visibility = Visibility.Visible;
            CheckWinCondition();
        }

        public void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            CheckWinCondition();
        }

        public void Button4_Click(object sender, RoutedEventArgs e)
        {
            Button3.Visibility = Visibility.Visible;
            CheckWinCondition();
        }
        public void Button5_Click(object sender, RoutedEventArgs e)
        {
            Button4.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Visible;
            CheckWinCondition();
        }
        
        private void CheckWinCondition()
        {
            if (Button1.Visibility == Visibility.Hidden &&
                Button2.Visibility == Visibility.Hidden &&
                Button3.Visibility == Visibility.Hidden &&
                Button4.Visibility == Visibility.Hidden &&
                Button5.Visibility == Visibility.Hidden)
            {
                MessageBox.Show("You win!");
            }
        }


    }
}
