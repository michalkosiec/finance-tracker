using Moq;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services;
using FinanceTracker.Api.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Tests.Services
{
    public class ValidationServiceTests {
        private readonly Mock<IBudgetRepo> _budgetRepoMock;
        private readonly Mock<ITransactionRepo> _transactionRepoMock;
        private readonly Mock<ICategoryRepo> _categoryRepoMock;
        private readonly ValidationService _validationService;

        public ValidationServiceTests()
        {
            _budgetRepoMock = new Mock<IBudgetRepo>();
            _transactionRepoMock = new Mock<ITransactionRepo>();
            _categoryRepoMock = new Mock<ICategoryRepo>();

            _validationService = new ValidationService(
                _budgetRepoMock.Object,
                _transactionRepoMock.Object,
                _categoryRepoMock.Object
            );
        }

        [Fact]
        public async Task AllowCategory_ShouldThrowBadHttpRequestException_WhenCategoryNameAlreadyExists()
        {
            var userId = Guid.NewGuid();
            var category = new Category { Name = "Food" };

            _categoryRepoMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>(), userId))
                .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<BadHttpRequestException>(() =>
                _validationService.AllowCategory(category, userId)
            );

            Assert.Equal("Category with the given name already exists.", exception.Message);
        }
    }
}