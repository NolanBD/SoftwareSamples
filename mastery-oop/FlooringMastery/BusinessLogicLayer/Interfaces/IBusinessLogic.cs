using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBusinessLogic
    {
        Order RetrieveOrderByID(DateTime orderDate, int orderNumber);
        List<Order> RetriveOrdersByDate(DateTime orderDate);
        Order AddOrderToRepository(Order order);
        Order ModifyOrder(Order order);
        bool DeleteOrder(DateTime orderDate, int orderNumber);

        Order CalculateOrder(Order order);
        Order CalculateCostForOrder(Order order);

        StateTax GetTaxInformationByState(string stateAbbreviation);
        List<StateTax> GetAllTaxInformation();
        Product GetProductInformationByID(string productName);
        List<Product> GetAllProducts();
    }
}
