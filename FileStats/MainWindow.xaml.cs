using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
                MessageBox.Show("Error Opening File");
                return;
            }

            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0) //no lines
            {
                MessageBox.Show("Empty File");
                return;
            }

            List<int> numList = new List<int>(); //array to list for dynamic sizing
            int i = 0;
            foreach (string line in lines)
            {
                try
                {
                    numList.Add(int.Parse(line));
                    i++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            if (numList.Count == 0)
            {
                MessageBox.Show("Only strings in file");
                return;
            }


            /*for (int i = 0; i < lines.Length; i++)
            {
                numList[i] = int.Parse(lines[i]);
            }
            Removed this because it worked with a fixed size and led to wrong averages
            */



            min = numList[0];
            max = numList[0];
            double sum = 0;



            for (int j = 0; j < numList.Count; j++) //Length to count because its a List not Array
            {
                if (numList[j] < min)
                {
                    min = numList[j];
                }
                if (numList[j] > max)
                {
                    max = numList[j];
                }
                sum = sum + numList[j];
            }

            avg = sum / numList.Count;

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