using CatApp.ViewModel;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace CatApp.Views
{
    /// <summary>
    /// Interaction logic for PDFReader.xaml
    /// </summary>
    public partial class PDFReader : Window
    {
        private string _pdf { get; set; }
        private CatParameters _selectedCat { get; set; }

        public PDFReader(string pdf, CatParameters selectedCat)
        {
            _pdf = pdf;
            _selectedCat = selectedCat;

            InitializeComponent();

            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Navigate(new Uri(_pdf));
            PDF.Content = webBrowser;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select new file with Vet findings";

            if (op.ShowDialog() == true)
            {
                string filepath = op.FileName;
                string thisCatPath = _pdf + "_" + DateTime.Now.ToShortDateString();
                File.Copy(filepath, thisCatPath, true);
                _selectedCat.vetFindings = thisCatPath;
            }
        }
    }
}
