using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Moq;

[TestClass]
public class MainPageSimpleTests
{
    [TestMethod]
    public void PageModel_ShowsLoginButton_ForUnauthenticatedUser()
    {
        // Arrange
        var identity = new ClaimsIdentity(); // Пустая идентичность = неаутентифицирован
        var principal = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(principal);

        var authProvider = new TestAuthStateProvider(authState);
        var model = new MainPageModel(authProvider);

        // Act
        var shouldShowLogin = model.ShouldShowLoginButton();

        // Assert
        Assert.IsTrue(shouldShowLogin);
    }

    [TestMethod]
    public void PageModel_ShowsUserName_ForAuthenticatedUser()
    {
        // Arrangeф
        var claims = new[] { new Claim(ClaimTypes.Name, "Иван Петров") };
        var authState = new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(claims)));

        var authProvider = new TestAuthStateProvider(authState);
        var model = new MainPageModel(authProvider);

        // Act
        var userName = model.GetUserName();

        // Assert
        Assert.AreEqual("Иван Петров", userName);
    }
}

// Модель для тестирования логики страницы
public class MainPageModel
{
    private readonly AuthenticationStateProvider _authProvider;

    public MainPageModel(AuthenticationStateProvider authProvider)
    {
        _authProvider = authProvider;
    }

    public bool ShouldShowLoginButton()
    {
        var authState = _authProvider.GetAuthenticationStateAsync().Result;
        return !authState.User.Identity.IsAuthenticated;
    }

    public string GetUserName()
    {
        var authState = _authProvider.GetAuthenticationStateAsync().Result;
        return authState.User.Identity.Name;
    }
}

// Реализация TestAuthStateProvider
public class TestAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationState _state;
    public TestAuthStateProvider(AuthenticationState state) => _state = state;
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(_state);
}