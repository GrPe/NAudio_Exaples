using NAudio.Wave;
using System;

namespace AudioPlayer
{
    public class AudioFileReader : IDisposable
    {
        private WaveFileReader wavFileReader; //wav format
        private Mp3FileReader mp3FileReader; // mp3 format;
        private bool isWavFormat;

        public TimeSpan CurrentTime { set
            {
                if (isWavFormat)
                    wavFileReader.CurrentTime = value;
                else
                    mp3FileReader.CurrentTime = value;
            }}


        public AudioFileReader(string filePath)
        {
            string extention = filePath.Split('.')[1].ToLower();
            if(extention == "mp3")
            {
                mp3FileReader = new Mp3FileReader(filePath);
                isWavFormat = false;
            }
            else
            {
                wavFileReader = new WaveFileReader(filePath);
                isWavFormat = true;
            }
        }

        public void Dispose()
        {
            if (isWavFormat)
                wavFileReader.Dispose();
            else
                mp3FileReader.Dispose();
        }

        public  IWaveProvider GetFileReader()
        {
            if (isWavFormat)
                return wavFileReader;
            else
                return mp3FileReader;
        }


    }
}
