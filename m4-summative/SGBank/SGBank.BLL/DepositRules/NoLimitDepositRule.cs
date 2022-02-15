using SGBank.Interfaces;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.DepositRules
{
    //NoLimitDepositRule is a deposit
    public class NoLimitDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();

            //if the account type is free, send an error message
            if (account.Type == AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: Only basic and premium accounts can deposit with no limit. Contact IT";
                return response;
            }

            //if the deposit is a negative number or 0, send an error message
            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amount must be positive!";
                return response;
            }

            //if all conditions are met, populate response
            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            response.Account.Balance = response.Account.Balance + amount;
            return response;
        }
    }
}
