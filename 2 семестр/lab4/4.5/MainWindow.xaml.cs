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

namespace lab.work._4._5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (myTextBox != null ) 
            {
            string input = myTextBox.Text;
                double pounds;
                if (double.TryParse(input, out pounds))
                {
                    double kilograms = pounds * 0.45359237;
                    myTextBlock.Text = kilograms.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for pounds.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
