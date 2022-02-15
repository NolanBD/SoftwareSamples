using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
<<<<<<< Updated upstream
=======
using System.Text;
using System.Threading.Tasks;
>>>>>>> Stashed changes

namespace DataAccessLayer.TestRepositories
{
    public class TestOrderRepository : IOrderRepository
    {
        Dictionary<int, Order> orders;
        Dictionary<DateTime, Dictionary<int, Order>> memoryRepo;
        private const string directory = @"C:\Software Guild\Summatives\mastery-oop\FlooringMastery\TestRepos\Orders\Orders_";
        List<string> rows;

        public TestOrderRepository()
        {
            rows = new List<string>();
            orders = new Dictionary<int, Order>();
            //memoryRepo is a collection of all our collections of orders, sorted by date.
            //we will use this to check and see if a collection of orders associated with this date exists.
            memoryRepo = new Dictionary<DateTime, Dictionary<int, Order>>();
            _populateRepositoryFromFile(DateTime.Parse("10/10/2022"));
        }

        //using the file name, amend the directory to include the file and extension,
        private void _populateRepositoryFromFile(DateTime orderDate)
        {
            string fileName = directory + orderDate.ToString("MMddyyyy") + ".txt";

            //if the file doesn't exist throw an exception
<<<<<<< Updated upstream
            if (!File.Exists(fileName))
=======
            try
            {
                File.Exists(fileName);
            }
            catch
>>>>>>> Stashed changes
            {
                throw new OrderDoesNotExistException("No Orders Matching This Date Exist.");
            }

<<<<<<< Updated upstream

=======
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
            //add our collection of orders to our memory repo
            memoryRepo.Add(orders[1].OrderDate, orders);
        }

        private Dictionary<int, Order> _createNewRepo(Order order)
        {
            Dictionary<int, Order> orderRepoInMemory = new Dictionary<int, Order>();
            orderRepoInMemory.Add(order.OrderNumber, order);

            return orderRepoInMemory;
        }

        private void _addOrdersToMemoryRepo(DateTime orderDate, Dictionary<int, Order> orderRepo)
        {
            //if our Memory Repo contains a collection of orders with a key matching our order date
            if (memoryRepo.ContainsKey(orderDate))
            {
                //upadate that repo
                memoryRepo[orderDate] = orderRepo;
            }
            //otherwise add our new collection of orders to our memory repository
            else if (!memoryRepo.ContainsKey(orderDate))
            {
                memoryRepo.Add(orderDate, orderRepo);
            }
            else
            {
                throw new RunningInTestModeException("A critical error has occured, I don't know what it is");
            }
        }

        public void CreateOrder(Order order)
        {
            //if our Memory Repo contains a collection of orders with a key matching our order date
            if (memoryRepo.ContainsKey(order.OrderDate))
            {
                //send our order through to the update function
                UpdateOrder(order);
            }
            else
            {
                //make a new collection of orders to store our new order in
                Dictionary<int, Order> newOrders = _createNewRepo(order);
                //add our collection of orders to our memory repo
                _addOrdersToMemoryRepo(order.OrderDate, newOrders);
            }

        }

        public void DeleteOrder(DateTime orderDate, int orderNumber)
        {
            //if an order repo matching the submitted date exists in our memory repo
            if (memoryRepo.ContainsKey(orderDate))
            {
                //and if an order inside that order repo matches the submitted order number
                if (memoryRepo[orderDate].ContainsKey(orderNumber))
                {
                    //remove that order
                    memoryRepo[orderDate].Remove(orderNumber);
                }
                else
                {
                    throw new OrderDoesNotExistException("No Order Matching This Order Number Exists");
                }
            }
            else
            {
                throw new OrderDoesNotExistException("No Order Matching This Date Exists");
            }
        }

        public List<Order> LoadAllOrders(DateTime orderDate)
        {
            List<Order> allOrders = new List<Order>();

            //if our memory repo of all our orders contains a collection with a key matching our order date
            if (memoryRepo.ContainsKey(orderDate))
            {
                //for each member matching this orderDate in our collection of orders
                foreach (var repo in memoryRepo)
                {
<<<<<<< Updated upstream
                    if (repo.Key == orderDate)
                    {
                        //take the information stored in the order repo and add it to a list of orders
                        foreach (var order in repo.Value)
                        {
                            allOrders.Add(order.Value);
                        }
                    }

=======
                    //take the information stored in the order repo and add it to a list of orders
                    foreach (var order in repo.Value)
                    {
                        allOrders.Add(order.Value);
                    }
>>>>>>> Stashed changes
                }

                allOrders.OrderBy(o => o.OrderNumber);
                return allOrders;
            }
            //if no orders corrispond to the given order date
            else if (!memoryRepo.ContainsKey(orderDate))
            {
                //return the empty list to the user
                allOrders.OrderBy(o => o.OrderNumber);
                return allOrders;
            }
            else
            {
                throw new OrderDoesNotExistException("No orders matching that date exist in our records.");
            }
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            Order orderToReturn = new Order();

            //if our memory repo of all our orders contains a collection with a key matching our order date
            if (memoryRepo.ContainsKey(orderDate))
            {
                //for each member matching this orderDate in our collection of orders
                foreach (var repo in memoryRepo[orderDate])
                {
<<<<<<< Updated upstream
                    if (repo.Key == orderNumber)
                    {
                        orderToReturn = repo.Value;
                        return orderToReturn;
                    }
=======
                        if (repo.Key == orderNumber)
                        {
                            orderToReturn = repo.Value;
                            return orderToReturn;
                        }
>>>>>>> Stashed changes
                }

                throw new OrderDoesNotExistException("No orders matching the submitted order number exist in our records.");
            }

            throw new OrderDoesNotExistException("No orders matching that date exist in our records.");
        }

        public Order UpdateOrder(Order updatedOrder)
        {
            //for every collection of orders on a certain date
            foreach (var orderRepo in memoryRepo)
            {
                try
                {
                    //if the order date of the submitted order matches a date on file
                    if (orderRepo.Key == updatedOrder.OrderDate)
                    {
                        //and if our order exists in the repositry replace it with the updated order
                        if (orderRepo.Value.ContainsKey(updatedOrder.OrderNumber))
                        {
                            orderRepo.Value[updatedOrder.OrderNumber] = updatedOrder;
                            //save our repository back to the memory repo of all order dates
                            _addOrdersToMemoryRepo(updatedOrder.OrderDate, orderRepo.Value);
                            //return the updated order to the user for confirmation
                            return updatedOrder;
                        }
                        //if this is a new order on this date, add the order to the repository
                        else
                        {
                            orderRepo.Value.Add(updatedOrder.OrderNumber, updatedOrder);
                            _addOrdersToMemoryRepo(updatedOrder.OrderDate, orderRepo.Value);
                            return updatedOrder;
                        }
                    }
                }
                catch
                {
                    throw new OrderDoesNotExistException("No order matching this date was found in our files.");
                }
            }

            throw new OrderDoesNotExistException("The Submitted Order Number Does Not Match Our Records");
        }
    }
}
