using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1.Services
{
    public class ClientAuthState : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public void SetAuthenticatedUser(string email, string name, string surname, string avatarUrl)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, $"{surname} {name}"), // Формат "Фамилия Имя"
        new Claim(ClaimTypes.GivenName, name),
        new Claim(ClaimTypes.Surname, surname),
        new Claim(ClaimTypes.Email, email),
        new Claim("AvatarUrl", avatarUrl ?? "")
    };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void ClearAuthentication()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}