using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BourbonCollection
{
    class Bottle
    {
        public static List<Bottle> _bourbonBottles;

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

        // Read data from file
        public static List<Bottle> ReadBourbonCollection(string filepath)
        {
            var bourbonCollection = new List<Bottle>();

            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                bourbonCollection = csv.GetRecords<Bottle>().OrderBy(x => x.Distillery).ToList();
            }

            return bourbonCollection;
        }


    }
}
