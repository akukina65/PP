using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Text.Json;

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
        await LoadAuthenticationStateAsync();
        return new AuthenticationState(_currentUser);
    }

    public async Task SetUserAsync(string email, string name, string surname, string avatarUrl)
    {
        string fullName = $"{surname} {name}";

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()), // Важно!
        new Claim(ClaimTypes.Name, fullName),
        new Claim(ClaimTypes.GivenName, name),
        new Claim(ClaimTypes.Surname, surname),
        new Claim(ClaimTypes.Email, email),
        new Claim("AvatarUrl", avatarUrl ?? ""),
        new Claim(ClaimTypes.AuthenticationMethod, "cookie")
    };

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        _currentUser = new ClaimsPrincipal(identity);

        await SaveAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task ClearUserAsync()
    {
        try
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authState");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при очистке пользователя: {ex.Message}");
        }
    }

    public async Task UpdateNameAsync(string newName)
    {
        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            // Сохраняем все существующие claims
            var claims = new List<Claim>();
            foreach (var claim in _currentUser.Claims)
            {
                // Оставляем все claims кроме обновляемых
                if (claim.Type != ClaimTypes.Name &&
                    claim.Type != ClaimTypes.GivenName)
                {
                    claims.Add(claim);
                }
            }

            // Добавляем обновленные claims
            var surname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value ?? "";
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "";
            var avatarUrl = claims.FirstOrDefault(c => c.Type == "AvatarUrl")?.Value ?? "";
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            claims.Add(new Claim(ClaimTypes.Name, $"{surname} {newName}"));
            claims.Add(new Claim(ClaimTypes.GivenName, newName));
            claims.Add(new Claim(ClaimTypes.Surname, surname));
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim("AvatarUrl", avatarUrl));
            claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "cookie"));

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            _currentUser = new ClaimsPrincipal(identity);

            await SaveAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
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

                if (!string.IsNullOrEmpty(state.Name))
                    claims.Add(new Claim(ClaimTypes.Name, state.Name));

                if (!string.IsNullOrEmpty(state.GivenName))
                    claims.Add(new Claim(ClaimTypes.GivenName, state.GivenName));

                if (!string.IsNullOrEmpty(state.Surname))
                    claims.Add(new Claim(ClaimTypes.Surname, state.Surname));

                if (!string.IsNullOrEmpty(state.Email))
                    claims.Add(new Claim(ClaimTypes.Email, state.Email));

                if (!string.IsNullOrEmpty(state.AvatarUrl))
                    claims.Add(new Claim("AvatarUrl", state.AvatarUrl));

                if (!string.IsNullOrEmpty(state.Role))
                    claims.Add(new Claim(ClaimTypes.Role, state.Role));

                if (claims.Count > 0)
                {
                    claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "cookie"));

                    var identity = new ClaimsIdentity(
                        claims,
                        "CustomAuth",
                        ClaimTypes.Name,
                        ClaimTypes.Role
                    );
                    _currentUser = new ClaimsPrincipal(identity);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                await ClearUserAsync();
            }
        }
    }

    private async Task SaveAuthenticationStateAsync()
    {
        var state = new AuthState
        {
            Name = _currentUser.FindFirst(ClaimTypes.Name)?.Value,
            GivenName = _currentUser.FindFirst(ClaimTypes.GivenName)?.Value,
            Surname = _currentUser.FindFirst(ClaimTypes.Surname)?.Value,
            Email = _currentUser.FindFirst(ClaimTypes.Email)?.Value,
            AvatarUrl = _currentUser.FindFirst("AvatarUrl")?.Value,
            Role = _currentUser.FindFirst(ClaimTypes.Role)?.Value
        };

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            "authState",
            JsonSerializer.Serialize(state)
        );
    }

    private class AuthState
    {
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Role { get; set; }
    }




}