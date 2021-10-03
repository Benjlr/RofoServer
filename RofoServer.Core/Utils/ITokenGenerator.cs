using RofoServer.Domain.IdentityObjects;
using System.Threading.Tasks;

namespace RofoServer.Core.Utils
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(User user, string purpose);
        Task<bool> ValidateAsync(string purpose, string token, User user);
    }
}
