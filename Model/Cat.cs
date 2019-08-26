using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatApp.Model
{
    public class Cat
    {
        protected string _race { get; set; }
        protected int _mass { get; set; }
        protected string _name { get; set; }
        protected string _food { get; set; }
        protected DateTime _lastVetVisit { get; set; }
        protected string _vetFindings { get; set; }
        protected string _imageSource { get; set; }
        protected DateTime _birthDate { get; set; }
        protected bool _isMale { get; set; }
        protected DateTime _creationDate { get; set; }

        public Cat(string Name,string Race, int Mass,string Food, bool isMale)
        {
            _name = Name;
            _race = Race;
            _mass = Mass;
            _food = Food;
            _isMale = isMale;
        }
    }
}
