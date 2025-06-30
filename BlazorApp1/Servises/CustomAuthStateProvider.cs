using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Net.Http.Json;
using System.Linq;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;
    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
    }

    // Вспомогательный метод для получения значения claim
    private string GetClaimValue(string claimType)
    {
        return _currentUser.FindFirst(claimType)?.Value ?? "";
    }

    // Вспомогательный метод для получения значения claim с default значением
    private string GetClaimValue(string claimType, string defaultValue)
    {
        return _currentUser.FindFirst(claimType)?.Value ?? defaultValue;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine("GetAuthenticationStateAsync called");

        try
        {
            await LoadAuthenticationStateFromLocalStorage();
            Console.WriteLine($"User authenticated: {_currentUser.Identity?.IsAuthenticated}");

            if (_currentUser.Identity?.IsAuthenticated != true)
            {
                Console.WriteLine("Loading from server...");
                await LoadAuthenticationStateFromServer();
            }

            return new AuthenticationState(_currentUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetAuthenticationStateAsync: {ex}");
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
    private async Task LoadAuthenticationStateFromLocalStorage()
    {
        var savedState = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authState");
        if (!string.IsNullOrEmpty(savedState))
        {
            try
            {
                var state = JsonSerializer.Deserialize<AuthState>(savedState);
                CreateUserFromState(state);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                await ClearUserAsync();
            }
        }
    }

    private async Task LoadAuthenticationStateFromServer()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/User/profile");
            if (response.IsSuccessStatusCode)
            {
                var profile = await response.Content.ReadFromJsonAsync<ProfileModel>();
                if (profile != null)
                {
                    // Предполагаем, что в ProfileModel есть свойство Role
                    await SetUserAsync(
                        profile.Email,
                        profile.FirstName,
                        profile.LastName,
                        profile.Patronymic,
                        profile.AvatarUrl,
                        profile.City,
                        profile.Bio,
                        profile.AvatarColor,
                        profile.Role // Передаем роль
                    );
                }
            }
            else
            {
                Console.WriteLine($"Failed to load profile: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading auth state: {ex.Message}");
        }
    }

    private void CreateUserFromState(AuthState state)
    {
        if (state == null) return;

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

        claims.Add(new Claim("city", state.City ?? ""));
        claims.Add(new Claim("bio", state.Bio ?? ""));
        claims.Add(new Claim("AvatarColor", state.AvatarColor ?? "#3498db"));

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

    public async Task SetUserAsync(
        string email,
        string name,
        string surname,
        string patronymic,
        string avatarUrl,
        string city,
        string bio,
        string avatarColor,
        string role
    )
    {
        // Логирование для отладки
        Console.WriteLine($"Setting user with color: {avatarColor}");

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
            new Claim("city", city ?? ""),
            new Claim("bio", bio ?? ""),
            new Claim("AvatarColor", avatarColor ?? "#3498db"),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.AuthenticationMethod, "cookie")
        };

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        _currentUser = new ClaimsPrincipal(identity);

        await SaveAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    // CustomAuthStateProvider.cs
    public async Task RefreshAuthenticationState()
    {
        var authState = await GetAuthenticationStateAsync();
        var user = authState.User;

        // Проверяем, действительно ли нужно обновление
        if (user.Identity?.IsAuthenticated == true)
        {
            // Только если данные изменились
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
    public async Task SimpleLogout()
    {
        try
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authState");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Logout error: {ex.Message}");
        }
    }

    public async Task ClearUserAsync()
    {
        try
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authState");

            // Принудительно обновляем состояние аутентификации
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));

            // Добавляем задержку для стабилизации состояния
            await Task.Delay(100);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Clear user error: {ex.Message}");
        }
    }

    public async Task UpdateProfileAsync(
        string email,
        string name,
        string surname,
        string patronymic,
        string avatarUrl,
        string city,
        string bio
    )
    {
        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, GetClaimValue(ClaimTypes.NameIdentifier)),
                new Claim(ClaimTypes.Name, $"{surname} {name} {patronymic}".Trim()),
                new Claim(ClaimTypes.GivenName, name),
                new Claim(ClaimTypes.Surname, surname),
                new Claim("Patronymic", patronymic ?? ""),
                new Claim(ClaimTypes.Email, email),
                new Claim("AvatarUrl", avatarUrl ?? ""),
                new Claim("city", city ?? ""),
                new Claim("bio", bio ?? ""),
                new Claim("AvatarColor", GetClaimValue("AvatarColor", "#3498db")),
                new Claim(ClaimTypes.Role, GetClaimValue(ClaimTypes.Role)),
                new Claim(ClaimTypes.AuthenticationMethod, "cookie")
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth", ClaimTypes.Name, ClaimTypes.Role);
            _currentUser = new ClaimsPrincipal(identity);

            await SaveAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public async Task UpdateAvatarAsync(string avatarUrl, string color)
    {
        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            var claims = new List<Claim>();

            foreach (var claim in _currentUser.Claims)
            {
                if (claim.Type != "AvatarUrl" &&
                    claim.Type != "AvatarColor" &&
                    claim.Type != ClaimTypes.AuthenticationMethod)
                {
                    claims.Add(claim);
                }
            }

            claims.Add(new Claim("AvatarUrl", avatarUrl ?? ""));
            claims.Add(new Claim("AvatarColor", color ?? "#3498db"));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "cookie"));

            // Добавляем все стандартные claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier,
                _currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? ""));

            claims.Add(new Claim(ClaimTypes.Name,
                _currentUser.FindFirst(ClaimTypes.Name)?.Value ?? ""));

            claims.Add(new Claim(ClaimTypes.GivenName,
                _currentUser.FindFirst(ClaimTypes.GivenName)?.Value ?? ""));

            claims.Add(new Claim(ClaimTypes.Surname,
                _currentUser.FindFirst(ClaimTypes.Surname)?.Value ?? ""));

            claims.Add(new Claim("Patronymic",
                _currentUser.FindFirst("Patronymic")?.Value ?? ""));

            claims.Add(new Claim(ClaimTypes.Email,
                _currentUser.FindFirst(ClaimTypes.Email)?.Value ?? ""));

            claims.Add(new Claim(ClaimTypes.Role,
                _currentUser.FindFirst(ClaimTypes.Role)?.Value ?? ""));

            claims.Add(new Claim("city",
                _currentUser.FindFirst("city")?.Value ?? ""));

            claims.Add(new Claim("bio",
                _currentUser.FindFirst("bio")?.Value ?? ""));

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            _currentUser = new ClaimsPrincipal(identity);

            await SaveAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
    private async Task SaveAuthenticationStateAsync()
    {
        var state = new AuthState
        {
            Name = GetClaimValue(ClaimTypes.Name),
            GivenName = GetClaimValue(ClaimTypes.GivenName),
            Surname = GetClaimValue(ClaimTypes.Surname),
            Patronymic = GetClaimValue("Patronymic"),
            Email = GetClaimValue(ClaimTypes.Email),
            AvatarUrl = GetClaimValue("AvatarUrl"),
            Role = GetClaimValue(ClaimTypes.Role),
            City = GetClaimValue("city"),
            Bio = GetClaimValue("bio"),
            AvatarColor = GetClaimValue("AvatarColor", "#3498db")
        };

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            "authState",
            JsonSerializer.Serialize(state)
        );
    }


    public void NotifyUserChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private class AuthState
    {
        public string Name { get; set; } = "";
        public string GivenName { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string Email { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public string Role { get; set; } = "";
        public string City { get; set; } = "";
        public string Bio { get; set; } = "";
        public string AvatarColor { get; set; } = "#3498db";
    }

    public class ProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Bio { get; set; }
        public string AvatarUrl { get; set; }
        public string AvatarColor { get; set; } = "#3498db";
        public string Role { get; set; }
    }
}