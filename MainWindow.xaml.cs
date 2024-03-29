﻿using CatApp.Model;
using CatApp.ViewModel;
using CatApp.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        bool catSex = false;
        string appPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Cats";

        public MainWindow()
        {
            InitializeComponent();
            todayDate.Text = todayDateTime.ToShortDateString();
            _catsList.Add(new CatParameters("Fenrir", "Ragdoll", 5, "Barf",true));
            Directory.CreateDirectory(appPath + @"\" + "Fenrir");
            foreach (var Cat in _catsList)
            {
                catsList.Items.Add(Cat.catName);
            }
            Closing += MainWindow_Closing;
        }
        // delete event after creating pernament DB
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Directory.Delete(appPath,true);
        }

        private void NewCat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = MessageBox.Show("Is " + newCatName.Text +" male?", "One more thing", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(res == MessageBoxResult.Yes)
                {
                    catSex = true;
                }
                CatParameters newCat = new CatParameters(newCatName.Text, newCatRace.Text, Convert.ToInt32(newCatMass.Text), food.Text,catSex);
                newCat.weightDate = DateTime.Now;
                _catsList.Add(newCat);

                catsList.Items.Add(newCat.catName);
                Directory.CreateDirectory(appPath + @"\" + newCatName.Text);
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
            if (catsList.SelectedItem != null)
            {
                string name = catsList.SelectedItem.ToString();
                CatParameters selectedCat = _catsList.Find(selCat => selCat.catName == name);
                var newWPF = new CatDetails(selectedCat);
                newWPF.CatDetail();
                newWPF.Show();
            }
            else
            {
                MessageBox.Show("Choose a cat!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        
    }
}
