using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummativeSums
{
    //creating a new class to contain the AddingElements method
    public class Calculations
    {
        //a method that calculates the sum of all elements in an
        //array and returns that value
        public static int AddingElements(int[] arrayInput)
        {
            int answer;
            int addend;
            int sum = 0;
            for (int i = 0; i < arrayInput.Length; i++)
            {
                addend = arrayInput[i];
                sum = sum + addend;
            }
            answer = sum;
            return answer;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] arrayOne = new int[] { 1, 90, -33, -55, 67, -16, 28, -55, 15 };
            int[] arrayTwo = new int[] { 999, -60, -77, 14, 160, 301 };
            int[] arrayThree = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130,
                140, 150, 160, 170, 180, 190, 200, -99 };
            int arraySum = Calculations.AddingElements(arrayOne);
            Console.WriteLine("Array #1 sum: " + arraySum);
            arraySum = Calculations.AddingElements(arrayTwo);
            Console.WriteLine("Array #2 sum: " + arraySum);
            arraySum = Calculations.AddingElements(arrayThree);
            Console.WriteLine("Array #3 sum: " + arraySum);
            Console.ReadLine();
        }
           
    }

}
