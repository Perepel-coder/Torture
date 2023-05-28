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

namespace View.Tutor
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow()
        {
            InitializeComponent();
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            CloseBTN.Visibility = Visibility.Visible;
            UserInfoPanel.Visibility = Visibility.Collapsed;
            Controls.Visibility = Visibility.Visible;
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            UserInfoPanel.Visibility = Visibility.Visible;
            CloseBTN.Visibility = Visibility.Collapsed;
            Controls.Visibility = Visibility.Collapsed;
        }
    }
}
