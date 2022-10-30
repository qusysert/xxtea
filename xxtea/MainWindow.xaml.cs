using System;
using System.IO;
using System.Text;
using System.Windows;

namespace xxtea
{
    public partial class MainWindow : Window
    {
        private string key = "1234567890";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".txt";
            openFileDlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                InputFileTextbox.Text = openFileDlg.FileName;
                InputFileContent.Text = File.ReadAllText(InputFileTextbox.Text);
            }
        }

        private void OutputFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".txt";
            openFileDlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                OutputFileTextbox.Text = openFileDlg.FileName;
            }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string text = "";
            using (StreamReader streamReader = new StreamReader(InputFileTextbox.Text, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            File.WriteAllBytes(OutputFileTextbox.Text, XXTEA.Encrypt(text, key));

            OutputFileContent.Text = File.ReadAllText(OutputFileTextbox.Text);
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            var bytes = File.ReadAllBytes(InputFileTextbox.Text);
            File.WriteAllText(OutputFileTextbox.Text, XXTEA.DecryptToString(bytes, key));

            OutputFileContent.Text = File.ReadAllText(OutputFileTextbox.Text);
        }
    }
}