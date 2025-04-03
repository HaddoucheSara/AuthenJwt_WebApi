using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentification.JWT.DAL.Entities;
using Authentification.JWT.DAL.Repositories;
using Authentification.JWT.Service.DTOs;
using AutoMapper;
using BCrypt.Net;

namespace Authentification.JWT.Service.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        // Inscription d'un utilisateur
        public async Task<UserDto> RegisterUserAsync(string username, string email, string passwordHash)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

           
          

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash
            };

            var createdUser = await _userRepository.RegisterUserAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }
        // Connexion de l'utilisateur
        public async Task<UserDto> LoginUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            return _mapper.Map<UserDto>(user);
        }
        // Vérification du mot de passe
        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
        }

       
        


    }
}
