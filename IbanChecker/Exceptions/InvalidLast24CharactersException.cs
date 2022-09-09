using System;

namespace IbanChecker.Exceptions
{
    public class InvalidLast24CharactersException : Exception
    {
        public InvalidLast24CharactersException(string iban) : base($"Your Last 24 Character Must Be Number. Your Character Is {iban}")
        {
            
        }
    }
}
