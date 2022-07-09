namespace EasyCommerce.Server.Shared.Common.TokenManager;

public interface IJwtAuthenticationManager
{
    string BuildToken();
}
