using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using BlazorApp1.Models; // Добавлено использование правильного пространства имен

[TestClass]
public class ShoppingCartServiceTests
{
    private Mock<HttpClient> _httpClientMock;
    private Mock<IJSRuntime> _jsRuntimeMock;
    private Mock<AuthenticationStateProvider> _authProviderMock;
    private ShoppingCartService _service;

    [TestInitialize]
    public async Task Initialize()
    {
        _httpClientMock = new Mock<HttpClient>();
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _authProviderMock = new Mock<AuthenticationStateProvider>();

        // Настройка моков по умолчанию
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user")
        }, "test-auth"));

        _authProviderMock.Setup(x => x.GetAuthenticationStateAsync())
            .ReturnsAsync(new AuthenticationState(user));

        _jsRuntimeMock.Setup(x => x.InvokeAsync<string>("localStorage.getItem", It.IsAny<object[]>()))
            .ReturnsAsync("[]");

        _service = new ShoppingCartService(
            _httpClientMock.Object,
            _jsRuntimeMock.Object,
            _authProviderMock.Object);

        // Даем время на инициализацию
        await Task.Delay(100);
    }

    [TestMethod]
    public async Task AddToCart_AddsNewItem()
    {
        // Arrange
        var course = new BlazorApp1.Models.CourseDto { Id = 1, Title = "Test Course" };

        // Act
        await _service.AddToCart(course);

        // Assert
        Assert.AreEqual(1, _service.Items.Count);
        Assert.AreEqual(course.Id, _service.Items[0].Id);
    }

    [TestMethod]
    public async Task RemoveFromCart_RemovesItem()
    {
        // Arrange
        var course = new BlazorApp1.Models.CourseDto { Id = 1 };
        await _service.AddToCart(course);

        // Act
        await _service.RemoveFromCart(course.Id);

        // Assert
        Assert.AreEqual(0, _service.Items.Count);
    }

    [TestMethod]
    public async Task ClearCart_RemovesAllItems()
    {
        // Arrange
        await _service.AddToCart(new BlazorApp1.Models.CourseDto { Id = 1 });
        await _service.AddToCart(new BlazorApp1.Models.CourseDto { Id = 2 });

        // Act
        await _service.ClearCart();

        // Assert
        Assert.AreEqual(0, _service.Items.Count);
    }

   

    
    
}