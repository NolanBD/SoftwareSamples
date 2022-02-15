using Models;
using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.Text.RegularExpressions;
=======
>>>>>>> Stashed changes
using View.Interfaces;

namespace View
{
    class UserIOConsoleImpl : IUserIO
    {

        public UserIOConsoleImpl()
        {
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public bool ReadBool(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            bool isProperInput = false;
            bool response = false;

            while (!isProperInput)
            {
                if (input.ToLower() == "y")
                {
                    isProperInput = true;
                    response = true;
                }
                else if (input.ToLower() == "n")
                {
                    isProperInput = true;
                    response = false;
                }
                else
                {
                    isProperInput = false;
                    Console.WriteLine("The value you entered must be either \"Y\" or \"N\"");
                    Console.WriteLine("Please re-enter your answer:");
                    input = Console.ReadLine();
                }
            }

            return response;
        }

        public DateTime ReadDate(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            DateTime orderDate = new DateTime();
            bool isDate = false;

            while (!isDate)
            {
                if (DateTime.TryParse(input, out orderDate))
                {
                    isDate = true;
                }
                else
                {
                    isDate = false;
<<<<<<< Updated upstream
                    Console.WriteLine("Order Date must be a numeric date IE: 01/02/2022");
                    Console.WriteLine("Order Date cannot exceed the year 9999");
=======
                    Console.WriteLine("Please enter a numeric date IE: 01/02/2022");
>>>>>>> Stashed changes
                    Console.WriteLine("Please re-enter your answer:");
                    input = Console.ReadLine();
                }
            }

            return orderDate;
        }

        public DateTime ReadDateAfterCurrentDate(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            bool isAfterToday = false;
            DateTime currentDate = DateTime.Now;
            DateTime orderDate = new DateTime();

            while (!isAfterToday)
            {
                if (DateTime.TryParse(input, out orderDate))
                {
                    if(orderDate > currentDate)
                    {
                        isAfterToday = true;
                    }
                    else
                    { 
                        isAfterToday = false;
                        Console.WriteLine("Your order fulfilment date must be later than today's date");
                        Console.WriteLine("Please re-enter your answer:");
                        input = Console.ReadLine();
                    }
                }
                else
                {
                    isAfterToday = false;
<<<<<<< Updated upstream
                    Console.WriteLine("Order Date must be a numeric date IE: 01/02/2022");
                    Console.WriteLine("Order Date cannot exceed the year 9999");
=======
                    Console.WriteLine("Please enter a numeric date IE: 01/02/2022");
>>>>>>> Stashed changes
                    Console.WriteLine("Please re-enter your answer:");
                    input = Console.ReadLine();
                }
            }

            return orderDate;
        }

        public decimal ReadDecimal(string prompt, decimal min)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            decimal output = 0;
            bool isDecimal = false;

            //making sure the input is a decimal
            while (!isDecimal)
            {
                //parsing input and, if it is a decimal, assigning it to our output
                if (decimal.TryParse(input, out output))
                {
                    isDecimal = true;
                }
                //if the value is not a decimal then ask the user to resubmit their input
                else
                {
                    Console.WriteLine("The value you entered must be a decimal");
                    Console.WriteLine("Please re-enter your value:");
                    input = Console.ReadLine();
                }
                //if our output is less than our pre-defined value, set isDecimal to false and ask the user to resubmit an input in our range
                if (output < min)
                {
                    isDecimal = false;
                    Console.WriteLine("The value you entered must be greater than 100.");
                    Console.WriteLine("Please re-enter your value:");
                    input = Console.ReadLine();
                }
            }

            return output;
        }

        public int ReadInt(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            int output = 0;
            bool isInt = false;

            //making sure the input is an int
            while (!isInt)
            {
                //parsing input and, if it is an int, assigning it to our output
                if (int.TryParse(input, out output))
                {
                    isInt = true;
                }
                //if the value is not an int then ask the user to resubmit their input
                else
                {
                    isInt = false;
                    Console.WriteLine("The value you entered must be an integer");
                    Console.WriteLine("Please re-enter your number:");
                    input = Console.ReadLine();
                }
            }

            return output;
        }

        public string ReadString(string prompt)
        {
<<<<<<< Updated upstream
            Regex rgx = new Regex("[^a-zA-Z0-9 , .]");
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            input = rgx.Replace(input, "");
=======
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

>>>>>>> Stashed changes
            return input;
        }

        public StateTax ReadState(List<StateTax> list)
        {
            bool matchesList = false;
            string input = Console.ReadLine();
            StateTax selectedState = new StateTax();

            while (!matchesList)
            {
                if (input.Length == 2)
                {
                    foreach (var member in list)
                    {
                        if (input.ToUpper() == member.StateAbbreviation)
                        {
                            matchesList = true;
                            selectedState = member;
<<<<<<< Updated upstream
                            break;
                        }
                    }
                    if (!matchesList)
                    {
                        Console.WriteLine("No state matching your selection exists");
                        Console.WriteLine("Please resubmit your choice:");
                        input = Console.ReadLine();
                    }
                }

=======
                            break; 
                        }
                    }
                }
>>>>>>> Stashed changes
                else
                {
                    Console.WriteLine("Not a valid entry");
                    Console.WriteLine("Please resubmit your choice:");
                    input = Console.ReadLine();
                }
            }
            
            return selectedState;
        }

        public Product ReadProduct(List<Product> list)
        {
            bool matchesList = false;
            string input = Console.ReadLine();
            Product selectedProduct = new Product();

            while (!matchesList)
            { 
                foreach (var member in list)
                {
                    if (input == member.ProductType)
                    {
                        matchesList = true;
                        selectedProduct = member;
                        break;
                    }
                }

                if (!matchesList)
                {
                    Console.WriteLine("Not a valid entry");
                    Console.WriteLine("Please resubmit your choice:");
                    input = Console.ReadLine();
                }
            }

            return selectedProduct;
        }
    }
}
