using NAudio.Wave;
using System;

namespace AudioPlayer
{
    public class Player
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;
        public bool IsPlaying { get; private set; }
        public string CurrentFilePath { get; private set; }

        public Player()
        {
            waveOut = new WaveOutEvent();
            audioFileReader = null;
            CurrentFilePath = string.Empty;
        }

        public void LoadFile(string filePath)
        {
            if(audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }

            CurrentFilePath = filePath;
            audioFileReader = new AudioFileReader(filePath);
            waveOut.Init(audioFileReader.GetFileReader());
        }

        public void Start()
        {
            waveOut.Play();
            IsPlaying = true;
        }

        public void Pause()
        {
            waveOut.Pause();
            IsPlaying = false;
        }

        public void Reset()
        {
            waveOut.Stop();
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(0);
            IsPlaying = false;
        }

        public void SetVolume(double volume, double maxVolume)
        {
            waveOut.Volume = (float)volume / (float)maxVolume;
        }
    }
}
