using NAudio.Wave;
using System;
using System.IO;

namespace AudioRecorder
{
    public class Recorder : IDisposable
    {
        private string outputPath;
        private WaveInEvent waveIn;
        private WaveFileWriter writer;

        private event Action OnStopRecording;
        private event Action OnStartRecording;

        public Recorder(Action onStartRecordingEvent, Action onStopRecordingEvent)
        {
            waveIn = new WaveInEvent();
            OnStopRecording += onStopRecordingEvent;
            OnStartRecording += onStartRecordingEvent;
            InitWaveInEvent();
        }

        public void StartRecording()
        {
            outputPath = Path.Combine(Environment.CurrentDirectory, GetNextFileName("record", ".wav"));
            writer = new WaveFileWriter(outputPath, waveIn.WaveFormat);

            OnStartRecording?.Invoke();

            waveIn.StartRecording();
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
        }

        private void InitWaveInEvent()
        {
            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 30)
                {
                    waveIn.StopRecording();
                }
            };

            waveIn.RecordingStopped += (s, a) =>
            {
                writer?.Dispose();
                writer = null;
                OnStopRecording?.Invoke();
            };
        }

        private string GetNextFileName(string fileName, string fileExtention)
        {
            int counter = 0;

            while (File.Exists(fileName + counter.ToString() + fileExtention)) counter++;

            return fileName + counter.ToString() + fileExtention;
        }

        public void Dispose()
        {
            waveIn?.Dispose();
        }
    }
}
