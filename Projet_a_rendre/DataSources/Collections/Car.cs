using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCSharpàRendre.DataSources.Collections
{
    internal class Car
    {
        public int Id { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public int Year { get; set; }


        public Car(int id, string marque, string modele, int year)
        {
            Id = id;
            Marque = marque;
            Modele = modele;
            Year = year;
        }
    }
}
