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

namespace View.Researcher
{
    /// <summary>
    /// Логика взаимодействия для PanelTasks.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public UserMainWindow()
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


        private void HomeBTN_Click(object sender, RoutedEventArgs e)
        {
            Cypher_Control.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.Visibility = Visibility.Collapsed;
            CryptoComparison_Control.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.Visibility = Visibility.Visible;
            Script_MainMenu_Control.ScriptMenu.Visibility = Visibility.Visible;

            Script_MainMenu_Control.CloseBTN.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.TrainInform_Control.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.TrainInform_Control.Inform.Visibility = Visibility.Visible;
            Script_MainMenu_Control.TrainInform_Control.Test_Control.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.TrainInform_Control.Test_Control.Test.Visibility = Visibility.Visible;
            Script_MainMenu_Control.TrainInform_Control.Test_Control.TaskPanel_Controls.Visibility = Visibility.Collapsed;
        }

        private void RunBTN_Click(object sender, RoutedEventArgs e)
        {
            Script_MainMenu_Control.CloseBTN.Visibility = Visibility.Collapsed;
            Cypher_Control.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.Visibility = Visibility.Collapsed;
            CryptoComparison_Control.Visibility = Visibility.Visible;
        }

        private void CryptoBTN_Click(object sender, RoutedEventArgs e)
        {
            Script_MainMenu_Control.CloseBTN.Visibility = Visibility.Collapsed;
            CryptoComparison_Control.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.Visibility = Visibility.Collapsed;
            Cypher_Control.Visibility = Visibility.Visible;
        }

        private void TasksBTN_Click(object sender, RoutedEventArgs e)
        {
            Script_MainMenu_Control.CloseBTN.Visibility = Visibility.Collapsed;
            Cypher_Control.Visibility = Visibility.Collapsed;
            CryptoComparison_Control.Visibility = Visibility.Collapsed;
            Script_MainMenu_Control.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.Visibility = Visibility.Visible;
            MainMenu_Tasks_Control.TaskMenu.Visibility = Visibility.Visible;
            MainMenu_Tasks_Control.CloseBTN.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.CombinationLock_Control.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.Rocket_Control.Visibility = Visibility.Collapsed;
            MainMenu_Tasks_Control.KeySelection_Control.Visibility = Visibility.Collapsed;
        }
    }
}
