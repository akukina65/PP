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

    public async Task SetUserAsync(
     string email,
     string name,
     string surname,
     string patronymic,
     string avatarUrl,
     string city,
     string bio)
    {
        string fullName = $"{surname} {name} {patronymic}".Trim();

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, fullName),
        new Claim(ClaimTypes.GivenName, name),
        new Claim(ClaimTypes.Surname, surname),
        new Claim("Patronymic", patronymic ?? ""),
        new Claim(ClaimTypes.Email, email),
        new Claim("AvatarUrl", avatarUrl ?? ""),
        new Claim("City", city ?? ""), // Сохраняем город
        new Claim("Bio", bio ?? ""),   // Сохраняем био
        new Claim(ClaimTypes.AuthenticationMethod, "cookie")
    };

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        _currentUser = new ClaimsPrincipal(identity);

        await SaveAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public async Task SimpleLogout()
    {
        try
        {
            // Очищаем состояние аутентификации
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

            // Удаляем сохраненное состояние
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authState");

            // Уведомляем систему об изменении состояния
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при выходе: {ex.Message}");
        }
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

    public async Task UpdateProfileAsync(
    string email,
    string name,
    string surname,
    string patronymic,
    string avatarUrl,
    string city,
    string bio)
    {
        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            var claims = new List<Claim>();

            foreach (var claim in _currentUser.Claims)
            {
                if (claim.Type != ClaimTypes.Name &&
                    claim.Type != ClaimTypes.GivenName &&
                    claim.Type != ClaimTypes.Surname &&
                    claim.Type != ClaimTypes.Email &&
                    claim.Type != "Patronymic" &&
                    claim.Type != "AvatarUrl" &&
                    claim.Type != "City" &&
                    claim.Type != "Bio")
                {
                    claims.Add(claim);
                }
            }

            // Добавляем ВСЕ обновленные claims
            claims.Add(new Claim(ClaimTypes.Name, $"{surname} {name} {patronymic}".Trim()));
            claims.Add(new Claim(ClaimTypes.GivenName, name));
            claims.Add(new Claim(ClaimTypes.Surname, surname));
            claims.Add(new Claim("Patronymic", patronymic ?? ""));
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim("AvatarUrl", avatarUrl ?? ""));
            claims.Add(new Claim("City", city ?? ""));   // Важно!
            claims.Add(new Claim("Bio", bio ?? ""));     // Важно!
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
                if (!string.IsNullOrEmpty(state.Patronymic))
                    claims.Add(new Claim("Patronymic", state.Patronymic));
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
            Patronymic = _currentUser.FindFirst("Patronymic")?.Value, // Добавлено
            Email = _currentUser.FindFirst(ClaimTypes.Email)?.Value,
            AvatarUrl = _currentUser.FindFirst("AvatarUrl")?.Value,
            Role = _currentUser.FindFirst(ClaimTypes.Role)?.Value,
            City = _currentUser.FindFirst("City")?.Value,
            Bio = _currentUser.FindFirst("Bio")?.Value
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
        public string Patronymic { get; set; } // Добавлено
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Role { get; set; }
        public string City { get; set; } // Добавляем
        public string Bio { get; set; }  // Добавляем
    }




}