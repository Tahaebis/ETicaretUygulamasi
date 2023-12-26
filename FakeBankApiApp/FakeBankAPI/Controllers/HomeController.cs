using FakeBankAPI.Entities;
using FakeBankAPI.Models;
using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Mvc;

namespace FakeBankAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private BankDatabaseContext _db;

        public HomeController(BankDatabaseContext db)
        {
            _db = db;
        }

        [HttpGet]
        public string InitialData()
        {
            for (int i = 0; i < 10; i++)
            {
                int month = NumberData.GetNumber(1, 12);
                int year = NumberData.GetNumber(DateTime.Now.Year, DateTime.Now.Year + i + 1);

                BankAccount bankAccount = new BankAccount()
                {
                    CardHolder = NameData.GetFullName(),
                    CardNo = TextData.GetNumeric(16),
                    Balance = NumberData.GetNumber(3000, 10000),
                    Month = month.ToString().PadLeft(2, '0'),
                    Year = year.ToString(),
                    CVC = NumberData.GetNumber(100, 999).ToString()
                };

                _db.BankAccounts.Add(bankAccount);
            }

            _db.SaveChanges();

            return "ok";
        }

        [HttpGet]
        public List<BankAccount> List()
        {
            return _db.BankAccounts.ToList();
        }

        [HttpPut]
        public IActionResult UpdateBalance([FromBody] UpdateBalanceModel model)
        {
            BankAccount bankAccount = _db.BankAccounts.FirstOrDefault(x => x.CardNo == model.CardNo);

            if (bankAccount == null)
            {
                return NotFound("Kart bulunamadý.");
            }

            if (model.NewBalance < 0)
            {
                return BadRequest("Yeni bakiye sýfýrdan küçük olamaz.");
            }

            bankAccount.Balance = model.NewBalance;
            _db.SaveChanges();

            return Ok();
        }

        
        // Home/DeleteBankAccount/12312312321/123
        [HttpDelete("{cardno}/{cvc}")]
        public IActionResult DeleteBankAccount(string cardno, string cvc)
        {
            BankAccount bankAccount = _db.BankAccounts.FirstOrDefault(x => x.CardNo == cardno && x.CVC == cvc);

            if (bankAccount == null)
            {
                return NotFound("Kart bulunamadý.");
            }

            _db.BankAccounts.Remove(bankAccount);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult WithdrawMoney([FromBody] WithdrawMoneyModel model)
        {
            BankAccount bankAccount = _db.BankAccounts
                .Where(x =>
                    x.CardNo == model.CardNo &&
                    x.CardHolder.ToLower() == model.CardHolder.ToLower() &&
                    x.Month == model.Month &&
                    x.Year == model.Year &&
                    x.CVC == model.CVC)
                .FirstOrDefault();

            if (bankAccount == null)
            {
                return NotFound("Kart bulunamadý.");
            }

            if (bankAccount.Balance < model.Amount)
            {
                return BadRequest("Kart bakiyesi yetersiz.");
            }

            bankAccount.Balance -= model.Amount;
            _db.SaveChanges();

            return Ok();
        }
    }
}
