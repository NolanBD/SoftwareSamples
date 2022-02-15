using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class StateDoesNotExistException : Exception
    {
        public StateDoesNotExistException()
        {
            throw new NotImplementedException();
        }

        public StateDoesNotExistException(string message) : base(message)
        {
        }

        public StateDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
            throw new NotImplementedException();
        }
    }
}
