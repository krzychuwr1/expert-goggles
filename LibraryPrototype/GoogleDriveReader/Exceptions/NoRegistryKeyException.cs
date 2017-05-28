using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDrive.Exceptions
{
    public class NoRegistryKeyException : Exception
    {
        public NoRegistryKeyException(string message = null) : base(message) { }
    }
}
