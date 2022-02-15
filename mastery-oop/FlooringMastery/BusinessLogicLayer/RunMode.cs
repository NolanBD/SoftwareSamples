using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RunMode
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;

        //the type of repo selected by the factory is stored in _orderRepository
        public RunMode(IOrderRepository orderRepository, ITaxRepository taxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = taxRepository;
            _productRepository = productRepository;
        }

        //return the selected IRepository
        public IOrderRepository OrderRepoType()
        {
            return _orderRepository;
        }

        public ITaxRepository TaxRepoType()
        {
            return _taxRepository;
        }

        public IProductRepository ProductRepoType()
        {
            return _productRepository;
        }
    }
}
