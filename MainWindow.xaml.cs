using CatApp.Model;
using CatApp.ViewModel;
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

namespace CatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CatParameters> _catsList = new List<CatParameters>();
        DateTime todayDateTime = DateTime.Now;
        string cat = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            todayDate.Text = todayDateTime.ToShortDateString();
            _catsList.Add(new CatParameters("Fenrir", "Ragdoll", 5, "Barf"));
            foreach(var Cat in _catsList)
            {
                catsList.Items.Add(Cat.catName);
            }
        }

        private void NewCat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CatParameters newCat = new CatParameters(newCatName.Text, newCatRace.Text, Convert.ToInt32(newCatMass.Text), food.Text);
                _catsList.Add(newCat);

                catsList.Items.Add(newCat.catName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveCat_Click(object sender, RoutedEventArgs e)
        {
            if (catsList.SelectedItem != null)
            {
                catsList.Items.Remove(catsList.SelectedItem);
            }
            else
            {
                MessageBox.Show("Choose a cat!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowCat_Click(object sender, RoutedEventArgs e)
        {
            cat = Convert.ToString(catsList.SelectedItem);
            CatParameters selectedCat = _catsList.Find(selCat => selCat.catName == cat);
            if (selectedCat == null)
            {
                MessageBox.Show("Choose a cat!","ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                catName.Text = selectedCat.catName;
                catMass.Text = Convert.ToString(selectedCat.catMass) +" kg";
                if(selectedCat.catFood.Equals("barf",StringComparison.OrdinalIgnoreCase))
                {
                    dailyFoodNeed.Text = "300 g";
                }
                else if (selectedCat.catFood.Equals("dry", StringComparison.OrdinalIgnoreCase))
                {
                    double foodNeed = 0.27 * 70 * selectedCat.catMass;
                    dailyFoodNeed.Text = Convert.ToString(foodNeed) + " g";
                }
                else if (selectedCat.catFood.Equals("can", StringComparison.OrdinalIgnoreCase))
                {
                    double foodNeed = 100 * selectedCat.catMass;
                    dailyFoodNeed.Text = Convert.ToString(foodNeed) + " g";
                }
                lastMed.Text = selectedCat.lastVetVisit;
                if(lastMed.Text == "01.01.0001")
                {
                    lastMed.Text = "go to Vet!";
                }
                if (lastMed.Text != string.Empty)
                {
                    try
                    {
                        DateTime nextVetVisit = DateTime.Parse(lastMed.Text);
                        nextVetVisit = nextVetVisit.AddDays(30);
                        nextMed.Text = nextVetVisit.ToShortDateString();
                        double days = nextVetVisit.Subtract(todayDateTime).TotalDays;
                        if (days <= 7)
                        {
                            nextMed.Foreground = new SolidColorBrush(Colors.Red);
                        }
                    }
                    catch(FormatException)
                    {
                        nextMed.Text = "go to Vet!NOW!";
                    }
                }
                else
                {
                    nextMed.Text = "brak daty badania!";
                }
            }

        }

        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            cat = Convert.ToString(catsList.SelectedItem);
            CatParameters selectedCat = _catsList.Find(selCat => selCat.catName == cat);
            selectedCat.lastVetVisit = lastMed.Text;
        }
    }
}
