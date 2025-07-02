using Microsoft.JSInterop;

using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

[TestClass]
public class CustomAuthStateProviderTests
{
    [TestMethod]
    public async Task GetAuthenticationStateAsync_WhenNotAuthenticated_ReturnsAnonymous()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var httpClientMock = new Mock<HttpClient>();

        var provider = new CustomAuthStateProvider(jsRuntimeMock.Object, httpClientMock.Object);

        // Act
        var authState = await provider.GetAuthenticationStateAsync();

        // Assert
        Assert.IsFalse(authState.User.Identity.IsAuthenticated);
    }

    [TestMethod]
    public async Task SetUserAsync_WithValidData_CreatesAuthenticatedUser()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var httpClientMock = new Mock<HttpClient>();

        var provider = new CustomAuthStateProvider(jsRuntimeMock.Object, httpClientMock.Object);

        // Act
        await provider.SetUserAsync(
            "test@example.com",
            "Иван",
            "Иванов",
            "Иванович",
            "avatar.jpg",
            "Москва",
            "Тестовая биография",
            "#ff0000",
            "admin");

        var authState = await provider.GetAuthenticationStateAsync();

        // Assert
        Assert.IsTrue(authState.User.Identity.IsAuthenticated);
        Assert.AreEqual("Иванов Иван Иванович", authState.User.Identity.Name);
        Assert.AreEqual("admin", authState.User.FindFirst(ClaimTypes.Role)?.Value);
    }
}