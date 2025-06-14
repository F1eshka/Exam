using EkzamenADO.Services;
using System.Threading.Tasks;
using Xunit;

namespace EkzamenADO.Tests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async Task GetUsdToUahRateAsync_ShouldReturn_ValidRate()
        {
            // Arrange
            var service = new CurrencyService();

            // Act
            decimal rate = await service.GetUsdToUahRateAsync();

            // Assert
            Assert.True(rate > 10 && rate < 100, $"Некорректный курс: {rate}");
        }
    }
}
