using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        Dictionary<int, Order> orders;
        private const string directory = @"C:\Software Guild\Summatives\mastery-oop\FlooringMastery\FileRepos\Orders\Orders_";
        List<string> rows;
        List<string> ordersInRepository;

        public OrderRepository()
        {
            rows = new List<string>();
            orders = new Dictionary<int, Order>();
            ordersInRepository = new List<string>();
        }

        //using the file name, amend the directory to include the file and extension,
        private void _populateRepositoryFromFile(DateTime orderDate)
        {
            orders.Clear();
            string fileName = directory + orderDate.ToString("MMddyyyy") + ".txt";

            //if the file doesn't exist throw an exception
<<<<<<< Updated upstream
                if (!File.Exists(fileName))
                {
                    throw new OrderDoesNotExistException("No Orders Matching This Date Exist.");
                }
=======
            try
            {
                File.Exists(fileName);
            }
            catch
            {
                throw new OrderDoesNotExistException("No Orders Matching This Date Exist.");
            }
>>>>>>> Stashed changes

            //populate a list of strings from the file dictated by the directory
            rows = File.ReadAllLines(fileName).ToList();

            //delimit and assign values from "rows" into "columns", then populate an Order object with that information
            for (int i = 1; i < rows.Count; i++)
            {
                
                string[] columns = rows[i].Split(',');

                Order o = new Order();
                o.OrderDate = orderDate;
                o.OrderNumber = int.Parse(columns[0]);
                o.CustomerName = columns[1].Replace("|", ",");
                StateTax s = new StateTax();
                o.StateTaxInfo = s;
                o.StateTaxInfo.StateAbbreviation = (columns[2]);
                o.StateTaxInfo.TaxRate = decimal.Parse(columns[3]);
                Product p = new Product();
                o.ProductInfo = p;
                o.ProductInfo.ProductType = (columns[4]);
                o.Area = decimal.Parse(columns[5]);
                o.ProductInfo.CostPerSquareFoot = decimal.Parse(columns[6]);
                o.ProductInfo.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                o.MaterialCost = decimal.Parse(columns[8]);
                o.LaborCost = decimal.Parse(columns[9]);
                o.Taxes = decimal.Parse(columns[10]);
                o.Total = decimal.Parse(columns[11]);

                //once the order object is populated, assign OrderNumber as the key, and save the order to the repository
                orders.Add(o.OrderNumber, o);
            }
        }

        private void _convertRepositoryToString()
        {
            ordersInRepository.Clear();
            ordersInRepository.Add("OrderNumber,CustomerName,StateAbbreviation,TaxRate,ProductType,Area,CostPerSquareFoot," +
                "LaborCostPerSquareFoot,MaterialCost,LaborCost,Taxes,Total");

            //for every order in orders convert the data in each order to a string with the correct delimiter
            //and store those strings in a list

            foreach (var order in orders)
            {
                if (order.Value != null)
                {
                    order.Value.CustomerName = order.Value.CustomerName.Replace(",", "|");

                    ordersInRepository.Add(order.Value.OrderNumber + "," + order.Value.CustomerName + "," + order.Value.StateTaxInfo.StateAbbreviation
                    + "," + order.Value.StateTaxInfo.TaxRate + "," + order.Value.ProductInfo.ProductType + "," + order.Value.Area + "," +
                    order.Value.ProductInfo.CostPerSquareFoot + "," + order.Value.ProductInfo.LaborCostPerSquareFoot +
                    "," + order.Value.MaterialCost + "," + order.Value.LaborCost + "," + order.Value.Taxes + "," + order.Value.Total);
                }
            }
        }

        private void _createNewFile()
        {
                string textFile = "";
                //convert our repository to a collection of strings
                _convertRepositoryToString();
                //for each string in our collection of strings populate a file with those strings
                //formatted to be read by our populate repository method
                foreach (var row in ordersInRepository)
                {
                    textFile += row + "\n";
                }

                string filePath = directory + orders[1].OrderDate.ToString("MMddyyy") + ".txt";
                //update the file at directory with our new data
                var orderFile = File.Create(filePath);
                orderFile.Close();
                File.WriteAllText(filePath, textFile);
        }

        private void _addOrdersToFile(DateTime orderDate)
        {
                string textFile = "";
                _convertRepositoryToString();

                foreach (var row in ordersInRepository)
                {
                    textFile += row + "\n";
                }

                File.WriteAllText(directory + orderDate.ToString("MMddyyy") + ".txt", textFile);
        }

        public void CreateOrder(Order order)
        {
<<<<<<< Updated upstream
            //if the file already exists
=======
            //if the file already exists, add the order to our file
>>>>>>> Stashed changes
            if (File.Exists(directory + order.OrderDate.ToString("MMddyyy") + ".txt"))
            {
                //add the given order to our repository
                UpdateOrder(order);
                return;
            }

            //if no file corresponds to our order date clear the repository
            orders.Clear();
            //add our order to the repository
            orders.Add(order.OrderNumber, order);
            //create a new file to house our order
            _createNewFile();
        }

        public void DeleteOrder(DateTime orderDate, int orderNumber)
        {
            _populateRepositoryFromFile(orderDate);

            //if we find an order with a matching key in our repository
            if (orders.ContainsKey(orderNumber))
            {
                //remove the order from our repository
                orders.Remove(orderNumber);
                //update our file with the updated repository
                _addOrdersToFile(orderDate);
            }
            else
            {
                throw new OrderDoesNotExistException("The Submitted Order Number Does Not Match Our Records");
            }
        }

        public List<Order> LoadAllOrders(DateTime orderDate)
        {
            List<Order> allOrders = new List<Order>();
            //if the file exists then populate the repository from the file
            if (File.Exists(directory + orderDate.ToString("MMddyyy") + ".txt"))
            {
                _populateRepositoryFromFile(orderDate);

                //add every entry in our repository to a list
                foreach (KeyValuePair<int, Order> entry in orders)
                {
                    allOrders.Add(entry.Value);
                }
                //return the list to the user
                allOrders.OrderBy(o => o.OrderNumber);
                return allOrders;
            }
            //if no file exists return 
            else
            {
                //return the empty list to the user
                allOrders.OrderBy(o => o.OrderNumber);
                return allOrders;
            }

        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            //populate our repository
            _populateRepositoryFromFile(orderDate);
            Order order = new Order();

            //if our given key matches a member of the repository, populate an order object
            //with the data from that member and return it
            if (orders.TryGetValue(orderNumber, out order))
            {
                return order;
            }

            throw new OrderDoesNotExistException("The Submitted Order Number Does Not Match Our Records");
        }

        public Order UpdateOrder(Order updatedOrder)
        {
            //populate our repository
            _populateRepositoryFromFile(updatedOrder.OrderDate);

            //if our order exists in the repositry replace it with the updated order
            if (orders.ContainsKey(updatedOrder.OrderNumber))
            {
                orders[updatedOrder.OrderNumber] = updatedOrder;
                //save our repository back to the file
                _addOrdersToFile(updatedOrder.OrderDate);
                //return the updated order to the user for confirmation
                return updatedOrder;
            }
            //if this is a new order on this date, add the order to the repository
            else
            {
                orders.Add(updatedOrder.OrderNumber, updatedOrder);
                _addOrdersToFile(updatedOrder.OrderDate);
                return updatedOrder;
            }

            throw new OrderDoesNotExistException("The Submitted Order Number Does Not Match Our Records");
        }
    }
}
