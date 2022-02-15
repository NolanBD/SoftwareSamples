using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Responses
{
    public class AccountWithdrawResponse : Response 
    {
        //defining properties of our withdraw response object
        public Account Account { get; set; }
        public decimal OldBalance { get; set; }
        public decimal Amount { get; set; }
    }
}
