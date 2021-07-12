﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BourbonCollection
{
    class BottleFunctions
    {
        public static List<Bottle> _bourbonBottles;

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

        // Add a new bottle to file/collection
        public static void AddBottle(Bottle bottle, string filepath)
        {

            using (StreamWriter writer = new StreamWriter(@filepath, true))
            {

                writer.Write($"\n{bottle.Name},{bottle.Distillery},{bottle.Size},{bottle.Proof},{bottle.Age},{bottle.MSRP},{bottle.SecondaryPrice},{bottle.BottlesOwned}");
            }

        }

        // Add/Remove another bottle to the collection if bottle is already in collection
        public static void UpdateBottle(string Name, int amount, string filepath)
        {
            _bourbonBottles = ReadBourbonCollection(filepath);
            using (var writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine("Name,Distillery,Size(ml/L),Proof,Age,MSRP,SecondaryPrice,BottlesOwned");
                foreach (var bottle in _bourbonBottles)
                {
                    if (bottle.Name.ToLower() == Name.ToLower())
                    {
                        bottle.BottlesOwned += amount;
                    }

                    writer.WriteLine(bottle);
                }
            }
        }

        //Remove a bottle
        public static void RemoveBottle(string Name, string filepath)
        {
            _bourbonBottles = ReadBourbonCollection(filepath);
            using (var writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine("Name,Distillery,Size(ml/L),Proof,Age,MSRP,SecondaryPrice,BottlesOwned");
                foreach (var bottle in _bourbonBottles)
                {
                    if (bottle.Name.ToLower() != Name.ToLower())
                    {
                        writer.WriteLine(bottle);
                    }
                }

            }

        }


        // Calculate the value of the collection

        public static void CalcValue(List<Bottle> bottles)
        {
            int msrpValue = 0;
            int secondaryValue = 0;

            foreach (var bottle in bottles)
            {
                if (bottle.BottlesOwned >= 1)
                {
                    bottle.MSRP *= bottle.BottlesOwned;
                    bottle.SecondaryPrice *= bottle.BottlesOwned;
                }

                msrpValue += bottle.MSRP;

                secondaryValue += bottle.SecondaryPrice;

            }
            Console.WriteLine($"The MSRP value of your collection is: ${msrpValue}");
            Console.WriteLine($"The secondary value of your collection is: ${secondaryValue}");

        }


        //Print bottles to console 
        public static void PrintBottles(List<Bottle> bottles)
        {
            foreach (var bottle in bottles)
            {
                Console.WriteLine(bottle);
            }

        }
    }
}