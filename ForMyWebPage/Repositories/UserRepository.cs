using ForMyWebPage.Repositories.IRepositories;
using ForMyWebPage.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ForMyWebPage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlbumAppContext _context;
        public UserRepository( AlbumAppContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            Random random = new Random();
            user.Id = random.Next(99);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<User> GetById(int? userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
