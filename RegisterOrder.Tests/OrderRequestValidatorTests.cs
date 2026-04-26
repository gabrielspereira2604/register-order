using RegisterOrder.API.DTOs;
using RegisterOrder.API.Validators;

namespace RegisterOrder.Tests;

public class OrderRequestValidatorTests
{
    private readonly OrderRequestValidator _sut = new();

    [Fact]
    public void Validate_WithValidSingleItem_ReturnsNoErrors()
    {
        // Arrange
        var request = new OrderRequest([1]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithValidThreeItems_ReturnsNoErrors()
    {
        // Arrange
        var request = new OrderRequest([1, 4, 5]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithNullMenuItemIds_ReturnsError()
    {
        // Arrange
        var request = new OrderRequest(null!);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.MenuItemIds));
    }

    [Fact]
    public void Validate_WithEmptyMenuItemIds_ReturnsError()
    {
        // Arrange
        var request = new OrderRequest([]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.MenuItemIds));
    }

    [Fact]
    public void Validate_WithMoreThanThreeItems_ReturnsError()
    {
        // Arrange
        var request = new OrderRequest([1, 2, 3, 4]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.MenuItemIds));
    }

    [Fact]
    public void Validate_WithDuplicateIds_ReturnsError()
    {
        // Arrange
        var request = new OrderRequest([1, 1]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(request.MenuItemIds));
    }

    [Fact]
    public void Validate_WithZeroId_ReturnsError()
    {
        // Arrange
        var request = new OrderRequest([0]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.StartsWith(nameof(request.MenuItemIds)));
    }

    [Fact]
    public void Validate_WithNegativeId_ReturnsError()
    {
        // Arrange
        var request = new OrderRequest([-1]);

        // Act
        var result = _sut.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.StartsWith(nameof(request.MenuItemIds)));
    }
}
