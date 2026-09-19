using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class BankException : Exception
    {
        public BankException(string message) : base(message) { }
    }
}
