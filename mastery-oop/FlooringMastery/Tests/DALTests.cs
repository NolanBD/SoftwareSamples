using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DALTests
    {
        Order order1;
        Order order2;
        OrderRepository orderRepository;
        TaxRepository taxRepository;
        ProductRepository productRepository;

        DateTime expectedDate;

        public DALTests()
        {
            expectedDate = new DateTime(3000, 01, 01);
            orderRepository = new OrderRepository();
            taxRepository = new TaxRepository();
            productRepository = new ProductRepository();

            order1 = new Order
            {
                CustomerName = "Ugh",
                OrderDate = expectedDate,
                OrderNumber = 1,
                ProductInfo = productRepository.ReadByID("Carpet"),
                StateTaxInfo = taxRepository.ReadByID("OH"),
                Area = 100m,
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
                ProductInfo = productRepository.ReadByID("Tile"),
                StateTaxInfo = taxRepository.ReadByID("PA"),
                Area = 200m,
                LaborCost = 830.00m,
                MaterialCost = 700.00m,
                Taxes = 27.19m,
                Total = 462.19m
            };
        }

<<<<<<< Updated upstream
        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void CleanUp()
        {

        }

=======
>>>>>>> Stashed changes
        [Test]
        public void CanDeleteOrder()
        {
            Order testOrder1 = new Order();
            Order testOrder2 = new Order();
            order1.OrderDate = new DateTime(6000, 01, 01);
            order2.OrderDate = new DateTime(6000, 01, 01);

            testOrder1 = _createOrder(order1, orderRepository);
            testOrder2 = _createOrder(order2, orderRepository);

            List<Order> fullList = orderRepository.LoadAllOrders(testOrder1.OrderDate);
            int expected = fullList.Count;

            orderRepository.DeleteOrder(testOrder2.OrderDate, testOrder2.OrderNumber);

            List<Order> actual = orderRepository.LoadAllOrders(testOrder1.OrderDate);

            Assert.AreNotEqual(expected, actual.Count);

            _cleanUpFile(actual, orderRepository);
        }

        [Test]
        public void CanUpdateOrder() 
        {
            DateTime orderDate = new DateTime(5000, 10, 10);
            Order actual = new Order();
            Order orderToUpdate = orderRepository.LoadOrder(orderDate, 1);
            Order expected = orderRepository.LoadOrder(orderDate, 1);

            orderToUpdate.CustomerName = "Lug";

            orderRepository.UpdateOrder(orderToUpdate);

            actual = orderRepository.LoadOrder(orderDate, 1);

            Assert.AreEqual(expected.OrderDate, actual.OrderDate);
            Assert.AreEqual(expected.OrderNumber, actual.OrderNumber);
            Assert.AreNotEqual(expected.CustomerName, actual.CustomerName);

            orderRepository.UpdateOrder(expected);
        }

        [Test]
        public void CanCreateOrder()
        {
            List<Order> actual = new List<Order>();
            order1.OrderDate = new DateTime(4000, 10, 10);
            Order expected = _createOrder(order1, orderRepository); ;

            actual = orderRepository.LoadAllOrders(order1.OrderDate);

            Assert.AreEqual(expected.OrderNumber, actual[0].OrderNumber);

            _cleanUpFile(actual, orderRepository);
        }

        [Test]
        public void CanLoadOrderFromFile()
        {
            DateTime orderDate = new DateTime(2013, 06, 01);
            int orderNumber = 1;

            Order actual = orderRepository.LoadOrder(orderDate, orderNumber);

            Assert.IsTrue(actual.OrderNumber == 1);
        }

        [Test]
        public void CanLoadAllOrdersToList()
        {
            Order testOrder1 = new Order();
            Order testOrder2 = new Order();
            order1.OrderDate = new DateTime(3500, 01, 01);
            order2.OrderDate = new DateTime(3500, 01, 01);

            testOrder1 = _createOrder(order1, orderRepository);
            testOrder2 = _createOrder(order2, orderRepository);

            List<Order> actual = orderRepository.LoadAllOrders(testOrder1.OrderDate);

            Assert.IsTrue(actual.Count == 2);

            _cleanUpFile(actual, orderRepository);
        }

        private Order _createOrder(Order order, OrderRepository orderRepository)
        {
            orderRepository.CreateOrder(order);
            return order;
        }

        private void _cleanUpFile(List<Order> listOfOrders, OrderRepository orderRepository)
        {
            foreach (var order in listOfOrders)
            {
                orderRepository.DeleteOrder(order.OrderDate, order.OrderNumber);
            }
        }
    }
}
