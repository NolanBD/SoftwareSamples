using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensManager.Models;

namespace LensManager.View
{
    public class UserInterface
    {
        private UserIO userIO;

        public UserInterface()
        {
            userIO = new UserIO();
        }
        public int DisplayMenuAndGetUserChoice()
        {
            Console.WriteLine("\nEnter a choice from the menu below:");
            Console.WriteLine("1.Add Lens");
            Console.WriteLine("2.Show All Lenses");
            Console.WriteLine("3.Look Up Lens");
            Console.WriteLine("4.Edit Lens");
            Console.WriteLine("5.Remove Lens");
            Console.WriteLine("6.Exit Program");
            int userChoice = userIO.ReadInt("Enter your choice: ", 1, 6);

            return userChoice;
        }
        public Lenses GetNewLens()
        {
            Lenses lens = new Lenses();

            lens.LensName = userIO.ReadString("\nEnter the lens's name: ");
            lens.MinimumAperture = userIO.ReadDouble("Enter the lens's minimum aperture: ", 0.75, 22.0);
            lens.MinimumFocalLength = userIO.ReadInt("Enter the lens's base focal length: ", 5, 800);
            lens.IsCineLens = userIO.ReadBool("Is this a cinema lens? Yes or No: ");

            return lens;
        }
        public Lenses UpdateLensInfo(Lenses lens)
        {
            Console.WriteLine("\nLens ID: {0}", lens.LensID);
            Console.WriteLine("Current Lens Name: {0}", lens.LensName);
            lens.LensName = userIO.ReadString("Updated name: ");
            Console.WriteLine("Current Lens Minimum Aperture: f/{0}", lens.MinimumAperture);
            lens.MinimumAperture = userIO.ReadDouble("Enter New Minimum Aperture: ", 0.75, 22.0);
            Console.WriteLine("Current Lens Base Focal Length: {0}mm", lens.MinimumFocalLength);
            lens.MinimumFocalLength = userIO.ReadInt("Enter New Base Focal Length: ", 5, 800);
            Console.WriteLine($"Currently This Lense{(lens.IsCineLens ? " is " : " is not ")}a Cinema Lens.");
            lens.IsCineLens = userIO.ReadBool("Is This a Cinema Lens? Yes or No: ");

            return lens;
        }
        public void DisplayLenses(Lenses lens)
        {
            Console.WriteLine("\nLens ID: {0}", lens.LensID);
            Console.WriteLine("Lens Name: {0}", lens.LensName);
            Console.WriteLine("Lens Minimum Aperture: f/{0}", lens.MinimumAperture);
            Console.WriteLine("Lens Base Focal Length: {0}mm", lens.MinimumFocalLength);
            Console.WriteLine($"This Lense{(lens.IsCineLens ? " is " : " is not ")}a Cinema Lens.");
        }
        public int LensByID()
        {
            int userChoice = userIO.ReadInt("Enter a Lens ID Number: ", 0, 10);
            return userChoice;
        }
        public bool DeleteLensByID()
        {
            bool userChoice = userIO.ReadBool("Are you sure you want to delete? Yes or No?");
            return userChoice;
        }
        public void ShowActionSuccess(string actionName)
        {
            Console.WriteLine("\n{0} executed successfully!", actionName);
        }
        public void ShowActionFailure(string actionName)
        {
            Console.WriteLine("\n{0} failed to execute properly!", actionName);
        }
    }
}
