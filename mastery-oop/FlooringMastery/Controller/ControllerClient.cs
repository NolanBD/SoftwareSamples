using BusinessLogicLayer;
using DataAccessLayer.Exceptions;
using Models;
using System;
using System.Collections.Generic;
using View;


namespace Controller
{
    public class ControllerClient
    {
        ViewClient view;
        BusinessLogicImpl calculator;

        public ControllerClient()
        {
            view = new ViewClient();
            calculator = new BusinessLogicImpl();
        }

        public void Run()
        {
<<<<<<< Updated upstream
=======
            int userChoice = view.ShowMenuAndGetUserChoice();

>>>>>>> Stashed changes
            bool stillRunning = true;

            while (stillRunning)
            {
<<<<<<< Updated upstream
                int userChoice = view.ShowMenuAndGetUserChoice();
=======
>>>>>>> Stashed changes
                try
                {
                    switch (userChoice)
                    {
                        case 1:
                            ShowAllOrders();
                            break;
                        case 2:
                            ShowOrder();
                            break;
                        case 3:
                            CreateOrder();
                            break;
                        case 4:
                            _UpdateOrder();
                            break;
                        case 5:
                            DeleteOrder();
                            break;
                        case 6:
                            stillRunning = false;
                            break;
                    }
                }
                catch (OrderDoesNotExistException e)
                {
                    view.DisplayExceptionMessage(e.Message);
<<<<<<< Updated upstream
=======
                    Run();
>>>>>>> Stashed changes
                }
                catch (ProductDoesNotExistException e)
                {
                    view.DisplayExceptionMessage(e.Message);
<<<<<<< Updated upstream
=======
                    Run();
>>>>>>> Stashed changes
                }
                catch (StateDoesNotExistException e)
                {
                    view.DisplayExceptionMessage(e.Message);
<<<<<<< Updated upstream
=======
                    Run();
>>>>>>> Stashed changes
                }
                catch (RunningInTestModeException e)
                {
                    view.DisplayExceptionMessage(e.Message);
<<<<<<< Updated upstream
=======
                    Run();
>>>>>>> Stashed changes
                }
            }

            Quit();

        }

        private void ShowOrder()
        {
            //retrieve order date and order number from the client
            DateTime orderDate = view.GetOrderDate();
            int orderNumber = view.GetOrderID();

            //populate an order object from the repository and display to the user
            Order order = calculator.RetrieveOrderByID(orderDate, orderNumber);
            view.DisplayOrder(order);
            view.ShowActionSuccess("");
<<<<<<< Updated upstream
=======
            Run();
>>>>>>> Stashed changes
        }

        private void ShowAllOrders()
        {
            //retrieve order date from user
            DateTime orderDate = view.GetOrderDate();

            //populate a list of orders from the repository
            List<Order> orders = calculator.RetriveOrdersByDate(orderDate);
            view.DisplayAllOrders(orders);
            view.ShowActionSuccess("");
<<<<<<< Updated upstream
=======
            Run();
>>>>>>> Stashed changes
        }

        private void CreateOrder()
        {
            //populate a list of products and a list of state names
            List<Product> products = calculator.GetAllProducts();
            List<StateTax> states = calculator.GetAllTaxInformation();

            //take our user inputs and populate an order object.
            //calculate the cost and order number of the order
            Order order = view.GetNewOrderInfo(products, states);
            order = calculator.CalculateCostForOrder(order);
            order = calculator.CalculateOrder(order);

            //show the order to the client
            bool isCorrect = view.ConfirmCorrectOrder(order, "create the following order");

            //if the order is to the satisfaction of the customer, add the order to our repository
            if (isCorrect)
            {
                order = calculator.AddOrderToRepository(order);
<<<<<<< Updated upstream
                view.ShowActionSuccess($"Your order will be fullfilled on {order.OrderDate.ToString("MM / dd/ yyyy")} and your order number is {order.OrderNumber}");
=======
                view.ShowActionSuccess($"Your order will be fullfilled on {order.OrderDate.ToString("MM / dd / yyyy")} and your order number is {order.OrderNumber}");
                Run();
>>>>>>> Stashed changes
            }
            //don't save the order and return the user to the main menu
            else
            {
                view.ShowActionFailure("Order Creation Canceled.");
<<<<<<< Updated upstream
=======
                Run();
>>>>>>> Stashed changes
            }
        }

        private void _UpdateOrder()
        {
            //populate a list of products and a list of state names
            List<Product> products = calculator.GetAllProducts();
            List<StateTax> states = calculator.GetAllTaxInformation();
            //get an order date and number from the user
            DateTime orderDate = view.GetOrderDate();
            int orderNumber = view.GetOrderID();

            //retrieve the order from the repository
            Order order = calculator.RetrieveOrderByID(orderDate, orderNumber);
            //get updated information from the user
            order = view.GetUpdatedOrderInfo(order, products, states);
            //calculate the cost of the new order
            order = calculator.ModifyOrder(order);

            bool isCorrect = view.ConfirmCorrectOrder(order, "update the following order");

            //if the user is satisfied the the new information, save the order to the repository
            if (isCorrect)
            {
                order = calculator.AddOrderToRepository(order);
                view.ShowActionSuccess($"Your updated order will be fullfilled on {order.OrderDate.ToString("MM / dd / yyyy")} and your order number is still {order.OrderNumber}");
<<<<<<< Updated upstream
=======
                Run();
>>>>>>> Stashed changes
            }
            //do not save the order info and return to main menu
            else
            {
                view.ShowActionFailure("Order Update Canceled.");
<<<<<<< Updated upstream
=======
                Run();
>>>>>>> Stashed changes
            }
        }

        private void DeleteOrder()
        {
            //get order info from the user
            DateTime orderDate = view.GetOrderDate();
            int orderNumber = view.GetOrderID();

            //retrieve the order from the repository
            Order orderToDelete = calculator.RetrieveOrderByID(orderDate, orderNumber);

            //ask the user if this is the order they want to delete
            bool deleteThisOrder = view.DeleteOrder(orderToDelete);

            //if yes, delete the order
            if (deleteThisOrder)
            {
                bool hasBeenDeleted = calculator.DeleteOrder(orderDate, orderNumber);

                //if the deletion was a success, hasBeenDeleted is true
                if (hasBeenDeleted)
                {
                    view.ShowActionSuccess("Your order has been successfully removed from our inventory.");
<<<<<<< Updated upstream
=======
                    Run();
>>>>>>> Stashed changes
                }
                else
                {
                    view.ShowActionFailure("We were unable to delete this order.");
<<<<<<< Updated upstream
=======
                    Run();
>>>>>>> Stashed changes
                }
            }
            else
            {
                view.ShowActionFailure("Order Deletion Canceled.");
<<<<<<< Updated upstream
=======
                Run();
>>>>>>> Stashed changes
            }
        }

        private void Quit()
        {
            bool endProgram = view.EndProgram();

            if (endProgram)
            {
                Environment.Exit(0);
            }
            else
            {
                view.ShowActionFailure("The program will continue to run.");
                Run();
            }
        }
    }
}
