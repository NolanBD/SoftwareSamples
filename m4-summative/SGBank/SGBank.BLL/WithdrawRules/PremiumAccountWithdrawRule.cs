using SGBank.Interfaces;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            //if the account isn't premium, return an error message
            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "ERROR: A non-premium account type hit the Premium Withdraw Rule. Contact IT";
                return response;
            }

            //if the withdraw amount is not a negative number, return an error message
            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdraws amounts must be negative!";
                return response;
            }

            //if the withdraw amount exceeds the $500 overdraft limit, send an error message
            if (account.Balance + amount < -500)
            {
                response.Success = false;
                response.Message = "This withdraw exceeded your $-500 overdraft limit!";
                return response;
            }

            //success, populate response with the updated account information
            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            account.Balance = account.Balance + amount;

            return response;
        }
    }
}
