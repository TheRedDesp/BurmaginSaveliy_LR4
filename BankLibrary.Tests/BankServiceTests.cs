using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Tests
{
    public class BankServiceTests
    {
        private readonly BankService _bank = new BankService();

        [Fact]
        public void Deposit_IncreasesBalance()
        {
            var acc = _bank.Create("Иванов");
            _bank.Deposit(acc.Id, 1000);
            Assert.Equal(1000, acc.Balance);
        }

        [Fact]
        public void Withdraw_DecreasesBalance()
        {
            var acc = _bank.Create("Петров");
            _bank.Deposit(acc.Id, 500);
            _bank.Withdraw(acc.Id, 200);
            Assert.Equal(300, acc.Balance);
        }

        [Fact]
        public void Withdraw_TooMuch_Throws()
        {
            var acc = _bank.Create("Сидоров");
            _bank.Deposit(acc.Id, 100);
            var ex = Assert.Throws<BankException>(() => _bank.Withdraw(acc.Id, 500));
            Assert.Contains("Недостаточно средств", ex.Message);
        }

        [Fact]
        public void GetById_NotFound_Throws()
        {
            Assert.Throws<BankException>(() => _bank.GetById(999));
        }

        [Fact]
        public void Create_EmptyName_Throws()
        {
            Assert.Throws<BankException>(() => _bank.Create(""));
        }

        [Fact]
        public void TryCatchFinally_ContinuesWorking()
        {
            string log = "";
            try
            {
                _bank.Withdraw(1, 100);
                log += "не должно выполниться;";
            }
            catch (BankException ex)
            {
                log += $"поймано: {ex.Message};";
            }
            finally
            {
                log += "finally выполнен;";
            }

            var acc = _bank.Create("Новый клиент");
            log += $"создан счёт {acc.Id}";

            Assert.Contains("поймано:", log);
            Assert.Contains("finally выполнен", log);
            Assert.Contains("создан счёт 1", log);
        }
    }
}
