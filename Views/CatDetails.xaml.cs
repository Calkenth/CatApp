﻿using CatApp.ViewModel;
using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
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
using System.Diagnostics;

namespace CatApp.Views
{
    /// <summary>
    /// Interaction logic for CatDetails.xaml
    /// </summary>
    public partial class CatDetails : Window
    {
        string appPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Cats\";
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
                catRace.Text = _selectedCat.catRace;
                catFood.Text = _selectedCat.catFood.ToUpperInvariant();

                if(_selectedCat.birthDate == "01.01.0001")
                {
                    catBirthDate.Text = "the Unborned!";
                }
                else
                {
                    catBirthDate.Text = _selectedCat.birthDate;
                }

                if(_selectedCat.isMale == true)
                {
                    catSex.Content = "[M]";
                }
                else
                {
                    catSex.Content = "[F]";
                }

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
                    lastMed.Text = "dd.mm.yyyy";
                }
                if (lastMed.Text != string.Empty)
                {
                    try
                    {
                        DateTime nextVetVisit = DateTime.Parse(lastMed.Text);
                        DateTime todayDateTime = DateTime.Now;
                        nextVetVisit = nextVetVisit.AddYears(1);
                        nextMed.Text = nextVetVisit.ToShortDateString();
                        double days = nextVetVisit.Subtract(todayDateTime).TotalDays;
                        if (days <= 7)
                        {
                            nextMed.Foreground = new SolidColorBrush(Colors.Red);
                        }
                    } catch(FormatException)
                    {
                        nextMed.Text = "fill last visit date";
                    }
                }
                else
                {
                    nextMed.Text = "No last Vet visit date added!";
                }
            }

            if (_selectedCat.imageSource != null)
            {
                catPicture.Source = new BitmapImage(new Uri(_selectedCat.imageSource));
            }
            return _selectedCat;
        }
        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            _selectedCat.lastVetVisit = lastMed.Text;
            Close();
            var WPF = new CatDetails(_selectedCat);
            WPF.CatDetail();
            WPF.Show();
        }

        private void SaveCatandExit_Click(object sender, RoutedEventArgs e)
        {
            _selectedCat.lastVetVisit = lastMed.Text;
            _selectedCat.birthDate = catBirthDate.Text;
            Close();
        }

        private void LoadPic_Click(object sender, RoutedEventArgs e)
        {
            string iName = _selectedCat.catName;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                string filepath = op.FileName;
                string fileName = "catMainPic";
                string thisCatPath = appPath + iName + @"\" + fileName;
                if(Directory.Exists(appPath + iName) == false)
                {
                    Directory.CreateDirectory(appPath + iName);
                }
                File.Copy(filepath, thisCatPath, true);
                catPicture.Source = new BitmapImage(new Uri(thisCatPath));
                _selectedCat.imageSource = thisCatPath;
            }

        }

        private void Pdf_Click(object sender, RoutedEventArgs e)
        {
            string iName = _selectedCat.catName;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a file with findings";
            {
                Directory.CreateDirectory(appPath + iName);
            }
            op.InitialDirectory = appPath + iName;
            if (op.ShowDialog() == true)
            {
                string filepath = op.FileName;
                string fileName = "catFindings";
                string thisCatPath = appPath + iName + @"\" + fileName;
                if (Directory.Exists(appPath + iName) == false)
                File.Copy(filepath, thisCatPath, true);
                _selectedCat.vetFindings = thisCatPath;
                PDFReader reader = new PDFReader(thisCatPath,_selectedCat);
                reader.Show();
            }
        }
    }
}
