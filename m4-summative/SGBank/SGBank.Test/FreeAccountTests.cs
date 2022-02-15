using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Interfaces;
using SGBank.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Test
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [TestCase ("12345", "Free Account", 100, AccountType.Free, 250, false)]
        [TestCase ("12345", "Free Account", 100, AccountType.Free, -100, false)]
        [TestCase ("12345", "Free Account", 100, AccountType.Basic, 50, false)]
        [TestCase ("12345", "Free Account", 100, AccountType.Free, 50, true)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit testDeposit = new FreeAccountDepositRule();
            Account testAccount = new Account();
            testAccount.AccountNumber = accountNumber;
            testAccount.Name = name;
            testAccount.Balance = balance;
            testAccount.Type = accountType;

            AccountDepositResponse response = testDeposit.Deposit(testAccount, amount);

            Assert.AreEqual(response.Success, expectedResult);
        }

        [TestCase ("12345", "Free Account", 80, AccountType.Free, 100, false)]
        [TestCase ("12345", "Free Account", 80, AccountType.Free, -101, false)]
        [TestCase ("12345", "Free Account", 80, AccountType.Basic, -50, false)]
        [TestCase ("12345", "Free Account", 80, AccountType.Free, -99, false)]
        [TestCase ("12345", "Free Account", 80, AccountType.Free, -50, true)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw testWithdraw = new FreeAccountWithdrawRule();
            Account testAccount = new Account();

            testAccount.AccountNumber = accountNumber;
            testAccount.Name = name;
            testAccount.Balance = balance;
            testAccount.Type = accountType;

            AccountWithdrawResponse response = testWithdraw.Withdraw(testAccount, amount);

            Assert.AreEqual(response.Success, expectedResult);
        }
    }
}
