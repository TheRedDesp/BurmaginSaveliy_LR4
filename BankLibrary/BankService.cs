using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class BankService : IBankService
    {
        private readonly List<Account> _accounts = new();
        private int _nextId = 1;

        public Account Create(string owner)
        {
            if (string.IsNullOrWhiteSpace(owner))
                throw new BankException("Имя владельца не может быть пустым.");

            var acc = new Account { Id = _nextId++, Owner = owner, Balance = 0 };
            _accounts.Add(acc);
            return acc;
        }

        public Account GetById(int id)
        {
            var acc = _accounts.FirstOrDefault(a => a.Id == id);
            if (acc == null)
                throw new BankException($"Счёт с идентификатором {id} не найден.");
            return acc;
        }

        public void Deposit(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new BankException("Сумма пополнения должна быть больше нуля.");

            GetById(accountId).Balance += amount;
        }

        public void Withdraw(int accountId, decimal amount)
        {
            if (amount <= 0)
                throw new BankException("Сумма снятия должна быть больше нуля.");

            var acc = GetById(accountId);
            if (acc.Balance < amount)
                throw new BankException($"Недостаточно средств. Баланс: {acc.Balance}, запрошено: {amount}.");

            acc.Balance -= amount;
        }
    }
}
