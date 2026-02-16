using DrClinicSystem.Core.ValueObjects;
using FluentAssertions;
using Xunit;

namespace DrClinicSystem.Tests.Core;

public class MoneyTests
{
    [Theory]
    [InlineData(1000, "IRR", 1000, "IRR")]
    [InlineData(0, "USD", 0, "USD")]
    [InlineData(999999.99, "EUR", 999999.99, "EUR")]
    public void Constructor_ValidValues_ShouldCreateCorrectInstance(
        decimal amount, string currency,
        decimal expectedAmount, string expectedCurrency)
    {
        // Act
        var money = new Money(amount, currency);

        // Assert
        money.Amount.Should().Be(expectedAmount);
        money.Currency.Should().Be(expectedCurrency.ToUpperInvariant());
    }

    [Theory]
    [InlineData(-1, "IRR")]
    [InlineData(-0.01, "USD")]
    public void Constructor_NegativeAmount_ShouldThrowArgumentException(
        decimal amount, string currency)
    {
        // Act & Assert
        FluentActions.Invoking(() => new Money(amount, currency))
            .Should().Throw<ArgumentException>()
            .WithMessage("*cannot be negative*");
    }

    [Theory]
    [InlineData("", "IRR")]
    [InlineData("   ", "USD")]
    [InlineData(null, "EUR")]
    public void Constructor_InvalidCurrency_ShouldThrowArgumentException(
        string currency, string _)
    {
        // Act & Assert
        FluentActions.Invoking(() => new Money(100, currency))
            .Should().Throw<ArgumentException>()
            .WithMessage("*Currency is required*");
    }

    [Theory]
    [InlineData(100, "IRR", 50, "IRR", 150, "IRR")]
    [InlineData(0, "USD", 200, "USD", 200, "USD")]
    public void Add_ValidSameCurrency_ShouldReturnCorrectSum(
        decimal a1, string c1,
        decimal a2, string c2,
        decimal expectedAmount, string expectedCurrency)
    {
        var m1 = new Money(a1, c1);
        var m2 = new Money(a2, c2);

        var result = m1.Add(m2);

        result.Amount.Should().Be(expectedAmount);
        result.Currency.Should().Be(expectedCurrency);
    }

    [Theory]
    [InlineData(100, "IRR", 50, "USD")]
    [InlineData(0, "EUR", 100, "GBP")]
    public void Add_DifferentCurrencies_ShouldThrowInvalidOperationException(
        decimal a1, string c1,
        decimal a2, string c2)
    {
        var m1 = new Money(a1, c1);
        var m2 = new Money(a2, c2);

        FluentActions.Invoking(() => m1.Add(m2))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Currencies must match*");
    }

    public static TheoryData<decimal, string, decimal, string, decimal, string> AddValidData =>
    new()
    {
        { 100, "IRR", 50, "IRR", 150, "IRR" },
        { 0,   "USD", 200, "USD", 200, "USD" },
        { 999, "EUR", 1,   "EUR", 1000, "EUR" }
    };

    [Theory]
    [MemberData(nameof(AddValidData))]
    public void Add_ValidSameCurrency_MemberData_ShouldReturnCorrectSum(
        decimal a1, string c1,
        decimal a2, string c2,
        decimal expectedAmount, string expectedCurrency)
    {
        var m1 = new Money(a1, c1);
        var m2 = new Money(a2, c2);

        var result = m1.Add(m2);

        result.Amount.Should().Be(expectedAmount);
        result.Currency.Should().Be(expectedCurrency);
    }

    public class InvalidCurrencyTestData : TheoryData<string>
    {
        public InvalidCurrencyTestData()
        {
            Add("");
            Add("   ");
            Add(null!);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidCurrencyTestData))]
    public void Constructor_InvalidCurrency_ClassData_ShouldThrowArgumentException(string currency)
    {
        FluentActions.Invoking(() => new Money(100, currency))
            .Should().Throw<ArgumentException>()
            .WithMessage("*Currency is required*");
    }
}