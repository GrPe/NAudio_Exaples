using System.Windows;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AudioFileDialog fileDialog;
        Player player;

        public MainWindow()
        {
            InitializeComponent();
            fileDialog = new AudioFileDialog();
            player = new Player();
        }

        private void SelectTrackButton_Click(object sender, RoutedEventArgs e)
        {
            player.LoadFile(fileDialog.GetTrackPath());
            var file = player.CurrentFilePath.Split('\\', '/');
            CurrentTrackLabel.Content = file[file.Length-1];
        }

        private void PlayStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.CurrentFilePath == string.Empty) return;

            if (player.IsPlaying)
            {
                PlayStopButton.Content = "Play";
                player.Pause();
            }
            else
            {
                PlayStopButton.Content = "Pause";
                player.Start();
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player?.SetVolume(VolumeSlider.Value, VolumeSlider.Maximum);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            PlayStopButton.Content = "Play";
            player.Reset();
        }
    }
}
