using System;

namespace Expert.Goggles.GoogleDrive.Exceptions
{
    public class NoRegistryKeyException : Exception
    {
        public NoRegistryKeyException(string message = null) : base(message) { }
    }
}
