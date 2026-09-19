using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public interface IBankService
    {
        Account Create(string owner);
        Account GetById(int id);
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
    }
}
