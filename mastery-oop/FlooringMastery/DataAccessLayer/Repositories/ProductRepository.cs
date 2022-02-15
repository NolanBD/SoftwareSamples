using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        Dictionary<string, Product> products;
        string path;
        List<string> rows;


        public ProductRepository()
        {
            rows = new List<string>();
            path = @"C:\Software Guild\Summatives\mastery-oop\FlooringMastery\FileRepos\Products\products.txt";
            products = new Dictionary<string, Product>();
            _populateRepositoryFromFile(path);
        }

        private void _populateRepositoryFromFile(string path)
        {
            rows = File.ReadAllLines(path).ToList();

            for (int i = 1; i < rows.Count; i++)
            {
                string[] columns = rows[i].Split(',');
                Product product = new Product();

                product.ProductType = columns[0];
                product.CostPerSquareFoot = decimal.Parse(columns[1]);
                product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                products.Add(product.ProductType, product);
            }
        }

        public List<Product> ReadAll()
        {
            //create a list to house all of our products from the repository
            List<Product> allProducts = new List<Product>();

            //for each entry in products, add that entry to our list
            foreach (KeyValuePair<string, Product> entry in products)
            {
                allProducts.Add(entry.Value);
            }

            //return our list of products
            return allProducts;
        }

        public Product ReadByID(string productType)
        {
            //if a product with a key of productType exists, populate a product object
            //and return the product
            if (products.ContainsKey(productType))
            {
                Product product = products[productType];
                return product;
            }
            else
            {
                throw new ProductDoesNotExistException("This Product Does Not Exist in Our Files");
            }
            
        }
    }
}
