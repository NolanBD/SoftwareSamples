using DataAccessLayer.Repositories;
using DataAccessLayer.TestRepositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class RunModeFactory
    {
        public static RunMode Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            //depending on the value of the "mode" key in the config file the Create()
            //selects either the production repository or the test repository and sends it
            //back to RunMode
            switch (mode)
            {
                case "Prod":
                    return new RunMode(new OrderRepository(), new TaxRepository(), new ProductRepository());
                case "Test":
                    return new RunMode(new TestOrderRepository(), new TestTaxRepository(), new TestProductRepository());
                default:
                    throw new Exception("Mode value in app config is invalid.");
            }
        }
    }
}
