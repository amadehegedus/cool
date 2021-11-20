using System.Threading.Tasks;
using Cool.Bll.AccountService;
using Cool.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cool.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task Register(RegisterDto dto)
        {
            await _accountService.Register(dto);
        }

        [HttpGet]
        public async Task<string> GetSaltForUser(string userName)
        {
            return await _accountService.GetSaltForUser(userName);
        }

        [HttpPost]
        public async Task<string> Login(LoginDto dto)
        {
            return await _accountService.Login(dto);
        }
    }
}
