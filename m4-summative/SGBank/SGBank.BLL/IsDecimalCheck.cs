using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL
{
    public class IsDecimalCheck
    {
        public static decimal DecimalCheck(string input)
        {
            decimal amount = 0;
            bool isDecimal = false;

            //making sure the input is a decimal
            while (!isDecimal)
            {
                //parsing input and, if it is a decimal, assigning it to our withdraw ammount
                if (decimal.TryParse(input, out amount))
                {
                    isDecimal = true;
                }
                //if the value is not a decimal then ask the user to resubmit their input
                else
                {
                    Console.WriteLine("The value you entered must be a decimal");
                    Console.WriteLine("Please re-enter your amount:");
                    input = Console.ReadLine();
                }
            }

            return amount;
        }
    }
}
