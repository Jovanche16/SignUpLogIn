using ForMyWebPage.Models;
using ForMyWebPage.Models.Responses;
using ForMyWebPage.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ForMyWebPage.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly  AlbumAppContext _context;
        public RefreshTokenRepository( AlbumAppContext context)
        {
            _context = context;
        }

        public  async Task<RefreshTokenDto> Create(RefreshTokenDto refreshToken)
        {
            Random random = new Random();
            refreshToken.Id = random.Next(99);
             _context.RefreshTokenDtos.Add(refreshToken);
             await _context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task Delete(int id)
        {
            RefreshTokenDto refreshToken = await _context.RefreshTokenDtos.FindAsync(id);
            if(refreshToken !=null)
            {
                _context.RefreshTokenDtos.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RefreshTokenDto> GetByToken(string token)
        {

            return await _context.RefreshTokenDtos.FirstOrDefaultAsync(u => u.Token == token);
        }
    }
}
