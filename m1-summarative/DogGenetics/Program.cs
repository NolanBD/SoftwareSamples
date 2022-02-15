using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogGenetics
{
    class Program
    {
        static void Main(string[] args)
        {
            string dogName = "";
            Random randomizer = new Random();
            double percentage = 0;
            double[] storedPercents = new double[5];
            double runningTotal = 0;
            double newTotal = 0;

            //generating random values to populate the storedPercents array
           for (int i = 0; i < 5; i++)
           {
               percentage = randomizer.Next(1, 100) + 1;
               storedPercents[i] = percentage;
               runningTotal += percentage;
           }
           //taking the random values, dividing each value of the array by the runningTotal
           //this "normalizes" the values to be in a range of 0 to 1. These values are
           //then multiplied by 100 to create a percent value, then rounded down to the nearest
           //whole number
           for (int x = 0; x < 5; x++)
           {
               storedPercents[x] = Math.Floor((storedPercents[x] / runningTotal) * 100);
               newTotal += storedPercents[x];
           }
           //the last value of the array is set to the value plus 100 minus the current total inorder
           //to guarantee the sum of all elements the array are equal to 100
            storedPercents[4] += 100 - newTotal;
            newTotal = storedPercents.Sum();

            Console.WriteLine("What is your dog's name?");
            dogName = Console.ReadLine();
            Console.WriteLine(dogName + "? Great name! Here is their genetic makeup!");
            Console.WriteLine(storedPercents[0] + "% Keeshound");
            Console.WriteLine(storedPercents[1] + "% Beagle");
            Console.WriteLine(storedPercents[2] + "% French Mastiff");
            Console.WriteLine(storedPercents[3] + "% German Shepherd");
            Console.WriteLine(storedPercents[4] + "% Afghan Hound");
            Console.WriteLine("\nWow, " + newTotal + "% dog!");
            Console.ReadLine();
        }
    }
}
