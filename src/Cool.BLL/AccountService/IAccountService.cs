using System.Threading.Tasks;
using Cool.Common.DTOs;

namespace Cool.Bll.AccountService
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);

        Task<string> GetSaltForUser(string userName);

        Task<string> Login(LoginDto dto);
    }
}
