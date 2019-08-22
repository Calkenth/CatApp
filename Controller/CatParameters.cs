using CatApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatApp.ViewModel
{
    public class CatParameters : Cat
    {
        public CatParameters(string Name, string Race, int Mass, string Food) : base(Name, Race, Mass, Food)
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
        public string lastVetVisit
        {
            set
            {
                _lastVetVisit = DateTime.Parse(value);
            }
            get
            {
                return _lastVetVisit.ToShortDateString();
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
    }
}
