using System;
using System.Collections.Generic;
using System.IO;
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


namespace FileStats
{
    public partial class MainWindow : Window
    {
        private double min;
        private double max;
        private double avg;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnProcess_Click(object sender, RoutedEventArgs e)
        {
            FileIO();
        }

        private void fileInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FileIO();
            }
        }

        private void FileIO()
        {   //rb is radio button
            rbMin.IsEnabled = false;
            rbMax.IsEnabled = false;
            rbAvg.IsEnabled = false;
            rbMin.IsChecked = false;
            rbMax.IsChecked = false;
            rbAvg.IsChecked = false;

            string filename = fileInput.Text;

            if (filename == "" || !File.Exists(filename))
            {
                lblValue.Content = "Error"; //not found
                return;
            }

            string[] lines = File.ReadAllLines(filename);
            int[] numList = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                numList[i] = int.Parse(lines[i]);
            }

            min = numList[0];
            max = numList[0];
            double sum = 0;

            for (int i = 0; i < numList.Length; i++)
            {
                if (numList[i] < min)
                {
                    min = numList[i];
                }
                if (numList[i] > max)
                {
                    max = numList[i];
                }
                sum = sum + numList[i];
            }

            avg = sum / numList.Length;

            rbMin.IsEnabled = true;
            rbMax.IsEnabled = true;
            rbAvg.IsEnabled = true;

            lblValue.Content = "ready";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbMin.IsChecked == true)
            {
                lblValue.Content = min;
            }
            if (rbMax.IsChecked == true)
            {
                lblValue.Content = max;
            }
            if (rbAvg.IsChecked == true)
            {
                lblValue.Content = avg;
            }
        }
    }
}