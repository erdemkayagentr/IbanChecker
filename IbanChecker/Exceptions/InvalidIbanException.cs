using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbanChecker.Exceptions
{
    public class InvalidIbanException : Exception
    {
        public InvalidIbanException() : base("Iban cannot generated!")
        {

        }
    }
}
