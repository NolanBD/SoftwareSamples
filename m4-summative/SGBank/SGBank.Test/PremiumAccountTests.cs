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
    public class PremiumAccountTests
    {
        [Test]
        public void CanLoadPremiumAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("99999");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("99999", response.Account.AccountNumber);
        }

        //Case 1: Fail, account type incorrect
        //Case 2: Fail, cannot deposit a negative number
        //Case 3: Pass, all criteria met
        [TestCase("99999", "Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, -10, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, 10000, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
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

        //Case 1: Fail, exceeds the $500 overdraft limit
        //Case 2: Fail, attempt to withdraw a positive value
        //Case 3: Fail, account type free
        //Case 4: Fail, account type basic
        //Case 5: Pass, all standards met
        [TestCase ("99999", "Premium Account", 100, AccountType.Premium, -601, false)]
        [TestCase ("99999", "Premium Account", 100, AccountType.Premium, 100, false)]
        [TestCase ("99999", "Premium Account", 100, AccountType.Free, -200, false)]
        [TestCase ("99999", "Premium Account", -99, AccountType.Basic, -5, false)]
        [TestCase ("99999", "Premium Account", 100, AccountType.Premium, -200, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdrawTest = new PremiumAccountWithdrawRule();
            Account accountTest = new Account();

            accountTest.AccountNumber = accountNumber;
            accountTest.Name = name;
            accountTest.Type = accountType;
            accountTest.Balance = balance;

            AccountWithdrawResponse response = new AccountWithdrawResponse();
            response = withdrawTest.Withdraw(accountTest, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
