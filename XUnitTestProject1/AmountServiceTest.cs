using SalaryCalculates;
using System;
using Xunit;
using Moq;

namespace XUnitTestProject1
{
    public class AmountServiceTest
    {
        readonly AmountService _amountService;
        readonly Mock<ILogger> _mockLog;
        readonly Mock<IInflationRate> _mockInflationRate;

        public AmountServiceTest()
        {
            _mockLog = new Mock<ILogger>(MockBehavior.Loose);
            _mockInflationRate = new Mock<IInflationRate>(MockBehavior.Loose);

            _amountService = new AmountService(_mockInflationRate.Object, _mockLog.Object);
        }
        [Fact]
        public void GetAmountCalculateByYear_WhenYearInValid_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => _amountService.GetAmountCalculateByYear(1000, 0));
        }
        [Fact]
        public void GetAmountCalculateByYear_WhenYearInAmount_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => _amountService.GetAmountCalculateByYear(-3, 2020));
        }

        [Theory]
        [InlineData(100, 2020, 0.5, 150)]
        public void GetAmountCalculateByYear_WhenValid_ReturnValidAmount(double amount, int year, double rate, double expectedAmount)
        {
            _mockInflationRate.Setup(x => x.GetInflationRateByYear(year)).Returns(rate);
            var actualAmount = _amountService.GetAmountCalculateByYear(amount, year);

            Assert.Equal(expectedAmount, actualAmount);

            _mockLog.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }
    }
}
