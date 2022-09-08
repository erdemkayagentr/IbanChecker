string iban = String.Empty;
bool isCheck = false;
Console.WriteLine("Enter Your Iban: ");
iban = Console.ReadLine();

isCheck = IbanChecker.IbanChecker.CheckIban(iban);

Console.ReadLine();
