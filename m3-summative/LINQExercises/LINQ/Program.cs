using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Exercise21();
            Console.ReadLine();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        /// PASS
        static void Exercise1()
        {
            //place all products into a list
            List<Product> products = DataLoader.LoadProducts();
            //set var outOfStock to any members of products where UnitsInStock is equal to 0
            var outOfStock = products.Where(p => p.UnitsInStock == 0);
            PrintProductInformation(outOfStock);
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        /// PASS
        static void Exercise2()
        {
            //place all products into a list
            List<Product> products = DataLoader.LoadProducts();
            //place all members that meet the criteria into another enumerable
            var inStockAndGreaterThan3 = products.Where(p => p.UnitsInStock != 0 && p.UnitPrice > 3);
            PrintProductInformation(inStockAndGreaterThan3);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        /// PASS
        static void Exercise3()
        {
            //load customers into an enumberable
            var customers = DataLoader.LoadCustomers();
            //find customers in the Washington region
            var customersInWA = customers.Where(c => c.Region == "WA");
            PrintCustomerInformation(customersInWA);

        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        /// PASS
        static void Exercise4()
        {
            //place all products into a list
            List<Product> products = DataLoader.LoadProducts();
            //create an anonymous type that holds just the name of a product
            var justProductName = from product in products
                                  select new
                                  {
                                      ProductName = product.ProductName
                                  };
            //for each name in a collection of product names, print their name
            foreach (var name in justProductName)
            {
                Console.WriteLine(name.ProductName);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        /// PASS
        static void Exercise5()
        {
            //place all products into a list
            List<Product> products = DataLoader.LoadProducts();
            //create an anonymous type that holds the data of the products in the enumerable
            var moreExpensiveProducts = from product in products
                                        select new
                                        {
                                            ProductID = product.ProductID,
                                            ProductName = product.ProductName,
                                            Category = product.Category,
                                            //make UnitPrice the value of unit price plus 25% of that value
                                            UnitPrice = product.UnitPrice + (product.UnitPrice * .25m),
                                            UnitsInStock = product.UnitsInStock
                                        };

            //format and output the new anonymous type
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var moreExpensiveProduct in moreExpensiveProducts)
            {
                Console.WriteLine(line, moreExpensiveProduct.ProductID, moreExpensiveProduct.ProductName,
                    moreExpensiveProduct.Category, moreExpensiveProduct.UnitPrice, moreExpensiveProduct.UnitsInStock);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        /// PASS
        static void Exercise6()
        {
            //place all products into list
            List<Product> products = DataLoader.LoadProducts();
            //create an anonymous type that holds just the name and category of a product
            var capitalizedNamesAndCategories = from product in products
                                                select new
                                                {
                                                    //Capitalize all the names and categories
                                                    ProductName = product.ProductName.ToUpper(),
                                                    Category = product.Category.ToUpper()
                                                };

            //format and output the new anonymous type
            string line = "{0, -40} {1, -20}";
            Console.WriteLine(line, "Product Name", "Category");
            Console.WriteLine("==============================================================================");

            foreach (var capitalizedNameAndCategory in capitalizedNamesAndCategories)
            {
                Console.WriteLine(line, capitalizedNameAndCategory.ProductName, capitalizedNameAndCategory.Category);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        /// PASS
        static void Exercise7()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts();

            //create an anonymous type that contains all members of products and an extra bool
            var allProducts = from product in products
                              select new
                              {
                                  ProductID = product.ProductID,
                                  ProductName = product.ProductName,
                                  Category = product.Category,
                                  UnitPrice = product.UnitPrice,
                                  UnitsInStock = product.UnitsInStock,
                                  //using a ternary expression, set NeedsReOrder to false if UnitsInStock > 3
                                  NeedsReOrder = true ? product.UnitsInStock < 3 : product.UnitsInStock > 3
                              };


            //format and output the new anonymous type
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5, -13}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "ReOrder");
            Console.WriteLine("==============================================================================");

            foreach (var needsReOrder in allProducts)
            {
                Console.WriteLine(line, needsReOrder.ProductID, needsReOrder.ProductName,
                    needsReOrder.Category, needsReOrder.UnitPrice, needsReOrder.UnitsInStock,
                    needsReOrder.NeedsReOrder);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        /// PASS
        static void Exercise8()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts();

            //create an anonymous type that contains all members of products and an extra bool
            var allProducts = from product in products
                              select new
                              {
                                  ProductID = product.ProductID,
                                  ProductName = product.ProductName,
                                  Category = product.Category,
                                  UnitPrice = product.UnitPrice,
                                  UnitsInStock = product.UnitsInStock,
                                  //multiply the units in stock by their value
                                  StockValue = product.UnitsInStock * product.UnitPrice
                              };

            //format and output the new anonymous type
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5, -6:c}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "Stock Value");
            Console.WriteLine("==============================================================================");

            foreach (var valueOfStock in allProducts)
            {
                Console.WriteLine(line, valueOfStock.ProductID, valueOfStock.ProductName,
                    valueOfStock.Category, valueOfStock.UnitPrice, valueOfStock.UnitsInStock,
                    valueOfStock.StockValue);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        /// PASS
        static void Exercise9()
        {
            //assign all the values of the array NumbersA to a list
            var allNumbers = (DataLoader.NumbersA).ToList().Where(n => n % 2 == 0);

            foreach (int number in allNumbers)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        /// PASS
        static void Exercise10()
        {
            //load customers into a list
            var customers = DataLoader.LoadCustomers();
            var ordersSub500 = (from customer in customers
                               from order in customer.Orders
                               where order.Total < 500
                               select new
                               {
                                   Name = customer.CompanyName
                               }).Distinct();
           
            foreach (var customer in ordersSub500)
            {
                Console.WriteLine(customer.Name);
            }
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        /// PASS
        static void Exercise11()
        {
            //load NumbersC into a list
            var numbersC = (DataLoader.NumbersC).ToList();
            //take only odd numbers from numbersC
            var oddNumbers = numbersC.Where(n => n % 2 != 0);
            // take the first three members of oddNumbers
            var firstThreeOdds = oddNumbers.Take(3);
            //for each int in firstThreeOdds, print those ints
            foreach (var number in firstThreeOdds)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        /// PASS
        static void Exercise12()
        {
            //load NumbersB into a list
            var numbersB = (DataLoader.NumbersB).ToList();
            //load numbersB into a list excluding the first 3 members
            var excludingThree = numbersB.Skip(3);

            //for each number in excludingThree, print those numbers
            foreach (int number in excludingThree)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        /// PASS
        static void Exercise13()
        {
            //load customers into an enumberable
            var customers = DataLoader.LoadCustomers();
            //find customers in the Washington region
            var customersInWA = customers.Where(c => c.Region == "WA");
            //create a new date time object to access outside of the loop
            DateTime lowestTime = new DateTime();

            foreach (var customer in customersInWA)
            {
                foreach (var order in customer.Orders)
                {
                    //set orderDates equal to order.OrderDate, then compare orderDates
                    //to order.OrderDate. If greater than or equal to order.OrderDate,
                    //assign orderDates to lowestTime
                    var orderDates = order.OrderDate;
                    if (orderDates >= order.OrderDate)
                    {
                        lowestTime = order.OrderDate;
                    }
                }
                //convert lowestTime to strig and print
                string mostRecentOrder = lowestTime.ToString("yyyy-MM-dd");
                Console.WriteLine(customer.CompanyName + " " + mostRecentOrder);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        /// PASS
        static void Exercise14()
        {
            //load all nubmers into a list
            var allNumbers = (DataLoader.NumbersC).ToList().TakeWhile(o => o <= 6);

            foreach (var number in allNumbers)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        /// PASS
        static void Exercise15()
        {
            //load numbers into a list
            var allNumbers = (DataLoader.NumbersC).ToList().SkipWhile(o => o % 3 != 0).Skip(1);
            //create a variable to store the index of the first number divisble by thre
            
            foreach (var number in allNumbers)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        /// PASS
        static void Exercise16()
        {
            //load products into a lst
            var products = DataLoader.LoadProducts().OrderBy(o => o.ProductName);

            PrintProductInformation(products);

        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        /// PASS
        static void Exercise17()
        {
            //load products into a list
            var products = DataLoader.LoadProducts();
            //make another list and populate it with products in descending order
            var productsDescending = products.OrderByDescending(o => o.UnitsInStock);
            //print the newly organized list
            PrintProductInformation(productsDescending);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            //load products into a list
            var products = DataLoader.LoadProducts();

            //order products by descending unit price and then by category
            var orderingProducts = from product in products
                                   .OrderByDescending(p => p.UnitPrice)
                                   .OrderBy(p => p.Category)
                                   select new
                                   {
                                       ProductID = product.ProductID,
                                       ProductName = product.ProductName,
                                       Category = product.Category,
                                       UnitPrice = product.UnitPrice,
                                       UnitsInStock = product.UnitsInStock,
                                   };

            //format the output for the members of orderingProducts
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in orderingProducts)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName,
                    product.Category, product.UnitPrice, product.UnitsInStock);
            }
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        /// PASS
        static void Exercise19()
        {
            //load numbers into list
            var allNumbers = (DataLoader.NumbersB).ToList();
            //reverse the numbers
            allNumbers.Reverse();
            //for each member, print that member
            allNumbers.ForEach(Console.WriteLine);
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        /// PASS
        static void Exercise20()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts();

            //create a group with a key of category, members by product name
            var groupedByCategory = from product in products
                                    orderby product.Category, product.ProductName
                                    group product by product.Category;

            //for each member of the group
            foreach (var group in groupedByCategory)
            {
                //write the category
                Console.WriteLine(group.Key);

                //then write each member of the group
                foreach (var product in group)
                {
                    Console.WriteLine("\t{0}", product.ProductName);
                }
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        /// PASS
        static void Exercise21()
        {
            //load customers into a list
            var customers = DataLoader.LoadCustomers();

            //from customers, take name
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.CompanyName);

                //make a new Orders object with data from customer, group by year
                var sortedOrders = from order in customer.Orders
                                   group order by order.OrderDate.Year;

                //for each year of orders print the year
                foreach (var group in sortedOrders)
                {
                    var yearOfOrder = group.Key;
                    Console.WriteLine(yearOfOrder);

                    //for each month in the year, print the month and the order total for that month
                    foreach (var month in group)
                    {
                        string months = month.OrderDate.ToString("MM");
                        var cost = month.Total;

                        if (months == month.OrderDate.ToString())
                        {
                            Console.WriteLine("\t${0}", cost);
                        }
                        else
                        {
                            Console.WriteLine("\t{0} -  ${1}", months, cost);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        /// PASS
        static void Exercise22()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts().GroupBy(o => o.Category).Distinct();

            foreach (var product in products)
            {
                Console.WriteLine(product.Key);
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        /// PASS
        static void Exercise23()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts();
            //create a bool to check against a productID of 789
            bool hasAMatch = false;

            foreach (var product in products)
            {
                //if productID 789 is found, hasAMatch becomes true
                if (product.ProductID == 789)
                {
                    hasAMatch = true;
                }
            }
            //has a match, print that product 789 exists
            if (hasAMatch)
            {
                Console.WriteLine("Product 789 exists");
            }
            //789 does not exist
            else
            {
                Console.WriteLine("Product 789 does not exist");
            }
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        /// PASS
        static void Exercise24()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts().Where(o => o.UnitsInStock == 0).GroupBy(o => o.Category);

            foreach (var product in products)
            {
                Console.WriteLine(product.Key);
            }

        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        /// PASS
        static void Exercise25()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts().GroupBy(o => o.Category).Where(o => o.All(u => u.UnitsInStock != 0));

            foreach (var product in products)
            {
                Console.WriteLine(product.Key);
            }

        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        /// PASS
        static void Exercise26()
        {
            //load numbers into list
            var allNumbers = (DataLoader.NumbersA).ToList().Where(o => o % 2 == 1);
            int oddNumberCounter = allNumbers.Count();
 
            Console.WriteLine("There are {0} odd numbers in Collection A", oddNumberCounter);
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        /// PASS
        static void Exercise27()
        {
            //load customers into a list
            var customers = DataLoader.LoadCustomers();
            //create a format for the output
            string line = "CUSTOMER ID: {0} ORDER COUNT: {1}";
            //make an anonymous type that contains the customerID and a count of their orders
            var customersByIdAndNumberOfOrders = from customer in customers
                                                 select new
                                                 {
                                                     CustomerID = customer.CustomerID,
                                                     OrderCount = customer.Orders.Length
                                                 };
            //for each customer from the anonymous type, print their ID and order count
            foreach (var customer in customersByIdAndNumberOfOrders)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(line, customer.CustomerID, customer.OrderCount);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        /// PASS
        static void Exercise28()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts();

            //group products by category and make a new anonymous type containing the category and a counter
            var categoriesAndStock = from product in products
                                     group product by product.Category into Categories
                                     select new
                                     {
                                         Name = Categories.Key,
                                         //take the a count unique products
                                         Inventory = Categories.Count()
                                     };

            foreach (var product in categoriesAndStock)
            {
                Console.WriteLine($"CATEGORY: {product.Name} -- UNIQUE ITEMS IN INVENTORY: {product.Inventory}");
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        /// PASS
        static void Exercise29()
        {
            //place all products into an enumerable
            List<Product> products = DataLoader.LoadProducts();

            //group products by category and make a new anonymous type containing the category and units in stock
            var categoriesAndStock = from product in products
                                     group product by product.Category into Categories
                                     select new
                                     {
                                         Name = Categories.Key,
                                         //take the sum of units in stock
                                         SumOfUnits = Categories.Sum(u => u.UnitsInStock)
                                     };

            foreach (var product in categoriesAndStock)
            {
                Console.WriteLine($"CATEGORY: {product.Name} -- TOTAL INVENTORY {product.SumOfUnits}");
            }

        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        /// PASS
        static void Exercise30()
        {
            //place all products into an enumerable
            List<Product> products = DataLoader.LoadProducts();
            //create a group with a key of category, members by unit price
            var categoriesAndAveragePrice = from product in products
                                            group product by product.Category into Categories
                                            select new
                                            {
                                                Category = Categories.Key,
                                                //find the lowest priced item in stock
                                                LowestPricedUnit = Categories.Min(u => u.UnitPrice)
                                            };

            foreach (var product in categoriesAndAveragePrice)
            {
                Console.WriteLine($"Category: {product.Category} -- Cheapest Product: {product.LowestPricedUnit:c}");
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        /// PASS
        static void Exercise31()
        {
            //place all products into a list
            var products = DataLoader.LoadProducts();
            //create a group with a key of category, members by unit price
            var categoriesAndAveragePrice = from product in products
                                            group product by product.Category into Categories
                                            select new
                                            {
                                                Name = Categories.Key,
                                                //take the average of a categorie's product price
                                                AverageOfUnits = Categories.Average(u => u.UnitPrice)
                                            };

            //order the list by descending value of the average unit cost, then take the top 3 members and put them in a seperate list
            var orderedList = categoriesAndAveragePrice.OrderByDescending(p => p.AverageOfUnits).Take(3).ToList();

            foreach (var product in orderedList)
            {
                Console.WriteLine($"CATEGORY: {product.Name} --- AVERAGE COST PER PRODUCT: {product.AverageOfUnits:c}");
            }
        }
    }

}
