using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cority.Camping
{
    public class CampingTrip
    {
        public static double Average;
        public static string OutputFile { get; private set; }


        public CampingTrip(string outputFile)
        {
            OutputFile = outputFile;
        }

        //get  receipts/charges  for each trip
        public static void GetTripCharges(string[] text)
        {
            int numberOfParticipants = 0;
            double totalCharge = 0; //total charge for each participant 
            double charge = 0;
            int numberOfReceipt = 0;
            List<double> trip = new List<double>();

            if (text.Length > 0)
            {
                for (int i = 0; i < text.Length;)
                {
                    //get number of participants in this trip
                    int.TryParse(text[i], out numberOfParticipants);
                    i++;
                    //check if end of file
                    if (numberOfParticipants > 0)
                    {
                        if (trip.Count > 0)
                            trip.Clear();

                        for (int k = 0; k < numberOfParticipants; k++)
                        {
                            totalCharge = 0;

                            //get number of receipts paid by each person 
                            int.TryParse(text[i], out numberOfReceipt);
                            i++;
                            //get amount paid
                            for (int j = 0; j < numberOfReceipt; j++)
                            {
                                double.TryParse(text[i], out charge);
                                totalCharge += charge;
                                i++;
                            }

                            // add first person on this trip to array
                            trip.Add(totalCharge);
                        }
                        SplitBill(trip);
                    }

                }
            }
        }

        //balance bills for each person in each trip
        public static void SplitBill(List<double> trip)
        {
            Average = 0;
            double balance = 0;
            //share total cost equal among participants
            Average = trip.Average();
            foreach (var p in trip)
            {
                balance = Average - p;
                WriteBalance(balance);
            }
        }

        //writes split bill to output file
        public static void WriteBalance(double balance)
        {
            using (StreamWriter sw = new StreamWriter(OutputFile, true))
            {
                sw.WriteLine(balance.ToString("C", new System.Globalization.CultureInfo("en-US", false).NumberFormat));
            }
        }
    }
}
