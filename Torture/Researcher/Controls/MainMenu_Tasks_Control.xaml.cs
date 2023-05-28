using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WpfAnimatedGif;

namespace View.Researcher.Controls
{
    /// <summary>
    /// Логика взаимодействия для MainMenu_Tasks.xaml
    /// </summary>
    public partial class MainMenu_Tasks_Control : UserControl
    {
        public MainMenu_Tasks_Control()
        {
            InitializeComponent();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            CombinationLock_Control.Visibility = Visibility.Collapsed;
            Rocket_Control.Visibility = Visibility.Collapsed;
            KeySelection_Control.Visibility = Visibility.Collapsed;
            TaskMenu.Visibility = Visibility.Visible;
            CloseBTN.Visibility = Visibility.Collapsed;
            ImageBehavior.SetRepeatBehavior(CombinationLock_Control.GIF, new RepeatBehavior(0));
            ImageBehavior.SetRepeatBehavior(Rocket_Control.GIF, new RepeatBehavior(0));
        }

        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TaskMenu.Visibility = Visibility.Collapsed;
            CloseBTN.Visibility = Visibility.Visible;
        }
    }
}
