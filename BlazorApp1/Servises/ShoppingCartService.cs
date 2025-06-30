using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using BlazorApp1.Models;

public class ShoppingCartService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authProvider;
    private List<CourseDto> _items = new List<CourseDto>();
    private string _currentUserId = "anonymous";
    private bool _isInitialized = false;

    public event Action OnChange;
    public IReadOnlyList<CourseDto> Items => _items.AsReadOnly();

    public ShoppingCartService(
        HttpClient httpClient,
        IJSRuntime jsRuntime,
        AuthenticationStateProvider authProvider)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _authProvider = authProvider;

        _authProvider.AuthenticationStateChanged += AuthStateChanged;
        _ = InitializeAsync();
    }

    public void Dispose()
    {
        _authProvider.AuthenticationStateChanged -= AuthStateChanged;
        GC.SuppressFinalize(this);
    }

    private async Task InitializeAsync()
    {
        _currentUserId = await GetUserIdAsync();
        await LoadCart();
        _isInitialized = true;
    }

    private async void AuthStateChanged(Task<AuthenticationState> task)
    {
        if (!_isInitialized) return;

        var previousUserId = _currentUserId;
        _currentUserId = await GetUserIdAsync();

        if (previousUserId != _currentUserId)
        {
            // Сохраняем корзину предыдущего пользователя
            await SaveCart(previousUserId);

            // Очищаем текущую корзину в памяти
            _items.Clear();

            // Загружаем корзину нового пользователя
            await LoadCart();

            // Уведомляем подписчиков об изменении
            OnChange?.Invoke();
        }
    }

    private async Task<string> GetUserIdAsync()
    {
        try
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous";
        }
        catch
        {
            return "anonymous";
        }
    }

    private string GetCartStorageKey(string userId = null)
    {
        return $"shoppingCart_{userId ?? _currentUserId}";
    }

    public async Task AddToCart(CourseDto course)
    {
        if (course == null) return;

        if (!_items.Any(c => c.Id == course.Id))
        {
            _items.Add(course);
            await SaveCart();
            OnChange?.Invoke();
        }
    }

    public async Task RemoveFromCart(int courseId)
    {
        var item = _items.FirstOrDefault(c => c.Id == courseId);
        if (item != null)
        {
            _items.Remove(item);
            await SaveCart();
            OnChange?.Invoke();
        }
    }

    public async Task ClearCart()
    {
        _items.Clear();
        await SaveCart();
        OnChange?.Invoke();
    }

    public async Task<bool> Checkout()
    {
        if (!_items.Any()) return false;

        try
        {
            var courseIds = _items.Select(i => i.Id).ToList();
            var response = await _httpClient.PostAsJsonAsync("api/Orders/create", courseIds);

            if (response.IsSuccessStatusCode)
            {
                await ClearCart();
                return true;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Checkout failed: {response.StatusCode}, {errorContent}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Checkout exception: {ex}");
            return false;
        }
    }

    private async Task LoadCart()
    {
        try
        {
            var cartJson = await _jsRuntime.InvokeAsync<string>(
                "localStorage.getItem",
                GetCartStorageKey());

            _items = string.IsNullOrEmpty(cartJson)
                ? new List<CourseDto>()
                : System.Text.Json.JsonSerializer.Deserialize<List<CourseDto>>(cartJson) ?? new List<CourseDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading cart: {ex}");
            _items = new List<CourseDto>();
        }
    }

    private async Task SaveCart(string userId = null)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                GetCartStorageKey(userId),
                System.Text.Json.JsonSerializer.Serialize(_items));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving cart: {ex}");
        }
    }
}