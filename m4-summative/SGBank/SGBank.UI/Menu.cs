using SGBank.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                //formating menu output
                Console.Clear();
                Console.WriteLine("SG BANK APPLICATION");
                Console.WriteLine("----------------");
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");

                Console.WriteLine("\nQ to Quit");
                Console.Write("\nEnter Selection\n");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    //look up account
                    case "1":
                        AccountLookupWorkflow lookupWorkFlow = new AccountLookupWorkflow();
                        lookupWorkFlow.Execute();
                        break;
                        //deposit
                    case "2":
                        DepositWorkflow depositWorkflow = new DepositWorkflow();
                        depositWorkflow.Execute();
                        break;
                        //withdraw
                    case "3":
                        WithdrawWorkflow withdrawWorkflow = new WithdrawWorkflow();
                        withdrawWorkflow.Execute();
                        break;
                        //exit
                    case "Q":
                        return;

                }
            }
        }
    }
}
