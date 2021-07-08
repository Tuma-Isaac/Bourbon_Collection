using System;
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

            // List of options 

            StringBuilder options = new StringBuilder();

            options.Append("\n");
            options.Append("\nWelcome to the Bourbon Collection");
            options.Append("\n****************************");
            options.Append("\nTo view the current collection, press 1.");
            options.Append("\nTo add a new bottle, press 2");
            options.Append("\nTo update a current bottle, press 3");
            options.Append("\nTo remove a bottle, press 4");
            options.Append("\nTo calculate the value of the collection, press 5");
            options.Append("\n****************************");
            options.Append("\nType quit to quit");

            while (true)
            {
                try
                {
                    Console.WriteLine(options);
                    var response = Console.ReadLine();
                  

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



                    }
                    // Update an existing bottle count
                    else if (Int32.Parse(response) == 3)
                    {
                       
                    }
                    // Remove a bottle from collection
                    else if (Int32.Parse(response) == 4)
                    {
                       
                    }
                    // Calculate value of collection
                    else if (Int32.Parse(response) == 5)
                    {
                      
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

