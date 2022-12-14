using IbanChecker.BankCodes;
using IbanChecker.Consts;
using IbanChecker.Exceptions;
using IbanChecker.Models;
using IbanChecker.Models.Enums;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace IbanChecker.Services
{
    public class BankCheckerService : IBankCheckerService
    {
        private const int MOD_97_10 = 97;
        public const string ZERO = "0";
        public const int MOD_CONTROL_NUMBER = 98;
        public string GetBankByIban(string iban)
        {
            string result = string.Empty;


            var bankCode = iban.Substring(5, 4);

            return GetBankName(bankCode);
        }

        public string GetBankByBankCode(string bankCode)
        {

            FixBankCode(ref bankCode);

            return GetBankName(bankCode);

        }



        public bool IsAkbankByBankCode(string bankCode)
        {
            var result = false;

            FixBankCode(ref bankCode);
            if (bankCode.Equals(BankCodesConts.AKBANK_CODES))
            {
                result = true;
            }

            return result;
        }

        public bool IsAkbankByBankIban(string iban)
        {
            var result = false;
            var bankCode = iban.Substring(5, 4);
            FixBankCode(ref bankCode);
            if (bankCode.Equals(BankCodesConts.AKBANK_CODES))
            {
                result = true;
            }

            return result;
        }
        public bool CheckIban(string iban)
        {

            iban = Helpers.ToDeleteSpace(iban);
            decimal last24number = 0;
            decimal controlDecimal = Decimal.Zero;
            if (iban.Length != 26)
            {
                throw new InvalidNumberOfDigitException(iban);
            }

            var ibanLast22 = iban.Substring(iban.Length - 22);
            var isNumber = Decimal.TryParse(ibanLast22, out last24number);
            if (isNumber == false) throw new InvalidLast24CharactersException(ibanLast22);

            var controlDigits = iban.Substring(2, 2);

            var ibanFirstCharacters = iban.Substring(0, 2);
            var charArray = ibanFirstCharacters.ToCharArray();
            string conversionStr = ibanLast22;

            foreach (var item in charArray)
            {
                var conversion = (int)Helpers.ToEnum<ConversionEnum>(item.ToString());
                conversionStr = conversionStr + conversion.ToString();
            }

            conversionStr = conversionStr + controlDigits;

            var isDecimal = Decimal.TryParse(conversionStr, out controlDecimal);

            if (!isDecimal) throw new InvalidLast24CharactersException(iban);

            var mod97 = (int)(controlDecimal % MOD_97_10);

            if (mod97 == 1)
            {
                return true;
            }

            return false;
        }

        public string AkbankIbanGenerate(string depertmantcode, string accountCode)
        {
            string ibanResult = string.Empty;

            var lenghtDep = depertmantcode.Length;
            if (lenghtDep < 5)
            {
                for (int i = 0; i < 5 - lenghtDep; i++)
                {
                    depertmantcode = ZERO + depertmantcode;
                }
            }

            var lengthAccount = accountCode.Length;
            if (lengthAccount < 9)
            {
                for (int i = 0; i < 9-lengthAccount; i++)
                {
                    accountCode = ZERO + accountCode;
                }
            }

            ibanResult =  "TR"+GetControlCode(depertmantcode, accountCode)+ZERO+BankCodesConts.AKBANK_CODES + depertmantcode+BankCodesConts.AKBANK_TL_ACCOUNT_CODE+ accountCode;
            var isValid = CheckIban(ibanResult);
            if (isValid)
                return ibanResult;
            else
                throw new InvalidGeneratedIbanException();

        }

        public AkbankTLAccountResponse AkbankIbanToAccountCodes(string iban)
        {
            var isCorrectIban = CheckIban(iban);
            if (!isCorrectIban) throw new InvalidIbanException();
            
            var isAkbank = IsAkbankByBankIban(iban);
            if (!isAkbank) throw new InvalidIbanException();

            var branchCode = iban.Substring(10, 4);
            var accountCode = iban.Substring(17, 9);

            return new AkbankTLAccountResponse
            {
                AccountCode = accountCode,
                BranchCode = branchCode
            };

        }


        #region Private
        private string GetControlCode(string departmentCode, string accountCode)
        {
            decimal controlNumber = decimal.Zero;
            var controlDigits = BankCodesConts.AKBANK_CODES +departmentCode+BankCodesConts.AKBANK_TL_ACCOUNT_CODE+ accountCode+ ((int)Helpers.ToEnum<ConversionEnum>("T")).ToString()
                                +((int)Helpers.ToEnum<ConversionEnum>("R")).ToString()+ZERO+ZERO;

            var isDecimal = Decimal.TryParse(controlDigits, out controlNumber);

            if (!isDecimal) throw new InvalidIbanException();

            var control = MOD_CONTROL_NUMBER - (controlNumber % MOD_97_10);

            return control.ToString();
        }




        private string GetBankName(string bankCode)
        {

            XmlSerializer serializer =
       new XmlSerializer(typeof(BankaSubeTumListe));

       
            BankaSubeTumListe listBank;

            string url = "https://eftemkt.tcmb.gov.tr/bankasubelistesi/bankaSubeTumListe.xml";

            XmlSerializer ser = new XmlSerializer(typeof(BankaSubeTumListe));

            using (WebClient client = new WebClient())
            {
                string data = Encoding.Default.GetString(client.DownloadData(url));

                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));

                listBank = (BankaSubeTumListe)ser.Deserialize(stream);
            }


            string result = string.Empty;
            var bank = listBank.BankaSubeleri.Where(x => x.Banka.BKd.Equals(bankCode)).FirstOrDefault();
            if (bank!=null)
            {
                result = bank.Banka.BAd;
            }

            return result;
        }

        private static void FixBankCode(ref string bankCode)
        {
            if (bankCode.Length<4)
            {
                var lengthStr = bankCode.Length;
                for (int i = 0; i < 4-lengthStr; i++)
                {
                    bankCode = ZERO +bankCode;
                }
            }

        }

        #endregion
    }
}
