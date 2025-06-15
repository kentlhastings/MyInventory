using Microsoft.AspNetCore.Mvc;
using MyInventory.Logic;

namespace MyInventory.Service
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountLogic _accountLogic;

        public AccountController(AccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }
    }
}
