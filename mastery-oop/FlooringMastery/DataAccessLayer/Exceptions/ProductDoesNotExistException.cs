using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException()
        {
            throw new NotImplementedException();
        }

        public ProductDoesNotExistException(string message) : base(message)
        {
        }

        public ProductDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
            throw new NotImplementedException();
        }
    }
}
