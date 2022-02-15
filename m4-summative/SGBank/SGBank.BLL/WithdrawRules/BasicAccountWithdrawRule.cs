using SGBank.Interfaces;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    //BasicAccountWithdrawRule is a withdraw
    public class BasicAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            //if the account type is not basic, send an error message
            if (account.Type != AccountType.Basic)
            {
                response.Success = false;
                response.Message = "Error: a non-basic account hit the Basic Withdraw Rule. Contact IT";
                return response;
            }

            //if the withdraw amount is not negative, send an error message
            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdraw must be negative!";
                return response;
            }

            //if the amount to be withdrawn exceeds $500, send an error message
            if (amount < -500)
            {
                response.Success = false;
                response.Message = "Basic Accounts can't withdraw more than $500 at a time!";
                return response;
            }

            //if the with withdraw sends the account below $-100, send an error message
            if (account.Balance < -100)
            {
                response.Success = false;
                response.Message = "This withdraw will exceed your $-100 limit!";
                return response;
            }


            //success, populate response with the updated account info
            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            account.Balance = account.Balance + amount;

            //if the new balance is below 0, deduct $10
            if (account.Balance < 0)
            {
                account.Balance = account.Balance - 10;
            }

            return response;
        }
    }
}
