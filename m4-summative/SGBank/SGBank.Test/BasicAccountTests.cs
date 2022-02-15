using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Interfaces;
using SGBank.Responses;

namespace SGBank.Test
{
    [TestFixture]
    public class BasicAccountTests
    {
        [Test]
        public void CanLoadBasicAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("33333");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("33333", response.Account.AccountNumber);
        }

        [TestCase ("33333", "Basic Account", 100, AccountType.Free, 250, false)]
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, -10, false)]
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, 250, true)]
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit depositTest = new NoLimitDepositRule();
            Account accountTest = new Account();
            
            accountTest.AccountNumber = accountNumber;
            accountTest.Name = name;
            accountTest.Type = accountType;
            accountTest.Balance = balance;

            AccountDepositResponse response = new AccountDepositResponse();
            response = depositTest.Deposit(accountTest, amount);

            Assert.AreEqual(expectedResult, response.Success);
        } 

        [TestCase ("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)]
        [TestCase ("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]
        [TestCase ("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdrawTest = new BasicAccountWithdrawRule();
            Account accountTest = new Account();
            accountTest.AccountNumber = accountNumber;
            accountTest.Name = name;
            accountTest.Balance = balance;
            accountTest.Type = accountType;

            AccountWithdrawResponse response = new AccountWithdrawResponse();
        }
    }
}
