using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class ViewClient
    {
        UserIOConsoleImpl userIO;

        public ViewClient()
        {
            userIO = new UserIOConsoleImpl();
        }

        public int ShowMenuAndGetUserChoice()
        {
            Console.Clear();
            Console.WriteLine("**************************************");
            Console.WriteLine("* Flooring Program");
            Console.WriteLine("**************************************");
            Console.WriteLine("* 1. Display All Orders");
            Console.WriteLine("* 2. Display Order");
            Console.WriteLine("* 3. Create an Order");
            Console.WriteLine("* 4. Edit an Order");
            Console.WriteLine("* 5. Remove an Order");
            Console.WriteLine("* 6. Quit");
            Console.WriteLine("**************************************");
            int userChoice = userIO.ReadInt("\nPlease Enter Your Choice: ");

            return userChoice;
        }

        public Order GetNewOrderInfo(List<Product> products, List<StateTax> states)
        {
<<<<<<< Updated upstream
            bool isNotBlank = false;

=======
>>>>>>> Stashed changes
            Console.Clear();
            Order order = new Order();
            Console.WriteLine("***ENTER YOUR ORDER INFO***");
            Console.WriteLine();
<<<<<<< Updated upstream
            string customerName = userIO.ReadString("Enter a Name for Your Order: ");

            while (!isNotBlank)
            {
                if (String.IsNullOrEmpty(customerName))
                {
                    customerName = userIO.ReadString("Your name may not be blank! Enter a Name for Your Order: ");
                }
                else
                {
                    isNotBlank = true;
                }
            }

            order.CustomerName = customerName;
=======
            order.CustomerName = userIO.ReadString("Enter a Name for Your Order: ");
>>>>>>> Stashed changes
            order.OrderDate = userIO.ReadDateAfterCurrentDate("Enter a Date For Your Order (MUST BE AFTER TODAY'S DATE): ");
            Console.WriteLine("Enter Your State (ABBREVIATION ONLY) From the Following List: ");
            DisplayAllStates(states);
            order.StateTaxInfo = userIO.ReadState(states);
            order.Area = userIO.ReadDecimal("Enter the Area of Your Floor (100 SQUARE FOOT MINIMUM): ", 100);
            Console.WriteLine("Enter the Type Of Flooring You Would Like From The Following List: ");
            DisplayAllProducts(products);
            order.ProductInfo = userIO.ReadProduct(products);

            return order;
        }

        public Order GetUpdatedOrderInfo(Order order, List<Product> products, List<StateTax> states)
        {
            Console.Clear();
            Console.WriteLine("***ENTER YOUR UPDATED INFORMATION***");
            Console.WriteLine("Your Order Name is: " + order.CustomerName);
<<<<<<< Updated upstream
            string newName = userIO.ReadString("Enter Your Updated Name (press 'enter' to maintain your previous name): ");
            if (newName != "")
            {
                order.CustomerName = newName;
            }
=======
            order.CustomerName = userIO.ReadString("Enter Your Updated Name: ");
>>>>>>> Stashed changes
            Console.WriteLine("Your Current State is: " + order.StateTaxInfo.StateAbbreviation);
            Console.WriteLine("Enter Your State (ABBREVIATION ONLY): ");
            order.StateTaxInfo = userIO.ReadState(states);
            Console.WriteLine("Your Current Square Footage is: " + order.Area);
            order.Area = userIO.ReadDecimal("Enter the Updated Area of Your Floor (100 SQUARE FOOT MINIMUM): ", 100);
            Console.WriteLine("Your Current Product is: " + order.ProductInfo.ProductType);
            Console.WriteLine("Enter the New Type of Flooring You Would Like From the Following List: ");
            DisplayAllProducts(products);
            order.ProductInfo = userIO.ReadProduct(products);

            return order;
        }

        public bool DeleteOrder(Order order)
        {
            Console.Clear();
            Console.WriteLine("Do You Wish to Delete the Following Order?");
            DisplayOrder(order);

            bool userChoice = userIO.ReadBool("Enter 'Y' for yes or 'N' for no: ");

            return userChoice;
        }

        public DateTime GetOrderDate()
        {
            DateTime orderDate = new DateTime();
            orderDate = userIO.ReadDate("Enter the Fulfilment Date of the Order You Want to Retrieve: ");

            return orderDate;
        }

        public int GetOrderID()
        {
            int orderNumber;
            orderNumber = userIO.ReadInt("Enter the Order Number of the Order You Want To Retrieve: ");

            return orderNumber;
        }

        public bool ConfirmCorrectOrder(Order order, string actionName)
        {
            Console.Clear();
            Console.WriteLine($"Are you sure you want to {actionName}?");
            DisplayOrder(order);

            bool userChoice = userIO.ReadBool("Enter 'Y' for yes or 'N' for no: ");
            return userChoice;
        }

        public void DisplayOrder(Order order)
        {
            Console.WriteLine("**************************************");
            Console.WriteLine($"[{order.OrderNumber}] | [{order.OrderDate.ToString("MM/dd/yyyy")}]");
            Console.WriteLine($"[{order.CustomerName}]");
            Console.WriteLine($"[{order.StateTaxInfo.StateAbbreviation}]");
<<<<<<< Updated upstream
            Console.WriteLine($"Product: [{order.ProductInfo.ProductType}] | Square Footage: [{order.Area}]");
            Console.WriteLine($"Material Cost: [{order.MaterialCost:c}]");
            Console.WriteLine($"Labor Cost: [{order.LaborCost:c}]");
            Console.WriteLine($"Tax: [{order.Taxes:c}]");
            Console.WriteLine($"Total: [{order.Total:c}]");
=======
            Console.WriteLine($"Product: [{order.ProductInfo.ProductType}]");
            Console.WriteLine($"Material Cost: [{order.MaterialCost}]");
            Console.WriteLine($"Labor Cost: [{order.LaborCost}]");
            Console.WriteLine($"Tax: [{order.Taxes}]");
            Console.WriteLine($"Total: [{order.Total}]");
>>>>>>> Stashed changes
            Console.WriteLine("**************************************");
            Console.WriteLine("");
        }

        public void DisplayAllOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                DisplayOrder(order);
            }
        }

        public bool EndProgram()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to end the program?");

            bool userChoice = userIO.ReadBool("Please respond 'Y' to quit or 'N' to continue running");

            return userChoice;
        }

        public void DisplayAllProducts(List<Product> products)
        {
            Console.WriteLine("***PRODUCTS***");
            foreach (var product in products)
            {
                Console.WriteLine("Product Name: " + product.ProductType);
                Console.WriteLine("Product Cost Per Square Foot: " + product.CostPerSquareFoot);
                Console.WriteLine("Product Installation Cost Per Square Foot: " + product.LaborCostPerSquareFoot);
                Console.WriteLine("");
            }
        }

        public void DisplayAllStates(List<StateTax> states)
        {
            Console.WriteLine("***STATES***");
            foreach (var state in states)
            {
                Console.WriteLine(state.StateAbbreviation + "--" + state.StateName);
            }
        }

        public void ShowActionFailure(string actionName)
        {
            Console.WriteLine(actionName);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public void ShowActionSuccess(string actionName)
        {
            Console.WriteLine(actionName);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public void DisplayExceptionMessage(string message)
        {
            Console.WriteLine("FATAL ERROR:");
            Console.WriteLine(message);
            Console.WriteLine("Returning to main menu. Press any key...");
            Console.ReadKey();
        }
    }
}
