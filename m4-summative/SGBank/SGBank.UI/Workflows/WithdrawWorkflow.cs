using SGBank.BLL;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class WithdrawWorkflow
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager accountManager = AccountManagerFactory.Create();

            Console.WriteLine("Please enter an account number");
            string accountNumberInput = Console.ReadLine();

            Console.WriteLine("Please enter a withdraw amount");
            string withdrawAmountInput = Console.ReadLine();
            //IsDecimalCheck takes the input of the withdrawAmountInput and parses it to confirm it is a decimal
            decimal amount = IsDecimalCheck.DecimalCheck(withdrawAmountInput);

            AccountWithdrawResponse response = accountManager.Withdraw(accountNumberInput, amount);

            if (response.Success)
            {
                Console.WriteLine("Withdraw Complete!");
                Console.WriteLine($"Account number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old balance: ${response.OldBalance}");
                Console.WriteLine($"Withdraw amount: {response.Amount:c}");
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
