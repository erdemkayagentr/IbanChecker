namespace IbanChecker.Services
{
    public interface IBankCheckerService
    {
        /// <summary>
        /// Get Bank Name By Iban Number
        /// </summary>
        /// <param name="iban">
        /// You must enter iban with country code for example: TRxxxxxx
        /// </param>
        /// <returns></returns>
        string GetBankByIban(string iban);

        /// <summary>
        /// Get Bank name by bankCode
        /// This method work with online
        /// </summary>
        /// <param name="bankCode">
        /// bankCode Length must be 4 or less
        /// </param>
        /// <returns></returns>
        string GetBankByBankCode(string bankCode);
        /// <summary>
        /// Akbank BankCode Control
        /// </summary>
        /// <param name="bankCode">
        /// bankCode Length must be 4 or less</param>
        /// <returns></returns>
        bool IsAkbankByBankCode(string bankCode);
        /// <summary>
        /// Akbank Iban Control
        /// </summary>
        /// <param name="iban">
        /// You must enter iban with country code for example: TRxxxxxx
        /// </param>
        /// <returns></returns>
        bool IsAkbankByBankIban(string iban);
        bool CheckIban(string iban);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchCode">
        /// bankCode Length must be 4 or less
        /// </param>
        /// <param name="accountCode">
        /// </param>
        /// <returns></returns>
        string AkbankIbanGenerate(string branchCode, string accountCode);
    }
}
