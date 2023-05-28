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

namespace View.Researcher.Controls
{
    /// <summary>
    /// Логика взаимодействия для Script_MainMenu_Control.xaml
    /// </summary>
    public partial class Script_MainMenu_Control : UserControl
    {
        public Script_MainMenu_Control()
        {
            InitializeComponent();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            CloseBTN.Visibility = Visibility.Collapsed;
            TrainInform_Control.Visibility = Visibility.Collapsed;
            TrainInform_Control.Test_Control.Visibility = Visibility.Collapsed;
            TrainInform_Control.Test_Control.TaskPanel_Controls.Visibility = Visibility.Collapsed;

            ScriptMenu.Visibility = Visibility.Visible;
            TrainInform_Control.Inform.Visibility = Visibility.Visible;
            TrainInform_Control.Test_Control.Test.Visibility = Visibility.Visible;
        }

        private void BaseScript_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScriptMenu.Visibility = Visibility.Collapsed;
            CloseBTN.Visibility = Visibility.Visible;
            TrainInform_Control.Visibility = Visibility.Visible;
        }
    }
}
