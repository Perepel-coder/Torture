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

namespace View.Tutor.Controls
{
    /// <summary>
    /// Логика взаимодействия для Settings_Control.xaml
    /// </summary>
    public partial class Settings_Control : UserControl
    {
        public Settings_Control()
        {
            InitializeComponent();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            Settings_Panel.Visibility = Visibility.Visible;
            CloseBTN.Visibility = Visibility.Collapsed;
            CreatQuestion_Control.Visibility = Visibility.Collapsed;
            CuratorRegistration_Controls.Visibility = Visibility.Collapsed;
        }

        private void CreatQuestions_BTN_Click(object sender, RoutedEventArgs e)
        {
            CloseBTN.Visibility = Visibility.Visible;
            CreatQuestion_Control.Visibility = Visibility.Visible;
            Settings_Panel.Visibility = Visibility.Collapsed;
            CuratorRegistration_Controls.Visibility = Visibility.Collapsed;
        }

        private void CuratorRegistration_BTN_Click(object sender, RoutedEventArgs e)
        {
            CloseBTN.Visibility = Visibility.Visible;
            CuratorRegistration_Controls.Visibility = Visibility.Visible;
            Settings_Panel.Visibility = Visibility.Collapsed;
            CreatQuestion_Control.Visibility = Visibility.Collapsed;
        }
    }
}
