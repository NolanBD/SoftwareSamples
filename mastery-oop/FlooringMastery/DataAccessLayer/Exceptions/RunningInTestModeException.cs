using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class RunningInTestModeException : Exception
    {
        public RunningInTestModeException()
        {
            throw new NotImplementedException();
        }

        public RunningInTestModeException(string message) : base(message)
        {
        }

        public RunningInTestModeException(string message, Exception inner) : base(message, inner)
        {
            throw new NotImplementedException();
        }
    }
}

