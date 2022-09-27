using ForMyWebPage.Models;

namespace ForMyWebPage.Repositories.IRepositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenDto> GetByToken(string token);
        Task<RefreshTokenDto> Create(RefreshTokenDto refreshToken);
        Task Delete(int id);
    }
}
