using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto1
{
    public class Pippo
    {
        private readonly IAccountService _accountService;

        public Pippo(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void AddNewUser()
        {
            _accountService.AddAccount();
        }
    }


    public interface IAccountService
    {
        Account AddAccount();
    }


    public class AccountService : IAccountService
    {
        public Account AddAccount()
        {
            throw new NotImplementedException();
        }
    }

    public class Account
    {
        public string name { get; set; }
        public string surname { get; set; }
    }
}
