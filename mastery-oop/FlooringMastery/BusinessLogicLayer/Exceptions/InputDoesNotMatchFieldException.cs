using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Exceptions
{
    class InputDoesNotMatchFieldException : Exception
    {
        public InputDoesNotMatchFieldException()
        {
            throw new NotImplementedException();
        }

        public InputDoesNotMatchFieldException(string message) : base(message)
        {
        }

        public InputDoesNotMatchFieldException(string message, Exception inner) : base(message, inner)
        {
            throw new NotImplementedException();
        }
    }
}
