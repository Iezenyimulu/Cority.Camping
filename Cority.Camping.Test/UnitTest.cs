using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Cority.Camping.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void WriteBalanceToOutputFile()
        {
            //arrange
            double balance = -8.355;
            string expected = "($8.36)";

            //act  
            var outputFile = File.Create("test1.txt.out");
            outputFile.Close();
            CampingTrip camping = new CampingTrip(outputFile.Name);
            CampingTrip.WriteBalance(balance);

            //assert
            using (StreamReader sr = new StreamReader(outputFile.Name))
            {
                var content = sr.ReadToEnd();
                if (!content.Contains(expected))
                {
                    throw new Exception(String.Format($"WritBalanceToOutputFile Test Failed: Wrong Output, expexcted {expected}"));
                }
            }
        }

        // test average computation
        [TestMethod]
        public void SplitBill_Average()
        {
            //arrange
            List<double> trip = new List<double> { 6, 4.25 };
            double expected = 5.125;
            var outputFile = File.Create("test2.txt.out");
            outputFile.Close();
            CampingTrip camping = new CampingTrip(outputFile.Name);

            //act
            CampingTrip.SplitBill(trip);
            double actual = trip.Average();

            //assert
            Assert.AreEqual(expected, actual, "Error SplitBill_Average failed: could not compute average");
        }


        [TestMethod]
        public void GetTripCharges_SplitBill_WriteBalance()
        {
            //arrange
            string[] text = { "2", "2", "5.60", "5.00", "2", "9.75", "3.50" };
            string expected_1 = "($1.33)";
            string expected_2 = "$1.33";

            //act
            var outputFile = File.Create("test3.txt.out");
            outputFile.Close();
            CampingTrip camping = new CampingTrip(outputFile.Name);
            CampingTrip.GetTripCharges(text);

            //assert
            using (StreamReader sr = new StreamReader(outputFile.Name))
            {
                var content = sr.ReadToEnd();
                if (!content.Contains(expected_1) && content.Contains(expected_2))
                {
                    throw new Exception("Error: GetTripCharges_SplitBill_WriteBalance() failed. \n " +
                        "Charges and Bill not caculated\n No output");
                }
            }

        }
    }
}
