using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cool.Common.DTOs;
using Cool.Common.Exceptions;
using Cool.Common.Options;
using Cool.Dal;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cool.Bll.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly CoolDbContext _context;
        private readonly AuthenticationOptions _options;

        public AccountService(CoolDbContext context, IOptions<AuthenticationOptions> options)
        {
            _context = context;
            _options = options.Value;
        }

        public async Task Register(RegisterDto dto)
        {
            if(_context.Users.Any(u => u.UserName == dto.UserName))
                throw new BadRequestException("Username already taken!");

            await _context.Users.AddAsync(new User
            {
                Email = dto.Email,
                Name = dto.FullName,
                UserName = dto.UserName,
                Password = dto.PasswordHash,
                Salt = dto.Salt,
                Role = "User"
            });

            await _context.SaveChangesAsync();
        }

        public async Task<string> GetSaltForUser(string userName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);

            if(user == null)
                throw new NotFoundException($"User {userName} was not found!");

            return user.Salt;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
                throw new NotFoundException($"User {dto.UserName} was not found!");

            if(user.Password != dto.PasswordHash)
                throw new BadRequestException("Wrong username or password!");

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role), 
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(_options.JwtKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
