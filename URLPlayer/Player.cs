using NAudio.Wave;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace URLPlayer
{
    public class Player : IDisposable
    {
        private WaveOutEvent waveOut;
        private MediaFoundationReader media;

        private CancellationTokenSource tokenSource;

        public Player()
        {
            tokenSource = null;
        }

        public void Play(string url)
        {
            if (tokenSource != null)
                Dispose();

            tokenSource = new CancellationTokenSource();

            Task.Run(() => PlayStream(url));
        }

        private void PlayStream(string url)
        {
            waveOut = new WaveOutEvent();
            media = new MediaFoundationReader(url);
            waveOut.Init(media);
            waveOut.Play();

            while(waveOut.PlaybackState == PlaybackState.Playing)
            {
                if (tokenSource.Token.IsCancellationRequested)
                {
                    waveOut.Stop();
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = null;
            waveOut?.Dispose();
            media?.Dispose();
        }
    }
}
