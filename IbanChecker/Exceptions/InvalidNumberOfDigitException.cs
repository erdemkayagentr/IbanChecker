namespace IbanChecker.Exceptions
{
    public class InvalidNumberOfDigitException : Exception
    {
        public InvalidNumberOfDigitException(string iban) : base($"Your Iban Number Must Consist Of 26 Character With Your Country Code TRXX...XX Your Iban is {iban.Length} Character")
        {
            
        }
    }
}
