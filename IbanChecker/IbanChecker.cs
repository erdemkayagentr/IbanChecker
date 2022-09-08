using IbanChecker.Exceptions;

namespace IbanChecker
{
    public static class IbanChecker
    {
        public static bool CheckIban(string iban)
        {
            iban = iban.Trim().Replace(" ","");
            decimal last24number = 0;
            decimal controlDecimal = Decimal.Zero;
            if (iban.Length != 26)
            {
                throw new InvalidNumberOfDigitException(iban);
            }

            var ibanLast22 = iban.Substring(iban.Length - 22);
            var isNumber = Decimal.TryParse(ibanLast22, out last24number);
            if (isNumber == false) throw new InvalidLast24CharactersException(ibanLast22);

            var controlCharacters = iban.Substring(2, 2);

            var ibanFirstCharacters = iban.Substring(0, 2);
            var charArray = ibanFirstCharacters.ToCharArray();
            string conversionStr = ibanLast22;
            foreach (var item in charArray)
            {
                var conversion = (int)Helpers.ToEnum<Conversion>(item.ToString());
                conversionStr = conversionStr + conversion.ToString();
            }

            conversionStr = conversionStr + controlCharacters;

            var isDecimal = Decimal.TryParse(conversionStr,out controlDecimal);

            if (!isDecimal) throw new InvalidLast24CharactersException(iban);

            var mod97 = (int)(controlDecimal % 97);

            if (mod97 == 1)
            {
                return true;
            }

            return false;
        }
    }
}
