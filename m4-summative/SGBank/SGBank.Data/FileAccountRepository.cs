using SGBank.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        string[] rows;
        List<Account> accounts;

        public FileAccountRepository()
        {
            //add the contents of our repository to rows
            rows = File.ReadAllLines(@"C:\Software Guild\Summatives\m4-summative\SGBank\SGBank.Data\Accounts.txt");
            accounts = new List<Account>();
            _createListFromFile();
        }

        //delimit and assign values from rows into columns, then populate an account object with information from each column, add account to a list
        private void _createListFromFile()
        {
            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Account a = new Account();
                a.AccountNumber = columns[0];
                a.Name = columns[1];
                a.Balance = Convert.ToDecimal(columns[2]);

                if (columns[3] == "F")
                {
                    a.Type = AccountType.Free;
                }

                if (columns[3] == "B")
                {
                    a.Type = AccountType.Basic;
                }

                if (columns[3] == "P")
                {
                    a.Type = AccountType.Premium;
                }

                accounts.Add(a);
            }
        }

        //convert account information into string information to be saved back into our rows[] string array
        private void _saveTextFromList()
        {
            //take the AccountType of an account and convert to a single character
            for (int i = 0; i < accounts.Count; i++)
            {
                string accountType = "";

                if (accounts[i].Type == AccountType.Free)
                {
                    accountType = "F";
                }

                if (accounts[i].Type == AccountType.Basic)
                {
                    accountType = "B";
                }

                if (accounts[i].Type == AccountType.Premium)
                {
                    accountType = "P";
                }

                rows[i + 1] = accounts[i].AccountNumber + "," + accounts[i].Name + "," + accounts[i].Balance + "," + accountType;
            }
        }

        public Account LoadAccount(string accountNumber)
        {  
            //for each account in our list of accounts, if the accountNumber submitted matches the account number of that account, return the account
            foreach (var account in accounts)
            {
                if (accountNumber == account.AccountNumber)
                {
                    return account;
                }
            }

            return null;
        }

        public void SaveAccount(Account account)
        {
            _saveTextFromList();
            string textFile = "";
            //save each memeber of our string array rows[] to a single string formated for re-entry into our file repository
            foreach (var row in rows)
            {
                textFile += row + "\n";
            }

            File.WriteAllText(@"C:\Software Guild\Summatives\m4-summative\SGBank\SGBank.Data\Accounts.txt", textFile);
        }
    }
}
