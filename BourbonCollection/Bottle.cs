using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BourbonCollection
{
    class Bottle
    {
        [Name("Name")]
        public string Name { get; set; }
        [Name("Distillery")]
        public string Distillery { get; set; }
        [Name("Size(ml/L)")]
        public double Size { get; set; }
        [Name("Proof")]
        public double Proof { get; set; }
        [Name("Age")]
        public string Age { get; set; }
        [Name("MSRP")]
        public int MSRP { get; set; }
        [Name("SecondaryPrice")]
        public int SecondaryPrice { get; set; }
        [Name("BottlesOwned")]
        public int BottlesOwned { get; set; }



        public override string ToString()
        {
            return $"{Name},{Distillery},{Size},{Proof},{Age},{MSRP},{SecondaryPrice},{BottlesOwned}";
        }


    }
}
