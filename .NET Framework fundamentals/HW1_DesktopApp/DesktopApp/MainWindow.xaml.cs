using System.Windows;
using ClassLibrary;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PressButton_Click(object sender, RoutedEventArgs e)
        {
            //just add comment here
            NameLabel.Content = UserInteraction.GetHello(InputTextBox.Text);
        }
    }
}
