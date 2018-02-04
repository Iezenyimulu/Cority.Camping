using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cority.Camping
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("No file specified.");
                Console.ReadLine();
            }
            else
            {
                var inputFile =   args[0].Trim()+".txt";

                if (!File.Exists(inputFile))
                {
                    return;
                }

                var outputFile = File.Create(inputFile + ".out");
                outputFile.Close();                

                string[] text = File.ReadAllLines(inputFile);

                CampingTrip camping = new CampingTrip(outputFile.Name);

                CampingTrip.GetTripCharges(text);

                Console.WriteLine($"Camping bills are ready in the file: \n {outputFile.Name}");
                Console.ReadLine();

            }
        }
    }
}
