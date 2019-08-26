using CatApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatApp.ViewModel
{
    public class CatParameters : Cat
    {
        public CatParameters(string Name, string Race, int Mass, string Food, bool isMale) : base(Name, Race, Mass, Food, isMale)
        {
            if (!(Food.Equals("dry", StringComparison.OrdinalIgnoreCase) || 
                Food.Equals("barf", StringComparison.OrdinalIgnoreCase) || 
                Food.Equals("can", StringComparison.OrdinalIgnoreCase)))
            {
                throw new FormatException(message:"Food can be described as: DRY/BARF/CAN");
            }
        }
        public string catName
        {
            get
            {
                return _name;
            }
        }
        public string catRace
        {
            get
            {
                return _race;
            }
        }
        public int catMass
        {
            set
            {
                _mass = value;
            }
            get
            {
                return _mass;
            }
        }
        public string catFood
        {
            get
            {
                return _food;
            }
        }
        public bool isMale { get
            {
                return _isMale;
            }
        }
        public string lastVetVisit
        {
            set
            {
                try
                {
                    _lastVetVisit = DateTime.Parse(value);
                }
                catch(FormatException)
                {
                    MessageBox.Show("Last Vet visit date was not filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            get
            {
                return _lastVetVisit.ToShortDateString();
            }
        }
        public string vetFindings
        {
            get
            {
                return _vetFindings;
            }
            set
            {
                _vetFindings = value;
            }
        }
        public string imageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
            }
        }
        public string birthDate
        {
            set
            {
                try
                {
                    _birthDate = DateTime.Parse(value);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Birth date is not filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            get
            {
                return _birthDate.ToShortDateString();
            }
        }
        public DateTime weightDate
        {
            set
            {
                _weightDate = value;
            }
            get
            {
                return _weightDate;
            }
        }
    }
}
