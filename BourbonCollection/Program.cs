﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Text;

namespace BourbonCollection
{
    class Program
    {
        private static List<Bottle> _bourbonBottles;
        static void Main(string[] args)
        {
            // Get file path
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "BourbonCollection.csv");
            _bourbonBottles = ReadBourbonCollection(fileName);

            //Cosmetic stuff
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();

            // List of options 

            StringBuilder options = new StringBuilder();

            options.Append("\n");
            options.Append("\n             Welcome to the Bourbon Bank               ");
            options.Append("\n*******************************************************");
            options.Append("\n*  To view the current collection, press 1            *");
            options.Append("\n*  To add a new bottle, press 2                       *");
            options.Append("\n*  To update a current bottle, press 3                *"); 
            options.Append("\n*  To remove a bottle, press 4                        *");
            options.Append("\n*  To calculate the value of the collection, press 5  *");
            options.Append("\n*  Type quit to quit                                  *");
            options.Append("\n*******************************************************");
         

            while (true)
            {
                try
                {
                    Console.WriteLine(options);
                    var response = Console.ReadLine();
                    int intParse;
                    double doubleParse;


                    if (response.ToLower() == "quit")
                    {
                        break;
                    }

                    // View current collection
                    else if (Int32.Parse(response) == 1)
                    {
                        _bourbonBottles = ReadBourbonCollection(fileName);
                        Console.WriteLine();
                        PrintBottles(_bourbonBottles);
                        continue;

                    }
                    // Add a new bottle to the collection
                    else if (Int32.Parse(response) == 2)
                    {
                        var bottle = new Bottle();


                        Console.WriteLine("Please enter the name of the bottle.");
                        bottle.Name = Console.ReadLine();
                        Console.WriteLine("Please enter the Distillery name");
                        bottle.Distillery = Console.ReadLine();

                        Console.WriteLine("Please enter the size of the bottle in ml/L.");
                        if (double.TryParse(Console.ReadLine(), out doubleParse))
                        {
                            bottle.Size = doubleParse;
                        }
                        Console.WriteLine("Please enter the bottles proof.");
                        if (double.TryParse(Console.ReadLine(), out doubleParse))
                        {
                            bottle.Proof = doubleParse;
                        }
                        Console.WriteLine("Please enter the bottles age. If the age is unknown, please enter NAS for No Age Statement.");
                        bottle.Age = Console.ReadLine();
                        Console.WriteLine("Please enter the MSRP value.");
                        if (Int32.TryParse(Console.ReadLine(), out intParse))
                        {
                            bottle.MSRP = intParse;
                        }
                        Console.WriteLine("Please enter the secondary value.");
                        if (Int32.TryParse(Console.ReadLine(), out intParse))
                        {
                            bottle.SecondaryPrice = intParse;

                        }
                        Console.WriteLine("Please enter the number of bottles you own");
                        if (Int32.TryParse(Console.ReadLine(), out intParse))
                        {
                            bottle.BottlesOwned = intParse;
                        }
                        AddBottle(bottle, "BourbonCollection.csv");
                        continue;



                    }
                    // Update an existing bottle count
                    else if (Int32.Parse(response) == 3)
                    {
                        var amountofBottles = 0;
                        Console.WriteLine("Which bottle would you like to update?");
                        var updatedBottle = Console.ReadLine();
                        Console.WriteLine("How many bottles would you like to add or subtract? Type - for subtraction.");
                        if (Int32.TryParse(Console.ReadLine(), out intParse)) { amountofBottles = intParse; }
                        UpdateBottle(updatedBottle, amountofBottles, "BourbonCollection.csv");
                        continue;

                    }
                    // Remove a bottle from collection
                    else if (Int32.Parse(response) == 4)
                    {
                        Console.WriteLine("What bottle would you like to remove from the collection?");
                        var bottleRemoved = Console.ReadLine();
                        RemoveBottle(bottleRemoved, "BourbonCollection.csv");
                        continue;
                    }
                    // Calculate value of collection
                    else if (Int32.Parse(response) == 5)
                    {
                        CalcValue(_bourbonBottles);
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
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

        // Add a new bottle to file/collection
        private static void AddBottle(Bottle bottle, string filepath)
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
                    bottle.MSRP = bottle.MSRP * bottle.BottlesOwned;
                    bottle.SecondaryPrice = bottle.SecondaryPrice * bottle.BottlesOwned;
                }

                msrpValue += bottle.MSRP;

                secondaryValue += bottle.SecondaryPrice;

            }
            Console.WriteLine($"The MSRP value of your collection is: ${msrpValue}");
            Console.WriteLine($"The secondary value of your collection is: ${secondaryValue}");

        }


        //Print bottles to console 
        static void PrintBottles(List<Bottle> bottles)
        {
            foreach (var bottle in bottles)
            {
                Console.WriteLine(bottle);
            }

        }
    }
}

