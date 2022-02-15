using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class OrderDoesNotExistException : Exception
    {
        public OrderDoesNotExistException()
        {
            throw new NotImplementedException();
        }

        public OrderDoesNotExistException(string message) : base(message)
        {
        }

        public OrderDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
            throw new NotImplementedException();
        }
    }
}
