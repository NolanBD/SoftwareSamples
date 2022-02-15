using SGBank.Interfaces;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    //FreeAccountWithdrawRule is a withdraw
    public class FreeAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            //if the account type isn't free, return an error message
            if (account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "ERROR: A non-free account hit the Free Withdraw Rule. Contact IT.";
                return response;
            }

            //if the withdraw amount is not negative, return an error message
            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdraws amounts must be negative!";
                return response;
            }

            //if the withdraw amount exceeds $100, return an error message
            if (amount < -100)
            {
                response.Success = false;
                response.Message = "Free Accounts cannot withdraw more than $100 at a time!";
                return response;
            }

            //if the withdraw would make the account balance a negative number, send an error message
            if (account.Balance + amount < 0)
            {
                response.Success = false;
                response.Message = "Free accounts cannot overdraft!";
                return response;
            }

            //success, update response with the new account info
            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            account.Balance = account.Balance + amount;
            return response;
        }
    }
}
