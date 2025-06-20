// ClientAuthStateProvider.cs
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

public class ClientAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public void SetAuthenticatedUser(string email, string name, string surname, string avatarUrl)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Surname, surname),
            new Claim("AvatarUrl", avatarUrl),
        }, "custom_auth");

        _currentUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_currentUser));
    }
}