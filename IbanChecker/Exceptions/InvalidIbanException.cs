using System;

namespace IbanChecker.Exceptions
{
    public class InvalidIbanException : Exception
    {
        public InvalidIbanException() : base("Invalid Iban")
        {
                
        }
    }
}
