using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensManager.View
{
    public class UserIO
    {
        public string ReadString(string prompt)
        {
            string userInput = "";
            //while userInput is empy
            while(userInput == "")
            {
                //a prompt is written asking user for input
                Console.WriteLine(prompt);
                //user provides input, erroneous spaces are trimed
                userInput = Console.ReadLine().Trim();

                //if after the input is collected and the value of string is still ""
                //an error message is given
                if(userInput == "")
                {
                    Console.WriteLine("That was not a valid input, please try again.");
                }
            }
            //valid input is returned
            return userInput;
        }

        //collecting int inputs from user
        public int ReadInt(string prompt, int min, int max)
        {
            int output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                //validating input as int
                if(int.TryParse(userInput, out output))
                {
                    if(output >= min && output <= max)
                    {
                        //input is valid
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your number must be between {0} and {1}. Please try again.", 5, 800);
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid input, whole numbers only! Please try again.");
                }
            }
            return output;
        }

        //collecting doubble input from user
        public double ReadDouble(string prompt, double min, double max)
        {
            double output;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                //validating input as double
                if(double.TryParse(userInput, out output))
                {
                    if (output >= min && output <= max)
                    {
                        //input is valid
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your input must be between {0} and {1} please try again.", .75, 22.00);
                    }
                }
                else
                {
                    Console.WriteLine("Your input must be a floating-point number (i.e., 1.2, 22.0, 4.5). Please try again.");
                }
            }

            return output;
        }

        //collecting bool input from user
        public bool ReadBool(string prompt)
        {
            bool output = true;

            while (true)
            {
                Console.WriteLine(prompt);
                string userInput = Console.ReadLine();
                //normalizing input to always be uppercase
                string normalizedInput = userInput.ToUpper();

                if (normalizedInput == "YES")
                {
                    //valid input
                    output = true;
                    break;
                }
                if (normalizedInput == "NO")
                {
                    //valid input
                    output = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Please respond with either \"yes\" or \"no\". Please try again.");
                }
            }

            return output;
        }
    }
}
