using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Responses
{
    public class Response
    {
        //populating our base class of response with a simple pass/fail bool and a string for sending error messages
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
