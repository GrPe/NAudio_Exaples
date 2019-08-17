using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioExtract
{
    public class VideoFileDialog
    {
        private OpenFileDialog fileDialog;

        public VideoFileDialog()
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "MP4 files (*.mp4)|*.mp4";
        }

        public string GetFilePath()
        {
            fileDialog.ShowDialog();
            return fileDialog.FileName;
        }
    }
}
