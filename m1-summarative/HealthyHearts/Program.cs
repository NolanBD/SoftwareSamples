using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyHearts
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxRange = 0;
            string userInput = "";
            int userAge = 0;
            bool isInputValid = true;
            int lowRange = 0;
            int highRange = 0;

            Console.WriteLine("How old are you?");
            userInput = Console.ReadLine();

            //validating user input then calculating their maximum heart rate
            while (isInputValid)
            {
                if (int.TryParse(userInput, out userAge))
                {
                    if (userAge < 0)
                    {
                        Console.WriteLine("Your age must be at least 0");
                        Console.WriteLine("How old are you?");
                        userInput = Console.ReadLine();
                    }
                    maxRange = 220 - userAge;
                    isInputValid = false;
                }
                if (!int.TryParse(userInput, out userAge))
                {
                    Console.WriteLine("Your age has to be a number!");
                    Console.WriteLine("How old are you?");
                    userInput = Console.ReadLine();
                }
            }
            //calculating the target hear rate zone
            lowRange = Convert.ToInt32(Math.Round(maxRange * .5f));
            highRange = Convert.ToInt32(Math.Round(maxRange * .85f));
            Console.WriteLine("Your maximum heart rate should be " + maxRange + " beats per minute.");
            Console.WriteLine("Your target heart rate zone is " + lowRange + " - " + highRange +
                " beats per minute");
            Console.ReadLine();
        }
    }
}
