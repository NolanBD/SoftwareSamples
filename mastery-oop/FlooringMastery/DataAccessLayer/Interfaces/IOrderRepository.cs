using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrder(DateTime orderDate, int orderNumber);
        List<Order> LoadAllOrders(DateTime orderDate);
        void CreateOrder(Order order);
        Order UpdateOrder(Order order);
        void DeleteOrder(DateTime orderDate, int orderNumber);
    }
}
