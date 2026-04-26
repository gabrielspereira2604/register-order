using NSubstitute;
using RegisterOrder.API.Domain.Entities;
using RegisterOrder.API.Domain.Enums;
using RegisterOrder.API.DTOs;
using RegisterOrder.API.Exceptions;
using RegisterOrder.API.Repositories;
using RegisterOrder.API.Services;

namespace RegisterOrder.Tests;

public class OrderServiceTests
{
    private readonly IOrderRepository _orderRepository = Substitute.For<IOrderRepository>();
    private readonly IMenuRepository _menuRepository = Substitute.For<IMenuRepository>();
    private readonly OrderService _sut;

    private static readonly MenuItem Sandwich = new() { Id = 1, Name = "X Burger", Price = 5.00m, Type = MenuItemType.Sandwich };
    private static readonly MenuItem Side = new() { Id = 4, Name = "Batata frita", Price = 2.00m, Type = MenuItemType.Side };
    private static readonly MenuItem Drink = new() { Id = 5, Name = "Refrigerante", Price = 2.50m, Type = MenuItemType.Drink };

    public OrderServiceTests()
    {
        _sut = new OrderService(_orderRepository, _menuRepository);

        _menuRepository.GetByIdAsync(1).Returns(Sandwich);
        _menuRepository.GetByIdAsync(4).Returns(Side);
        _menuRepository.GetByIdAsync(5).Returns(Drink);

        _orderRepository.CreateAsync(Arg.Any<Order>()).Returns(call => call.Arg<Order>());
    }

    [Fact]
    public async Task Create_WithSandwichSideDrink_Applies20PercentDiscount()
    {
        // Arrange
        var request = new OrderRequest([1, 4, 5]);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.Equal(0.20m, result.DiscountPercent);
        Assert.Equal(9.50m, result.Subtotal);
        Assert.Equal(1.90m, result.Discount);
        Assert.Equal(7.60m, result.Total);
    }

    [Fact]
    public async Task Create_WithSandwichDrink_Applies15PercentDiscount()
    {
        // Arrange
        var request = new OrderRequest([1, 5]);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.Equal(0.15m, result.DiscountPercent);
        Assert.Equal(7.50m, result.Subtotal);
        Assert.Equal(1.125m, result.Discount);
        Assert.Equal(6.375m, result.Total);
    }

    [Fact]
    public async Task Create_WithSandwichSide_Applies10PercentDiscount()
    {
        // Arrange
        var request = new OrderRequest([1, 4]);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.Equal(0.10m, result.DiscountPercent);
        Assert.Equal(7.00m, result.Subtotal);
        Assert.Equal(0.70m, result.Discount);
        Assert.Equal(6.30m, result.Total);
    }

    [Fact]
    public async Task Create_WithSandwichOnly_AppliesNoDiscount()
    {
        // Arrange
        var request = new OrderRequest([1]);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.Equal(0m, result.DiscountPercent);
        Assert.Equal(5.00m, result.Subtotal);
        Assert.Equal(0m, result.Discount);
        Assert.Equal(5.00m, result.Total);
    }

    [Fact]
    public async Task Create_WithDuplicateMenuItemId_ThrowsDuplicateItemException()
    {
        // Arrange
        var request = new OrderRequest([1, 1]);

        // Act & Assert
        await Assert.ThrowsAsync<DuplicateItemException>(() => _sut.CreateAsync(request));
    }

    [Fact]
    public async Task Create_WithDuplicateItemType_ThrowsDuplicateItemException()
    {
        // Arrange
        var sandwich2 = new MenuItem { Id = 2, Name = "X Egg", Price = 4.50m, Type = MenuItemType.Sandwich };
        _menuRepository.GetByIdAsync(2).Returns(sandwich2);

        var request = new OrderRequest([1, 2]);

        // Act & Assert
        await Assert.ThrowsAsync<DuplicateItemException>(() => _sut.CreateAsync(request));
    }

    [Fact]
    public async Task GetById_WithInvalidId_ThrowsOrderNotFoundException()
    {
        // Arrange
        _orderRepository.GetByIdAsync(99).Returns((Order?)null);

        // Act & Assert
        await Assert.ThrowsAsync<OrderNotFoundException>(() => _sut.GetByIdAsync(99));
    }

    [Fact]
    public async Task Delete_WithInvalidId_ThrowsOrderNotFoundException()
    {
        // Arrange
        _orderRepository.GetByIdAsync(99).Returns((Order?)null);

        // Act & Assert
        await Assert.ThrowsAsync<OrderNotFoundException>(() => _sut.DeleteAsync(99));
    }
}
