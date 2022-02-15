using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Models;

namespace BusinessLogicLayer
{
    public class BusinessLogicImpl : IBusinessLogic
    {
        IOrderRepository orderRepo;
        ITaxRepository taxRepo;
        IProductRepository productRepo;
        RunMode selector;

        public BusinessLogicImpl()
        {
            //selector is given a type of repository to choose from
            selector = RunModeFactory.Create();
            //the repository type that was selected is assigned to our various repositories
            orderRepo = selector.OrderRepoType();
            taxRepo = selector.TaxRepoType();
            productRepo = selector.ProductRepoType();
        }

        public Order AddOrderToRepository(Order order)
        {
            orderRepo.CreateOrder(order);
            return order;
        }

        public Order CalculateOrder(Order order)
        {
            List<Order> ordersInRepository = orderRepo.LoadAllOrders(order.OrderDate);

            //if our repository is empty then the order is assigned an order number of 1
            if(ordersInRepository.Count == 0)
            {
                order.OrderNumber = 1;
            }
            //otherwise take the highest order number in the repository and make our new order's
            //order number 1 + the current highest value
            else
            {
                order.OrderNumber = ordersInRepository.Max(o => o.OrderNumber) + 1;
            }
            
            return order;
        }

        public Order CalculateCostForOrder(Order order)
        {
            StateTax stateTaxInfo = new StateTax();
            Product productInfo = new Product();

            //populate our StateTaxInfo and ProductInfo fields from their respective repositories
            stateTaxInfo = taxRepo.ReadByID(order.StateTaxInfo.StateAbbreviation);
            productInfo = productRepo.ReadByID(order.ProductInfo.ProductType);
            order.StateTaxInfo = stateTaxInfo;
            order.ProductInfo = productInfo;

            decimal taxRate = order.StateTaxInfo.TaxRate;
            decimal costPerSquareFoot = order.ProductInfo.CostPerSquareFoot;
            decimal laborCostPerSquareFoot = order.ProductInfo.LaborCostPerSquareFoot;
            decimal area = order.Area;

            decimal footageCost = area * costPerSquareFoot;
            decimal laborCost = area * laborCostPerSquareFoot;
            decimal salesTax = (laborCost + footageCost) * (taxRate / 100);
            decimal totalCost = footageCost + laborCost + salesTax;

            order.MaterialCost = Decimal.Round(footageCost, 2);
            order.Taxes = Decimal.Round(salesTax, 2);
            order.LaborCost = Decimal.Round(laborCost, 2);
            order.Total = Decimal.Round(totalCost, 2);
            return order;
        }

        public bool DeleteOrder(DateTime orderDate, int orderNumber)
        {
            bool hasBeenDeleted = false;
            orderRepo.DeleteOrder(orderDate, orderNumber);

            //if the repository, after being repopulated, no longer contains the order, return true.
            hasBeenDeleted = true;
            return hasBeenDeleted;
        }

        public List<Product> GetAllProducts()
        {
            List <Product> allProducts = new List<Product>();
            allProducts = productRepo.ReadAll();
            return allProducts;
        }

        public List<StateTax> GetAllTaxInformation()
        {
            List<StateTax> allTaxes = new List<StateTax>();
            allTaxes = taxRepo.ReadAll();
            return allTaxes;
        }

        public Product GetProductInformationByID(string productName)
        {
            Product productByID = new Product();
            productByID = productRepo.ReadByID(productName);
            return productByID;
        }

        public StateTax GetTaxInformationByState(string stateAbbreviation)
        {
            StateTax taxByID = new StateTax();
            taxByID = taxRepo.ReadByID(stateAbbreviation);
            return taxByID;
        }

        public Order ModifyOrder(Order updatedOrder)
        {
            //take an order object with the user's updated information
            //populate an order matching the date and ID of the new object
            Order orderToUpdate = RetrieveOrderByID(updatedOrder.OrderDate, updatedOrder.OrderNumber);

            //change the necessary fields in the stored object
            orderToUpdate.CustomerName = updatedOrder.CustomerName;
            orderToUpdate.StateTaxInfo.StateAbbreviation = updatedOrder.StateTaxInfo.StateAbbreviation;
            orderToUpdate.ProductInfo.ProductType = updatedOrder.ProductInfo.ProductType;
            orderToUpdate.Area = updatedOrder.Area;

            //calculate our new cost
            orderToUpdate = CalculateCostForOrder(orderToUpdate);
            //return the updated order
            return orderToUpdate;
        }

        public Order RetrieveOrderByID(DateTime orderDate, int orderNumber)
        {
            Order retrivedOrder = orderRepo.LoadOrder(orderDate, orderNumber);

            return retrivedOrder;
        }

        public List<Order> RetriveOrdersByDate(DateTime orderDate)
        {
            List<Order> allOrdersByDate = orderRepo.LoadAllOrders(orderDate);

            if (allOrdersByDate.Count == 0)
            {
                throw new OrderDoesNotExistException("There were no orders to return on " + orderDate.ToString("MM/dd/yyyy"));
            }

            return allOrdersByDate;
        }
    }
}
