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

namespace lab.work._4._3
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            myTextBox.Visibility = Visibility.Hidden;
        }
        public void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            myTextBox.Visibility = Visibility.Visible;
        }
        public void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            myTextBox.Text = "";
        }

        private void ButtonClean_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
