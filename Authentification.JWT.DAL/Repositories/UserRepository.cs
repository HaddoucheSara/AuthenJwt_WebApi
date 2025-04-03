using Authentification.JWT.DAL.Data.Context;
using Authentification.JWT.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Authentification.JWT.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
