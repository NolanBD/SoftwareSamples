using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        Product ReadByID(string productType);
        List<Product> ReadAll();
     }
}
