using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Text.Json;
using YourProjectNamespace.Models;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Загружаем состояние при инициализации
        await LoadAuthenticationStateAsync();
        return new AuthenticationState(_currentUser);
    }

    public async Task SetUserAsync(string email, string name, string surname, string avatarUrl)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, $"{surname} {name}"),
            new Claim("AvatarUrl", avatarUrl ?? "")
        };

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        _currentUser = new ClaimsPrincipal(identity);

        // Сохраняем в localStorage
        await SaveAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task ClearUserAsync()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authState");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private async Task SaveAuthenticationStateAsync()
    {
        var state = new
        {
            Email = _currentUser.FindFirst(ClaimTypes.Email)?.Value,
            Name = _currentUser.FindFirst(ClaimTypes.Name)?.Value,
            AvatarUrl = _currentUser.FindFirst("AvatarUrl")?.Value
        };

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            "authState",
            JsonSerializer.Serialize(state)
        );
    }

    private async Task LoadAuthenticationStateAsync()
    {
        var savedState = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authState");
        if (!string.IsNullOrEmpty(savedState))
        {
            try
            {
                var state = JsonSerializer.Deserialize<AuthState>(savedState);

                var claims = new List<Claim>();
                if (!string.IsNullOrEmpty(state.Email))
                    claims.Add(new Claim(ClaimTypes.Email, state.Email));

                // Важное изменение: правильно формируем имя
                if (!string.IsNullOrEmpty(state.Name))
                {
                    claims.Add(new Claim(ClaimTypes.Name, state.Name));

                    // Добавляем признак аутентификации
                    claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "CustomAuth"));
                }

                if (!string.IsNullOrEmpty(state.AvatarUrl))
                    claims.Add(new Claim("AvatarUrl", state.AvatarUrl));

                if (claims.Count > 0)
                {
                    // Указываем тип аутентификации для корректной работы IsAuthenticated
                    var identity = new ClaimsIdentity(
                        claims,
                        "CustomAuth",
                        ClaimTypes.Name,
                        ClaimTypes.Role
                    );

                    _currentUser = new ClaimsPrincipal(identity);
                }
            }
            catch (JsonException)
            {
                await ClearUserAsync();
            }
        }
    }
    public async Task UpdateUserProfileAsync(ProfileData profileData)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, $"{profileData.FirstName} {profileData.LastName}"),
        new Claim(ClaimTypes.Email, profileData.Email),
        new Claim("AvatarUrl", profileData.AvatarUrl ?? ""),
        new Claim("Course", profileData.Course ?? ""),
        new Claim("City", profileData.City ?? ""),
        new Claim("Bio", profileData.Bio ?? "")
    };

        var identity = new ClaimsIdentity(
            claims,
            "CustomAuth",
            ClaimTypes.Name,
            ClaimTypes.Role
        );

        _currentUser = new ClaimsPrincipal(identity);

        await SaveAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    private class AuthState
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
    }
}