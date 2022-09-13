using IbanChecker.Services;

//string depertmant = String.Empty;
//string account = String.Empty;
string iban = string.Empty;
bool isCheck = false;
Console.WriteLine("Enter Your Department: ");
iban = Console.ReadLine();
//Console.WriteLine("Enter Your Account COde: ");
//account = Console.ReadLine();

IBankCheckerService service = new BankCheckerService();
//service.GetBankByIban(iban);
//var iban = IbanChecker.IbanChecker.AkbankIbanGenerate(depertmant,account);
Console.WriteLine(service.AkbankIbanGenerate("0646", "000170851"));
Console.ReadLine();
