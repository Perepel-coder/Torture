using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
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

namespace View.Researcher.Scripts.Controls
{
    /// <summary>
    /// Логика взаимодействия для TrainInform_Control.xaml
    /// </summary>
    public partial class TrainInform_Control : UserControl
    {
        public TrainInform_Control()
        {
            InitializeComponent();
        }

        private void ForthBTN_Click(object sender, RoutedEventArgs e)
        {
            Inform.Visibility = Visibility.Collapsed;
            Test_Control.Visibility = Visibility.Visible;
        }
    }
}
