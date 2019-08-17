using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioExtract
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoFileDialog fileDialog;
        private AudioConverter converter;
        private string filePath;

        public MainWindow()
        {
            InitializeComponent();
            fileDialog = new VideoFileDialog();
            converter = new AudioConverter();
            filePath = string.Empty;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == string.Empty) return;
            
            converter.Convert(filePath);
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            filePath = fileDialog.GetFilePath();
            FileNameLabel.Content = filePath;
        }
    }
}
