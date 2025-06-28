using Xunit;
using FluentAssertions;
using System;

namespace AccountTests
{
    public class AccountTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var account = new Account("123", "USD", 1000);

            // Assert
            account.Id.Should().Be("123");
            account.Currency.Should().Be("USD");
            account.Balance.Should().Be(1000);
        }

        [Fact]
        public void Deposit_ShouldIncreaseBalance()
        {
            // Arrange
            var account = new Account("123", "USD", 1000);

            // Act
            account.Deposit(500);

            // Assert
            account.Balance.Should().Be(1500);
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalance()
        {
            // Arrange
            var account = new Account("123", "USD", 1000);

            // Act
            account.Withdraw(400);

            // Assert
            account.Balance.Should().Be(600);
        }

        [Fact]
        public void Withdraw_WithInsufficientBalance_ShouldThrow()
        {
            // Arrange
            var account = new Account("123", "USD", 300);

            // Act
            Action act = () => account.Withdraw(500);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Insufficient funds");
        }
    }
}
