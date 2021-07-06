using System.Windows;

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
            NameLabel.Content = $"Hello, {InputTextBox.Text}!";
        }
    }
}
