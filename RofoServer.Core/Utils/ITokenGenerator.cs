using System;
using RofoServer.Domain.IdentityObjects;
using System.Threading.Tasks;

namespace RofoServer.Core.Utils
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(RofoUser user, string purpose);
        Task<string> GenerateTokenAsync(Guid group, string right, string email);
        Task<Tuple<string, string>> ValidateAsync(string email, string token);
        Task<bool> ValidateAsync(string purpose, string token, RofoUser user);
    }
}
