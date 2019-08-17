using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioExtract
{
    public class AudioConverter
    {
        public void Convert(string filePath)
        {
            using(var reader = new MediaFoundationReader(filePath))
            {
                MediaFoundationEncoder.EncodeToMp3(reader, GetNextFileName("convert", ".mp3"));
            }
        }

        private string GetNextFileName(string fileName, string fileExtention)
        {
            int counter = 0;

            while (File.Exists(fileName + counter.ToString() + fileExtention)) counter++;

            return fileName + counter.ToString() + fileExtention;
        }
    }
}
