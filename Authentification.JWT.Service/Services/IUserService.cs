using Authentification.JWT.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(string username, string email, string password);
        Task<UserDto> LoginUserAsync(string username, string password);
        bool VerifyPassword(string enteredPassword, string storedPasswordHash);
    }
}
