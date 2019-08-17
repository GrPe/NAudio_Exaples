using System.Windows.Forms;

namespace AudioPlayer
{
    public class AudioFileDialog
    {
        private OpenFileDialog fileDialog;

        public AudioFileDialog()
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Audio files (*.mp3; *.wav)|*.mp3;*.wav";
            fileDialog.Filter += "|Wave files (*.wav)|*.wav";
            fileDialog.Filter += "|MP3 files (*.mp3)|*.mp3";
        }

        public string GetTrackPath()
        {
            fileDialog.ShowDialog();
            return fileDialog.FileName;
        }
    }
}
