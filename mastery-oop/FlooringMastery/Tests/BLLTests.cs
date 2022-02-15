using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayer.Repositories;
using Models;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using NUnit.Framework;

namespace Tests
{
    class BLLTests
    {
        Order order1;
        Order order2;
        DateTime expectedDate;
        BusinessLogicImpl businessLogic;

        public BLLTests()
        {
            businessLogic = new BusinessLogicImpl();
            expectedDate = new DateTime(7000, 01, 01);

            order1 = new Order
            {
                CustomerName = "Ugh",
                OrderDate = expectedDate,
                OrderNumber = 1,
                Area = 100m,
                StateTaxInfo = businessLogic.GetTaxInformationByState("OH"),
                ProductInfo = businessLogic.GetProductInformationByID("Carpet"),
                MaterialCost = 225.00m,
                LaborCost = 210.00m,
                Taxes = 27.19m,
                Total = 462.19m,
            };

            order2 = new Order
            {
                CustomerName = "Grug",
                OrderDate = expectedDate,
                OrderNumber = 2,
                Area = 200m,
                StateTaxInfo = businessLogic.GetTaxInformationByState("PA"),
                ProductInfo = businessLogic.GetProductInformationByID("Tile"),
                LaborCost = 830.00m,
                MaterialCost = 700.00m,
                Taxes = 27.19m,
                Total = 462.19m
            };
        }

        [Test]
        public void CanAddOrderToRepository()
        {
            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            List<Order> expected = businessLogic.RetriveOrdersByDate(expectedDate);

            Assert.AreEqual(expected[0].OrderNumber, order1.OrderNumber);

            _cleanUpFile(expected, businessLogic);
        }

        [Test]
        public void CanCalculateCostForOrder()
        {
            order2.StateTaxInfo = order1.StateTaxInfo;
            order2.Area = order1.Area;
            order2.ProductInfo = order1.ProductInfo;

            Order actual = businessLogic.CalculateCostForOrder(order2);

            Assert.AreEqual(actual.LaborCost, order1.LaborCost);
        }

        [Test]
        public void CanDeleteOrder()
        {
            order1.OrderDate = new DateTime(9000, 10, 10);

            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            bool actual = businessLogic.DeleteOrder(new DateTime(9000, 10, 10), 1);

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void CanGetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            string tile = "Tile";
            bool actual = false;

            allProducts = businessLogic.GetAllProducts();

            foreach (var product in allProducts)
            {
                if (product.ProductType == tile)
                {
                    actual = true;
                }
            }

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void CanGetAllTaxes()
        {
            List<StateTax> allTaxes = new List<StateTax>();
            string state = "OH";
            bool actual = false;

            allTaxes = businessLogic.GetAllTaxInformation();

            foreach (var product in allTaxes)
            {
                if (product.StateAbbreviation == state)
                {
                    actual = true;
                }
            }

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void CanGetProductInformationByID()
        {
            Product actual = new Product();
            string expected = "Tile";

            actual = businessLogic.GetProductInformationByID(expected);

            Assert.AreEqual(expected, actual.ProductType);
        }

        [Test]
        public void CanGetTaxInformationByState()
        {
            StateTax actual = new StateTax();
            string expected = "OH";

            actual = businessLogic.GetTaxInformationByState(expected);

            Assert.AreEqual(expected, actual.StateAbbreviation);
        }

        [Test]
        public void CanModifyOrder()
        {
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
            order1.OrderDate = new DateTime(9500, 10, 10);
            Order retrievedOrder = new Order();
            Order actual = new Order();

            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            List<Order> allOrders = businessLogic.RetriveOrdersByDate(new DateTime(9500, 10, 10));
            retrievedOrder = allOrders[0];

            retrievedOrder.Area = 200m;

            businessLogic.CalculateCostForOrder(retrievedOrder);
            businessLogic.AddOrderToRepository(retrievedOrder);

            allOrders = businessLogic.RetriveOrdersByDate(new DateTime(9500, 10, 10));
            actual = allOrders[0];

            Assert.AreEqual(order1.OrderNumber, actual.OrderNumber);
            Assert.AreEqual(order1.OrderDate, actual.OrderDate);
            Assert.AreNotEqual(order1.LaborCost, actual.LaborCost);

            _cleanUpFile(allOrders, businessLogic);
        }

        [Test]
        public void CanRetrieveOrderByID()
        {
            order1.OrderDate = new DateTime(9600, 10, 10);
            Order retrievedOrder = new Order();
            Order actual = new Order();

            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            actual = businessLogic.RetrieveOrderByID(new DateTime(9600, 10, 10), 1);

            Assert.AreEqual(order1.CustomerName, actual.CustomerName);
            Assert.AreEqual(order1.Area, actual.Area);
            Assert.AreEqual(order1.OrderNumber, actual.OrderNumber);

            List<Order> allOrders = businessLogic.RetriveOrdersByDate(new DateTime(9600, 10, 10));
            _cleanUpFile(allOrders, businessLogic);
        }

        [Test]
        public void CanRetriveOrdersByDate()
        {
            List<Order> orders = new List<Order>();
            order1.OrderDate = new DateTime(8000, 10, 10);
            order2.OrderDate = new DateTime(8000, 10, 10);

            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            order2 = businessLogic.CalculateCostForOrder(order2);
            order2 = businessLogic.CalculateOrder(order2);
            order2 = businessLogic.AddOrderToRepository(order2);

            orders = businessLogic.RetriveOrdersByDate(new DateTime(8000, 10, 10));

            Assert.AreEqual(2, orders.Count);

            _cleanUpFile(orders, businessLogic);
        }

        [Test]
        public void CanCalculateOrderNumber()
        {
            List<Order> orders = new List<Order>();
            order1.OrderDate = new DateTime(8000, 10, 10);
            order2.OrderDate = new DateTime(8000, 10, 10);

            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            order2 = businessLogic.CalculateCostForOrder(order2);
            order2 = businessLogic.CalculateOrder(order2);
            order2 = businessLogic.AddOrderToRepository(order2);

            orders = businessLogic.RetriveOrdersByDate(new DateTime(8000, 10, 10));

            Assert.AreEqual(order2.OrderNumber, orders[1].OrderNumber);

            _cleanUpFile(orders, businessLogic);
        }

<<<<<<< Updated upstream
        [Test]
        public void CanCalculateOrder()
        {
            List<Order> orders = new List<Order>();
            order1.OrderDate = new DateTime(9999, 10, 10);
            order1.ProductInfo.ProductType = "Wood";

            order1 = businessLogic.CalculateCostForOrder(order1);
            order1 = businessLogic.CalculateOrder(order1);
            order1 = businessLogic.AddOrderToRepository(order1);

            orders = businessLogic.RetriveOrdersByDate(new DateTime(9999, 10, 10));

            decimal expectedMaterial = 515.00m;
            decimal expectedLabor = 475.00m;
            decimal expectedTax = 61.88m;
            decimal expectedTotal = 1051.88m;

            Assert.AreEqual(expectedMaterial, orders[0].MaterialCost);
            Assert.AreEqual(expectedLabor, orders[0].LaborCost);
            Assert.AreEqual(expectedTax, orders[0].Taxes);
            Assert.AreEqual(expectedTotal, orders[0].Total);

            _cleanUpFile(orders, businessLogic);
        }

=======
>>>>>>> Stashed changes
        private void _cleanUpFile(List<Order> listOfOrders, BusinessLogicImpl businessLogic)
        {
            foreach (var order in listOfOrders)
            {
                businessLogic.DeleteOrder(order.OrderDate, order.OrderNumber);
            }
        }
    }
}
