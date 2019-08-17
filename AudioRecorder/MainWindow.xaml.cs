using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace AudioRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isRecording;
        private Recorder recorder;

        private DateTime currentTime;
        private CancellationTokenSource tokenSource;
        private CancellationToken token;

        public MainWindow()
        {
            InitializeComponent();
            recorder = new Recorder(StartRecordingEvent, StopRecordingEvent);
            isRecording = false;
        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (isRecording)
            {
                isRecording = false;
                recorder.StopRecording();
            }
            else
            {
                isRecording = true;
                recorder.StartRecording();
            }
        }

        private void StartRecordingEvent()
        {
            RecordButton.Content = "Stop";

            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

            Task.Factory.StartNew(TimerUpdate, token);
        }

        private void StopRecordingEvent()
        {
            RecordButton.Content = "Start";

            tokenSource?.Cancel();
            tokenSource?.Dispose();
        }

        private void TimerUpdate()
        {
            currentTime = new DateTime(1970, 1, 1, 0, 0, 0);

            while (true)
            {
                Thread.Sleep(1000);

                if (token.IsCancellationRequested)
                    break;

                currentTime = currentTime.AddSeconds(1);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    TimeDisplayer.Content = $"{currentTime.Hour}:{currentTime.Minute}:{currentTime.Second}";

                }));
            }

        }
    }
}
