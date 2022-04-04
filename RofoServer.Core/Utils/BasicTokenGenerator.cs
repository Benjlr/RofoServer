using RofoServer.Domain.IdentityObjects;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RofoServer.Core.Utils;

public class BasicTokenGenerator : ITokenGenerator
{
    public async Task<string> GenerateTokenAsync(RofoUser user, string purpose) {
        await using var ms = new MemoryStream();
        var binWriter = new BinaryWriter(ms);
        binWriter.Write(BitConverter.GetBytes(DateTimeOffset.UtcNow.Ticks));
        binWriter.Write(BitConverter.GetBytes(user.Id));
        binWriter.Write(purpose);
        binWriter.Write(user.UserAuthDetails.SecurityStamp.ToString());
        var protectedBytes = EncryptionService.Encrypt(ms.ToArray());
        return Convert.ToBase64String(protectedBytes);
    }

    public async Task<string> GenerateTokenAsync(Guid group, string right, string email) {
        await using var ms = new MemoryStream();
        var binWriter = new BinaryWriter(ms);
        binWriter.Write(BitConverter.GetBytes(DateTimeOffset.UtcNow.Ticks));
        binWriter.Write(group.ToString());
        binWriter.Write(email);
        binWriter.Write(right);
        var protectedBytes = EncryptionService.Encrypt(ms.ToArray());
        return Convert.ToBase64String(protectedBytes);
    }

    public async Task<Tuple<string, string>> ValidateAsync(string email, string token) {
        var unprotectedData = EncryptionService.Decrypt(Convert.FromBase64String(token));
        await using var ms = new MemoryStream(unprotectedData);
        var binReader = new BinaryReader(ms);

        var creationTime = new DateTimeOffset(binReader.ReadInt64(), TimeSpan.Zero);
        var expirationTime = creationTime + TimeSpan.FromHours(72);

        if (expirationTime < DateTimeOffset.UtcNow)
            return null;

        var stamp = binReader.ReadString();

        var emailBytes = binReader.ReadString();
        if (emailBytes != email)
            return null;

        return new Tuple<string, string>(stamp, binReader.ReadString());
    }

    public async Task<bool> ValidateAsync(string purpose, string token, RofoUser user) {
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
        if (binReader.PeekChar() != -1 || user.UserAuthDetails.SecurityStamp != new Guid(stamp))
            return false;

        return stamp == user.UserAuthDetails.SecurityStamp.ToString();
    }
}