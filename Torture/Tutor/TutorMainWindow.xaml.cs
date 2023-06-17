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
using View.Tutor.Controls;

namespace View.Tutor
{
    /// <summary>
    /// Логика взаимодействия для TutorMainWindow.xaml
    /// </summary>
    public partial class TutorMainWindow : Window
    {
        public TutorMainWindow()
        {
            InitializeComponent();
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }
        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void UserDB_BTN_Click(object sender, RoutedEventArgs e)
        {
            CreatScript_Control.Visibility = Visibility.Collapsed;
            Settings_Control.Visibility = Visibility.Collapsed;
            UserDB_Control.Visibility = Visibility.Visible;
        }

        private void TrainDB_BTN_Click(object sender, RoutedEventArgs e)
        {
            UserDB_Control.Visibility = Visibility.Collapsed;
            Settings_Control.Visibility = Visibility.Collapsed;
            CreatScript_Control.Visibility = Visibility.Visible;
        }

        private void SettingsDB_BTN_Click(object sender, RoutedEventArgs e)
        {
            CreatScript_Control.Visibility = Visibility.Collapsed;
            UserDB_Control.Visibility = Visibility.Collapsed;
            Settings_Control.CloseBTN.Visibility = Visibility.Collapsed;
            Settings_Control.CreatQuestion_Control.Visibility = Visibility.Collapsed;
            Settings_Control.CuratorRegistration_Controls.Visibility = Visibility.Collapsed;
            Settings_Control.Visibility = Visibility.Visible;
            Settings_Control.Settings_Panel.Visibility = Visibility.Visible;
        }
    }
}
