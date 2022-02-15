using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Interfaces;
using SGBank.Responses;

namespace SGBank.BLL.DepositRules
{
    //FreeAccountDepositRule is a deposit
    public class FreeAccountDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();

            //if the account is of the wrong type, send an error message
            if(account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "ERROR: A non free account hit the Free Deposit Rule. Contact IT";
                return response;
            }

            //if a deposit over $100 is made, throw an error message
            if(amount > 100)
            {
                response.Success = false;
                response.Message = "Free accounts can't deposit more than $100 at a time";
                return response;
            }

            //if a deposit is not a positive number or greater than zero, send an error message
            if(amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amount must be greater than 0";
                return response;
            }

            //populate response if all conditions are met
            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
