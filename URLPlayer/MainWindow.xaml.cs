using System.Windows;

namespace URLPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlaying;
        private Player player;

        public MainWindow()
        {
            InitializeComponent();
            isPlaying = false;
            player = new Player();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if(isPlaying)
            {
                PlayButton.Content = "Play";
                player.Stop();
                isPlaying = false;
            }
            else
            {
                PlayButton.Content = "Stop";
                player.Play(UrlTextBox.Text);
                isPlaying = true;
            }
        }
    }
}
