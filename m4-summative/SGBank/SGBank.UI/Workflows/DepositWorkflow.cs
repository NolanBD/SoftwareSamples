using SGBank.BLL;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class DepositWorkflow
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager accountManager = AccountManagerFactory.Create();

            Console.Write("Enter and account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter a deposit amount: ");
            string depositInputAmount = Console.ReadLine();
            //IsDecimalCheck takes the input of the depositInputAmount and parses it to confirm it is a decimal
            decimal amount = IsDecimalCheck.DecimalCheck(depositInputAmount);

            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Deposit Completed!");
                Console.WriteLine($"Account number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old balance: ${response.OldBalance}");
                Console.WriteLine($"Deposit amount: {response.Amount:c}");
                Console.WriteLine($"New balance: ${response.Account.Balance}");
            }
            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
