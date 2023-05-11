namespace Ahsan.Service.Interfaces;

public interface IAuthService
{
    ValueTask<string> GenerateTokenAsync(string username, string password);
}
