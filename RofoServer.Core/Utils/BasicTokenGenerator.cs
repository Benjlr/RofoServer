using RofoServer.Domain.IdentityObjects;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RofoServer.Core.Utils
{
    public class BasicTokenGenerator : ITokenGenerator
    {
        public async Task<string> GenerateTokenAsync(User user, string purpose) {
            await using var ms = new MemoryStream();
            var binWriter = new BinaryWriter(ms);
            binWriter.Write(BitConverter.GetBytes(DateTimeOffset.UtcNow.Ticks));
            binWriter.Write(BitConverter.GetBytes(user.Id));
            binWriter.Write(purpose);
            binWriter.Write(user.UserAuthDetails.SecurityStamp.ToString());
            var protectedBytes = EncryptionService.Encrypt(ms.ToArray());
            return Convert.ToBase64String(protectedBytes);
        }

        public async Task<bool> ValidateAsync(string purpose, string token, User user) {
            var unprotectedData = EncryptionService.Decrypt(Convert.FromBase64String(token));
            await using var ms = new MemoryStream(unprotectedData);
            var binReader = new BinaryReader(ms);
            var creationTime = new DateTimeOffset(binReader.ReadInt64(), TimeSpan.Zero);
            var expirationTime = creationTime + TimeSpan.FromHours(1);
            if (expirationTime < DateTimeOffset.UtcNow) 
                return false;
            
            var userId = binReader.ReadInt32();
            var actualUserId = user.Id;
            if (userId != actualUserId) 
                return false;
            
            var purp = binReader.ReadString();
            if (!string.Equals(purp, purpose)) 
                return false;
            
            var stamp = binReader.ReadString();
            if (binReader.PeekChar() != -1) 
                return false;
            
            return stamp == user.UserAuthDetails.SecurityStamp.ToString();
        }
    }
}
