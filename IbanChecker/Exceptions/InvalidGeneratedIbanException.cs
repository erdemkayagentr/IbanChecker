using System;

namespace IbanChecker.Exceptions
{
    public class InvalidGeneratedIbanException : Exception
    {
        public InvalidGeneratedIbanException() : base("Iban cannot generated!")
        {

        }
    }
}
