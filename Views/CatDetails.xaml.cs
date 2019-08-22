using CatApp.ViewModel;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace CatApp.Views
{
    /// <summary>
    /// Interaction logic for CatDetails.xaml
    /// </summary>
    public partial class CatDetails : Window
    {
        string cat = string.Empty;
        private CatParameters _selectedCat;

        public CatDetails(CatParameters selectedCat)
        {
            _selectedCat = selectedCat;
        }

        public CatParameters CatDetail()
        {
            InitializeComponent();

            if (_selectedCat == null)
            {
                MessageBox.Show("Choose a cat!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                catName.Text = _selectedCat.catName;
                catMass.Text = Convert.ToString(_selectedCat.catMass) + " kg";
                if (_selectedCat.catFood.Equals("barf", StringComparison.OrdinalIgnoreCase))
                {
                    dailyFoodNeed.Text = "300 g";
                }
                else if (_selectedCat.catFood.Equals("dry", StringComparison.OrdinalIgnoreCase))
                {
                    double foodNeed = 0.27 * 70 * _selectedCat.catMass;
                    dailyFoodNeed.Text = Convert.ToString(foodNeed) + " g";
                }
                else if (_selectedCat.catFood.Equals("can", StringComparison.OrdinalIgnoreCase))
                {
                    double foodNeed = 100 * _selectedCat.catMass;
                    dailyFoodNeed.Text = Convert.ToString(foodNeed) + " g";
                }
                lastMed.Text = _selectedCat.lastVetVisit;
                if (lastMed.Text == "01.01.0001")
                {
                    lastMed.Text = "go to Vet!";
                }
                if (lastMed.Text != string.Empty)
                {
                    try
                    {
                        DateTime nextVetVisit = DateTime.Parse(lastMed.Text);
                        DateTime todayDateTime = DateTime.Now;
                        nextVetVisit = nextVetVisit.AddDays(30);
                        nextMed.Text = nextVetVisit.ToShortDateString();
                        double days = nextVetVisit.Subtract(todayDateTime).TotalDays;
                        if (days <= 7)
                        {
                            nextMed.Foreground = new SolidColorBrush(Colors.Red);
                        }
                    }
                    catch (FormatException)
                    {
                        nextMed.Text = "go to Vet!NOW!";
                    }
                }
                else
                {
                    nextMed.Text = "No last Vet visit date added!";
                }
            }

            if(_selectedCat.imageSource == null)
            {
                string imgSc = @"C:\Users\Plucio\source\repos\CatApp\Resources\anonymousCat.jpg";
                catPicture.Source = new BitmapImage(new Uri(imgSc));
            }
            else
            {
                catPicture.Source = new BitmapImage(new Uri(_selectedCat.imageSource));
            }
            return _selectedCat;
        }
        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            _selectedCat.lastVetVisit = lastMed.Text;
            Close();
        }

        private void LoadPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                catPicture.Source = new BitmapImage(new Uri(op.FileName));
                _selectedCat.imageSource = op.FileName;
            }

        }
    }
}
