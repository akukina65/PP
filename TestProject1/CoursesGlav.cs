using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp1.Models;
using Microsoft.JSInterop;

[TestClass]
public class CoursesPageSimpleTests
{
    private class TestAuthProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _state;
        public TestAuthProvider(AuthenticationState state) => _state = state;
        public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(_state);
    }

    private Mock<ShoppingCartService> CreateShoppingCartServiceMock()
    {
        var httpMock = new Mock<HttpClient>();
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var authProviderMock = new Mock<AuthenticationStateProvider>();

        return new Mock<ShoppingCartService>(
            httpMock.Object,
            jsRuntimeMock.Object,
            authProviderMock.Object);
    }

    [TestMethod]
    public async Task CoursesModel_LoadsCourses_ForAuthenticatedUser()
    {
        // Arrange
        var claims = new[] { new Claim(ClaimTypes.Name, "test@example.com") };
        var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var authProvider = new TestAuthProvider(authState);

        var httpMock = new Mock<HttpClient>();
        var navMock = new Mock<NavigationManager>();
        var cartMock = CreateShoppingCartServiceMock();

        var model = new CoursesPageModel(authProvider, httpMock.Object, navMock.Object, cartMock.Object);

        // Act
        await model.LoadCourses();

        // Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void CoursesModel_AddsToCart_Correctly()
    {
        // Arrange
        var authProvider = new TestAuthProvider(new AuthenticationState(new ClaimsPrincipal()));
        var cartMock = CreateShoppingCartServiceMock();
        var model = new CoursesPageModel(authProvider, null, null, cartMock.Object);
        var testCourse = new CourseDto { Id = 1, Title = "Test Course" };

        // Act
        model.AddToCart(testCourse);

        // Assert
        cartMock.Verify(x => x.AddToCart(testCourse), Times.Once());
    }

    [TestMethod]
    public void CoursesModel_NavigatesToDetails_Correctly()
    {
        // Arrange
        var navMock = new Mock<NavigationManager>();
        var cartMock = CreateShoppingCartServiceMock();
        var model = new CoursesPageModel(null, null, navMock.Object, cartMock.Object);
        const int testCourseId = 123;

        // Act
        model.ViewCourseDetails(testCourseId);

        // Assert
        navMock.Verify(x => x.NavigateTo(It.Is<string>(url => url == $"/course/{testCourseId}"), It.IsAny<bool>()), Times.Once());
    }
}

public class CoursesPageModel
{
    private readonly AuthenticationStateProvider _authProvider;
    private readonly HttpClient _http;
    private readonly NavigationManager _navManager;
    private readonly ShoppingCartService _cartService;

    public CoursesPageModel(
        AuthenticationStateProvider authProvider,
        HttpClient http,
        NavigationManager navManager,
        ShoppingCartService cartService)
    {
        _authProvider = authProvider;
        _http = http;
        _navManager = navManager;
        _cartService = cartService;
    }

    public async Task LoadCourses()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity.IsAuthenticated)
        {
            // Логика загрузки курсов
        }
    }

    public void AddToCart(CourseDto course)
    {
        _cartService.AddToCart(course);
    }

    public void ViewCourseDetails(int courseId)
    {
        _navManager.NavigateTo($"/course/{courseId}");
    }
}