using CatApp.Model;
using CatApp.ViewModel;
using CatApp.Views;
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
                string name = catsList.SelectedItem.ToString();

                catsList.Items.Remove(catsList.SelectedItem);
                _catsList.Remove(_catsList.Find(cat => cat.catName == name));
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
            var newWPF = new CatDetails(selectedCat);
            newWPF.CatDetail();
            newWPF.Show();
        }
    }
}
