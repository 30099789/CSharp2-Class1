using Microsoft.Win32;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Multiplication button click event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMulti_Click(object sender, RoutedEventArgs e)
        {
            double number;
            double number2;
            // Validate input fields
            if (string.IsNullOrWhiteSpace(barN1.Text) || string.IsNullOrWhiteSpace(barN2.Text))
            {
                MessageBox.Show("Please enter both numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            // Try to parse the input as double
            if (!double.TryParse(barN1.Text, out number) || !double.TryParse(barN2.Text, out number2))
            {
                MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                barN1.Clear();
                barN2.Clear();
                return;
            }
            // Perform multiplication and show result
            double result = number * number2;
            MessageBox.Show($"Multiplication of {number} and {number2} is: {result}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
            // Save result to a file
            SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                    DefaultExt = "txt",
                    FileName = $"calc.txt"
                };
            // Show save file dialog
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        writer.WriteLine($"Multiplication of {number} and {number2} is: {result}");
                    }
                    MessageBox.Show("Result saved successfully!", "Success", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (MessageBox.Show("Do you want to clear the input fields?", "Clear Input", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    barN1.Clear();
                    barN2.Clear();
                }
            }
        }
    }
}